//
// eula.js
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

NumberGame.Client.Eula = {
	"licenseVersion": 1,
	"decline": function() {
		var date = new Date();
		date.setTime(date.getTime() + 30 * 24 * 60 * 60 * 1000);
		document.cookie = "license=0; expires=" + date.toUTCString();
		close();
		document.body.innerHTML = "";
		document.head.innerHTML = "";
		document.body.parentNode.removeAttribute("lang");
		document.body.removeAttribute("class");
		document.body.removeAttribute("ng-controller");
		document.body.removeAttribute("ng-app");
	},
	"accept": function() {
		var date = new Date();
		date.setTime(date.getTime() + 30 * 24 * 60 * 60 * 1000);
		document.cookie = "license=" + NumberGame.Client.Eula.licenseVersion + "; expires=" + date.toUTCString();
		$("#eula").hide();
		$("div.modal-backdrop").hide();
	},
	"hasAccepted": function() {
		var cookies = document.cookie.split(";");
		for ( var i = 0; i < cookies.length; ++i ) {
			var cookie = cookies[i];
			while ( cookie.charAt(0) == ' ' ) {
				cookie = cookie.substring(1);
			}
			if ( cookie.indexOf("license=") == 0 ) {
				return cookie.substring(8, cookie.length) >= NumberGame.Client.Eula.licenseVersion;
			}
		}
		return false;
	},
	"show": function() {
		$("#eula").modal({
			"backdrop": "status",
			"keyboard": false
		});
	},
	"showIfNeeded": function() {
		if ( !NumberGame.Client.Eula.hasAccepted() ) {
			NumberGame.Client.Eula.show();
		}
	}
};
