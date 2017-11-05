using System;

namespace cocos2d
{
	public class CCObject : CCCopying
	{
		public CCObject()
		{
		}

		public virtual CCObject copy()
		{
			return this.copyWithZone(null);
		}

		public virtual CCObject copyWithZone(CCZone zone)
		{
			return null;
		}
	}
}