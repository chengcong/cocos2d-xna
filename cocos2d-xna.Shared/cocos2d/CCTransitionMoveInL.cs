using System;

namespace cocos2d
{
	public class CCTransitionMoveInL : CCTransitionScene, ICCTransitionEaseScene
	{
		public CCTransitionMoveInL()
		{
		}

		public virtual CCActionInterval action()
		{
			return CCMoveTo.actionWithDuration(this.m_fDuration, new CCPoint(0f, 0f));
		}

		public CCFiniteTimeAction easeActionWithAction(CCActionInterval action)
		{
			return CCEaseOut.actionWithAction(action, 2f);
		}

		public virtual void initScenes()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.m_pInScene.position = new CCPoint(-winSize.width, 0f);
		}

		public override void onEnter()
		{
			base.onEnter();
			this.initScenes();
			CCActionInterval cCActionInterval = this.action();
			CCScene mPInScene = this.m_pInScene;
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { this.easeActionWithAction(cCActionInterval), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			mPInScene.runAction(CCSequence.actions(cCFiniteTimeActionArray));
		}

		public static new CCTransitionMoveInL transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionMoveInL cCTransitionMoveInL = new CCTransitionMoveInL();
			if (cCTransitionMoveInL != null && cCTransitionMoveInL.initWithDuration(t, scene))
			{
				return cCTransitionMoveInL;
			}
			cCTransitionMoveInL = null;
			return null;
		}
	}
}