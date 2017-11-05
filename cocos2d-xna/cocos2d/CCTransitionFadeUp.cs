using System;

namespace cocos2d
{
	public class CCTransitionFadeUp : CCTransitionFadeTR
	{
		public CCTransitionFadeUp()
		{
		}

		public override CCActionInterval actionWithSize(ccGridSize size)
		{
			return CCFadeOutUpTiles.actionWithSize(size, this.m_fDuration);
		}

		public static new CCTransitionFadeUp transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionFadeUp cCTransitionFadeUp = new CCTransitionFadeUp();
			if (cCTransitionFadeUp.initWithDuration(t, scene))
			{
				return cCTransitionFadeUp;
			}
			return null;
		}
	}
}