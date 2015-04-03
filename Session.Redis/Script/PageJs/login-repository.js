'use strict';
loginModule.factory('loginRepository', function($http, $q)  {
    return {
        get: function (json) {
            var deferred = $q.defer();
            $http.post('Login/LoginControl', json).success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
      
        }
    };
});