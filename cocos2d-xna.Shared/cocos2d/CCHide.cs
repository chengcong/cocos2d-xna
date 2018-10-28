using System;

namespace cocos2d
{
	public class CCHide : CCActionInstant
	{
		public CCHide()
		{
		}

		public static new CCHide action()
		{
			return new CCHide();
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCHide cCHide = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCHide = new CCHide();
				pZone = new CCZone(cCHide);
			}
			else
			{
				cCHide = (CCHide)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			return cCHide;
		}

		~CCHide()
		{
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCShow.action();
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			pTarget.visible = false;
		}
	}
}