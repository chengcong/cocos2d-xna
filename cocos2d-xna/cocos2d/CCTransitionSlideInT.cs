using System;

namespace cocos2d
{
	public class CCTransitionSlideInT : CCTransitionSlideInL
	{
		public CCTransitionSlideInT()
		{
		}

		public override CCActionInterval action()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			return CCMoveBy.actionWithDuration(this.m_fDuration, new CCPoint(0f, -(winSize.height - 0.5f)));
		}

		public override void initScenes()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.m_pInScene.position = new CCPoint(0f, winSize.height - 0.5f);
		}

		protected override void sceneOrder()
		{
			this.m_bIsInSceneOnTop = false;
		}

		public static new CCTransitionSlideInT transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionSlideInT cCTransitionSlideInT = new CCTransitionSlideInT();
			if (cCTransitionSlideInT != null && cCTransitionSlideInT.initWithDuration(t, scene))
			{
				return cCTransitionSlideInT;
			}
			cCTransitionSlideInT = null;
			return null;
		}
	}
}