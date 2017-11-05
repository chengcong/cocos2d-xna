using System;

namespace cocos2d
{
	public class CCShow : CCActionInstant
	{
		public CCShow()
		{
		}

		public static new CCShow action()
		{
			return new CCShow();
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCShow cCShow = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCShow = new CCShow();
				pZone = new CCZone(cCShow);
			}
			else
			{
				cCShow = (CCShow)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			return cCShow;
		}

		~CCShow()
		{
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCHide.action();
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			pTarget.visible = true;
		}
	}
}