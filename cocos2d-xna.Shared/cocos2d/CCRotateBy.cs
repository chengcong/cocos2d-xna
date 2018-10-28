using System;

namespace cocos2d
{
	public class CCRotateBy : CCActionInterval
	{
		protected float m_fAngle;

		protected float m_fStartAngle;

		public CCRotateBy()
		{
		}

		public static CCRotateBy actionWithDuration(float duration, float fDeltaAngle)
		{
			CCRotateBy cCRotateBy = new CCRotateBy();
			cCRotateBy.initWithDuration(duration, fDeltaAngle);
			return cCRotateBy;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCRotateBy cCRotateBy = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCRotateBy = new CCRotateBy();
				cCZone = new CCZone(cCRotateBy);
			}
			else
			{
				cCRotateBy = cCZone.m_pCopyObject as CCRotateBy;
				if (cCRotateBy == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCRotateBy.initWithDuration(this.m_fDuration, this.m_fAngle);
			return cCRotateBy;
		}

		public bool initWithDuration(float duration, float fDeltaAngle)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_fAngle = fDeltaAngle;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCRotateBy.actionWithDuration(this.m_fDuration, -this.m_fAngle);
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_fStartAngle = target.rotation;
		}

		public override void update(float dt)
		{
			if (this.m_pTarget != null)
			{
				this.m_pTarget.rotation = this.m_fStartAngle + this.m_fAngle * dt;
			}
		}
	}
}