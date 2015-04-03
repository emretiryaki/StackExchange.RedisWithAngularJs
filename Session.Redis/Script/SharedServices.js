angular.module('SharedServices', [])
    .config(function ($httpProvider) {
        $httpProvider.responseInterceptors.push('myHttpInterceptor');
        var spinnerFunction = function (data, headersGetter) {           
            $('.container').showLoading();
            return data;
        };
        $httpProvider.defaults.transformRequest.push(spinnerFunction);
    })
    .factory('myHttpInterceptor', function ($q, $window) {
        return function (promise) {
            return promise.then(function (response) {
                $('.container').hideLoading();
                return response;

            }, function (response) {
                $('.container').hideLoading();
                return $q.reject(response);
            });
        };
    })
    .factory('qsService', function () {
        var service = {};
        service.getQueryVariable = function (qsKey) {
            var query = window.location.search.substring(1);
            var vars = query.split('&');
            for (var i = 0; i < vars.length; i++) {
                var indxOf = vars[i].indexOf("=");
                var key = vars[i].substring(0, indxOf);
                var value = vars[i].substring(indxOf + 1, vars[i].length);
                if (decodeURIComponent(key) == qsKey) {
                    return decodeURIComponent(value);
                }
            }
            return null;
        };
        return service;
    })

    .factory('MessageService', function () {

        var service = {};
        var messageTimeout;

        function removeMsgClasses(control) {
            if (control.hasClass('alert-danger'))
                control.removeClass('alert-danger');
            else if (control.hasClass('alert-success'))
                control.removeClass('alert-success');
            else if (control.hasClass('alert-info'))
                control.removeClass('alert-info');
        }

        function _message(message, cssClass) {
            var control = $('#messageBox');
            removeMsgClasses(control);
            control.addClass(cssClass);
            if (control.css('display') == 'none') {
                control.show('slow', function () {
                    if (cssClass != 'alert-danger') {
                        if (messageTimeout != false)
                            clearTimeout(messageTimeout);
                        messageTimeout = setTimeout(function () {
                            if (control.css('display') != 'none')
                                control.hide('slow');
                        }, 5000);
                    }
                });
            }
            $('#message').html(message);
        }

        service.successMsg = function (message) {
            _message(message, 'alert-success');
        };

        service.errMsg = function (message) {
            _message(message, 'alert-danger');
        };

        service.infoMsg = function (message) {
            _message(message, 'alert-info');
        };

        return service;
});