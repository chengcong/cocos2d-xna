using System;

namespace cocos2d
{
	public class CCEaseElastic : CCActionEase
	{
		protected float m_fPeriod;

		public float Period
		{
			get
			{
				return this.m_fPeriod;
			}
			set
			{
				this.m_fPeriod = value;
			}
		}

		public CCEaseElastic()
		{
		}

		public static new CCEaseElastic actionWithAction(CCActionInterval pAction)
		{
			CCEaseElastic cCEaseElastic = new CCEaseElastic();
			if (cCEaseElastic != null)
			{
				cCEaseElastic.initWithAction(pAction);
			}
			return cCEaseElastic;
		}

		public static CCEaseElastic actionWithAction(CCActionInterval pAction, float fPeriod)
		{
			CCEaseElastic cCEaseElastic = new CCEaseElastic();
			if (cCEaseElastic != null)
			{
				cCEaseElastic.initWithAction(pAction, fPeriod);
			}
			return cCEaseElastic;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseElastic cCEaseElastic = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseElastic = new CCEaseElastic();
				pZone = new CCZone(cCEaseElastic);
			}
			else
			{
				cCEaseElastic = pZone.m_pCopyObject as CCEaseElastic;
			}
			cCEaseElastic.initWithAction((CCActionInterval)this.m_pOther.copy(), this.m_fPeriod);
			return cCEaseElastic;
		}

		public bool initWithAction(CCActionInterval pAction, float fPeriod)
		{
			if (!base.initWithAction(pAction))
			{
				return false;
			}
			this.m_fPeriod = fPeriod;
			return true;
		}

		public new bool initWithAction(CCActionInterval pAction)
		{
			return this.initWithAction(pAction, 0.3f);
		}

		public override CCFiniteTimeAction reverse()
		{
			return null;
		}
	}
}