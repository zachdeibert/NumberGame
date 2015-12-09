//
// init.js
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

NumberGame.Client.Angular = {
	"update": function() {
		$("input[ng-model=\"refresh\"]").val(NumberGame.Client.Network.randomTag())[0].dispatchEvent(new Event("change"));
	}
};
NumberGame.Client.Angular.app = angular.module("numberGame", []);
NumberGame.Client.Angular.app.controller("numberGameCtrl", function($scope) {
	NumberGame.Client.Angular.scope = $scope;
	$scope.game = {
		"players": [],
		"playerLookup": {},
		"match": null,
		"you": "00000000-0000-0000-0000-000000000000"
	};
	$scope.lobby = {
		"selectedPlayers": [],
		"size": 8
	};
	$scope.play = {
		"row": -1
	};
	$scope.contains = function(array, obj) {
		for ( var i = 0; i < array.length; ++i ) {
			if ( array[i] == obj ) {
				return true;
			}
		}
		return false;
	};
	$scope.range = function(n) {
		var a = [];
		for ( var i = 0; i < n; ++i ) {
			a.push(i);
		}
		return a;
	};
});

$(".page").hide();
$("#connect").show();
NumberGame.Client.Eula.showIfNeeded();
