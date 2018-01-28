angular.
    module('core.billbook').
    factory('Billbook', ['$http',
        function($http) {
            return {
                getBills: function() {
                    return $http.get('http://localhost:5000/api/billbook');
                },

                getBill: function(billId) {
                    return $http.get('http://localhost:5000/api/billbook' + '/' + billId);
                },
                printBill: function (orderId) {
                    var dataObj = {
                        id: orderId
                    };	
                    return $http.post('http://localhost:5000/api/billbook', dataObj);
                }
            }
        }
    ]);