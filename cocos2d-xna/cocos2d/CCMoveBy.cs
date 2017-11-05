using System;

namespace cocos2d
{
	public class CCMoveBy : CCMoveTo
	{
		public CCMoveBy()
		{
		}

		public static new CCMoveBy actionWithDuration(float duration, CCPoint position)
		{
			CCMoveBy cCMoveBy = new CCMoveBy();
			cCMoveBy.initWithDuration(duration, position);
			return cCMoveBy;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCMoveBy cCMoveBy = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCMoveBy = new CCMoveBy();
				cCZone = new CCZone(cCMoveBy);
			}
			else
			{
				cCMoveBy = cCZone.m_pCopyObject as CCMoveBy;
				if (cCMoveBy == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCMoveBy.initWithDuration(this.m_fDuration, this.m_delta);
			return cCMoveBy;
		}

		public new bool initWithDuration(float duration, CCPoint position)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_delta = position;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCMoveBy.actionWithDuration(this.m_fDuration, CCPointExtension.ccp(-this.m_delta.x, -this.m_delta.y));
		}

		public override void startWithTarget(CCNode target)
		{
			CCPoint mDelta = this.m_delta;
			base.startWithTarget(target);
			this.m_delta = mDelta;
		}
	}
}