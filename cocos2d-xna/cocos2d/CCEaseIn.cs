using System;

namespace cocos2d
{
	public class CCEaseIn : CCEaseRateAction
	{
		public CCEaseIn()
		{
		}

		public static new CCEaseIn actionWithAction(CCActionInterval pAction, float fRate)
		{
			CCEaseIn cCEaseIn = new CCEaseIn();
			if (cCEaseIn != null)
			{
				cCEaseIn.initWithAction(pAction, fRate);
			}
			return cCEaseIn;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseIn cCEaseIn = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseIn = new CCEaseIn();
				pZone = new CCZone(cCEaseIn);
			}
			else
			{
				cCEaseIn = pZone.m_pCopyObject as CCEaseIn;
			}
			cCEaseIn.initWithAction((CCActionInterval)this.m_pOther.copy(), this.m_fRate);
			return cCEaseIn;
		}

		public override void update(float time)
		{
			this.m_pOther.update((float)Math.Pow((double)time, (double)this.m_fRate));
		}
	}
}