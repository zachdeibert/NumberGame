"use strict";

function loadMenus() {
    var xmlhttp;
    if ( window.XMLHttpRequest ) {
        xmlhttp = new XMLHttpRequest();
    } else if ( window.ActiveXObject ) {
        xmlhttp = new ActiveXObject();
    } else {
        alert("Unable to initialize!");
        return;
    }
    xmlhttp.open("GET", "json/menus.json", true);
    xmlhttp.onreadystatechange = function() {
        if ( xmlhttp.readyState == 4 ) {
            if ( xmlhttp.status == 200 ) {
                var obj = JSON.parse(xmlhttp.responseText);
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
                    var style = document.createElement("style");
                    style.setAttribute("rel", "stylesheet");
                    style.setAttribute("type", "text/css");
                    style.setAttribute("src", "css/" + obj.menus[i] + ".css");
                    document.head.appendChild(script);
                    document.head.appendChild(style);
                }
            } else {
                alert("Internal error!");
            }
        }
    };
    xmlhttp.send();
}
