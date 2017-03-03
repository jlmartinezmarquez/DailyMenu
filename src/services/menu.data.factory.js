'use strict';

var angular = require('angular');

MenuFactory.$inject = [
	'$http',
	'$q',
	'apiConfig'
];

function MenuFactory($http, $q, apiConfig){

	var menus = [];

	var service = {
		getAvailableMenus: getAvailableMenus
	};

	/* Menu object definition */
	function Menu(menu){

		if (menu.Id === null || menu.Id === undefined){
			throw 'Menu ID required';
		}

		this.id = menu.id;
		this.menuName = menu.menuName;
		this.menuTypeId = menu.menuTypeId;
		this.ingredients = menu.ingredients;
	}

	/* TODO: Develop API endpoint returning a promise using $http and $q objects; hardcoded for now */
	function getAvailableMenus(menuTypeId){
		if (menuTypeId === '') {
			return {
				menuName: null,
				menuTypeId: null,
				ingredients: []
			};
		}

		var toReturn = 
		[
			{
				'id': '1',
				'menuName': 'Arroz con pollo',
				'menuTypeId': '1',
				'hadLastTimeOn': '16/02/2017',
				'ingredients': [
					{
						'id': '1',
						'name': 'Arroz'
					},
					{
						'id': '2',
						'name': 'Pollo'
					},
					{
						'id': '3',
						'name': 'Zanahorias'
					}]				
			},
			{
				'id': '2',
				'menuName': 'Spaghetti con carne picada',
				'menuTypeId': '1',
				'hadLastTimeOn': '16/02/2017',
				'ingredients': [
					{
						'id': '4',
						'name': 'Pasta'
					},
					{
						'id': '5',
						'name': 'Carne picada'
					},
					{
						'id': '6',
						'name': 'Tomate frito'
					}]
			}
		];

		return toReturn;
	}

	/**
	 * This method will return the data only from the response
	 * @param {Object} response - the entire response from the api
	 * @returns {Object} data
	 */
	function interceptResponse(response) {
		var data = response.data;
		return data;
	}

	return service;
}

module.exports = MenuFactory;