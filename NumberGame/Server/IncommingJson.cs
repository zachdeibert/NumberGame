//
// IncommingJson.cs
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
	public class IncommingJson {
		public string newName;
		public byte row;
		public ushort[] turnDeltas;
		public string[] players;
		public byte size;
		public bool ping;
		public bool refresh;
		public string tag;

		public string NewName {
			get {
				return newName;
			}
			set {
				newName = value;
			}
		}
		public byte Row {
			get {
				return row;
			}
			set {
				row = value;
			}
		}
		public ushort[] TurnDeltas {
			get {
				return turnDeltas;
			}
			set {
				turnDeltas = value;
			}
		}
		public string[] Players {
			get {
				return players;
			}
			set {
				players = value;
			}
		}
		public byte Size {
			get {
				return size;
			}
			set {
				size = value;
			}
		}
		public bool Ping {
			get {
				return ping;
			}
			set {
				ping = value;
			}
		}
		public bool Refresh {
			get {
				return refresh;
			}
			set {
				refresh = value;
			}
		}
		public string Tag {
			get {
				return tag;
			}
			set {
				tag = value;
			}
		}

		public IncommingJson() {
			NewName = null;
			Row = 0;
			TurnDeltas = null;
			Players = null;
			Size = 0;
			Ping = false;
			Refresh = false;
		}
	}
}

