using System;

namespace cocos2d
{
	public class CCActionEase : CCActionInterval
	{
		protected CCActionInterval m_pOther;

		public CCActionEase()
		{
		}

		public static CCActionEase actionWithAction(CCActionInterval pAction)
		{
			CCActionEase cCActionEase = new CCActionEase();
			if (cCActionEase != null)
			{
				cCActionEase.initWithAction(pAction);
			}
			return cCActionEase;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCActionEase cCActionEase = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCActionEase = new CCActionEase();
				pZone = new CCZone(cCActionEase);
			}
			else
			{
				cCActionEase = pZone.m_pCopyObject as CCActionEase;
			}
			base.copyWithZone(pZone);
			cCActionEase.initWithAction((CCActionInterval)this.m_pOther.copy());
			return cCActionEase;
		}

		public bool initWithAction(CCActionInterval pAction)
		{
			if (!base.initWithDuration(pAction.duration))
			{
				return false;
			}
			this.m_pOther = pAction;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCActionEase.actionWithAction((CCActionInterval)this.m_pOther.reverse());
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_pOther.startWithTarget(this.m_pTarget);
		}

		public override void stop()
		{
			this.m_pOther.stop();
			base.stop();
		}

		public override void update(float time)
		{
			this.m_pOther.update(time);
		}
	}
}