//jshint strict: false
module.exports = function(config) {
  config.set({

    basePath: './App',

    files: [
      '../Scripts/angular.js',
      '../Scripts/angular-mocks.js',
      '../App/**/*.module.js',
      '*!(.module|.spec).js',
      '!../Scripts/**/*!(.module|.spec).js',
      '**/*.spec.js'
    ],

    autoWatch: true,

    frameworks: ['jasmine'],

    browsers: ['Chrome', 'Firefox'],

    plugins: [
      'karma-chrome-launcher',
      'karma-firefox-launcher',
      'karma-jasmine'
    ]

  });
};