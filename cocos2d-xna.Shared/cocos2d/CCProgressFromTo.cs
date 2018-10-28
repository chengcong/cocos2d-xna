using System;

namespace cocos2d
{
	public class CCProgressFromTo : CCActionInterval
	{
		protected float m_fTo;

		protected float m_fFrom;

		public CCProgressFromTo()
		{
		}

		public static CCProgressFromTo actionWithDuration(float duration, float fFromPercentage, float fToPercentage)
		{
			CCProgressFromTo cCProgressFromTo = new CCProgressFromTo();
			cCProgressFromTo.initWithDuration(duration, fFromPercentage, fToPercentage);
			return cCProgressFromTo;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCProgressFromTo cCProgressFromTo = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCProgressFromTo = new CCProgressFromTo();
				pZone = new CCZone(cCProgressFromTo);
			}
			else
			{
				cCProgressFromTo = (CCProgressFromTo)pZone.m_pCopyObject;
			}
			this.copyWithZone(pZone);
			cCProgressFromTo.initWithDuration(this.m_fDuration, this.m_fFrom, this.m_fTo);
			return cCProgressFromTo;
		}

		public bool initWithDuration(float duration, float fFromPercentage, float fToPercentage)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_fTo = fToPercentage;
			this.m_fFrom = fFromPercentage;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCProgressFromTo.actionWithDuration(this.m_fDuration, this.m_fTo, this.m_fFrom);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
		}

		public override void update(float time)
		{
			((CCProgressTimer)this.m_pTarget).Percentage = this.m_fFrom + (this.m_fTo - this.m_fFrom) * time;
		}
	}
}