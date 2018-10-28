using System;

namespace cocos2d
{
	public class CCTransitionSlideInR : CCTransitionSlideInL
	{
		public CCTransitionSlideInR()
		{
		}

		public override CCActionInterval action()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			return CCMoveBy.actionWithDuration(this.m_fDuration, new CCPoint(-(winSize.width - 0.5f), 0f));
		}

		public override void initScenes()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.m_pInScene.position = new CCPoint(winSize.width - 0.5f, 0f);
		}

		protected override void sceneOrder()
		{
			this.m_bIsInSceneOnTop = true;
		}

		public static new CCTransitionSlideInR transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionSlideInR cCTransitionSlideInR = new CCTransitionSlideInR();
			if (cCTransitionSlideInR != null && cCTransitionSlideInR.initWithDuration(t, scene))
			{
				return cCTransitionSlideInR;
			}
			cCTransitionSlideInR = null;
			return null;
		}
	}
}