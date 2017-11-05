using System;

namespace cocos2d
{
	public class CCTransitionPageTurn : CCTransitionScene
	{
		protected bool m_bBack;

		public CCTransitionPageTurn()
		{
		}

		public CCActionInterval actionWithSize(ccGridSize vector)
		{
			if (!this.m_bBack)
			{
				return CCPageTurn3D.actionWithSize(vector, this.m_fDuration);
			}
			return CCReverseTime.actionWithAction(CCPageTurn3D.actionWithSize(vector, this.m_fDuration));
		}

		public virtual bool initWithDuration(float t, CCScene scene, bool backwards)
		{
			this.m_bBack = backwards;
			base.initWithDuration(t, scene);
			return true;
		}

		public override void onEnter()
		{
			int num;
			int num1;
			base.onEnter();
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			if (winSize.width <= winSize.height)
			{
				num = 12;
				num1 = 16;
			}
			else
			{
				num = 16;
				num1 = 12;
			}
			CCActionInterval cCActionInterval = this.actionWithSize(ccTypes.ccg(num, num1));
			if (!this.m_bBack)
			{
				CCScene mPOutScene = this.m_pOutScene;
				CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { cCActionInterval, CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)), CCStopGrid.action() };
				mPOutScene.runAction(CCSequence.actions(cCFiniteTimeActionArray));
				return;
			}
			this.m_pInScene.visible = false;
			CCScene mPInScene = this.m_pInScene;
			CCFiniteTimeAction[] cCFiniteTimeActionArray1 = new CCFiniteTimeAction[] { CCShow.action(), cCActionInterval, CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)), CCStopGrid.action() };
			mPInScene.runAction(CCSequence.actions(cCFiniteTimeActionArray1));
		}

		protected override void sceneOrder()
		{
			this.m_bIsInSceneOnTop = this.m_bBack;
		}

		public static CCTransitionPageTurn transitionWithDuration(float t, CCScene scene, bool backwards)
		{
			CCTransitionPageTurn cCTransitionPageTurn = new CCTransitionPageTurn();
			cCTransitionPageTurn.initWithDuration(t, scene, backwards);
			return cCTransitionPageTurn;
		}
	}
}