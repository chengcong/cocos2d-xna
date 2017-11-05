using System;

namespace cocos2d
{
	public class CCZHeader
	{
		public byte[] sig = new byte[4];

		public ushort compression_type;

		public ushort version;

		private uint reserved;

		private uint len;

		public CCZHeader()
		{
		}
	}
}