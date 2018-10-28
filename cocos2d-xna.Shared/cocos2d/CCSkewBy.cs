using System;

namespace cocos2d
{
	public class CCSkewBy : CCSkewTo
	{
		public CCSkewBy()
		{
		}

		public static new CCSkewBy actionWithDuration(float t, float deltaSkewX, float deltaSkewY)
		{
			CCSkewBy cCSkewBy = new CCSkewBy();
			if (cCSkewBy != null)
			{
				cCSkewBy.initWithDuration(t, deltaSkewX, deltaSkewY);
			}
			return cCSkewBy;
		}

		public override bool initWithDuration(float t, float sx, float sy)
		{
			bool flag = false;
			if (base.initWithDuration(t, sx, sy))
			{
				this.m_fSkewX = sx;
				this.m_fSkewY = sy;
				flag = true;
			}
			return flag;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCSkewBy.actionWithDuration(this.m_fDuration, -this.m_fSkewX, -this.m_fSkewY);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_fDeltaX = this.m_fSkewX;
			this.m_fDeltaY = this.m_fSkewY;
			this.m_fEndSkewX = this.m_fStartSkewX + this.m_fDeltaX;
			this.m_fEndSkewY = this.m_fStartSkewY + this.m_fDeltaY;
		}
	}
}