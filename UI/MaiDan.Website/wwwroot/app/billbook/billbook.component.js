'use strict';

angular.
    module('billbook').
    component('billbook', {
        templateUrl: 'billbook/billbook-template.html',
        controller: function BillbookController($scope, Billbook) {
            Billbook.getBills(function (data) {
                $scope.billbook = data;
            });
        }
    });