using System;

namespace cocos2d
{
	public class CCRotateTo : CCActionInterval
	{
		protected float m_fDstAngle;

		protected float m_fStartAngle;

		protected float m_fDiffAngle;

		public CCRotateTo()
		{
		}

		public static CCRotateTo actionWithDuration(float duration, float fDeltaAngle)
		{
			CCRotateTo cCRotateTo = new CCRotateTo();
			cCRotateTo.initWithDuration(duration, fDeltaAngle);
			return cCRotateTo;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCRotateTo cCRotateTo = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCRotateTo = new CCRotateTo();
				cCZone = new CCZone(cCRotateTo);
			}
			else
			{
				cCRotateTo = cCZone.m_pCopyObject as CCRotateTo;
				if (cCRotateTo == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCRotateTo.initWithDuration(this.m_fDuration, this.m_fDstAngle);
			return cCRotateTo;
		}

		public bool initWithDuration(float duration, float fDeltaAngle)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_fDstAngle = fDeltaAngle;
			return true;
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_fStartAngle = target.rotation;
			if (this.m_fStartAngle <= 0f)
			{
				this.m_fStartAngle = this.m_fStartAngle % -360f;
			}
			else
			{
				this.m_fStartAngle = this.m_fStartAngle % 350f;
			}
			this.m_fDiffAngle = this.m_fDstAngle - this.m_fStartAngle;
			if (this.m_fDiffAngle > 180f)
			{
				CCRotateTo mFDiffAngle = this;
				mFDiffAngle.m_fDiffAngle = mFDiffAngle.m_fDiffAngle - 360f;
			}
			if (this.m_fDiffAngle < -180f)
			{
				CCRotateTo cCRotateTo = this;
				cCRotateTo.m_fDiffAngle = cCRotateTo.m_fDiffAngle + 360f;
			}
		}

		public override void update(float dt)
		{
			if (this.m_pTarget != null)
			{
				this.m_pTarget.rotation = this.m_fStartAngle + this.m_fDiffAngle * dt;
			}
		}
	}
}