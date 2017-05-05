var gulp = require('gulp');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var minifyCSS = require('gulp-clean-css');
var rename = require('gulp-rename');
var jshint = require('gulp-jshint');
var del = require('del');
var browserify = require('browserify');
var uglify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');
var source = require('vinyl-source-stream');
var gutil = require('gulp-util');
var buffer = require('vinyl-buffer');
var gulpif = require('gulp-if');
var runSequence = require('run-sequence');
var templateCache = require('gulp-angular-templatecache');
var template = require('gulp-template');
var taskListing = require('gulp-task-listing');
var replace = require('gulp-replace');

// Add a task to render the output
gulp.task('help', taskListing);
// Config
var now = new Date();
var config = {
  buildLabel: '' + now.getFullYear() + now.getMonth() + now.getDate() + now.getHours() + now.getMinutes() + now.getSeconds(),
  buildMode: null
};

// Karma Server instance
// var KarmaServer = require('karma').Server;

/*****************************TASKS********************************/
/**************************COMMON TASKS****************************/

// Config DEV
gulp.task('config:dev', function() {
  config.buildMode = 'dev';
});

// Config PROD
gulp.task('config:prod', function() {
  config.buildMode = 'prod';
});

// Clean CSS
gulp.task('clean:css', function() {
  return del([
    './dist/css/**/*',
    './dist/fonts/**/*'
  ]);
});

// Clean JS
gulp.task('clean:js', function() {
  return del([
    './dist/js/**/*'
  ]);
});

// Clean Everything
gulp.task('clean', ['clean:js', 'clean:css'], function() {
  return true;
});

gulp.task('copy:res', function() {
  gulp.src(['./img/**/*'], {
    base: './img/'
  }).pipe(gulp.dest('./dist/img/'));

  // bootstrap
  gulp.src('./node_modules/bootstrap/dist/css/bootstrap.min.css')
    .pipe(gulp.dest('./dist/style/'));
  gulp.src('./node_modules/bootstrap/dist/js/bootstrap.min.js')
    .pipe(gulp.dest('./dist/js/'));

  // jQuery
  gulp.src('./node_modules/jquery/dist/jquery.min.js')
    .pipe(gulp.dest('./dist/js/'));

  // textAngular
  gulp.src('./node_modules/textangular/dist/textAngular.css')
    .pipe(gulp.dest('./dist/style/'));

  // We can copy the font files into the dist folder, otherwise we could use the CDN
  // and override the variable $fa-font-path: "//netdna.bootstrapcdn.com/font-awesome/4.5.0/fonts" !default;
  // Copy font files
  gulp.src(['./node_modules/mdi/fonts/**/*'], {
    base: './node_modules/mdi/fonts/'
  }).pipe(gulp.dest('./dist/fonts/'));
});

gulp.task('compile:css', function() {
  // dailymenu.scss should have: @import "../node_modules/font-awesome/scss/font-awesome.scss";
  // compiling the style
  return gulp.src('./src/style/main.scss')
    // The onerror handler prevents Gulp from crashing when you make a mistake in your SASS
    .pipe(sass({
      onError: function(e) {
        console.log(e);
        gutil.log(e);
      }
    }))
    // Optionally add autoprefixer
    .pipe(autoprefixer('last 2 versions', '> 1%', 'ie 8'))
    .pipe(rename('style.css'))
    .pipe(gulp.dest('./dist/style/'))
    .pipe(gulpif(config.buildMode === 'prod', minifyCSS()))
    .pipe(gulp.dest('./dist/style/'));
});

gulp.task('build:style', function() {
  runSequence(['copy:res', 'compile:css']);
});

// create module for templates
gulp.task('create:templates', function() {
  gulp.src('./src/**/*.html')
    .pipe(templateCache({
      transformUrl: function(url) {
        return '/dist/js/' + url;
      }
    }))
    .pipe(gulp.dest('./src/templates'));
});

// JSHint task
gulp.task('lint:js', function() {
	return gulp.src(['./src/**/*.js', '!./src/templates/**/*.js'])
		.pipe(jshint())
		// You can look into pretty reporters as well, but that's another story
		.pipe(jshint.reporter('default'));
});

// gulp.task('build:indexfile', function() {
//   return gulp.src('./index.template.html')
//     // And put it in the dist folder
//     .pipe(template({
//       version: config.version,
//       buildLabel: config.buildLabel
//     }))
//     .pipe(rename('index.html'))
//     .pipe(gulp.dest('./'));
// });

gulp.task('build:del:tempfiles', function() {
  // to clean temp files
});

// build for development
gulp.task('build:dev', function() {
	runSequence(
		['clean', 'config:dev'],
		['create:templates'],
		['build:style', 'lint:js'],
		['build:js:dailymenu'], //'build:indexfile'],
		['build:del:tempfiles']
	);
});

//  build for production
gulp.task('build:prod', function() {
	runSequence(
		['clean', 'config:prod'],
		['create:templates'],
		['build:style', 'lint:js'],
		['build:js:dailymenu'], //'build:indexfile'],
		['build:del:tempfiles']
	);
});


// watchers
gulp.task('watch', ['build:dev'], function() {
	// JS watcher
	gulp.watch([
		'./index.html',
		'./**/*.html',
		'./src/**/*.js',
		'!./src/**/templates.js'
	], [
		'create:templates',
		'lint:js',
		'build:js:dailymenu'
	]);

	// SCSS watcher
	gulp.watch([
		'./style/**/*.scss'
	], ['compile:css']);
});

/**************************DAILYMENU APP**************************/
// Build JavaScript
gulp.task('build:js:dailymenu', ['create:templates'], function() {
  console.log('AngJS build version: ' + config.buildMode);

  // using the template create the env config constant
  gulp.src('./js/src/config/env.config.tmpl.js')
    .pipe(template({config: config.buildMode}))
    .pipe(rename('env.config.js'))
    .pipe(gulp.dest('./js/src/config/'));

  // set up the browserify instance on a task basis
  var b = browserify({
    entries: './src/main.dailymenu.js',
    debug: config.buildMode === 'prod'
  });

  return b.bundle()
    .pipe(source('angularjs.dailymenu.all.js'))
    .pipe(buffer())
    //.pipe(cachebust.resources())
    //.pipe(sourcemaps.init({ loadMaps: true }))
    // Add transformation tasks to the pipeline here.
    // only if in production UGLIFY
    .pipe(gulpif(config.buildMode === 'prod', uglify()))
    .on('error', function(error) {
      console.log(error);
      gutil.log(error);
    })
    .pipe(sourcemaps.write('./maps/'))
    .pipe(gulp.dest('./dist/js/'));
});
