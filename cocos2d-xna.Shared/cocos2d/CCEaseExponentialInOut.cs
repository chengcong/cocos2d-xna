using System;

namespace cocos2d
{
	public class CCEaseExponentialInOut : CCActionEase
	{
		public CCEaseExponentialInOut()
		{
		}

		public static new CCEaseExponentialInOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseExponentialInOut cCEaseExponentialInOut = new CCEaseExponentialInOut();
			if (cCEaseExponentialInOut != null)
			{
				cCEaseExponentialInOut.initWithAction(pAction);
			}
			return cCEaseExponentialInOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseExponentialInOut cCEaseExponentialInOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseExponentialInOut = new CCEaseExponentialInOut();
				pZone = new CCZone(cCEaseExponentialInOut);
			}
			else
			{
				cCEaseExponentialInOut = pZone.m_pCopyObject as CCEaseExponentialInOut;
			}
			cCEaseExponentialInOut.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseExponentialInOut;
		}

		public override void update(float time)
		{
			time = time / 0.5f;
			if (time >= 1f)
			{
				time = 0.5f * (-(float)Math.Pow(2, (double)(10f * (time - 1f))) + 2f);
			}
			else
			{
				time = 0.5f * (float)Math.Pow(2, (double)(10f * (time - 1f)));
			}
			this.m_pOther.update(time);
		}
	}
}