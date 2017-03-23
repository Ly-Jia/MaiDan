'use strict';

describe('menu', function() {

    beforeEach(module('menu'));

    describe('MenuController', function () {
        var $httpBackend, ctrl;

        beforeEach(inject(function ($componentController, _$httpBackend_) {
            $httpBackend = _$httpBackend_;
            $httpBackend.expectGET('Service/Data/menu.json').respond([{ name: 'Fried noodles' }, { name: 'Dim sum' }]);

            ctrl = $componentController('menu');
        }));

        it('should create a `menu` model with 2 dishes fetched with `$http`', function () {
            expect(ctrl.menu).toBeUndefined();

            $httpBackend.flush();
            expect(ctrl.phones).toEqual([{ name: 'Fried noodles' }, { name: 'Dim sum' }]);
        });

        it('should set a default value for the `orderProp` property', function() {
            expect(ctrl.orderProp).toBe('id');
        });
    });
});