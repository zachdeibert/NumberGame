"use strict";

function openGameView(replace) {
    if ( typeof(replace) === "undefined" ) {
        replace = true;
    }
    var menu = document.createElement("div");
    menu.classList.add("menu");
    menu.classList.add("game-view");
    var table = document.createElement("table");
    for ( var i = 7; i >= 0; i-- ) {
        var tr = document.createElement("tr");
        var leftpad = document.createElement("td");
        leftpad.className = "padding";
        leftpad.setAttribute("colspan", i + 1);
        tr.appendChild(leftpad);
        for ( var j = i; j < 15 - i; j += 2 ) {
            var td = document.createElement("td");
            td.className = "cell";
            td.setAttribute("colspan", 2);
            tr.appendChild(td);
        }
        var rightpad = document.createElement("td");
        rightpad.className = "padding";
        rightpad.setAttribute("colspan", i + 1);
        tr.appendChild(rightpad);
        table.appendChild(tr);
    }
    menu.appendChild(table);
    if ( replace ) {
        while ( document.body.children.length > 0 ) {
            document.body.removeChild(document.body.children[0]);
        }
    }
    document.body.appendChild(menu);
}
