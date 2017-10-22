'use strict';

angular.
  module('menu').
  component('menu', {
      templateUrl: 'menu/menu-template.html',
      /*controller: ['Menu',
          function MenuController(Menu) {
              this.menu = Menu.query();
              this.orderProp = 'id';
          }
      ]*/
      controller: function MenuController($http) {
          var self = this;
          self.orderProp = 'id';

          $http.get('data/menu.json').then(function (response) {
              self.menu = response.data;
          });
      }
    });