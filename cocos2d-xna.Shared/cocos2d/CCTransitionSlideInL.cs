using System;

namespace cocos2d
{
	public class CCTransitionSlideInL : CCTransitionScene, ICCTransitionEaseScene
	{
		public CCTransitionSlideInL()
		{
		}

		public virtual CCActionInterval action()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			return CCMoveBy.actionWithDuration(this.m_fDuration, new CCPoint(winSize.width - 0.5f, 0f));
		}

		CCFiniteTimeAction cocos2d.ICCTransitionEaseScene.easeActionWithAction(CCActionInterval action)
		{
			throw new NotImplementedException();
		}

		public virtual CCActionInterval easeActionWithAction(CCActionInterval action)
		{
			return CCEaseOut.actionWithAction(action, 2f);
		}

		public virtual void initScenes()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.m_pInScene.position = new CCPoint(-(winSize.width - 0.5f), 0f);
		}

		public override void onEnter()
		{
			base.onEnter();
			this.initScenes();
			CCActionInterval cCActionInterval = this.action();
			CCActionInterval cCActionInterval1 = this.action();
			CCActionInterval cCActionInterval2 = this.easeActionWithAction(cCActionInterval);
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { this.easeActionWithAction(cCActionInterval1), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			CCActionInterval cCActionInterval3 = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray);
			this.m_pInScene.runAction(cCActionInterval2);
			this.m_pOutScene.runAction(cCActionInterval3);
		}

		protected override void sceneOrder()
		{
			this.m_bIsInSceneOnTop = false;
		}

		public static new CCTransitionSlideInL transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionSlideInL cCTransitionSlideInL = new CCTransitionSlideInL();
			if (cCTransitionSlideInL != null && cCTransitionSlideInL.initWithDuration(t, scene))
			{
				return cCTransitionSlideInL;
			}
			cCTransitionSlideInL = null;
			return null;
		}
	}
}