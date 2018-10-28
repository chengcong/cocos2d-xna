using System;

namespace cocos2d
{
	public class CCTransitionRotoZoom : CCTransitionScene
	{
		public CCTransitionRotoZoom()
		{
		}

		public override void onEnter()
		{
			base.onEnter();
			this.m_pInScene.scale = 0.001f;
			this.m_pOutScene.scale = 1f;
			this.m_pInScene.anchorPoint = new CCPoint(0.5f, 0.5f);
			this.m_pOutScene.anchorPoint = new CCPoint(0.5f, 0.5f);
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[2];
			CCFiniteTimeAction[] cCFiniteTimeActionArray1 = new CCFiniteTimeAction[] { CCScaleBy.actionWithDuration(this.m_fDuration / 2f, 0.001f), CCRotateBy.actionWithDuration(this.m_fDuration / 2f, 720f) };
			cCFiniteTimeActionArray[0] = CCSpawn.actions(cCFiniteTimeActionArray1);
			cCFiniteTimeActionArray[1] = CCDelayTime.actionWithDuration(this.m_fDuration / 2f);
			CCActionInterval cCActionInterval = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray);
			this.m_pOutScene.runAction(cCActionInterval);
			CCScene mPInScene = this.m_pInScene;
			CCFiniteTimeAction[] cCFiniteTimeActionArray2 = new CCFiniteTimeAction[] { cCActionInterval.reverse(), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			mPInScene.runAction(CCSequence.actions(cCFiniteTimeActionArray2));
		}

		public static new CCTransitionRotoZoom transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionRotoZoom cCTransitionRotoZoom = new CCTransitionRotoZoom();
			if (cCTransitionRotoZoom != null && cCTransitionRotoZoom.initWithDuration(t, scene))
			{
				return cCTransitionRotoZoom;
			}
			cCTransitionRotoZoom = null;
			return null;
		}
	}
}