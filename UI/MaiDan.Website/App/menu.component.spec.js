'use strict';

describe('menu', function() {

    beforeEach(module('maidanApp'));

    describe('MenuController', function() {
        it('should create a `menu` model with 3 dishes', inject(function($componentController) {
            var ctrl = $componentController('menu');
            expect(ctrl.menu.length).toBe(3);
        }));
    });
});