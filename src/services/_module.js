'use strict';

var angular = require('angular');
var ListsStateFactory = require('../services/menu.data.factory');
var HttpService = require('../services/http.service.factory');

// Module definition

angular.module('DailyMenu.Services', [])
	.constant('DailyMenu.Services.Config', {
		partialPath: '/dist/js/services/'
	})
	.config(config)
	.factory('MenuDataFactory', ListsStateFactory)
	.factory('HttpService', HttpService);

// Config function

config.$inject = [];

function config() {
	// config
}