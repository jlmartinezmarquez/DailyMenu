'use strict';

var angular = require('angular');
var DayDirective = require('./day.directive')

// Module definition

angular.module('DailyMenu.Day', [])
	.constant('DailyMenu.Day.Config', {
		partialPath: '/dist/js/day/'
	})	
	.config(config)
	.factory('MenuFactory', MenuFactory)
	.directive('day', 'DayDirective');

// Config function

var configInject = [];
function config() {
    // any config here
}
config.$inject = configInject;
