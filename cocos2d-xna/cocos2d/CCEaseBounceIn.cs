using System;

namespace cocos2d
{
	public class CCEaseBounceIn : CCEaseBounce
	{
		public CCEaseBounceIn()
		{
		}

		public static new CCEaseBounceIn actionWithAction(CCActionInterval pAction)
		{
			CCEaseBounceIn cCEaseBounceIn = new CCEaseBounceIn();
			if (cCEaseBounceIn != null)
			{
				cCEaseBounceIn.initWithAction(pAction);
			}
			return cCEaseBounceIn;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseBounceIn cCEaseBounceIn = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseBounceIn = new CCEaseBounceIn();
				pZone = new CCZone(cCEaseBounceIn);
			}
			else
			{
				cCEaseBounceIn = pZone.m_pCopyObject as CCEaseBounceIn;
			}
			cCEaseBounceIn.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseBounceIn;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseBounceOut.actionWithAction((CCActionInterval)this.m_pOther.reverse());
		}

		public override void update(float time)
		{
			float single = 1f - base.bounceTime(1f - time);
			this.m_pOther.update(single);
		}
	}
}