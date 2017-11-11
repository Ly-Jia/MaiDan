angular.
    module('core.orderbook').
    factory('Orderbook', ['$resource',
        function ($resource) {
            return $resource('http://localhost:5000/api/orderbook/:orderId', {}, {
                query: {
                    method: 'GET',
                    params: { orderId: '' },
                    isArray: true
                }
            });
        }
    ]);