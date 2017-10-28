angular.
    module('core.menu').
    factory('Menu', ['$resource',
        function ($resource) {
            return $resource('http://localhost:5000/api/menu/:dishId', {}, {
                query: {
                    method: 'GET',
                    params: { dishId: '' },
                    isArray: true
                }
            });
        }
    ]);