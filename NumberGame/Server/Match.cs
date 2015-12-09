//
// Match.cs
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
using SuperWebSocket;

namespace NumberGame.Server {
	public class Match {
		public Player[] ParticipatingPlayers;
		public Player RemovedOne;
		public Player RemovedAboveOne;
		public ushort[][] Board;
		public byte Turn;

		public bool IsPlaying(WebSocketSession session) {
			foreach ( Player player in ParticipatingPlayers ) {
				if ( player.Session == session ) {
					return true;
				}
			}
			return false;
		}

		public bool IsPlaying(Player player) {
			foreach ( Player p in ParticipatingPlayers ) {
				if ( p == player ) {
					return true;
				}
			}
			return false;
		}

		public bool IsTurn(WebSocketSession session) {
			return session == ParticipatingPlayers[Turn].Session;
		}

		public bool IsTurn(Player player) {
			return player == ParticipatingPlayers[Turn];
		}

		public bool TakeTurn(byte row, byte[] deltas, Player player, Game game) {
			if ( !IsTurn(player) ) {
				return false;
			}
			if ( deltas.Length == row + 1 ) {
				for ( byte i = 0; i < deltas.Length; ++i ) {
					if ( Board[row][i] < deltas[i] ) {
						return false;
					}
				}
				bool removedOne = false;
				bool removedAboveOne = false;
				bool oneAdded = false;
				for ( byte i = 0; i < deltas.Length; ++i ) {
					if ( deltas[i] != 0 ) {
						if ( Board[row][i] == 1 ) {
							removedOne = true;
						}
						if ( Board[row][i] > 1 ) {
							removedAboveOne = true;
						}
					}
					Board[row][i] -= deltas[i];
					if ( Board[row][i] == 1 ) {
						oneAdded = true;
					}
				}
				if ( RemovedOne == null ) {
					if ( removedOne ) {
						for ( byte x = 0; x < Board.Length; ++x ) {
							for ( byte y = 0; y < Board[x].Length; ++y ) {
								if ( Board[x][y] == 1 ) {
									removedOne = false;
									break;
								}
							}
							if ( !removedOne ) {
								break;
							}
						}
						if ( removedOne ) {
							RemovedOne = player;
							player.IsAlive = false;
						}
					}
				} else if ( oneAdded ) {
					RemovedOne.IsAlive = true;
					RemovedOne = null;
				}
				if ( RemovedAboveOne == null ) {
					if ( removedAboveOne ) {
						for ( byte x = 0; x < Board.Length; ++x ) {
							for ( byte y = 0; y < Board[x].Length; ++y ) {
								if ( Board[x][y] > 1 ) {
									removedAboveOne = false;
									break;
								}
							}
							if ( !removedAboveOne ) {
								break;
							}
						}
						if ( removedAboveOne ) {
							RemovedAboveOne = player;
							player.IsAlive = false;
						}
					}
				}
				do {
					if ( ++Turn >= ParticipatingPlayers.Length ) {
						Turn = 0;
					}
				} while ( !ParticipatingPlayers[Turn].IsAlive || !ParticipatingPlayers[Turn].IsConnected );
				byte players = 0;
				foreach ( Player p in ParticipatingPlayers ) {
					if ( p.IsAlive && p.IsConnected ) {
						if ( ++players > 1 ) {
							break;
						}
					}
				}
				if ( players < 2 ) {
					game.OnMatchFinish(this);
				} else {
					bool empty = true;
					for ( ushort x = 0; x < Board.Length; ++x ) {
						for ( ushort y = 0; y < Board[x].Length; ++y ) {
							if ( Board[x][y] != 0 ) {
								empty = false;
								break;
							}
						}
						if ( !empty ) {
							break;
						}
					}
					if ( empty ) {
						game.OnMatchFinish(this);
					}
				}
			} else {
				return false;
			}
			return true;
		}
	}
}

