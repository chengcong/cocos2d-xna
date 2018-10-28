using System;

namespace cocos2d
{
	public class CCEaseBounce : CCActionEase
	{
		public CCEaseBounce()
		{
		}

		public static new CCEaseBounce actionWithAction(CCActionInterval pAction)
		{
			CCEaseBounce cCEaseBounce = new CCEaseBounce();
			if (cCEaseBounce != null)
			{
				cCEaseBounce.initWithAction(pAction);
			}
			return cCEaseBounce;
		}

		public float bounceTime(float time)
		{
			if ((double)time < 0.363636363636364)
			{
				return 7.5625f * time * time;
			}
			if ((double)time < 0.727272727272727)
			{
				time = time - 0.545454562f;
				return 7.5625f * time * time + 0.75f;
			}
			if ((double)time < 0.909090909090909)
			{
				time = time - 0.8181818f;
				return 7.5625f * time * time + 0.9375f;
			}
			time = time - 0.954545438f;
			return 7.5625f * time * time + 0.984375f;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseBounce cCEaseBounce = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseBounce = new CCEaseBounce();
				pZone = new CCZone(cCEaseBounce);
			}
			else
			{
				cCEaseBounce = pZone.m_pCopyObject as CCEaseBounce;
			}
			cCEaseBounce.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseBounce;
		}
	}
}