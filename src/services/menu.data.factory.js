'use strict';

//var angular = require('angular');

MenuFactory.$inject = [
	'$http',
	'$q',
	'apiConfig',
	'HttpService'
];

function MenuFactory($http, $q, apiConfig, HttpService){

	var service = {
		getAvailableMenus: getAvailableMenus
	};

	/* Menu object definition */
//	function Menu(menu){
//
//		if (menu.Id === null || menu.Id === undefined){
//			throw 'Menu ID required';
//		}
//
//		this.id = menu.id;
//		this.menuName = menu.menuName;
//		this.menuTypeId = menu.menuTypeId;
//		this.ingredients = menu.ingredients;
//	}

	function getAvailableMenus(menuTypeId){
		if (menuTypeId === '') {
			return {
				menuName: null,
				menuTypeId: null,
				ingredients: []
			};
		}

		var uri = apiConfig.API_PROTOCOL + apiConfig.BASE_URL + apiConfig.API_URLS.GET_MENU;
		console.log('uri: ', uri);

		return HttpService.getWrapper(uri);

	//	var toReturn = 
	//	[
	//		{
	//			'id': '1',
	//			'menuName': 'Arroz con pollo',
	//			'menuTypeId': '1',
	//			'hadLastTimeOn': '16/02/2017',
	//			'ingredients': [
	//				{
	//					'id': '1',
	//					'name': 'Arroz'
	//				},
	//				{
	//					'id': '2',
	//					'name': 'Pollo'
	//				},
	//				{
	//					'id': '3',
	//					'name': 'Zanahorias'
	//				}]				
	//		},
	//		{
	//			'id': '2',
	//			'menuName': 'Spaghetti con carne picada',
	//			'menuTypeId': '1',
	//			'hadLastTimeOn': '16/02/2017',
	//			'ingredients': [
	//				{
	//					'id': '4',
	//					'name': 'Pasta'
	//				},
	//				{
	//					'id': '5',
	//					'name': 'Carne picada'
	//				},
	//				{
	//					'id': '6',
	//					'name': 'Tomate frito'
	//				}]
	//		}
	//	];
	//	return toReturn;
	}

	return service;
}

module.exports = MenuFactory;