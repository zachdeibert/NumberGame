//
// AdvancedMath.cs
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
	public static class AdvancedMath {
		// Calculate n
		public static long Identity(long n) {
			return n;
		}

		//            e
		// Calculate  Π f(i)
		//           i=s
		public static long ProductSeq(Func<long, long> f, long s, long e) {
			long r = 1;
			for ( long i = s; i <= e; ++i ) {
				r *= f(i);
			}
			return r;
		}

		// Calculate n!
		public static ulong Factorial(ulong n) {
			return (ulong) ProductSeq(Identity, 2, (long) n);
		}

		// Calculate   C 
		//            n r
		public static ulong Combinations(ulong n, ulong r) {
			return (ulong) (ProductSeq(Identity, (long) r + 1, (long) n) / (long) Factorial(n - r));
		}
	}
}

