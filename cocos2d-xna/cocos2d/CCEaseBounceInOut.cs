using System;

namespace cocos2d
{
	public class CCEaseBounceInOut : CCEaseBounce
	{
		public CCEaseBounceInOut()
		{
		}

		public static new CCEaseBounceInOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseBounceInOut cCEaseBounceInOut = new CCEaseBounceInOut();
			if (cCEaseBounceInOut != null)
			{
				cCEaseBounceInOut.initWithAction(pAction);
			}
			return cCEaseBounceInOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseBounceInOut cCEaseBounceInOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseBounceInOut = new CCEaseBounceInOut();
				pZone = new CCZone(cCEaseBounceInOut);
			}
			else
			{
				cCEaseBounceInOut = pZone.m_pCopyObject as CCEaseBounceInOut;
			}
			cCEaseBounceInOut.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseBounceInOut;
		}

		public override void update(float time)
		{
			float single = 0f;
			if (time >= 0.5f)
			{
				single = base.bounceTime(time * 2f - 1f) * 0.5f + 0.5f;
			}
			else
			{
				time = time * 2f;
				single = (1f - base.bounceTime(1f - time)) * 0.5f;
			}
			this.m_pOther.update(single);
		}
	}
}