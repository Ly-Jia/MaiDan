'use strict';

angular.
    module('orderprint').
    component('orderprint', {
        controller: function OrderPrintController($routeParams, Billbook) {
            Billbook.printBill({ orderId: $routeParams.orderId });
            document.location.href = 'http://manouvellepage.com';
        }
    });