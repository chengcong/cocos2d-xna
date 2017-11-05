using System;

namespace cocos2d
{
	public class CCBlink : CCActionInterval
	{
		protected uint m_nTimes;

		public CCBlink()
		{
		}

		public static CCBlink actionWithDuration(float duration, uint uBlinks)
		{
			CCBlink cCBlink = new CCBlink();
			cCBlink.initWithDuration(duration, uBlinks);
			return cCBlink;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCBlink cCBlink = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCBlink = new CCBlink();
				pZone = new CCZone(cCBlink);
			}
			else
			{
				cCBlink = (CCBlink)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCBlink.initWithDuration(this.m_fDuration, this.m_nTimes);
			return cCBlink;
		}

		public bool initWithDuration(float duration, uint uBlinks)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_nTimes = uBlinks;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCBlink.actionWithDuration(this.m_fDuration, this.m_nTimes);
		}

		public override void update(float time)
		{
			if (this.m_pTarget != null && !this.isDone())
			{
				float mNTimes = 1f / (float)((float)this.m_nTimes);
				float single = time % mNTimes;
				this.m_pTarget.visible = (single > mNTimes / 2f ? true : false);
			}
		}
	}
}