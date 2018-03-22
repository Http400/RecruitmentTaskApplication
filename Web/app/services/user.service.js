(function () {
    'use strict';

    angular
        .module('app')
        .service('userService', userService);

    userService.$inject = ['$http'];

    function userService($http) {
        return {
            getUsers: _getUsers
        }

        function _getUsers() {
            return $http.get('/api/User/GetUsers');
        }

    }
})();