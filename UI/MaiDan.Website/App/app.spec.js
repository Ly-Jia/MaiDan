'use strict';

describe('MenuController', function () {

    beforeEach(module('maidanApp'));

    it('should create a `menu` model with 3 dishes', inject(function ($controller) {
        var scope = {};
        var ctrl = $controller('MenuController', { $scope: scope });

        expect(scope.menu.length).toBe(3);
    }));

});