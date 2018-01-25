angular.
    module('core.billbook').
    factory('Billbook', ['$http',
        function ($http) {
            var baseUrl = 'http://localhost:5000/api/billbook';
            var BillBook = {};

            BillBook.getBills = function() {
                return $http.get(baseUrl);
            };

            BillBook.getBill = function(id) {
                return $http.get(baseUrl + '/' + id);
            }
            
            BillBook.printBill = function(id) {
                return $http.post(baseUrl + '/' + id);
            }
        }
    ]);