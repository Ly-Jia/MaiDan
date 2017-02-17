'use strict';

describe('menu', function() {

    beforeEach(module('menu'));

    describe('MenuController', function () {
        var ctrl;

        beforeEach(inject(function ($componentController) {
            ctrl = $componentController('menu');
        }));

        it('should create a `menu` model with 3 dishes', function() {
            expect(ctrl.menu.length).toBe(3);
        });

        it('should set a default value for the `orderProp` model', function() {
            expect(ctrl.orderProp).toBe('id');
        });
    });
});