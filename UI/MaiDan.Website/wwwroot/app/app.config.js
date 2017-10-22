angular.
    module('maidanApp').
    config(['$locationProvider', '$routeProvider',
        function config($locationProvider, $routeProvider) {
            $locationProvider.hashPrefix('!');

            $routeProvider.
                when('/menu', {
                    template: '<menu></menu>'
                }).
                when('/menu/:dishId', {
                    template: '<dish></dish>'
                }).
                otherwise('/menu');
        }
    ]);