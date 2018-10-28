using System;

namespace cocos2d
{
	public class CCActionInterval : CCFiniteTimeAction
	{
		protected float m_elapsed;

		protected bool m_bFirstTick;

		public float elapsed
		{
			get
			{
				return this.m_elapsed;
			}
		}

		public CCActionInterval()
		{
		}

		public static CCActionInterval actionWithDuration(float d)
		{
			CCActionInterval cCActionInterval = new CCActionInterval();
			cCActionInterval.initWithDuration(d);
			return cCActionInterval;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCActionInterval cCActionInterval = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCActionInterval = new CCActionInterval();
				cCZone = new CCZone(cCActionInterval);
			}
			else
			{
				cCActionInterval = (CCActionInterval)cCZone.m_pCopyObject;
			}
			base.copyWithZone(cCZone);
			cCActionInterval.initWithDuration(this.m_fDuration);
			return cCActionInterval;
		}

		public float getAmplitudeRate()
		{
			throw new NotImplementedException();
		}

		public bool initWithDuration(float d)
		{
			this.m_fDuration = d;
			if (this.m_fDuration == 0f)
			{
				this.m_fDuration = (float)ccMacros.FLT_EPSILON;
			}
			this.m_elapsed = 0f;
			this.m_bFirstTick = true;
			return true;
		}

		public override bool isDone()
		{
			return this.m_elapsed >= this.m_fDuration;
		}

		public override CCFiniteTimeAction reverse()
		{
			throw new NotImplementedException();
		}

		public void setAmplitudeRate(float amp)
		{
			throw new NotImplementedException();
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_elapsed = 0f;
			this.m_bFirstTick = true;
		}

		protected void startWithTargetUsedByCCOrbitCamera(CCNode target)
		{
			base.startWithTarget(target);
			this.m_elapsed = 0f;
			this.m_bFirstTick = true;
		}

		public override void step(float dt)
		{
			if (!this.m_bFirstTick)
			{
				CCActionInterval mElapsed = this;
				mElapsed.m_elapsed = mElapsed.m_elapsed + dt;
			}
			else
			{
				this.m_bFirstTick = false;
				this.m_elapsed = 0f;
			}
			this.update(Math.Min(1f, this.m_elapsed / this.m_fDuration));
		}
	}
}