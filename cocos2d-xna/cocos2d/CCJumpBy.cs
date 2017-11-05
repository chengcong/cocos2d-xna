using System;

namespace cocos2d
{
	public class CCJumpBy : CCActionInterval
	{
		protected CCPoint m_startPosition;

		protected CCPoint m_delta;

		protected float m_height;

		protected uint m_nJumps;

		public CCJumpBy()
		{
		}

		public static CCJumpBy actionWithDuration(float duration, CCPoint position, float height, uint jumps)
		{
			CCJumpBy cCJumpBy = new CCJumpBy();
			cCJumpBy.initWithDuration(duration, position, height, jumps);
			return cCJumpBy;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCJumpBy cCJumpBy = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCJumpBy = new CCJumpBy();
				cCZone = new CCZone(cCJumpBy);
			}
			else
			{
				cCJumpBy = cCZone.m_pCopyObject as CCJumpBy;
				if (cCJumpBy == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCJumpBy.initWithDuration(this.m_fDuration, this.m_delta, this.m_height, this.m_nJumps);
			return cCJumpBy;
		}

		public bool initWithDuration(float duration, CCPoint position, float height, uint jumps)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_delta = position;
			this.m_height = height;
			this.m_nJumps = jumps;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCJumpBy.actionWithDuration(this.m_fDuration, CCPointExtension.ccp(-this.m_delta.x, -this.m_delta.y), this.m_height, this.m_nJumps);
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_startPosition = target.position;
		}

		public override void update(float dt)
		{
			if (this.m_pTarget != null)
			{
				float single = dt * (float)((float)this.m_nJumps) % 1f;
				float mHeight = this.m_height * 4f * single * (1f - single);
				mHeight = mHeight + this.m_delta.y * dt;
				float mDelta = this.m_delta.x * dt;
				this.m_pTarget.position = CCPointExtension.ccp(this.m_startPosition.x + mDelta, this.m_startPosition.y + mHeight);
			}
		}
	}
}