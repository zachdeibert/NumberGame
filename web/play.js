//
// play.js
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

NumberGame.Client.Play = {
	"copyValues": function(row) {
		NumberGame.Client.Angular.scope.play.turn = [];
		for ( var i = 0; i < NumberGame.Client.Angular.scope.game.match.board[row].length; ++i ) {
			NumberGame.Client.Angular.scope.play.turn[i] = NumberGame.Client.Angular.scope.game.match.board[row][i];
		}
	},
	"takeTurn": function() {
		var deltas = [];
		var valueInputs = $("#play input[type=\"number\"]").get();
		var valid = false;
		var invalid = false;
		for ( var i = 0; i < valueInputs.length; ++i ) {
			deltas.push(NumberGame.Client.Angular.scope.game.match.board[valueInputs.length - 1][i] - valueInputs[i].value);
			if ( deltas[i] > 0 ) {
				valid = true;
			}
			if ( deltas[i] < 0 || deltas[i] > NumberGame.Client.Angular.scope.game.match.board[valueInputs.length - 1][i] ) {
				invalid = true;
			}
		}
		if ( invalid ) {
			return true;
		}
		if ( !valid ) {
			alert("You must change something!");
			return false;
		}
		NumberGame.Client.Network.send({
			"row": valueInputs.length - 1,
			"turnDeltas": deltas
		}, function(obj) {
			if ( !obj.success ) {
				alert("Moving failure :(");
			}
		});
		return false;
	}
};

NumberGame.Client.Network.onUpdate(function(obj) {
	if ( $("#play").is(":visible") && obj.match == null ) {
		$("#play").hide();
		$("#lobby").show();
	}
});
