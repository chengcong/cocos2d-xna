using System;

namespace cocos2d
{
	public class CCFadeOut : CCActionInterval
	{
		public CCFadeOut()
		{
		}

		public static new CCFadeOut actionWithDuration(float d)
		{
			CCFadeOut cCFadeOut = new CCFadeOut();
			cCFadeOut.initWithDuration(d);
			return cCFadeOut;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCFadeOut cCFadeOut = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCFadeOut = new CCFadeOut();
				pZone = new CCZone(cCFadeOut);
			}
			else
			{
				cCFadeOut = (CCFadeOut)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			return cCFadeOut;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCFadeIn.actionWithDuration(this.m_fDuration);
		}

		public override void update(float time)
		{
			ICCRGBAProtocol mPTarget = this.m_pTarget as ICCRGBAProtocol;
			if (mPTarget != null)
			{
				mPTarget.Opacity = (byte)(255f * (1f - time));
			}
		}
	}
}