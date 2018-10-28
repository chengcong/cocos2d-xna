using System;

namespace cocos2d
{
	public class CCDelayTime : CCActionInterval
	{
		public CCDelayTime()
		{
		}

		public static new CCDelayTime actionWithDuration(float d)
		{
			CCDelayTime cCDelayTime = new CCDelayTime();
			cCDelayTime.initWithDuration(d);
			return cCDelayTime;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCDelayTime cCDelayTime = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCDelayTime = new CCDelayTime();
				pZone = new CCZone(cCDelayTime);
			}
			else
			{
				cCDelayTime = (CCDelayTime)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			return cCDelayTime;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCDelayTime.actionWithDuration(this.m_fDuration);
		}

		public override void update(float time)
		{
		}
	}
}