'use strict';

//var _ = window._ || require('lodash');

Menu.$inject = 	[
	'DailyMenu.Menu.Config'
];

function Menu(DailyMenuMenuConfig) {
	var directive = {
		restrict: 'E',
		replace: true,
		templateUrl: DailyMenuMenuConfig.partialPath.concat('menu.template.html'),
		scope: {},
		bindtoController: {

		},
		controllerAs: 'ctrl',
		controller: MenuController
	};

	
	MenuController.$inject = [
		'MenuFactory'
	];


	function MenuController(MenuFactory) {
		var vm = this;

		//properties
		vm.AvailableLunchMenus = [];
		vm.AvailableDinnerMenus = [];

		//methods
		vm.getAvailableMenusForLunch = getAvailableMenusForLunch;
		vm.setAvailableMenusForLunch = setAvailableMenusForLunch;

		return init();

		/**
		   * This is the init method
		*/
		function init() {
			vm.getAvailableMenusForLunch();
		}

		function getAvailableMenusForLunch() {
			MenuFactory
				.getAvailableMenus('Lunch')
				.then(vm.setAvailableMenusForLunch)
				.catch(onGetError);

//			var toReturn = MenuFactory.getAvailableMenus('Lunch');
//			console.log(toReturn);
//			return toReturn;
		}

		function setAvailableMenusForLunch(availableMenus) {
			vm.AvailableLunchMenus = availableMenus;
		}

		function onGetError(error){
			console.error(error);
		}		

	}
	
	return directive;
}

module.exports = Menu;