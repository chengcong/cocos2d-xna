using System;

namespace cocos2d
{
	public class CCTransitionMoveInR : CCTransitionMoveInL
	{
		public CCTransitionMoveInR()
		{
		}

		public override void initScenes()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.m_pInScene.position = new CCPoint(winSize.width, 0f);
		}

		public static new CCTransitionMoveInR transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionMoveInR cCTransitionMoveInR = new CCTransitionMoveInR();
			if (cCTransitionMoveInR != null && cCTransitionMoveInR.initWithDuration(t, scene))
			{
				return cCTransitionMoveInR;
			}
			cCTransitionMoveInR = null;
			return null;
		}
	}
}