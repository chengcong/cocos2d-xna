using System;

namespace cocos2d
{
	public class CCEaseBackIn : CCActionEase
	{
		public CCEaseBackIn()
		{
		}

		public static new CCEaseBackIn actionWithAction(CCActionInterval pAction)
		{
			CCEaseBackIn cCEaseBackIn = new CCEaseBackIn();
			if (cCEaseBackIn != null)
			{
				cCEaseBackIn.initWithAction(pAction);
			}
			return cCEaseBackIn;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseBackIn cCEaseBackIn = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseBackIn = new CCEaseBackIn();
				pZone = new CCZone(cCEaseBackIn);
			}
			else
			{
				cCEaseBackIn = pZone.m_pCopyObject as CCEaseBackIn;
			}
			cCEaseBackIn.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseBackIn;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseBackOut.actionWithAction((CCActionInterval)this.m_pOther.reverse());
		}

		public override void update(float time)
		{
			float single = 1.70158f;
			this.m_pOther.update(time * time * ((single + 1f) * time - single));
		}
	}
}