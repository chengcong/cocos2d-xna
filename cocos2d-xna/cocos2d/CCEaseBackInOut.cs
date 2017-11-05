using System;

namespace cocos2d
{
	public class CCEaseBackInOut : CCActionEase
	{
		public CCEaseBackInOut()
		{
		}

		public static new CCEaseBackInOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseBackInOut cCEaseBackInOut = new CCEaseBackInOut();
			if (cCEaseBackInOut != null)
			{
				cCEaseBackInOut.initWithAction(pAction);
			}
			return cCEaseBackInOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseBackInOut cCEaseBackInOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseBackInOut = new CCEaseBackInOut();
				pZone = new CCZone(cCEaseBackInOut);
			}
			else
			{
				cCEaseBackInOut = pZone.m_pCopyObject as CCEaseBackInOut;
			}
			cCEaseBackInOut.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCEaseBackInOut;
		}

		public override void update(float time)
		{
			float single = 2.59490943f;
			time = time * 2f;
			if (time < 1f)
			{
				this.m_pOther.update(time * time * ((single + 1f) * time - single) / 2f);
				return;
			}
			time = time - 2f;
			this.m_pOther.update(time * time * (single + 1f + single) / 2f + 1f);
		}
	}
}