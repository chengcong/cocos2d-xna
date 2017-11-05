using System;

namespace cocos2d
{
	public class CCAccelDeccelAmplitude : CCActionInterval
	{
		protected float m_fRate;

		protected CCActionInterval m_pOther;

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

		public CCAccelDeccelAmplitude()
		{
		}

		public static CCAccelDeccelAmplitude actionWithAction(CCAction pAction, float duration)
		{
			CCAccelDeccelAmplitude cCAccelDeccelAmplitude = new CCAccelDeccelAmplitude();
			if (cCAccelDeccelAmplitude.initWithAction(pAction, duration))
			{
				return cCAccelDeccelAmplitude;
			}
			return null;
		}

		public bool initWithAction(CCAction pAction, float duration)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_fRate = 1f;
			this.m_pOther = pAction as CCActionInterval;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCAccelDeccelAmplitude.actionWithAction(this.m_pOther.reverse(), this.m_fDuration);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_pOther.startWithTarget(pTarget);
		}

		public override void update(float time)
		{
			float single = time * 2f;
			if (single > 1f)
			{
				single = single - 1f;
				single = 1f - single;
			}
			((CCAccelDeccelAmplitude)this.m_pOther).setAmplitudeRate((float)Math.Pow((double)single, (double)this.m_fRate));
		}
	}
}