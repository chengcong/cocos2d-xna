using System;

namespace cocos2d
{
	public class CCTransitionRadialCW : CCTransitionRadialCCW
	{
		private const int kSceneRadial = 2147483647;

		public CCTransitionRadialCW()
		{
		}

		public override void onExit()
		{
			base.removeChildByTag(2147483647, false);
			base.onExit();
		}

		protected override CCProgressTimerType radialType()
		{
			return CCProgressTimerType.kCCProgressTimerTypeRadialCW;
		}

		public static new CCTransitionRadialCW transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionRadialCW cCTransitionRadialCW = new CCTransitionRadialCW();
			cCTransitionRadialCW.initWithDuration(t, scene);
			return cCTransitionRadialCW;
		}
	}
}