using System;

namespace cocos2d
{
	public class CCTransitionFadeBL : CCTransitionFadeTR
	{
		public CCTransitionFadeBL()
		{
		}

		public override CCActionInterval actionWithSize(ccGridSize size)
		{
			return CCFadeOutBLTiles.actionWithSize(size, this.m_fDuration);
		}

		public static new CCTransitionFadeBL transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionFadeBL cCTransitionFadeBL = new CCTransitionFadeBL();
			if (cCTransitionFadeBL.initWithDuration(t, scene))
			{
				return cCTransitionFadeBL;
			}
			return null;
		}
	}
}