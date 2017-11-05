using System;

namespace cocos2d
{
	public class CCTransitionFadeTR : CCTransitionScene, ICCTransitionEaseScene
	{
		public CCTransitionFadeTR()
		{
		}

		public virtual CCActionInterval actionWithSize(ccGridSize size)
		{
			return CCFadeOutTRTiles.actionWithSize(size, this.m_fDuration);
		}

		public virtual CCFiniteTimeAction easeActionWithAction(CCActionInterval action)
		{
			return action;
		}

		public override void onEnter()
		{
			base.onEnter();
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			int num = (int)(12f * (winSize.width / winSize.height));
			CCActionInterval cCActionInterval = this.actionWithSize(new ccGridSize(num, 12));
			CCScene mPOutScene = this.m_pOutScene;
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { this.easeActionWithAction(cCActionInterval), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)), CCStopGrid.action() };
			mPOutScene.runAction(CCSequence.actions(cCFiniteTimeActionArray));
		}

		protected override void sceneOrder()
		{
			this.m_bIsInSceneOnTop = false;
		}

		public static new CCTransitionFadeTR transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionFadeTR cCTransitionFadeTR = new CCTransitionFadeTR();
			if (cCTransitionFadeTR.initWithDuration(t, scene))
			{
				return cCTransitionFadeTR;
			}
			return null;
		}
	}
}