using System;

namespace cocos2d
{
	public class CCEaseElasticIn : CCEaseElastic
	{
		public CCEaseElasticIn()
		{
		}

		public static new CCEaseElasticIn actionWithAction(CCActionInterval pAction)
		{
			CCEaseElasticIn cCEaseElasticIn = new CCEaseElasticIn();
			if (cCEaseElasticIn != null)
			{
				cCEaseElasticIn.initWithAction(pAction);
			}
			return cCEaseElasticIn;
		}

		public static new CCEaseElasticIn actionWithAction(CCActionInterval pAction, float fPeriod)
		{
			CCEaseElasticIn cCEaseElasticIn = new CCEaseElasticIn();
			if (cCEaseElasticIn != null)
			{
				cCEaseElasticIn.initWithAction(pAction, fPeriod);
			}
			return cCEaseElasticIn;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseElasticIn cCEaseElasticIn = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseElasticIn = new CCEaseElasticIn();
				pZone = new CCZone(cCEaseElasticIn);
			}
			else
			{
				cCEaseElasticIn = pZone.m_pCopyObject as CCEaseElasticIn;
			}
			cCEaseElasticIn.initWithAction((CCActionInterval)this.m_pOther.copy(), this.m_fPeriod);
			return cCEaseElasticIn;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseElasticOut.actionWithAction((CCActionInterval)this.m_pOther.reverse(), this.m_fPeriod);
		}

		public override void update(float time)
		{
			float single = 0f;
			if (time == 0f || time == 1f)
			{
				single = time;
			}
			else
			{
				float mFPeriod = this.m_fPeriod / 4f;
				time = time - 1f;
				single = -(float)(Math.Pow(2, (double)(10f * time)) * Math.Sin((double)((time - mFPeriod) * 3.14159274f * 2f / this.m_fPeriod)));
			}
			this.m_pOther.update(single);
		}
	}
}