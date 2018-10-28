using System;

namespace cocos2d
{
	public class CCCatmullRomBy : CCCardinalSplineBy
	{
		public CCCatmullRomBy()
		{
		}

		public static CCCatmullRomBy actionWithDuration(float dt, CCPointArray points)
		{
			return CCCatmullRomBy.create(dt, points);
		}

		public static CCCatmullRomBy create(float dt, CCPointArray points)
		{
			CCCatmullRomBy cCCatmullRomBy = new CCCatmullRomBy();
			cCCatmullRomBy.initWithDuration(dt, points);
			return cCCatmullRomBy;
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