"use strict";

function loadMenus() {
    getRequest("json/menus.json", function(response, status) {
        if ( status == 200 ) {
            var obj = JSON.parse(response);
            for ( var i = 0; i < obj.menus.length; i++ ) {
                var script = document.createElement("script");
                script.setAttribute("type", "text/javascript");
                script.setAttribute("src", "js/" + obj.menus[i] + ".js");
                if ( obj.menus[i] == obj.default ) {
                    var name = "open" + obj.menus[i].charAt(0).toUpperCase() + obj.menus[i].substr(1);
                    script.onload = function() {
                        window[name]();
                    };
                }
                var style = document.createElement("link");
                style.setAttribute("rel", "stylesheet");
                style.setAttribute("type", "text/css");
                style.setAttribute("href", "css/" + obj.menus[i] + ".css");
                document.head.appendChild(script);
                document.head.appendChild(style);
            }
        } else {
            alert("Server error!");
        }
    });
}
