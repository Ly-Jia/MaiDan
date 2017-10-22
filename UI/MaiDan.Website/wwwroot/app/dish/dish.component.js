'use strict';

angular.
  module('dish').
  component('dish', {
      templateUrl: 'dish/dish-template.html',
      controller: function DishController($routeParams, Menu) {
          var self = this;
          self.dish = Menu.get({ dishId: $routeParams.dishId });
        }
    });