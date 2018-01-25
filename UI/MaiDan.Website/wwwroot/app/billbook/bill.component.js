'use strict';

angular.
    module('bill').
    component('bill', {
        templateUrl: 'billbook/bill-template.html',
        controller: function BillController($routeParams, BillBook) {
            var self = this;
            self.bill = BillBook.getBill({ billId: $routeParams.billId });
        }
    });