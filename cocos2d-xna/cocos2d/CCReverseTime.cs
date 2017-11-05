using System;

namespace cocos2d
{
	public class CCReverseTime : CCActionInterval
	{
		protected CCFiniteTimeAction m_pOther;

		public CCReverseTime()
		{
		}

		public static CCReverseTime actionWithAction(CCFiniteTimeAction action)
		{
			CCReverseTime cCReverseTime = new CCReverseTime();
			cCReverseTime.initWithAction(action);
			return cCReverseTime;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCReverseTime cCReverseTime = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCReverseTime = new CCReverseTime();
				cCZone = new CCZone(cCReverseTime);
			}
			else
			{
				cCReverseTime = cCZone.m_pCopyObject as CCReverseTime;
				if (cCReverseTime == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCReverseTime.initWithAction(this.m_pOther.copy() as CCFiniteTimeAction);
			return cCReverseTime;
		}

		public bool initWithAction(CCFiniteTimeAction action)
		{
			if (!base.initWithDuration(action.duration))
			{
				return false;
			}
			this.m_pOther = action;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return this.m_pOther.copy() as CCFiniteTimeAction;
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_pOther.startWithTarget(target);
		}

		public override void stop()
		{
			this.m_pOther.stop();
			base.stop();
		}

		public override void update(float dt)
		{
			if (this.m_pOther != null)
			{
				this.m_pOther.update(1f - dt);
			}
		}
	}
}