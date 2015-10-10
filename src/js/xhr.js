"use strict";

function createXHR() {
    if ( window.XMLHttpRequest ) {
        return new XMLHttpRequest();
    } else if ( window.ActiveXObject ) {
        return new ActiveXObject("Microsoft.XMLHTTP");
    } else {
        alert("Unable to construct XHR");
        return null;
    }
}

function getRequest(url, callback) {
    var xhr = createXHR();
    if ( xhr == null ) {
        return;
    }
    xhr.open("GET", url, true);
    xhr.onreadystatechange = function() {
        if ( xhr.readyState == 4 ) {
            callback(xhr.responseText, xhr.status, xhr.responseXml);
        }
    };
    xhr.send();
}

function postRequest(url, data, callback) {
    var xhr = createXHR();
    if ( xhr == null ) {
        return;
    }
    xhr.open("POST", url, true);
    xhr.onreadystatechange = function() {
        if ( xhr.readyState == 4 ) {
            callback(xhr.responseText, xhr.status, xhr.responseXml);
        }
    };
    xhr.send(data);
}
