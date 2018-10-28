using System;

namespace cocos2d
{
	public class CCTransitionShrinkGrow : CCTransitionScene, ICCTransitionEaseScene
	{
		public CCTransitionShrinkGrow()
		{
		}

		CCFiniteTimeAction cocos2d.ICCTransitionEaseScene.easeActionWithAction(CCActionInterval action)
		{
			throw new NotImplementedException();
		}

		public virtual CCActionInterval easeActionWithAction(CCActionInterval action)
		{
			return CCEaseOut.actionWithAction(action, 2f);
		}

		public override void onEnter()
		{
			base.onEnter();
			this.m_pInScene.scale = 0.001f;
			this.m_pOutScene.scale = 1f;
			this.m_pInScene.anchorPoint = new CCPoint(0.6666667f, 0.5f);
			this.m_pOutScene.anchorPoint = new CCPoint(0.333333343f, 0.5f);
			CCActionInterval cCActionInterval = CCScaleTo.actionWithDuration(this.m_fDuration, 0.01f);
			CCActionInterval cCActionInterval1 = CCScaleTo.actionWithDuration(this.m_fDuration, 1f);
			this.m_pInScene.runAction(this.easeActionWithAction(cCActionInterval1));
			CCScene mPOutScene = this.m_pOutScene;
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { this.easeActionWithAction(cCActionInterval), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			mPOutScene.runAction(CCSequence.actions(cCFiniteTimeActionArray));
		}

		public static new CCTransitionShrinkGrow transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionShrinkGrow cCTransitionShrinkGrow = new CCTransitionShrinkGrow();
			if (cCTransitionShrinkGrow != null && cCTransitionShrinkGrow.initWithDuration(t, scene))
			{
				return cCTransitionShrinkGrow;
			}
			cCTransitionShrinkGrow = null;
			return null;
		}
	}
}