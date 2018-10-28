using System;

namespace cocos2d
{
	public class CCTransitionSplitCols : CCTransitionScene, ICCTransitionEaseScene
	{
		public CCTransitionSplitCols()
		{
		}

		public virtual CCActionInterval action()
		{
			return CCSplitCols.actionWithCols(3, this.m_fDuration / 2f);
		}

		public virtual CCFiniteTimeAction easeActionWithAction(CCActionInterval action)
		{
			return CCEaseInOut.actionWithAction(action, 3f);
		}

		public override void onEnter()
		{
			base.onEnter();
			this.m_pInScene.visible = false;
			CCActionInterval cCActionInterval = this.action();
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { cCActionInterval, CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.hideOutShowIn)), cCActionInterval.reverse() };
			CCActionInterval cCActionInterval1 = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray);
			CCFiniteTimeAction[] cCFiniteTimeActionArray1 = new CCFiniteTimeAction[] { this.easeActionWithAction(cCActionInterval1), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)), CCStopGrid.action() };
			base.runAction(CCSequence.actions(cCFiniteTimeActionArray1));
		}

		public static new CCTransitionSplitCols transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionSplitCols cCTransitionSplitCol = new CCTransitionSplitCols();
			if (cCTransitionSplitCol.initWithDuration(t, scene))
			{
				return cCTransitionSplitCol;
			}
			return null;
		}
	}
}