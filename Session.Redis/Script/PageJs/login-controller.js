'use strict';
loginModule.controller('LoginController', function ($scope, loginRepository, $http, qsService, MessageService) {
    $scope.EntryLogin = function (username, password) {
        var json = JSON.stringify({ username: username, password: password });
        var ctx = loginRepository.get(json).then(function (d) {
            if (d.status == 500)
                MessageService.errMsg(d.data);
            else {
                window.location.href =d.data;
            }
        });

    };
});