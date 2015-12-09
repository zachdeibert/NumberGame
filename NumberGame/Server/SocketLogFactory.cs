//
// SocketLogFactory.cs
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
using SuperSocket.SocketBase.Logging;

namespace NumberGame.Server {
	public class SocketLogFactory : ILogFactory {
		public class SocketLog : ILog {
			public bool IsDebugEnabled {
				get {
					return false;
				}
			}
			public bool IsErrorEnabled {
				get {
					return true;
				}
			}
			public bool IsFatalEnabled {
				get {
					return true;
				}
			}
			public bool IsInfoEnabled {
				get {
					return true;
				}
			}
			public bool IsWarnEnabled {
				get {
					return true;
				}
			}

			public void Log(string level, object message) {
				Console.WriteLine("{0}: {1}", level, message);
			}

			public void Log(string level, object message, Exception exception) {
				Console.Error.WriteLine("{0}: {1}", level, message);
				Console.Error.WriteLine(exception);
			}

			public void Log(string level, string format, object arg0, object arg1, object arg2) {
				Log(level, string.Format(format, arg0, arg1, arg2));
			}

			public void Log(string level, string format, object arg0, object arg1) {
				Log(level, string.Format(format, arg0, arg1));
			}

			public void Log(string level, IFormatProvider provider, string format, params object[] args) {
				Log(level, string.Format(provider, format, args));
			}

			public void Log(string level, string format, params object[] args) {
				Log(level, string.Format(format, args));
			}

			public void Log(string level, string format, object arg0) {
				Log(level, string.Format(format, arg0));
			}

			public void Debug(object message) {
			}

			public void Debug(object message, Exception exception) {
			}

			public void DebugFormat(string format, object arg0, object arg1, object arg2) {
			}

			public void DebugFormat(string format, object arg0, object arg1) {
			}

			public void DebugFormat(IFormatProvider provider, string format, params object[] args) {
			}

			public void DebugFormat(string format, params object[] args) {
			}

			public void DebugFormat(string format, object arg0) {
			}

			public void Error(object message) {
				Log("Error", message);
			}

			public void Error(object message, Exception exception) {
				Log("Error", message, exception);
			}

			public void ErrorFormat(string format, object arg0, object arg1, object arg2) {
				Log("Error", format, arg0, arg1, arg2);
			}

			public void ErrorFormat(string format, object arg0, object arg1) {
				Log("Error", format, arg0, arg1);
			}

			public void ErrorFormat(IFormatProvider provider, string format, params object[] args) {
				Log("Error", provider, format, args);
			}

			public void ErrorFormat(string format, params object[] args) {
				Log("Error", format, args);
			}

			public void ErrorFormat(string format, object arg0) {
				Log("Error", format, arg0);
			}

			public void Fatal(object message) {
				Log("Fatal", message);
			}

			public void Fatal(object message, Exception exception) {
				Log("Fatal", message, exception);
			}

			public void FatalFormat(string format, object arg0, object arg1, object arg2) {
				Log("Fatal", format, arg0, arg1, arg2);
			}

			public void FatalFormat(string format, object arg0, object arg1) {
				Log("Fatal", format, arg0, arg1);
			}

			public void FatalFormat(IFormatProvider provider, string format, params object[] args) {
				Log("Fatal", provider, format, args);
			}

			public void FatalFormat(string format, params object[] args) {
				Log("Fatal", format, args);
			}

			public void FatalFormat(string format, object arg0) {
				Log("Fatal", format, arg0);
			}

			public void Info(object message) {
				Log("Info", message);
			}

			public void Info(object message, Exception exception) {
				Log("Info", message, exception);
			}

			public void InfoFormat(string format, object arg0, object arg1, object arg2) {
				Log("Info", format, arg0, arg1, arg2);
			}

			public void InfoFormat(string format, object arg0, object arg1) {
				Log("Info", format, arg0, arg1);
			}

			public void InfoFormat(IFormatProvider provider, string format, params object[] args) {
				Log("Info", provider, format, args);
			}

			public void InfoFormat(string format, params object[] args) {
				Log("Info", format, args);
			}

			public void InfoFormat(string format, object arg0) {
				Log("Info", format, arg0);
			}

			public void Warn(object message) {
				Log("Warn", message);
			}

			public void Warn(object message, Exception exception) {
				Log("Warn", message, exception);
			}

			public void WarnFormat(string format, object arg0, object arg1, object arg2) {
				Log("Warn", format, arg0, arg1, arg2);
			}

			public void WarnFormat(string format, object arg0, object arg1) {
				Log("Warn", format, arg0, arg1);
			}

			public void WarnFormat(IFormatProvider provider, string format, params object[] args) {
				Log("Warn", provider, format, args);
			}

			public void WarnFormat(string format, params object[] args) {
				Log("Warn", format, args);
			}

			public void WarnFormat(string format, object arg0) {
				Log("Warn", format, arg0);
			}
		}

		public ILog GetLog(string name) {
			return new SocketLog();
		}
	}
}

