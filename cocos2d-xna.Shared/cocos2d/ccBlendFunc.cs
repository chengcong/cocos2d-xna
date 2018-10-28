using System;

namespace cocos2d
{
	public class ccBlendFunc
	{
		public int src;

		public int dst;

		public ccBlendFunc()
		{
		}

		public ccBlendFunc(int src, int dst)
		{
			this.src = src;
			this.dst = dst;
		}
	}
}