using System;

namespace cocos2d
{
	public class CCEaseInOut : CCEaseRateAction
	{
		public CCEaseInOut()
		{
		}

		public static new CCEaseInOut actionWithAction(CCActionInterval pAction, float fRate)
		{
			CCEaseInOut cCEaseInOut = new CCEaseInOut();
			if (cCEaseInOut != null)
			{
				cCEaseInOut.initWithAction(pAction, fRate);
			}
			return cCEaseInOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseInOut cCEaseInOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseInOut = new CCEaseInOut();
				pZone = new CCZone(cCEaseInOut);
			}
			else
			{
				cCEaseInOut = pZone.m_pCopyObject as CCEaseInOut;
			}
			cCEaseInOut.initWithAction((CCActionInterval)this.m_pOther.copy(), this.m_fRate);
			return cCEaseInOut;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseInOut.actionWithAction((CCActionInterval)this.m_pOther.reverse(), this.m_fRate);
		}

		public override void update(float time)
		{
			int num = 1;
			if ((int)this.m_fRate % 2 == 0)
			{
				num = -1;
			}
			time = time * 2f;
			if (time < 1f)
			{
				this.m_pOther.update(0.5f * (float)Math.Pow((double)time, (double)this.m_fRate));
				return;
			}
			this.m_pOther.update((float)num * 0.5f * ((float)Math.Pow((double)(time - 2f), (double)this.m_fRate) + (float)(num * 2)));
		}
	}
}