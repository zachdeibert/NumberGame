//
// connect.js
//
// Author:
//       Zach Deibert <zachdeibert@gmail.com>
//
// Copyright (c) 2015 Zach Deibert
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
"use strict";

if ( !window.NumberGame ) {
	window.NumberGame = {};
}
if ( !NumberGame.Client ) {
	NumberGame.Client = {};
}

NumberGame.Client.Connect = {
	"connect": function() {
		var host = $("#connect input[name=\"host\"]").val();
		var port = $("#connect input[name=\"port\"]").val();
		var name = $("#connect input[name=\"name\"]").val();
		var url = "ws://" + host + ":" + port + "/";
		NumberGame.Client.Network.connect(url, function(success) {
			if ( success ) {
				NumberGame.Client.Network.send({
					"newName": name,
					"refresh": true
				}, function(obj) {
					if ( obj.success ) {
						document.title = "Number Game | " + name;
					} else {
						alert("Name was invalid");
					}
				});
				$(".page").hide();
				$("#lobby").show();
			} else {
				alert("Unable to connect to server!");
			}
		});
		return false;
	}
};

$(document).ready(function() {
	var id = setInterval(function() {
		if ( $("#connect input[name=\"host\"]").val(location.hostname).length > 0 ) {
			clearTimeout(id);
		}
	}, 10);
});
