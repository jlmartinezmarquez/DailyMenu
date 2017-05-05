'use strict';

//var _ = window._ || require('lodash');

HttpService.$inject = [
	'$q',
	'$http'
];

function HttpService($q, $http){
	var service = {
		getWrapper: getWrapper,
		interceptResponse: interceptResponse
	};

	return service;

	function getWrapper(uri){

		var promise = $http.get(uri)
			.then(function(/*result*/){
				return interceptResponse(result);
			})
			.catch(function(error) {
				console.log(error);
				console.error('Error on GET call');
        	})
    	;

		return promise;
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

}

module.exports = HttpService;