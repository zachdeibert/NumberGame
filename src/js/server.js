"use strict";

var server = {
    "connected": false,
    "connect": function(callback) {
        server.socket = new WebSocket(config.server);
        server.socket.onopen = function() {
            server.connected = true;
            if ( typeof(callback) === "function" ) {
                callback();
            }
        };
    }
};
