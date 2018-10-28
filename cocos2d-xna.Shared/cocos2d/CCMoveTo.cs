using System;

namespace cocos2d
{
	public class CCMoveTo : CCActionInterval
	{
		protected CCPoint m_endPosition = new CCPoint(0f, 0f);

		protected CCPoint m_startPosition = new CCPoint(0f, 0f);

		protected CCPoint m_delta = new CCPoint(0f, 0f);

		public CCMoveTo()
		{
		}

		public static CCMoveTo actionWithDuration(float duration, CCPoint position)
		{
			CCMoveTo cCMoveTo = new CCMoveTo();
			cCMoveTo.initWithDuration(duration, position);
			return cCMoveTo;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCMoveTo cCMoveTo = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCMoveTo = new CCMoveTo();
				cCZone = new CCZone(cCMoveTo);
			}
			else
			{
				cCMoveTo = (CCMoveTo)cCZone.m_pCopyObject;
			}
			base.copyWithZone(cCZone);
			cCMoveTo.initWithDuration(this.m_fDuration, this.m_endPosition);
			return cCMoveTo;
		}

		public bool initWithDuration(float duration, CCPoint position)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_endPosition = position;
			return true;
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_startPosition = target.position;
			this.m_delta = CCPointExtension.ccpSub(this.m_endPosition, this.m_startPosition);
		}

		public override void update(float dt)
		{
			if (this.m_pTarget != null)
			{
				this.m_pTarget.position = CCPointExtension.ccp(this.m_startPosition.x + this.m_delta.x * dt, this.m_startPosition.y + this.m_delta.y * dt);
			}
		}
	}
}