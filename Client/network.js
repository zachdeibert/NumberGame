//
// network.js
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

NumberGame.Client.Network = {
	"tagChars": "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".split(""),
	"updaters": [],
	"tags": {},
	"handlers": {},
	"randomTag": function() {
		var chars = [];
		for ( var i = 0; i < 16; ++i ) {
			chars.push(NumberGame.Client.Network.tagChars[Math.floor(Math.random() * NumberGame.Client.Network.tagChars.length)]);
		}
		return chars.join("");
	},
	"connect": function(url, callback) {
		var socket = new WebSocket(url);
		socket.onerror = function() {
			socket.close();
			callback(false);
		};
		socket.onopen = function() {
			socket.onerror = function(err) {
				alert(err);
				socket.onclose();
			};
			socket.onclose = function() {
				location.reload();
			};
			socket.onmessage = function(res) {
				var obj = JSON.parse(res.data);
				if ( NumberGame.Client.Network.handlers[obj.type] ) {
					NumberGame.Client.Network.handlers[obj.type](obj);
				}
			};
			NumberGame.Client.Network.socket = socket;
			callback(true);
		};
	},
	"send": function(obj, callback, tag) {
		if ( callback ) {
			if ( !tag ) {
				tag = NumberGame.Client.Network.randomTag();
				obj.tag = tag;
			}
			NumberGame.Client.Network.tags[tag] = callback;
		}
		NumberGame.Client.Network.socket.send(JSON.stringify(obj));
	},
	"addProtocolHandler": function(type, callback) {
		if ( NumberGame.Client.Network.handlers[type] ) {
			var old = NumberGame.Client.Network.handlers[type];
			NumberGame.Client.Network.handlers[type] = function(obj) {
				old(obj);
				callback(obj);
			};
		} else {
			NumberGame.Client.Network.handlers[type] = callback;
		}
	},
	"clearProtocolHandlers": function(type) {
		if ( type ) {
			delete NumberGame.Client.Network.handlers[type];
		} else {
			NumberGame.Client.Network.handlers = {};
		}
	},
	"onUpdate": function(callback) {
		NumberGame.Client.Network.updaters.push(callback);
	}
};

{
	var tagHandler = function(obj) {
		if ( obj.tag && NumberGame.Client.Network.tags[obj.tag] ) {
			NumberGame.Client.Network.tags[obj.tag](obj);
			delete NumberGame.Client.Network.tags[obj.tag];
		}
	};
	NumberGame.Client.Network.addProtocolHandler("success", tagHandler);
	NumberGame.Client.Network.addProtocolHandler("failure", tagHandler);
	NumberGame.Client.Network.addProtocolHandler("ping", tagHandler);
	NumberGame.Client.Network.addProtocolHandler("update", function(obj) {
		for ( var i = 0; i < NumberGame.Client.Network.updaters.length; ++i ) {
			NumberGame.Client.Network.updaters[i](obj);
		}
	});
	NumberGame.Client.Network.onUpdate(function(obj) {
		NumberGame.Client.Angular.scope.game.players = obj.players;
		NumberGame.Client.Angular.scope.game.playerLookup = {};
		for ( var i = 0; i < obj.players.length; ++i ) {
			NumberGame.Client.Angular.scope.game.playerLookup[obj.players[i].uuid] = obj.players[i];
		}
		NumberGame.Client.Angular.scope.game.match = obj.match;
		NumberGame.Client.Angular.scope.game.you = obj.you;
		NumberGame.Client.Angular.update();
	});
}
