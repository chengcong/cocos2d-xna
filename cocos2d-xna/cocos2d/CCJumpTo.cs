using System;

namespace cocos2d
{
	public class CCJumpTo : CCJumpBy
	{
		public CCJumpTo()
		{
		}

		public static new CCJumpTo actionWithDuration(float duration, CCPoint position, float height, uint jumps)
		{
			CCJumpTo cCJumpTo = new CCJumpTo();
			cCJumpTo.initWithDuration(duration, position, height, jumps);
			return cCJumpTo;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCJumpTo cCJumpTo = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCJumpTo = new CCJumpTo();
				cCZone = new CCZone(cCJumpTo);
			}
			else
			{
				cCJumpTo = cCZone.m_pCopyObject as CCJumpTo;
				if (cCJumpTo == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCJumpTo.initWithDuration(this.m_fDuration, this.m_delta, this.m_height, this.m_nJumps);
			return cCJumpTo;
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_delta = CCPointExtension.ccp(this.m_delta.x - this.m_startPosition.x, this.m_delta.y - this.m_startPosition.y);
		}
	}
}