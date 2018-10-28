using cocos2d;
using System;

namespace cocos2d.actions.action_intervals.action_ease
{
	public class CCEaseElasticInOut : CCEaseElastic
	{
		public CCEaseElasticInOut()
		{
		}

		public static new CCEaseElasticInOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseElasticInOut cCEaseElasticInOut = new CCEaseElasticInOut();
			if (cCEaseElasticInOut != null)
			{
				cCEaseElasticInOut.initWithAction(pAction);
			}
			return cCEaseElasticInOut;
		}

		public static new CCEaseElasticInOut actionWithAction(CCActionInterval pAction, float fPeriod)
		{
			CCEaseElasticInOut cCEaseElasticInOut = new CCEaseElasticInOut();
			if (cCEaseElasticInOut != null)
			{
				cCEaseElasticInOut.initWithAction(pAction, fPeriod);
			}
			return cCEaseElasticInOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseElasticInOut cCEaseElasticInOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseElasticInOut = new CCEaseElasticInOut();
				pZone = new CCZone(cCEaseElasticInOut);
			}
			else
			{
				cCEaseElasticInOut = pZone.m_pCopyObject as CCEaseElasticInOut;
			}
			cCEaseElasticInOut.initWithAction((CCActionInterval)this.m_pOther.copy(), this.m_fPeriod);
			return cCEaseElasticInOut;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseInOut.actionWithAction((CCActionInterval)this.m_pOther.reverse(), this.m_fPeriod);
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
				time = time * 2f;
				if (this.m_fPeriod == 0f)
				{
					this.m_fPeriod = 0.450000018f;
				}
				float mFPeriod = this.m_fPeriod / 4f;
				time = time - 1f;
				single = (time >= 0f ? (float)(Math.Pow(2, (double)(-10f * time)) * Math.Sin((double)((time - mFPeriod) * 6.28318548f / this.m_fPeriod)) * 0.5 + 1) : (float)(-0.5 * Math.Pow(2, (double)(10f * time)) * Math.Sin((double)((time - mFPeriod) * 6.28318548f / this.m_fPeriod))));
			}
			this.m_pOther.update(single);
		}
	}
}