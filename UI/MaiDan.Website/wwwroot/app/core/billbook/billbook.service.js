angular.
    module('core.billbook').
    factory('Billbook', ['$http',
        function($http) {
            return {
                getBills: function(callback) {
                    return $http.get('http://localhost:5000/api/billbook').success(function(data) {
                        callback(data);
                    });
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