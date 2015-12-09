//
// Entry.cs
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
using System;
using System.Threading;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperWebSocket;

namespace NumberGame.Server {
	public static class Server {
		private static Game Game;

		private static void OnConnect(WebSocketSession session) {
			Console.WriteLine("New player connected from {0}.", session.RemoteEndPoint);
			Game.GetPlayer(session);
		}

		private static void OnMessage(WebSocketSession session, string value) {
			Console.WriteLine(value);
			Player player = Game.GetPlayer(session);
			IncommingJson obj = (IncommingJson) session.AppServer.JsonDeserialize(value, typeof(IncommingJson));
			if ( obj.NewName != null ) {
				BasicResponse resp = new BasicResponse();
				resp.Tag = obj.Tag;
				if ( Game.OnPlayerName(player, obj.NewName) ) {
					resp.Type = "success";
					resp.Success = true;
				} else {
					resp.Type = "failure";
					resp.Success = false;
				}
				session.Send(session.AppServer.JsonSerialize(resp));
			}
			if ( obj.TurnDeltas != null && obj.TurnDeltas.Length > 0 ) {
				BasicResponse resp = new BasicResponse();
				resp.Tag = obj.Tag;
				if ( Game.OnPlayerMove(player, obj.Row, obj.TurnDeltas) ) {
					resp.Type = "success";
					resp.Success = true;
				} else {
					resp.Type = "failure";
					resp.Success = false;
				}
				session.Send(session.AppServer.JsonSerialize(resp));
			}
			if ( obj.Players != null && obj.Players.Length > 0 ) {
				BasicResponse resp = new BasicResponse();
				resp.Tag = obj.Tag;
				Player[] players = new Player[obj.Players.Length];
				bool valid = true;
				for ( int i = 0; i < players.Length; ++i ) {
					players[i] = Game.GetPlayer(Guid.Parse(obj.Players[i]));
					if ( players[i] == null ) {
						valid = false;
						break;
					}
				}
				if ( valid ) {
					valid = Game.StartMatch(players, obj.Size);
				}
				if ( valid ) {
					resp.Type = "success";
					resp.Success = true;
				} else {
					resp.Type = "failure";
					resp.Success = false;
				}
				session.Send(session.AppServer.JsonSerialize(resp));
			}
			if ( obj.Ping ) {
				BasicResponse resp = new BasicResponse();
				resp.Type = "ping";
				resp.Success = true;
				resp.Tag = obj.Tag;
				session.Send(session.AppServer.JsonSerialize(resp));
			}
			if ( obj.Refresh ) {
				Game.OnRefreshPlayer(player);
			}
		}

		private static void OnDisconnect(WebSocketSession session, CloseReason value) {
			Player player = Game.GetPlayer(session);
			Console.WriteLine("Player {0} disconnected", player.Name);
			Game.OnPlayerQuit(player);
		}

		public static void Main(string[] args) {
			Game = new Game();
			ServerConfig config = new ServerConfig();
			config.LogAllSocketException = true;
			config.LogBasicSessionActivity = true;
			config.LogCommand = true;
			config.ServerType = "Number Game";
			config.ServerTypeName = "Number Game/1.0";
			config.Port = 8080;
			Console.WriteLine("Starting server on port 8080.");
			WebSocketServer server = new WebSocketServer();
			if ( !server.Setup(config, null, null, new SocketLogFactory(), null, null) ) {
				Console.Error.WriteLine("Unable to configure server!");
				Console.ReadKey();
				return;
			}
			server.NewMessageReceived += OnMessage;
			server.NewSessionConnected += OnConnect;
			server.SessionClosed += OnDisconnect;
			if ( !server.Start() ) {
				Console.Error.WriteLine("Unable to start server!");
				Console.ReadKey();
				return;
			}
			Console.WriteLine("Press any key to stop the server.");
			try {
				Console.ReadKey();
			} catch ( InvalidOperationException ) {
				Console.WriteLine("Nevermind, you are not a human :)");
				Thread.Sleep(int.MaxValue);
			}
			server.Stop();
		}
	}
}

