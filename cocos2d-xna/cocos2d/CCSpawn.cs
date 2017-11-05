using System;

namespace cocos2d
{
	public class CCSpawn : CCActionInterval
	{
		protected CCFiniteTimeAction m_pOne = new CCFiniteTimeAction();

		protected CCFiniteTimeAction m_pTwo = new CCFiniteTimeAction();

		public CCSpawn()
		{
		}

		public static CCSpawn actionOneTwo(CCFiniteTimeAction action1, CCFiniteTimeAction action2)
		{
			CCSpawn cCSpawn = new CCSpawn();
			cCSpawn.initOneTwo(action1, action2);
			return cCSpawn;
		}

		public static CCFiniteTimeAction actions(params CCFiniteTimeAction[] actions)
		{
			return CCSpawn.actionsWithArray(actions);
		}

		public static CCFiniteTimeAction actionsWithArray(CCFiniteTimeAction[] actions)
		{
			CCFiniteTimeAction cCFiniteTimeAction = actions[0];
			for (int i = 1; i < (int)actions.Length; i++)
			{
				cCFiniteTimeAction = CCSpawn.actionOneTwo(cCFiniteTimeAction, actions[i]);
			}
			return cCFiniteTimeAction;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCSpawn cCSpawn = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCSpawn = new CCSpawn();
				cCZone = new CCZone(cCSpawn);
			}
			else
			{
				cCSpawn = cCZone.m_pCopyObject as CCSpawn;
				if (cCSpawn == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			CCFiniteTimeAction cCFiniteTimeAction = this.m_pOne.copy() as CCFiniteTimeAction;
			CCFiniteTimeAction cCFiniteTimeAction1 = this.m_pTwo.copy() as CCFiniteTimeAction;
			if (cCFiniteTimeAction == null || cCFiniteTimeAction1 == null)
			{
				return null;
			}
			cCSpawn.initOneTwo(cCFiniteTimeAction, cCFiniteTimeAction1);
			return cCSpawn;
		}

		public bool initOneTwo(CCFiniteTimeAction action1, CCFiniteTimeAction action2)
		{
			bool flag = false;
			float single = action1.duration;
			float single1 = action2.duration;
			if (base.initWithDuration(Math.Max(single, single1)))
			{
				this.m_pOne = action1;
				this.m_pTwo = action2;
				if (single > single1)
				{
					this.m_pTwo = CCSequence.actionOneTwo(action2, CCDelayTime.actionWithDuration(single - single1));
				}
				else if (single < single1)
				{
					this.m_pOne = CCSequence.actionOneTwo(action1, CCDelayTime.actionWithDuration(single1 - single));
				}
				flag = true;
			}
			return flag;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCSpawn.actionOneTwo(this.m_pOne.reverse(), this.m_pTwo.reverse());
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_pOne.startWithTarget(target);
			this.m_pTwo.startWithTarget(target);
		}

		public override void stop()
		{
			this.m_pOne.stop();
			this.m_pTwo.stop();
			base.stop();
		}

		public override void update(float dt)
		{
			if (this.m_pOne != null)
			{
				this.m_pOne.update(dt);
			}
			if (this.m_pTwo != null)
			{
				this.m_pTwo.update(dt);
			}
		}
	}
}