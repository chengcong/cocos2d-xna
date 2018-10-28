using System;

namespace cocos2d
{
	public class CCTintTo : CCActionInterval
	{
		protected ccColor3B m_to;

		protected ccColor3B m_from;

		public CCTintTo()
		{
		}

		public static CCTintTo actionWithDuration(float duration, byte red, byte green, byte blue)
		{
			CCTintTo cCTintTo = new CCTintTo();
			cCTintTo.initWithDuration(duration, red, green, blue);
			return cCTintTo;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCTintTo cCTintTo = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCTintTo = new CCTintTo();
				cCZone = new CCZone(cCTintTo);
			}
			else
			{
				cCTintTo = cCZone.m_pCopyObject as CCTintTo;
				if (cCTintTo == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCTintTo.initWithDuration(this.m_fDuration, this.m_to.r, this.m_to.g, this.m_to.b);
			return cCTintTo;
		}

		public bool initWithDuration(float duration, byte red, byte green, byte blue)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_to = new ccColor3B(red, green, blue);
			return true;
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			ICCRGBAProtocol mPTarget = this.m_pTarget as ICCRGBAProtocol;
			if (mPTarget != null)
			{
				this.m_from = mPTarget.Color;
			}
		}

		public override void update(float dt)
		{
			ICCRGBAProtocol mPTarget = this.m_pTarget as ICCRGBAProtocol;
			if (mPTarget != null)
			{
				mPTarget.Color = new ccColor3B((byte)((float)this.m_from.r + (float)(this.m_to.r - this.m_from.r) * dt), (byte)((float)this.m_from.g + (float)(this.m_to.g - this.m_from.g) * dt), (byte)((float)this.m_from.b + (float)(this.m_to.b - this.m_from.b) * dt));
			}
		}
	}
}