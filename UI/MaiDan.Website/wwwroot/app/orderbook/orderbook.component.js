'use strict';

angular.
    module('orderbook').
    component('orderbook', {
        templateUrl: 'orderbook/orderbook-template.html',
        controller: ['Orderbook',
            function OrderbookController(Orderbook) {
                this.orderbook = Orderbook.query();
                this.orderProp = 'id';
            }
        ]
    });