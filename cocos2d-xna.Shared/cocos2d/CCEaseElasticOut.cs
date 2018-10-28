using System;

namespace cocos2d
{
	public class CCEaseElasticOut : CCEaseElastic
	{
		public CCEaseElasticOut()
		{
		}

		public static new CCEaseElasticOut actionWithAction(CCActionInterval pAction)
		{
			CCEaseElasticOut cCEaseElasticOut = new CCEaseElasticOut();
			if (cCEaseElasticOut != null)
			{
				cCEaseElasticOut.initWithAction(pAction);
			}
			return cCEaseElasticOut;
		}

		public static new CCEaseElasticOut actionWithAction(CCActionInterval pAction, float fPeriod)
		{
			CCEaseElasticOut cCEaseElasticOut = new CCEaseElasticOut();
			if (cCEaseElasticOut != null)
			{
				cCEaseElasticOut.initWithAction(pAction, fPeriod);
			}
			return cCEaseElasticOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCEaseElasticOut cCEaseElasticOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCEaseElasticOut = new CCEaseElasticOut();
				pZone = new CCZone(cCEaseElasticOut);
			}
			else
			{
				cCEaseElasticOut = pZone.m_pCopyObject as CCEaseElasticOut;
			}
			cCEaseElasticOut.initWithAction((CCActionInterval)this.m_pOther.copy(), this.m_fPeriod);
			return cCEaseElasticOut;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCEaseElasticIn.actionWithAction((CCActionInterval)this.m_pOther.reverse(), this.m_fPeriod);
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
				single = (float)(Math.Pow(2, (double)(-10f * time)) * Math.Sin((double)((time - mFPeriod) * 3.14159274f * 2f / this.m_fPeriod)) + 1);
			}
			this.m_pOther.update(single);
		}
	}
}