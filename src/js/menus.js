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
                var menus = JSON.parse(xmlhttp.responseText);
                for ( var i = 0; i < menus.length; i++ ) {
                    var script = document.createElement("script");
                    script.setAttribute("type", "text/javascript");
                    script.setAttribute("src", "js/" + menus[i] + ".js");
                    var style = document.createElement("style");
                    style.setAttribute("rel", "stylesheet");
                    style.setAttribute("type", "text/css");
                    style.setAttribute("src", "css/" + menus[i] + ".css");
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
