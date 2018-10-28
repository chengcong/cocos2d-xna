using System;

namespace WP7Contrib.Communications.Compression
{
	internal class ZlibException : Exception
	{
		public ZlibException()
		{
		}

		public ZlibException(string s) : base(s)
		{
		}
	}
}