using System;

namespace cocos2d
{
	public class CCEaseExponentialOut : CCActionEase
	{
		public CCEaseExponentialOut()
		{
		}

		public static new CCEaseExponentialOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseExponentialOut cCEaseExponentialOut = new CCEaseExponentialOut();
			if (cCEaseExponentialOut != null)
			{
				cCEaseExponentialOut.initWithAction(pAction);
			}
			return cCEaseExponentialOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseExponentialOut cCEaseExponentialOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseExponentialOut = new CCEaseExponentialOut();
				pZone = new CCZone(cCEaseExponentialOut);
			}
			else
			{
				cCEaseExponentialOut = pZone.m_pCopyObject as CCEaseExponentialOut;
			}
			cCEaseExponentialOut.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseExponentialOut;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseExponentialIn.actionWithAction((CCActionInterval)this.m_pOther.reverse());
		}

		public override void update(float time)
		{
			this.m_pOther.update((time == 1f ? 1f : -(float)Math.Pow(2, (double)(-10f * time / 1f)) + 1f));
		}
	}
}