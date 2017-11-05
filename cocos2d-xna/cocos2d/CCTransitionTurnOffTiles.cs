using System;

namespace cocos2d
{
	public class CCTransitionTurnOffTiles : CCTransitionScene, ICCTransitionEaseScene
	{
		public CCTransitionTurnOffTiles()
		{
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
			int num1 = 12;
			CCTurnOffTiles cCTurnOffTile = CCTurnOffTiles.actionWithSize(new ccGridSize(num, num1), this.m_fDuration);
			CCFiniteTimeAction cCFiniteTimeAction = this.easeActionWithAction(cCTurnOffTile);
			CCScene mPOutScene = this.m_pOutScene;
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { cCFiniteTimeAction, CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)), CCStopGrid.action() };
			mPOutScene.runAction(CCSequence.actions(cCFiniteTimeActionArray));
		}

		protected override void sceneOrder()
		{
			this.m_bIsInSceneOnTop = false;
		}

		public static new CCTransitionTurnOffTiles transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionTurnOffTiles cCTransitionTurnOffTile = new CCTransitionTurnOffTiles();
			if (cCTransitionTurnOffTile.initWithDuration(t, scene))
			{
				return cCTransitionTurnOffTile;
			}
			return null;
		}
	}
}