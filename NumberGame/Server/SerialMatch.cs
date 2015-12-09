//
// SerialMatch.cs
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

namespace NumberGame.Server {
	public class SerialMatch {
		public string[] players;
		public string removedOne;
		public string removedAboveOne;
		public ushort[][] board;
		public string turn;

		public SerialMatch(Match match) {
			players = new string[match.ParticipatingPlayers.Length];
			for ( uint i = 0; i < players.Length; ++i ) {
				players[i] = match.ParticipatingPlayers[i].Guid.ToString();
			}
			removedOne = match.RemovedOne == null ? null : match.RemovedOne.Guid.ToString();
			removedAboveOne = match.RemovedAboveOne == null ? null : match.RemovedAboveOne.Guid.ToString();
			board = match.Board;
			turn = match.ParticipatingPlayers[match.Turn].Guid.ToString();
		}
	}
}

