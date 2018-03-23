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
        	    });


			//.state('mainSite', {
			//    templateUrl: 'app/main-site/layout/layout.html',
			//    controller: 'navigationController',
			//    //abstract: true
			//})
			//	.state('mainSite.home', {
			//	    url: '/',
			//	    templateUrl: 'app/main-site/home/home.html',
			//	    controller: 'homeController',
			//	    controllerAs: 'vm'
			//	})
			//	.state('mainSite.about', {
			//	    url: '/about',
			//	    templateUrl: 'app/main-site/about/about.html',
			//	    controller: 'aboutController',
			//	    controllerAs: 'vm'
			//	})
			//	.state('mainSite.contact', {
			//	    url: '/contact',
			//	    templateUrl: 'app/main-site/contact/contact.html',
			//	    controller: 'contactController',
			//	    controllerAs: 'vm'
			//	})
			//.state('adminPanel', {
			//    url: '/admin-panel',
			//    templateUrl: 'app/admin-panel/layout/layout.html',
			//    controller: 'adminPanel/navigationController',
			//    abstract: true,
			//    resolve: { isAdminAuthenticated: isAdminAuthenticated }
			//})
			//	.state('adminPanel.main', {
			//	    url: '/main',
			//	    templateUrl: 'app/admin-panel/main/main.html',
			//	    controller: 'adminPanel/mainController',
			//	    controllerAs: 'vm'
			//	})
			//	.state('adminPanel.projects', {
			//	    url: '/projects',
			//	    abstract: true
			//	})
			//		.state('adminPanel.projects.list', {
			//		    url: '/list',
			//		    templateUrl: 'app/admin-panel/projects/projects.html',
			//		    controller: 'adminPanel/projectsController',
			//		    controllerAs: 'vm'
			//		})
			//		.state('adminPanel.projects.details', {
			//		    url: '/:id',
			//		    templateUrl: 'app/admin-panel/projects/projectDetails.html',
			//		    controller: 'adminPanel/projectDetailsController',
			//		    controllerAs: 'vm'
			//		})
			//	.state('adminPanel.account', {
			//	    url: '/account',
			//	    templateUrl: 'app/admin-panel/account/account.html',
			//	    controller: 'adminPanel/accountController',
			//	    controllerAs: 'vm'
			//	})

        //$urlRouterProvider.when('/admin-panel', '/admin-panel/main');
        $urlRouterProvider.otherwise('/');

        //$locationProvider.html5Mode(true);
        //$locationProvider.hashPrefix('');
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

    //isAdminAuthenticated.$inject = ['$state'];

    //function isAdminAuthenticated($state) {
    //    console.log("isAdminAuthenticated");
    //    //$state.go('main');
    //}
}());