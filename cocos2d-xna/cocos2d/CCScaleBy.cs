using System;

namespace cocos2d
{
	public class CCScaleBy : CCScaleTo
	{
		public CCScaleBy()
		{
		}

		public static new CCScaleBy actionWithDuration(float duration, float s)
		{
			CCScaleBy cCScaleBy = new CCScaleBy();
			cCScaleBy.initWithDuration(duration, s);
			return cCScaleBy;
		}

		public static new CCScaleBy actionWithDuration(float duration, float sx, float sy)
		{
			CCScaleBy cCScaleBy = new CCScaleBy();
			cCScaleBy.initWithDuration(duration, sx, sy);
			return cCScaleBy;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCScaleTo cCScaleBy = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCScaleBy = new CCScaleBy();
				pZone = new CCZone(cCScaleBy);
			}
			else
			{
				cCScaleBy = (CCScaleBy)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCScaleBy.initWithDuration(this.m_fDuration, this.m_fEndScaleX, this.m_fEndScaleY);
			return cCScaleBy;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCScaleBy.actionWithDuration(this.m_fDuration, 1f / this.m_fEndScaleX, 1f / this.m_fEndScaleY);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_fDeltaX = this.m_fStartScaleX * this.m_fEndScaleX - this.m_fStartScaleX;
			this.m_fDeltaY = this.m_fStartScaleY * this.m_fEndScaleY - this.m_fStartScaleY;
		}
	}
}