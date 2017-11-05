using System;

namespace cocos2d
{
	public class CCDeccelAmplitude : CCActionInterval
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

		public CCDeccelAmplitude()
		{
		}

		public static CCDeccelAmplitude actionWithAction(CCAction pAction, float duration)
		{
			CCDeccelAmplitude cCDeccelAmplitude = new CCDeccelAmplitude();
			if (cCDeccelAmplitude.initWithAction(pAction, duration))
			{
				return cCDeccelAmplitude;
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
			return CCDeccelAmplitude.actionWithAction(this.m_pOther.reverse(), this.m_fDuration);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_pOther.startWithTarget(pTarget);
		}

		public override void update(float time)
		{
			((CCDeccelAmplitude)this.m_pOther).setAmplitudeRate((float)Math.Pow((double)(1f - time), (double)this.m_fRate));
			this.m_pOther.update(time);
		}
	}
}