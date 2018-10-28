using System;

namespace cocos2d
{
	public class CCActionTween : CCActionInterval
	{
		protected string m_strKey;

		protected float m_fFrom;

		protected float m_fTo;

		protected float m_fDelta;

		protected CCActionTweenDelegate m_pDelegate;

		public CCActionTween(CCActionTweenDelegate d)
		{
			this.m_pDelegate = d;
		}

		public static CCActionTween actionWithDuration(float aDuration, string key, float from, float to, CCActionTweenDelegate d)
		{
			return CCActionTween.create(aDuration, key, from, to, d);
		}

		public static CCActionTween create(float aDuration, string key, float from, float to, CCActionTweenDelegate d)
		{
			CCActionTween cCActionTween = new CCActionTween(d);
			cCActionTween.initWithDuration(aDuration, key, from, to);
			return cCActionTween;
		}

		public virtual bool initWithDuration(float aDuration, string key, float from, float to)
		{
			if (!base.initWithDuration(aDuration))
			{
				return false;
			}
			this.m_strKey = key;
			this.m_fTo = to;
			this.m_fFrom = from;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCActionTween.create(this.m_fDuration, this.m_strKey, this.m_fTo, this.m_fFrom, this.m_pDelegate);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_fDelta = this.m_fTo - this.m_fFrom;
		}

		public override void update(float dt)
		{
			this.m_pDelegate(this.m_fTo - this.m_fDelta * (1f - dt), this.m_strKey);
		}
	}
}