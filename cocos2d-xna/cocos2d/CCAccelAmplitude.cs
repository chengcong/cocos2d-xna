using System;

namespace cocos2d
{
	public class CCAccelAmplitude : CCActionInterval
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

		public CCAccelAmplitude()
		{
		}

		public static CCAccelAmplitude actionWithAction(CCAction pAction, float duration)
		{
			CCAccelAmplitude cCAccelAmplitude = new CCAccelAmplitude();
			if (cCAccelAmplitude.initWithAction(pAction, duration))
			{
				return cCAccelAmplitude;
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
			return CCAccelAmplitude.actionWithAction(this.m_pOther.reverse(), this.m_fDuration);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_pOther.startWithTarget(pTarget);
		}

		public override void update(float time)
		{
			((CCAccelAmplitude)this.m_pOther).setAmplitudeRate((float)Math.Pow((double)time, (double)this.m_fRate));
			this.m_pOther.update(time);
		}
	}
}