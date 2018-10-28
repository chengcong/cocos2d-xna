using System;

namespace cocos2d
{
	public class CCTransitionJumpZoom : CCTransitionScene
	{
		public CCTransitionJumpZoom()
		{
		}

		public override void onEnter()
		{
			base.onEnter();
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.m_pInScene.scale = 0.5f;
			this.m_pInScene.position = new CCPoint(winSize.width, 0f);
			this.m_pInScene.anchorPoint = new CCPoint(0.5f, 0.5f);
			this.m_pOutScene.anchorPoint = new CCPoint(0.5f, 0.5f);
			CCActionInterval cCActionInterval = CCJumpBy.actionWithDuration(this.m_fDuration / 4f, new CCPoint(-winSize.width, 0f), winSize.width / 4f, 2);
			CCActionInterval cCActionInterval1 = CCScaleTo.actionWithDuration(this.m_fDuration / 4f, 1f);
			CCActionInterval cCActionInterval2 = CCScaleTo.actionWithDuration(this.m_fDuration / 4f, 0.5f);
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { cCActionInterval2, cCActionInterval };
			CCActionInterval cCActionInterval3 = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray);
			CCFiniteTimeAction[] cCFiniteTimeActionArray1 = new CCFiniteTimeAction[] { cCActionInterval, cCActionInterval1 };
			CCActionInterval cCActionInterval4 = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray1);
			CCActionInterval cCActionInterval5 = CCDelayTime.actionWithDuration(this.m_fDuration / 2f);
			this.m_pOutScene.runAction(cCActionInterval3);
			CCScene mPInScene = this.m_pInScene;
			CCFiniteTimeAction[] cCFiniteTimeActionArray2 = new CCFiniteTimeAction[] { cCActionInterval5, cCActionInterval4, CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			mPInScene.runAction(CCSequence.actions(cCFiniteTimeActionArray2));
		}

		public static new CCTransitionJumpZoom transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionJumpZoom cCTransitionJumpZoom = new CCTransitionJumpZoom();
			if (cCTransitionJumpZoom != null && cCTransitionJumpZoom.initWithDuration(t, scene))
			{
				return cCTransitionJumpZoom;
			}
			cCTransitionJumpZoom = null;
			return null;
		}
	}
}