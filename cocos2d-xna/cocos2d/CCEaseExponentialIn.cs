using System;

namespace cocos2d
{
	public class CCEaseExponentialIn : CCActionEase
	{
		public CCEaseExponentialIn()
		{
		}

		public static new CCEaseExponentialIn actionWithAction(CCActionInterval pAction)
		{
			CCEaseExponentialIn cCEaseExponentialIn = new CCEaseExponentialIn();
			if (cCEaseExponentialIn != null)
			{
				cCEaseExponentialIn.initWithAction(pAction);
			}
			return cCEaseExponentialIn;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseExponentialIn cCEaseExponentialIn = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseExponentialIn = new CCEaseExponentialIn();
				pZone = new CCZone(cCEaseExponentialIn);
			}
			else
			{
				cCEaseExponentialIn = pZone.m_pCopyObject as CCEaseExponentialIn;
			}
			cCEaseExponentialIn.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseExponentialIn;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseExponentialOut.actionWithAction((CCActionInterval)this.m_pOther.reverse());
		}

		public override void update(float time)
		{
			this.m_pOther.update((time == 0f ? 0f : (float)Math.Pow(2, (double)(10f * (time / 1f - 1f))) - 0.001f));
		}
	}
}