'use strict';

angular.
    module('billpreview').
    component('billpreview', {
        templateUrl: 'billpreview/billpreview-template.html',
        controller: function BillpreviewController($routeParams, Billpreview) {
            var self = this;
            self.billpreview = Billpreview.get({ orderId: $routeParams.orderId });
        }
    });