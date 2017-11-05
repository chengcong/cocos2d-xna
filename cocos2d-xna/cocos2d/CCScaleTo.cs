using System;

namespace cocos2d
{
	public class CCScaleTo : CCActionInterval
	{
		protected float m_fScaleX;

		protected float m_fScaleY;

		protected float m_fStartScaleX;

		protected float m_fStartScaleY;

		protected float m_fEndScaleX;

		protected float m_fEndScaleY;

		protected float m_fDeltaX;

		protected float m_fDeltaY;

		public CCScaleTo()
		{
		}

		public static CCScaleTo actionWithDuration(float duration, float s)
		{
			CCScaleTo cCScaleTo = new CCScaleTo();
			cCScaleTo.initWithDuration(duration, s);
			return cCScaleTo;
		}

		public static CCScaleTo actionWithDuration(float duration, float sx, float sy)
		{
			CCScaleTo cCScaleTo = new CCScaleTo();
			cCScaleTo.initWithDuration(duration, sx, sy);
			return cCScaleTo;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCScaleTo cCScaleTo = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCScaleTo = new CCScaleTo();
				pZone = new CCZone(cCScaleTo);
			}
			else
			{
				cCScaleTo = (CCScaleTo)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCScaleTo.initWithDuration(this.m_fDuration, this.m_fEndScaleX, this.m_fEndScaleY);
			return cCScaleTo;
		}

		public bool initWithDuration(float duration, float s)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_fEndScaleX = s;
			this.m_fEndScaleY = s;
			return true;
		}

		public bool initWithDuration(float duration, float sx, float sy)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_fEndScaleX = sx;
			this.m_fEndScaleY = sy;
			return true;
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_fStartScaleX = pTarget.scaleX;
			this.m_fStartScaleY = pTarget.scaleY;
			this.m_fDeltaX = this.m_fEndScaleX - this.m_fStartScaleX;
			this.m_fDeltaY = this.m_fEndScaleY - this.m_fStartScaleY;
		}

		public override void update(float time)
		{
			if (this.m_pTarget != null)
			{
				this.m_pTarget.scaleX = this.m_fStartScaleX + this.m_fDeltaX * time;
				this.m_pTarget.scaleY = this.m_fStartScaleY + this.m_fDeltaY * time;
			}
		}
	}
}