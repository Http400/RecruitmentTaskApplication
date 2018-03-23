(function () {
    'use strict';

    angular
        .module('app')
        .service('userService', userService);

    userService.$inject = ['$rootScope', '$http', '$cookieStore'];

    function userService($rootScope, $http, $cookieStore) {
        return {
            getUsers: _getUsers,
            getUser: _getUser,
            editUser: _editUser,
            changePassword: _changePassword,
            signUp: _signUp,
            signIn: _signIn,
            saveCredentials: _saveCredentials,
            removeCredentials: _removeCredentials,
            isUserLoggedIn: _isUserLoggedIn,
            getSignedInUserEmail: _getSignedInUserEmail
        }

        function _getUsers() {
            return $http.get('/api/User/GetUsers');
        }

        function _getUser() {
            return $http.get('/api/User/GetUser');
        }

        function _editUser(userData) {
            return $http.put('/api/User/EditUser', userData);
        }

        function _changePassword(changePasswordData) {
            return $http.put('/api/User/ChangePassword', changePasswordData);
        }

        function _signUp(signUpData) {
            return $http.post('/api/User/SignUp', signUpData);
        }

        function _signIn(signInData) {
            return $http.post('/api/User/SignIn', signInData);
        }

        function _saveCredentials(token) {
            var encodedTokenData = token.split('.')[1];
            var decodedTokenData = atob(encodedTokenData);
            var tokenData = JSON.parse(decodedTokenData);
            console.log("tokenData: ");
            console.log(tokenData);
            $rootScope.repository = {
                user: {
                    email: tokenData.unique_name,
                    //roles: tokenData.role,
                    token: token,
                    tokenExp: tokenData.exp
                }
            };

            $http.defaults.headers.common['Authorization'] = 'Bearer ' + token;
            $cookieStore.put('repository', $rootScope.repository);
        }

        function _removeCredentials() {
            $rootScope.repository = {};
            $cookieStore.remove('repository');
            $http.defaults.headers.common.Authorization = '';
        };

        function _isUserLoggedIn() {
            return $rootScope.repository.loggedUser != null;
        }

        function _getSignedInUserEmail() {
            return ($rootScope.repository && $rootScope.repository.user) ? $rootScope.repository.user.email : null;
        }
    }
})();