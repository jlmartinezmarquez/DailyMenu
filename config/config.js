'use strict';

config.$inject = [
    '$locationProvider'
];

function config($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false,
        rewriteLinks: false
    });
}

module.exports = config;