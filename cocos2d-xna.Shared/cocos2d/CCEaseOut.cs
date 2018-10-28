using System;

namespace cocos2d
{
	public class CCEaseOut : CCEaseRateAction
	{
		public CCEaseOut()
		{
		}

		public static new CCEaseOut actionWithAction(CCActionInterval pAction, float fRate)
		{
			CCEaseOut cCEaseOut = new CCEaseOut();
			if (cCEaseOut != null)
			{
				cCEaseOut.initWithAction(pAction, fRate);
			}
			return cCEaseOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseOut cCEaseOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseOut = new CCEaseOut();
				pZone = new CCZone(cCEaseOut);
			}
			else
			{
				cCEaseOut = (CCEaseOut)pZone.m_pCopyObject;
			}
			cCEaseOut.initWithAction((CCActionInterval)this.m_pOther.copy(), this.m_fRate);
			return cCEaseOut;
		}

		public override void update(float time)
		{
			this.m_pOther.update((float)Math.Pow((double)time, (double)(1f / this.m_fRate)));
		}
	}
}