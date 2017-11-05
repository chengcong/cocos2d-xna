using System;

namespace cocos2d
{
	public class CCEaseBounceOut : CCEaseBounce
	{
		public CCEaseBounceOut()
		{
		}

		public static new CCEaseBounceOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseBounceOut cCEaseBounceOut = new CCEaseBounceOut();
			if (cCEaseBounceOut != null)
			{
				cCEaseBounceOut.initWithAction(pAction);
			}
			return cCEaseBounceOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseBounceOut cCEaseBounceOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseBounceOut = new CCEaseBounceOut();
				pZone = new CCZone(cCEaseBounceOut);
			}
			else
			{
				cCEaseBounceOut = pZone.m_pCopyObject as CCEaseBounceOut;
			}
			cCEaseBounceOut.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseBounceOut;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseBounceIn.actionWithAction((CCActionInterval)this.m_pOther.reverse());
		}

		public override void update(float time)
		{
			float single = base.bounceTime(time);
			this.m_pOther.update(single);
		}
	}
}