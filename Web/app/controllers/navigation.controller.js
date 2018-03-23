(function () {
    "use strict";

    angular
		.module('app')
		.controller('navigationController', navigationController);

    navigationController.$inject = ['$state', 'userService'];

    function navigationController($state, userService) {
        var vm = this;
        vm.user = {};
        vm.signedInUserEmail = '';
        vm.errorMessage = '';
        vm.successMessage = '';

        _getSignedInUser();

        vm.signIn = function () {
            console.log('signinin in');
            userService.signIn(vm.user)
                .then(function (response) {
                    userService.saveCredentials(response.data);
                    vm.user = {};
                    angular.element('#logInModal').modal('hide');
                    _getSignedInUser();
                }).catch(function (error) {
                    console.log(error);
                    vm.errorMessage = error.data;
                });
        }

        vm.signUp = function () {
            userService.signUp(vm.user)
                .then(function (response) {
                    vm.user = {};
                    vm.successMessage = response.data;
                }).catch(function (response) {
                    console.log(error);
                    vm.errorMessage = error.data;
                });
        }

        vm.signOut = function () {
            userService.removeCredentials();
            if ($state.is('layout.account')) {
                $state.go('layout.home');
            } else {
                vm.signedInUserEmail = null;
            }
        }

        vm.clearMessages = function () {
            vm.errorMessage = '';
            vm.successMessage = '';
        }

        function _getSignedInUser() {
            vm.signedInUserEmail = userService.getSignedInUserEmail();
        }
    }
}());