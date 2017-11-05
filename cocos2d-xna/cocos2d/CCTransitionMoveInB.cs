using System;

namespace cocos2d
{
	public class CCTransitionMoveInB : CCTransitionMoveInL
	{
		public CCTransitionMoveInB()
		{
		}

		public override void initScenes()
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.m_pInScene.position = new CCPoint(0f, -winSize.height);
		}

		public static new CCTransitionMoveInB transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionMoveInB cCTransitionMoveInB = new CCTransitionMoveInB();
			if (cCTransitionMoveInB != null && cCTransitionMoveInB.initWithDuration(t, scene))
			{
				return cCTransitionMoveInB;
			}
			cCTransitionMoveInB = null;
			return null;
		}
	}
}