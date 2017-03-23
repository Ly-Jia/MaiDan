'use strict';

angular.
  module('menu').
  component('menu', {
      templateUrl: 'Service/menu-template.html',
      controller: function MenuController($http) {
          var self = this;
          self.orderProp = 'id';

          $http.get('Service/Data/menu.json').then(function(response) {
              self.menu = response.data;
           });
        }
    });