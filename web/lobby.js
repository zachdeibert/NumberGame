//
// lobby.js
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

NumberGame.Client.Lobby = {
	"changeName": function() {
		var name = prompt("Please enter your new name", NumberGame.Client.Angular.scope.game.playerLookup[NumberGame.Client.Angular.scope.game.you].name);
		if ( name ) {
			NumberGame.Client.Network.send({
				"newName": name,
				"refresh": true
			}, function(obj) {
				if ( obj.success ) {
					document.title = "Number Game | " + name;
				} else {
					alert("Name was invalid");
					NumberGame.Client.Lobby.changeName();
				}
			});
		}
	},
	"start": function() {
		var size = $("input[name=\"size\"]").val();
		if ( size < 2 || size > 16 ) {
			return true;
		}
		if ( NumberGame.Client.Angular.scope.lobby.selectedPlayers < 2 ) {
			return false;
		}
		NumberGame.Client.Network.send({
			"players": NumberGame.Client.Angular.scope.lobby.selectedPlayers.concat(NumberGame.Client.Angular.scope.game.you),
			"size": size
		}, function(obj) {
			if ( !obj.success ) {
				alert("Error creating game");
			}
		});
		return false;
	},
	"select": function(uuid) {
		if ( uuid && NumberGame.Client.Angular.scope.lobby.selectedPlayers.indexOf(uuid) < 0 ) {
			NumberGame.Client.Angular.scope.lobby.selectedPlayers.push(uuid);
		}
		NumberGame.Client.Angular.update();
	},
	"deselect": function(uuid) {
		var selected = [];
		for ( var i = 0; i < NumberGame.Client.Angular.scope.lobby.selectedPlayers.length; ++i ) {
			if ( NumberGame.Client.Angular.scope.lobby.selectedPlayers[i] != uuid ) {
				selected.push(NumberGame.Client.Angular.scope.lobby.selectedPlayers[i]);
			}
		}
		NumberGame.Client.Angular.scope.lobby.selectedPlayers = selected;
		NumberGame.Client.Angular.update();
	}
};

NumberGame.Client.Network.onUpdate(function(obj) {
	var selected = [];
	for ( var i = 0; i < NumberGame.Client.Angular.scope.lobby.selectedPlayers.length; ++i ) {
		for ( var j = 0; j < obj.players.length; ++j ) {
			if ( obj.players[j].uuid == NumberGame.Client.Angular.scope.lobby.selectedPlayers[i] && obj.players[j].isConnected && !obj.players[j].isPlaying ) {
				selected.push(obj.players[j].uuid);
			}
		}
	}
	NumberGame.Client.Angular.scope.lobby.selectedPlayers = selected;
	for ( var i = 0; i < obj.players.length; ++i ) {
		if ( obj.players[i].uuid == obj.you && obj.players[i].isPlaying && $("#lobby").is(":visible") ) {
			$("#lobby").hide();
			$("#play").show();
		}
	}
	NumberGame.Client.Angular.update();
});
