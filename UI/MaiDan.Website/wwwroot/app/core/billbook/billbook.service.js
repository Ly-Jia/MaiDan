angular.
    module('core.billbook').
    factory('Billbook', ['$http',
        function($http) {
            return {
                getBills: function() {
                    return $http.get('http://localhost:5000/api/billbook');
                },

                getBill: function(billId, callback) {
                    return $http.get('http://localhost:5000/api/billbook' + '/' + billId).success(function(data) {
                        callback(data);
                    });
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