'use strict';

// Define the `maidanApp` module
var maidanApp = angular.module('maidanApp', []);

// Define the `MenuController` controller on the `maidanApp` module
maidanApp.controller('MenuController', function MenuController($scope) {
    $scope.menu = [
      {
          id: 1,
          name: 'Fried Rice'
      }, {
          id: 2,
          name: 'Spring roll'
      }, {
          id: 3,
          name: 'Beijing duck'
      }
    ];
});