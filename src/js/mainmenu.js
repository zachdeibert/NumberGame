"use strict";

function openMainMenu(replace = true) {
    var div = document.createElement("div");
    var h1 = document.createElement("h1");
    var text = document.createTextNode("Number Game");
    h1.appendChild(text);
    div.appendChild(h1);
    if ( replace ) {
        while ( document.body.children.length > 0 ) {
            document.body.removeChild(document.body.children[0]);
        }
    }
    document.body.appendChild(div);
}
