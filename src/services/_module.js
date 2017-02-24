'use strict';

var angular = require('angular');
var ListsStateFactory = require('../services/menu.data.factory');

// Module definition

angular.module('DailyMenu.Services', [])
	.constant('DailyMenu.Services.Config', {
		partialPath: '/dist/js/services/'
	})
	.config(config)
	.factory('MenuDataFactory', ListsStateFactory);

// Config function

config.$inject = [];

function config() {
	// config
}