using System;

namespace cocos2d
{
	public class CCFadeIn : CCActionInterval
	{
		public CCFadeIn()
		{
		}

		public static new CCFadeIn actionWithDuration(float d)
		{
			CCFadeIn cCFadeIn = new CCFadeIn();
			cCFadeIn.initWithDuration(d);
			return cCFadeIn;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCFadeIn cCFadeIn = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCFadeIn = new CCFadeIn();
				pZone = new CCZone(cCFadeIn);
			}
			else
			{
				cCFadeIn = (CCFadeIn)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			return cCFadeIn;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCFadeOut.actionWithDuration(this.m_fDuration);
		}

		public override void update(float time)
		{
			ICCRGBAProtocol mPTarget = this.m_pTarget as ICCRGBAProtocol;
			if (mPTarget != null)
			{
				mPTarget.Opacity = (byte)(255f * time);
			}
		}
	}
}