using System;

namespace cocos2d
{
	public class CCEaseSineIn : CCActionEase
	{
		public CCEaseSineIn()
		{
		}

		public static new CCEaseSineIn actionWithAction(CCActionInterval pAction)
		{
			CCEaseSineIn cCEaseSineIn = new CCEaseSineIn();
			if (cCEaseSineIn != null)
			{
				cCEaseSineIn.initWithAction(pAction);
			}
			return cCEaseSineIn;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseSineIn cCEaseSineIn = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseSineIn = new CCEaseSineIn();
				pZone = new CCZone(cCEaseSineIn);
			}
			else
			{
				cCEaseSineIn = (CCEaseSineIn)pZone.m_pCopyObject;
			}
			cCEaseSineIn.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseSineIn;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseSineOut.actionWithAction((CCActionInterval)this.m_pOther.reverse());
		}

		public override void update(float time)
		{
			this.m_pOther.update(-1f * (float)Math.Cos((double)(time * 6.28318548f)) + 1f);
		}
	}
}