(function () {
    "use strict";

    angular
		.module('app')
		.controller('homeController', homeController);

    homeController.$inject = ['userService'];

    function homeController(userService) {
        var vm = this;
        vm.users = [];

        initController();

        function initController() {
            userService.getUsers()
                .then(function (response) {
                    vm.users = response.data;
                }).catch(function (error) {
                    console.log(error);
                });
        }
    }

}());