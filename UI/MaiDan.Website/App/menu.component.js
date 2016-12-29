angular.
  module('maidanApp').
  component('menu', {
      template:
        '<ul>' +
          '<li ng-repeat="dish in $ctrl.menu">' +
            '<span>{{dish.id}}</span>' +
            '<p>{{dish.name}}</p>' +
          '</li>' +
        '</ul>',
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