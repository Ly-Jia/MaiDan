angular.
    module('core.billpreview').
    factory('Billpreview', ['$resource',
        function ($resource) {
            return $resource('http://localhost:5000/api/billpreviews/:orderId', {}, {
                query: {
                    method: 'GET',
                    params: { orderId: '' },
                    isArray: true
                }
            });
        }
    ]);