'use strict';

angular.
    module('billbook').
    component('billbook', {
        templateUrl: 'billbook/billbook-template.html',
        controller: ['Billbook',
            function BillbookController(Billbook) {
                this.billbook = Billbook.getBills();
                this.billbook = 'id';
            }
        ]
    });