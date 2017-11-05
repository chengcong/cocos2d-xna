using cocos2d;
using System;

namespace cocos2d.actions
{
	public class CCCatmullRomTo : CCCardinalSplineTo
	{
		public CCCatmullRomTo()
		{
		}

		public static CCCatmullRomTo actionWithDuration(float dt, CCPointArray points)
		{
			return CCCatmullRomTo.create(dt, points);
		}

		public static CCCatmullRomTo create(float dt, CCPointArray points)
		{
			CCCatmullRomTo cCCatmullRomTo = new CCCatmullRomTo();
			cCCatmullRomTo.initWithDuration(dt, points);
			return cCCatmullRomTo;
		}

		public virtual bool initWithDuration(float dt, CCPointArray points)
		{
			if (base.initWithDuration(dt, points, 0.5f))
			{
				return true;
			}
			return false;
		}
	}
}