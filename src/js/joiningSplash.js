"use strict";

function openJoiningSplash(replace) {
    if ( typeof(replace) === "undefined" ) {
        replace = true;
    }
    var menu = document.createElement("div");
    menu.classList.add("menu");
    menu.classList.add("join-splash");
    var div = document.createElement("div");
    div.className = "text-box";
    var h1 = document.createElement("h1");
    h1.appendChild(document.createTextNode("Joining Game..."));
    var h2 = document.createElement("h2");
    h2.appendChild(document.createTextNode("Waiting for other players to join"));
    var cancel = document.createElement("div");
    cancel.classList.add("btn");
    cancel.classList.add("btn-danger");
    cancel.appendChild(document.createTextNode("Cancel"));
    div.appendChild(h1);
    div.appendChild(h2);
    div.appendChild(cancel);
    menu.appendChild(div);
    if ( replace ) {
        while ( document.body.children.length > 0 ) {
            document.body.removeChild(document.body.children[0]);
        }
    }
    document.body.appendChild(menu);
}
