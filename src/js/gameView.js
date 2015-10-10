"use strict";

function openGameView(replace) {
    if ( typeof(replace) === "undefined" ) {
        replace = true;
    }
    var menu = document.createElement("div");
    menu.classList.add("menu");
    menu.classList.add("game-view");
    if ( replace ) {
        while ( document.body.children.length > 0 ) {
            document.body.removeChild(document.body.children[0]);
        }
    }
    document.body.appendChild(menu);
}
