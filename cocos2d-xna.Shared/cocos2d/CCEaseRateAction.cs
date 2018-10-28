using System;

namespace cocos2d
{
	public class CCEaseRateAction : CCActionEase
	{
		protected float m_fRate;

		public float Rate
		{
			get
			{
				return this.m_fRate;
			}
			set
			{
				this.m_fRate = value;
			}
		}

		public CCEaseRateAction()
		{
		}

		public static CCEaseRateAction actionWithAction(CCActionInterval pAction, float fRate)
		{
			CCEaseRateAction cCEaseRateAction = new CCEaseRateAction();
			if (cCEaseRateAction != null)
			{
				cCEaseRateAction.initWithAction(pAction, fRate);
			}
			return cCEaseRateAction;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseRateAction cCEaseRateAction = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseRateAction = new CCEaseRateAction();
				pZone = new CCZone(cCEaseRateAction);
			}
			else
			{
				cCEaseRateAction = (CCEaseRateAction)pZone.m_pCopyObject;
			}
			cCEaseRateAction.initWithAction((CCActionInterval)this.m_pOther.copy(), this.m_fRate);
			return cCEaseRateAction;
		}

		public bool initWithAction(CCActionInterval pAction, float fRate)
		{
			if (!base.initWithAction(pAction))
			{
				return false;
			}
			this.m_fRate = fRate;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseRateAction.actionWithAction((CCActionInterval)this.m_pOther.reverse(), 1f / this.m_fRate);
		}
	}
}