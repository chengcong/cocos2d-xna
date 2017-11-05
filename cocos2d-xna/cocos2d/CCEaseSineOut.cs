using System;

namespace cocos2d
{
	public class CCEaseSineOut : CCActionEase
	{
		public CCEaseSineOut()
		{
		}

		public static new CCEaseSineOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseSineOut cCEaseSineOut = new CCEaseSineOut();
			if (cCEaseSineOut != null)
			{
				cCEaseSineOut.initWithAction(pAction);
			}
			return cCEaseSineOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseSineOut cCEaseSineOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseSineOut = new CCEaseSineOut();
				pZone = new CCZone(cCEaseSineOut);
			}
			else
			{
				cCEaseSineOut = pZone.m_pCopyObject as CCEaseSineOut;
			}
			cCEaseSineOut.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseSineOut;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseSineIn.actionWithAction((CCActionInterval)this.m_pOther.reverse());
		}

		public override void update(float time)
		{
			this.m_pOther.update((float)Math.Sin((double)(time * 6.28318548f)));
		}
	}
}