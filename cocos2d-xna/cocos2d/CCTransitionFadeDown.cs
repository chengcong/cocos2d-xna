using System;

namespace cocos2d
{
	public class CCTransitionFadeDown : CCTransitionFadeTR
	{
		public CCTransitionFadeDown()
		{
		}

		public override CCActionInterval actionWithSize(ccGridSize size)
		{
			return CCFadeOutDownTiles.actionWithSize(size, this.m_fDuration);
		}

		public static new CCTransitionFadeDown transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionFadeDown cCTransitionFadeDown = new CCTransitionFadeDown();
			if (cCTransitionFadeDown.initWithDuration(t, scene))
			{
				return cCTransitionFadeDown;
			}
			return null;
		}
	}
}