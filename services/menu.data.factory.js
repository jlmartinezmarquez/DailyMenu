'use strict';

var angular = require('angular');

MenuFactory.$inject = [
	'$http',
	'$q',
	'apiConfig'
];

function MenuFactory($http, $q, apiConfig){

	var service = {
		getMenu: getMenu,
		Menu: Menu
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
	function getMenu(menuTypeId){
		if (menuTypeId <= 0) {
			return {
				menuName: null,
				menuTypeId: null,
				ingredients: []
			};
		}

		return {
				id: 1,
				menuName: 'Arroz con pollo',
				menuTypeId: 2,
				ingredients: [
					{
						id: 1,
						name: 'Arroz'
					},
					{
						id: 2,
						name: 'Pollo'
					},
					{
						id: 3,
						name: 'Zanahorias'
					},
				]
		};
	}

	return service;
}

module.exports = MenuFactory;