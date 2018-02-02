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
        var self = this;
        self.bill = Billbook.printBill(id);
        var url = '#!/billbook/' + id.toString();
        document.location.href = url;
    };
  }]
);