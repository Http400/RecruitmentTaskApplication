(function () {
    'use strict';

    angular
		.module('app')
		.config(routeConfig)
        .run(run);

    routeConfig.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider'];

    function routeConfig($stateProvider, $urlRouterProvider, $locationProvider) {

        $stateProvider
            .state('layout', {
			    templateUrl: 'Main/Layout',
			    controller: 'navigationController',
			    controllerAs: 'vm',
			    abstract: true
			})
        	    .state('layout.home', {
        	        url: '/',
        	        templateUrl: 'Main/HomePage',
        	        controller: 'homeController',
        	        controllerAs: 'vm'
        	    })
                .state('layout.account', {
                    url: '/account',
                    templateUrl: 'Main/AccountPage',
                    controller: 'accountController',
                    controllerAs: 'vm'
                });

        $urlRouterProvider.otherwise('/');
    }

    run.$inject = ['$rootScope', '$cookieStore', '$http', 'userService'];

    function run($rootScope, $cookieStore, $http, userService) {
        // handle page refreshes
        $rootScope.repository = $cookieStore.get('repository') || {};

        if ($rootScope.repository.user) {
            var tokenExp = new Date($rootScope.repository.user.tokenExp * 1000);
            if (tokenExp >= Date.now()) {
                $http.defaults.headers.common['Authorization'] = 'Bearer ' + $rootScope.repository.user.token;
            } else {
                userService.removeCredentials();
            }
        }
    }
}());