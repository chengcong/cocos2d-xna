using System;

namespace WP7Contrib.Communications.Compression
{
	internal enum FlushType
	{
		None,
		Partial,
		Sync,
		Full,
		Finish
	}
}