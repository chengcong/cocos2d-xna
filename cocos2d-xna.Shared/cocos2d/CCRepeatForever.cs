using System;

namespace cocos2d
{
	public class CCRepeatForever : CCActionInterval
	{
		protected CCActionInterval m_pInnerAction;

		public CCActionInterval InnerAction
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

		public CCRepeatForever()
		{
		}

		public static CCRepeatForever actionWithAction(CCActionInterval action)
		{
			CCRepeatForever cCRepeatForever = new CCRepeatForever();
			cCRepeatForever.initWithAction(action);
			return cCRepeatForever;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCRepeatForever cCRepeatForever = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCRepeatForever = new CCRepeatForever();
				cCZone = new CCZone(cCRepeatForever);
			}
			else
			{
				cCRepeatForever = cCZone.m_pCopyObject as CCRepeatForever;
				if (cCRepeatForever == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			CCActionInterval cCActionInterval = this.m_pInnerAction.copy() as CCActionInterval;
			if (cCActionInterval == null)
			{
				return null;
			}
			cCRepeatForever.initWithAction(cCActionInterval);
			return cCRepeatForever;
		}

		public bool initWithAction(CCActionInterval action)
		{
			this.m_pInnerAction = action;
			return true;
		}

		public override bool isDone()
		{
			return false;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCRepeatForever.actionWithAction(this.m_pInnerAction.reverse() as CCActionInterval);
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_pInnerAction.startWithTarget(target);
		}

		public override void step(float dt)
		{
			this.m_pInnerAction.step(dt);
			if (this.m_pInnerAction.isDone())
			{
				float single = dt + this.m_pInnerAction.duration - this.m_pInnerAction.elapsed;
				this.m_pInnerAction.startWithTarget(this.m_pTarget);
				this.m_pInnerAction.step(single);
			}
		}
	}
}