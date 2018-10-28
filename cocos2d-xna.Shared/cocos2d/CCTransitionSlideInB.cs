using System;

namespace cocos2d
{
	public class CCTransitionSlideInB : CCTransitionSlideInL
	{
		public CCTransitionSlideInB()
		{
		}

		public override CCActionInterval action()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			return CCMoveBy.actionWithDuration(this.m_fDuration, new CCPoint(0f, winSize.height - 0.5f));
		}

		public override void initScenes()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.m_pInScene.position = new CCPoint(0f, -(winSize.height - 0.5f));
		}

		protected override void sceneOrder()
		{
			this.m_bIsInSceneOnTop = true;
		}

		public static new CCTransitionSlideInB transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionSlideInB cCTransitionSlideInB = new CCTransitionSlideInB();
			if (cCTransitionSlideInB != null && cCTransitionSlideInB.initWithDuration(t, scene))
			{
				return cCTransitionSlideInB;
			}
			cCTransitionSlideInB = null;
			return null;
		}
	}
}