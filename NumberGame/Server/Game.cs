//
// Game.cs
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
using System.Collections.Generic;
using System.Threading;
using SuperWebSocket;

namespace NumberGame.Server {
	public class Game {
		private Mutex Lock;
		public List<Player> Players;
		public List<Player> FreePlayers;
		public List<Match> Matches;

		public void PerformSensitiveOperation<A, B>(Action<A, B> act, A arg, B arg2) {
			Lock.WaitOne();
			act.Invoke(arg, arg2);
			Lock.ReleaseMutex();
		}

		public void PerformSensitiveOperation<A>(Action<A> act, A arg) {
			Lock.WaitOne();
			act.Invoke(arg);
			Lock.ReleaseMutex();
		}

		public void PerformSensitiveOperation(Action act) {
			Lock.WaitOne();
			act.Invoke();
			Lock.ReleaseMutex();
		}

		public void OnPlayerQuit(Player player) {
			Lock.WaitOne();
			player.IsAlive = false;
			player.IsConnected = false;
			if ( FreePlayers.Contains(player) ) {
				FreePlayers.Remove(player);
			} else {
				Match match = GetMatch(player);
				byte playersAlive = 0;
				foreach ( Player p in match.ParticipatingPlayers ) {
					if ( p != match.RemovedAboveOne && p != match.RemovedOne && p != player && p.IsAlive ) {
						++playersAlive;
					}
				}
				if ( playersAlive == 1 ) {
					OnMatchFinish(match);
				}
			}
			Players.Remove(player);
			OnRefreshGame();
			Lock.ReleaseMutex();
		}

		public bool OnPlayerName(Player player, string name) {
			Lock.WaitOne();
			// TODO Data validation
			player.Name = name;
			OnRefreshGame();
			Lock.ReleaseMutex();
			return true;
		}

		public bool OnPlayerMove(Player player, byte row, byte[] deltas) {
			Lock.WaitOne();
			Match match = GetMatch(player);
			if ( match == null ) {
				Lock.ReleaseMutex();
				return false;
			} else {
				bool result = match.TakeTurn(row, deltas, player, this);
				OnRefreshMatch(match);
				Lock.ReleaseMutex();
				return result;
			}
		}

		public void OnMatchFinish(Match match) {
			Lock.WaitOne();
			foreach ( Player p in match.ParticipatingPlayers ) {
				if ( p != match.RemovedAboveOne && p != match.RemovedOne && p.IsAlive ) {
					p.Score += match.ParticipatingPlayers.Length;
				}
				p.IsPlaying = false;
				FreePlayers.Add(p);
			}
			Matches.Remove(match);
			OnRefreshGame();
			Lock.ReleaseMutex();
		}

		public void OnRefreshGame() {
			Lock.WaitOne();
			foreach ( Player player in Players ) {
				OnRefreshPlayer(player);
			}
			Lock.ReleaseMutex();
		}

		public void OnRefreshMatch(Match match) {
			Lock.WaitOne();
			foreach ( Player player in match.ParticipatingPlayers ) {
				OnRefreshPlayer(player);
			}
			Lock.ReleaseMutex();
		}

		public void OnRefreshPlayer(Player player) {
			Lock.WaitOne();
			player.Session.Send(player.Session.AppServer.JsonSerialize(new SerialGame(this, player)));
			Lock.ReleaseMutex();
		}

		public Player GetPlayer(Guid guid) {
			Lock.WaitOne();
			foreach ( Player p in Players ) {
				if ( p.Guid == guid ) {
					Lock.ReleaseMutex();
					return p;
				}
			}
			Lock.ReleaseMutex();
			return null;
		}

		public Player GetPlayer(WebSocketSession session) {
			Lock.WaitOne();
			foreach ( Player p in Players ) {
				if ( p.Session == session ) {
					Lock.ReleaseMutex();
					return p;
				}
			}
			Player player = new Player();
			player.Session = session;
			player.Name = "Anonymous Player";
			player.Guid = Guid.NewGuid();
			player.Score = 0;
			player.IsConnected = true;
			player.IsPlaying = false;
			Players.Add(player);
			FreePlayers.Add(player);
			OnRefreshGame();
			Lock.ReleaseMutex();
			return player;
		}

		public Match GetMatch(Player player) {
			Lock.WaitOne();
			foreach ( Match match in Matches ) {
				if ( match.IsPlaying(player) ) {
					Lock.ReleaseMutex();
					return match;
				}
			}
			Lock.ReleaseMutex();
			return null;
		}

		public bool StartMatch(Player[] players, byte size) {
			Lock.WaitOne();
			if ( players.Length < 3 || size < 2 || size > 16 ) {
				Lock.ReleaseMutex();
				return false;
			}
			foreach ( Player player in players ) {
				if ( !FreePlayers.Contains(player) ) {
					Lock.ReleaseMutex();
					return false;
				}
			}
			Match match = new Match();
			match.ParticipatingPlayers = players;
			match.RemovedOne = null;
			match.RemovedAboveOne = null;
			match.Board = new ushort[size][];
			for ( byte x = 0; x < size; ++x ) {
				match.Board[x] = new ushort[x + 1];
				for ( byte y = 0; y <= x; ++y ) {
					match.Board[x][y] = (ushort) AdvancedMath.Combinations(x, y);
				}
			}
			match.Turn = 0;
			foreach ( Player player in players ) {
				FreePlayers.Remove(player);
				player.IsPlaying = true;
				player.IsAlive = true;
			}
			Matches.Add(match);
			OnRefreshGame();
			Lock.ReleaseMutex();
			return true;
		}

		public Game() {
			Lock = new Mutex(false);
			Players = new List<Player>();
			FreePlayers = new List<Player>();
			Matches = new List<Match>();
		}

		~Game() {
			Lock.WaitOne(1000);
			Lock.Close();
			Lock.Dispose();
		}
	}
}

