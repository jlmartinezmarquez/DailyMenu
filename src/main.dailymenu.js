'use strict';

// Angular dependencies
var angular = require('angular');
require('angular-aria');
require('angular-animate');
require('angular-messages');
require('angular-sanitize');

// Other dependencies
require('textangular/dist/textAngular-sanitize.min');
require('textangular');

// Restangular expects underscore or lodash
window._ = require('lodash');

// Load config
var config = require('./config/config');
var apiConfig = require('./config/api.config');

// Load modules
require('./menu/_module');
require('./services/_module');
require('./templates/_module');

// Run Block
var runDependencies = [];

function run() {

	// any code which should run on start

}

run.$inject = runDependencies;

// App
angular
	.module('DailyMenu', [
		'ngAria',
		'ngAnimate',
		'ngMessages',
		'ngSanitize',
		'textAngular',
		//DailyMenu modules
		'DailyMenu.Services',
		'DailyMenu.Menu',
		'templates'
	])
	// conf
	.config(config)
	.constant('apiConfig', apiConfig)
	// run
	.run(run);