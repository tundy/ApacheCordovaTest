// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
(function () {
    "use strict";

    document.addEventListener('deviceready', onDeviceReady.bind(this), false);

    function onDeviceReady() {
        // Handle the Cordova pause and resume events
        document.addEventListener( 'pause', onPause.bind( this ), false );
        document.addEventListener( 'resume', onResume.bind( this ), false );
        
        // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.

        var outputElement = document.getElementById('output');
        outputElement.innerHTML += "DEBUG 1<br/>\n";

        document.getElementById('sendButton').addEventListener("click", onClick.bind( this ), false);

        var socket;

        function onClick() {
            outputElement.innerHTML += "Button Clicked<br/>\n";

            if (socket == null) {
                socket = io('localhost:8080');

                outputElement.innerHTML += "DEBUG 2<br/>\n";

                socket.on('connect', function () { outputElement.innerHTML += "connect<br/>\n"; });
                socket.on('event', function (data) { outputElement.innerHTML += "event" + data + "<br/>\n"; });
                socket.on('disconnect', function () { outputElement.innerHTML += "disconnect<br/>\n"; });
            }
        };
    };


    function onPause() {
        // TODO: This application has been suspended. Save application state here.
    };

    function onResume() {
        // TODO: This application has been reactivated. Restore application state here.
    };
} )();