using System;

namespace cocos2d
{
	public class CCToggleVisibility : CCActionInstant
	{
		public CCToggleVisibility()
		{
		}

		public static new CCToggleVisibility action()
		{
			return new CCToggleVisibility();
		}

		~CCToggleVisibility()
		{
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			pTarget.visible = !pTarget.visible;
		}
	}
}