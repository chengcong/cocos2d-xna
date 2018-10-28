using System;

namespace cocos2d
{
	public class CCSequence : CCActionInterval
	{
		protected CCFiniteTimeAction[] m_pActions;

		protected float m_split;

		protected int m_last;

		public CCSequence()
		{
			this.m_pActions = new CCFiniteTimeAction[2];
		}

		public static CCSequence actionOneTwo(CCFiniteTimeAction actionOne, CCFiniteTimeAction actionTwo)
		{
			CCSequence cCSequence = new CCSequence();
			cCSequence.initOneTwo(actionOne, actionTwo);
			return cCSequence;
		}

		public static CCFiniteTimeAction actions(params CCFiniteTimeAction[] actions)
		{
			return CCSequence.actionsWithArray(actions);
		}

		public static CCFiniteTimeAction actionsWithArray(CCFiniteTimeAction[] actions)
		{
			CCFiniteTimeAction cCFiniteTimeAction = actions[0];
			for (int i = 1; i < (int)actions.Length; i++)
			{
				if (actions[i] != null)
				{
					cCFiniteTimeAction = CCSequence.actionOneTwo(cCFiniteTimeAction, actions[i]);
				}
			}
			return cCFiniteTimeAction;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCSequence cCSequence = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCSequence = new CCSequence();
				cCZone = new CCZone(cCSequence);
			}
			else
			{
				cCSequence = cCZone.m_pCopyObject as CCSequence;
				if (cCSequence == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			CCFiniteTimeAction cCFiniteTimeAction = this.m_pActions[0].copy() as CCFiniteTimeAction;
			CCFiniteTimeAction cCFiniteTimeAction1 = this.m_pActions[1].copy() as CCFiniteTimeAction;
			if (cCFiniteTimeAction == null || cCFiniteTimeAction1 == null)
			{
				return null;
			}
			cCSequence.initOneTwo(cCFiniteTimeAction, cCFiniteTimeAction1);
			return cCSequence;
		}

		public bool initOneTwo(CCFiniteTimeAction actionOne, CCFiniteTimeAction aciontTwo)
		{
			float single = actionOne.duration + aciontTwo.duration;
			base.initWithDuration(single);
			this.m_pActions[0] = actionOne;
			this.m_pActions[1] = aciontTwo;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCSequence.actionOneTwo(this.m_pActions[1].reverse(), this.m_pActions[0].reverse());
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_split = this.m_pActions[0].duration / this.m_fDuration;
			this.m_last = -1;
		}

		public override void stop()
		{
			this.m_pActions[0].stop();
			this.m_pActions[1].stop();
			base.stop();
		}

		public override void update(float dt)
		{
			int num = 0;
			float single = 0f;
			if (dt < this.m_split)
			{
				num = 0;
				single = (this.m_split == 0f ? 1f : dt / this.m_split);
			}
			else
			{
				num = 1;
				single = (this.m_split != 1f ? (dt - this.m_split) / (1f - this.m_split) : 1f);
			}
			if (this.m_last == -1 && num == 1)
			{
				this.m_pActions[0].startWithTarget(this.m_pTarget);
				this.m_pActions[0].update(1f);
				this.m_pActions[0].stop();
			}
			if (this.m_last != num)
			{
				if (this.m_last != -1)
				{
					this.m_pActions[this.m_last].update(1f);
					this.m_pActions[this.m_last].stop();
				}
				this.m_pActions[num].startWithTarget(this.m_pTarget);
			}
			this.m_pActions[num].update(single);
			this.m_last = num;
		}
	}
}