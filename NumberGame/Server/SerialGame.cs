//
// SerialGame.cs
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
	public class SerialGame {
		public string type;
		public SerialPlayer[] players;
		public SerialMatch match;
		public string you;

		private void Init(Game game, Player target) {
			players = new SerialPlayer[game.Players.Count];
			int i = -1;
			foreach ( Player p in game.Players ) {
				players[++i] = new SerialPlayer(p);
			}
			if ( target.IsPlaying ) {
				match = new SerialMatch(game.GetMatch(target));
			}
		}

		public SerialGame(Game game, Player target) {
			type = "update";
			you = target.Guid.ToString();
			game.PerformSensitiveOperation(Init, game, target);
		}
	}
}

