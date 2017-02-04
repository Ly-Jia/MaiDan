'use strict';

angular.
  module('menu').
  component('menu', {
      templateUrl: 'Service/menu-template.html',
      controller: function MenuController() {
            this.menu = [
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
        }
    });