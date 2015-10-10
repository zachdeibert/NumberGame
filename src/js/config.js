"use strict";

var config = {
    "loaded": false
};

function loadConfig() {
    getRequest("json/config.json", function(response, status) {
        var newconfig = JSON.parse(response);
        newconfig.loaded = true;
        config = newconfig;
    });
}
