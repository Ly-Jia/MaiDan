'use strict';

angular.
  module('menu').
  component('menu', {
      templateUrl: 'Ordering/menu-template.html',
      controller: function MenuController($http) {
          var self = this;
          self.orderProp = 'id';

          $http.get('Ordering/Data/menu.json').then(function(response) {
              self.menu = response.data;
           });
        }
    });