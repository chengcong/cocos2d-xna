using System;

namespace cocos2d
{
	public class CCFadeTo : CCActionInterval
	{
		protected byte m_toOpacity;

		protected byte m_fromOpacity;

		public CCFadeTo()
		{
		}

		public static CCFadeTo actionWithDuration(float duration, byte opacity)
		{
			CCFadeTo cCFadeTo = new CCFadeTo();
			cCFadeTo.initWithDuration(duration, opacity);
			return cCFadeTo;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCFadeTo cCFadeTo = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCFadeTo = new CCFadeTo();
				pZone = new CCZone(cCFadeTo);
			}
			else
			{
				cCFadeTo = (CCFadeTo)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCFadeTo.initWithDuration(this.m_fDuration, this.m_toOpacity);
			return cCFadeTo;
		}

		public bool initWithDuration(float duration, byte opacity)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_toOpacity = opacity;
			return true;
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			ICCRGBAProtocol cCRGBAProtocol = pTarget as ICCRGBAProtocol;
			if (cCRGBAProtocol != null)
			{
				this.m_fromOpacity = cCRGBAProtocol.Opacity;
			}
		}

		public override void update(float time)
		{
			ICCRGBAProtocol mPTarget = this.m_pTarget as ICCRGBAProtocol;
			if (mPTarget != null)
			{
				mPTarget.Opacity = (byte)((float)this.m_fromOpacity + (float)(this.m_toOpacity - this.m_fromOpacity) * time);
			}
		}
	}
}