(function () {
    "use strict";

    angular
		.module('app')
		.controller('accountController', accountController);

    accountController.$inject = ['userService'];

    function accountController(userService) {
        var vm = this;
        vm.user = {};
        vm.password = '';
        vm.repeatPassword = '';
        vm.changePasswordData = {};

        getUserData();

        vm.editUser = function () {
            userService.editUser(vm.user)
                .then(function (response) {
                    console.log(response);
                }).catch(function (error) {
                    console.log(error);
                });
        };

        vm.changePassword = function () {
            userService.changePassword(vm.changePasswordData)
                .then(function (response) {
                    vm.changePasswordData.currentPassword = '';
                    vm.changePasswordData.newPassword = '';
                    vm.changePasswordData.repeatPassword = '';
                }).catch(function (error) {
                    console.log(error);
                });
        };

        function getUserData() {
            userService.getUser()
                .then(function (response) {
                    vm.user = response.data;
                }).catch(function (error) {
                    console.log(error);
                });
        }
    }

}());