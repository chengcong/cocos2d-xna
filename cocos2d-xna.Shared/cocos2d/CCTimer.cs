using System;

namespace cocos2d
{
	public class CCTimer
	{
		public SEL_SCHEDULE m_pfnSelector;

		public float m_fInterval;

		protected object m_pTarget;

		protected float m_fElapsed;

		public float elapsed
		{
			get
			{
				return this.m_fElapsed;
			}
			set
			{
				this.m_fElapsed = value;
			}
		}

		public CCTimer()
		{
		}

		public bool initWithTarget(object target, SEL_SCHEDULE selector)
		{
			return this.initWithTarget(target, selector, 0f);
		}

		public bool initWithTarget(object target, SEL_SCHEDULE selector, float fSeconds)
		{
			this.m_pTarget = target;
			this.m_pfnSelector = selector;
			this.m_fElapsed = -1f;
			this.m_fInterval = fSeconds;
			return true;
		}

		public void update(float dt)
		{
			if (this.m_fElapsed != -1f)
			{
				CCTimer mFElapsed = this;
				mFElapsed.m_fElapsed = mFElapsed.m_fElapsed + dt;
			}
			else
			{
				this.m_fElapsed = 0f;
			}
			if (this.m_fElapsed >= this.m_fInterval && this.m_pfnSelector != null)
			{
				this.m_pfnSelector(this.m_fElapsed);
				this.m_fElapsed = 0f;
			}
		}
	}
}