'use strict';

angular.
    module('bill').
    component('bill', {
        templateUrl: 'billbook/bill-template.html',
        controller: function BillController($routeParams, $scope, Billbook) {
            Billbook.getBill($routeParams.billId, function(data){
                $scope.bill = data;
            });
        }
    });