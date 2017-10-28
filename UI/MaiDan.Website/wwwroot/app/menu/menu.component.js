'use strict';

angular.
  module('menu').
  component('menu', {
      templateUrl: 'menu/menu-template.html',
      controller: ['Menu',
          function MenuController(Menu) {
              this.menu = Menu.query();
              this.orderProp = 'id';
          }
      ]
    });