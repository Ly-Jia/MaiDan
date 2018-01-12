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
                when('/orderbook', {
                    template: '<orderbook></orderbook>'
                }).
                when('/orderbook/:orderId', {
                    template: '<order></order>'
                }).
                otherwise('/orderbook');
        }
    ]);