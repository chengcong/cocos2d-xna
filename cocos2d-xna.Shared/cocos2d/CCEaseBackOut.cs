using System;

namespace cocos2d
{
	public class CCEaseBackOut : CCActionEase
	{
		public CCEaseBackOut()
		{
		}

		public static new CCEaseBackOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseBackOut cCEaseBackOut = new CCEaseBackOut();
			if (cCEaseBackOut != null)
			{
				cCEaseBackOut.initWithAction(pAction);
			}
			return cCEaseBackOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseBackOut cCEaseBackOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseBackOut = new CCEaseBackOut();
				pZone = new CCZone(cCEaseBackOut);
			}
			else
			{
				cCEaseBackOut = pZone.m_pCopyObject as CCEaseBackOut;
			}
			cCEaseBackOut.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseBackOut;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseBackIn.actionWithAction((CCActionInterval)this.m_pOther.reverse());
		}

		public override void update(float time)
		{
			float single = 1.70158f;
			time = time - 1f;
			this.m_pOther.update(time * time * ((single + 1f) * time + single) + 1f);
		}
	}
}