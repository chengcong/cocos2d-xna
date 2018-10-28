using System;

namespace cocos2d
{
	public class CCProgressTo : CCActionInterval
	{
		protected float m_fTo;

		protected float m_fFrom;

		public CCProgressTo()
		{
		}

		public static CCProgressTo actionWithDuration(float duration, float fPercent)
		{
			CCProgressTo cCProgressTo = new CCProgressTo();
			cCProgressTo.initWithDuration(duration, fPercent);
			return cCProgressTo;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCProgressTo cCProgressTo = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCProgressTo = new CCProgressTo();
				pZone = new CCZone(cCProgressTo);
			}
			else
			{
				cCProgressTo = (CCProgressTo)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCProgressTo.initWithDuration(this.m_fDuration, this.m_fTo);
			return cCProgressTo;
		}

		public bool initWithDuration(float duration, float fPercent)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_fTo = fPercent;
			return true;
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_fFrom = ((CCProgressTimer)pTarget).Percentage;
			if (this.m_fFrom == 100f)
			{
				this.m_fFrom = 0f;
			}
		}

		public override void update(float time)
		{
			((CCProgressTimer)this.m_pTarget).Percentage = this.m_fFrom + (this.m_fTo - this.m_fFrom) * time;
		}
	}
}