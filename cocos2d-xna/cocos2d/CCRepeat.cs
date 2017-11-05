using System;

namespace cocos2d
{
	public class CCRepeat : CCActionInterval
	{
		protected CCFiniteTimeAction m_pInnerAction;

		protected uint m_uTimes;

		protected uint m_uTotal;

		public CCFiniteTimeAction InnerAction
		{
			get
			{
				return this.m_pInnerAction;
			}
			set
			{
				this.m_pInnerAction = value;
			}
		}

		public CCRepeat()
		{
		}

		public static CCRepeat actionWithAction(CCFiniteTimeAction action, uint times)
		{
			CCRepeat cCRepeat = new CCRepeat();
			cCRepeat.initWithAction(action, times);
			return cCRepeat;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCRepeat cCRepeat = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCRepeat = new CCRepeat();
				cCZone = new CCZone(cCRepeat);
			}
			else
			{
				cCRepeat = cCZone.m_pCopyObject as CCRepeat;
				if (cCRepeat == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			CCFiniteTimeAction cCFiniteTimeAction = this.m_pInnerAction.copy() as CCFiniteTimeAction;
			if (cCFiniteTimeAction == null)
			{
				return null;
			}
			cCRepeat.initWithAction(cCFiniteTimeAction, this.m_uTimes);
			return cCRepeat;
		}

		public bool initWithAction(CCFiniteTimeAction action, uint times)
		{
			if (!base.initWithDuration(action.duration * (float)((float)times)))
			{
				return false;
			}
			this.m_uTimes = times;
			this.m_pInnerAction = action;
			this.m_uTotal = 0;
			return true;
		}

		public override bool isDone()
		{
			return this.m_uTotal == this.m_uTimes;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCRepeat.actionWithAction(this.m_pInnerAction.reverse(), this.m_uTimes);
		}

		public override void startWithTarget(CCNode target)
		{
			this.m_uTotal = 0;
			base.startWithTarget(target);
			this.m_pInnerAction.startWithTarget(target);
		}

		public override void stop()
		{
			this.m_pInnerAction.stop();
			base.stop();
		}

		public override void update(float dt)
		{
			float single = dt * (float)((float)this.m_uTimes);
			if (single <= (float)((float)(this.m_uTotal + 1)))
			{
				float single1 = single % 1f;
				if (dt == 1f)
				{
					single1 = 1f;
					CCRepeat mUTotal = this;
					mUTotal.m_uTotal = mUTotal.m_uTotal + 1;
				}
				this.m_pInnerAction.update((single1 > 1f ? 1f : single1));
				return;
			}
			this.m_pInnerAction.update(1f);
			CCRepeat cCRepeat = this;
			cCRepeat.m_uTotal = cCRepeat.m_uTotal + 1;
			this.m_pInnerAction.stop();
			this.m_pInnerAction.startWithTarget(this.m_pTarget);
			if (this.m_uTotal == this.m_uTimes)
			{
				this.m_pInnerAction.update(0f);
				return;
			}
			this.m_pInnerAction.update(single - (float)((float)this.m_uTotal));
		}
	}
}