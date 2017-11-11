'use strict';

angular.
  module('order').
  component('order', {
      templateUrl: 'orderbook/order-template.html',
      controller: function OrderController($routeParams, Orderbook) {
          var self = this;
          self.order = Orderbook.get({ orderId: $routeParams.orderId });
        }
    });