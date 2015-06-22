(function () {

    // construct the application.
    var app = angular.module('app', ['ngRoute', 'ngAnimate', 'toastr', 'ngCookies', 'ui.bootstrap', 'ui.mask','xtForm']);

    app.run(['$rootScope', '$location','toastr', 
    function ($rootScope, $location, toastr) {

        //Client-side security. Server-side framework MUST add it's
        //own security as well since client-based security is easily hacked
        $rootScope.$on("$routeChangeStart", function () {
            
        });

        $rootScope.$on("httpResponseError", function (event, args) {
            if (args.response.status === 0) {
                // Do not pop during demo
                toastr.error("It looks like there has been a disruption in service." +
                "  Please check your internet connection and try refreshing your browser.", "Connection Error " + args.response.status);
            }
            else if (args.response.status === 400) {
                // Do nothing when server side errors occur    
            }
            else {
                toastr.error("An error occurred while loading the web page.", "Connection Error " + args.response.status);
            }
        });
    }]);
}());