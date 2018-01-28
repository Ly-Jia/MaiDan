'use strict';

var orderModule = angular.module('order');

orderModule.component('order', {
      templateUrl: 'orderbook/order-template.html',
      controller: function OrderController($routeParams, Orderbook) {
          var self = this;
          self.order = Orderbook.get({ orderId: $routeParams.orderId });
        }
});

orderModule.controller('OrderPrintController', ['$scope', 'Billbook', function ($scope, Billbook) {
        $scope.print = function (id) {
            Billbook.printBill(id);
            //document.location.href = '/billbook/' + id;
        };
    }]
);