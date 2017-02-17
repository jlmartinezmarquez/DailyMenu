'use strict'

menu.$inject = 	'DailyMenu.Menu.Config'[

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
				.then(vm.setAvailableMenusForLunch, onGetError);
		}

		function setAvailableMenusForLunch(availableMenus) {
			vm.AvailableLunchMenus = availableMenus;
		}

		function onGetError(error){
			console.error(error);
		}		

		return directive;
	}
}

model.exports = menu;