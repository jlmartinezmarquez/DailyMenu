'use strict';

var angular = require('angular');

var MenuDirective = require('./menu.directive');
var MenuDataFactory = require('../services/menu.data.factory');

// Module definition

angular.module('DailyMenu.Menu', [])
	.constant('DailyMenu.Menu.Config', {
		partialPath: '/dist/js/menu/'
	})	
	.config(config)
	.directive('menu', MenuDirective)
	.factory('MenuFactory', MenuDataFactory);

// Config function

var configInject = [];
function config() {
    // any config here
}

config.$inject = configInject;