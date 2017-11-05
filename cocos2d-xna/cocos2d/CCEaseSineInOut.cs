using System;

namespace cocos2d
{
	public class CCEaseSineInOut : CCActionEase
	{
		public CCEaseSineInOut()
		{
		}

		public static new CCEaseSineInOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseSineInOut cCEaseSineInOut = new CCEaseSineInOut();
			if (cCEaseSineInOut != null)
			{
				cCEaseSineInOut.initWithAction(pAction);
			}
			return cCEaseSineInOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseSineInOut cCEaseSineInOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseSineInOut = new CCEaseSineInOut();
				pZone = new CCZone(cCEaseSineInOut);
			}
			else
			{
				cCEaseSineInOut = (CCEaseSineInOut)pZone.m_pCopyObject;
			}
			cCEaseSineInOut.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseSineInOut;
		}

		public override void update(float time)
		{
			this.m_pOther.update(-0.5f * ((float)Math.Cos((double)(3.14159274f * time)) - 1f));
		}
	}
}