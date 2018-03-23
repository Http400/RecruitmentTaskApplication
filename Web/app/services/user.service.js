(function () {
    'use strict';

    angular
        .module('app')
        .service('userService', userService);

    userService.$inject = ['$http'];

    function userService($http) {
        return {
            getUsers: _getUsers,
            signUp: _signUp,
            signIn: _signIn
        }

        function _getUsers() {
            return $http.get('/api/User/GetUsers');
        }

        function _signUp(signUpData) {
            return $http.post('/api/User/SignUp', signUpData);
        }

        function _signIn(signInData) {
            return $http.post('/api/User/SignIn', signInData);
        }
    }
})();