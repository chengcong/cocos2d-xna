using System;

namespace cocos2d
{
	public class CCTintBy : CCActionInterval
	{
		protected short m_deltaR;

		protected short m_deltaG;

		protected short m_deltaB;

		protected short m_fromR;

		protected short m_fromG;

		protected short m_fromB;

		public CCTintBy()
		{
		}

		public static CCTintBy actionWithDuration(float duration, short deltaRed, short deltaGreen, short deltaBlue)
		{
			CCTintBy cCTintBy = new CCTintBy();
			cCTintBy.initWithDuration(duration, deltaRed, deltaGreen, deltaBlue);
			return cCTintBy;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCTintBy cCTintBy = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCTintBy = new CCTintBy();
				cCZone = new CCZone(cCTintBy);
			}
			else
			{
				cCTintBy = cCZone.m_pCopyObject as CCTintBy;
				if (cCTintBy == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCTintBy.initWithDuration(this.m_fDuration, this.m_deltaR, this.m_deltaG, this.m_deltaB);
			return cCTintBy;
		}

		public bool initWithDuration(float duration, short deltaRed, short deltaGreen, short deltaBlue)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_deltaR = deltaRed;
			this.m_deltaG = deltaGreen;
			this.m_deltaB = deltaBlue;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCTintBy.actionWithDuration(this.m_fDuration, (short)(-this.m_deltaR), (short)(-this.m_deltaG), (short)(-this.m_deltaB));
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			ICCRGBAProtocol cCRGBAProtocol = target as ICCRGBAProtocol;
			if (cCRGBAProtocol != null)
			{
				ccColor3B color = cCRGBAProtocol.Color;
				this.m_fromR = color.r;
				this.m_fromG = color.g;
				this.m_fromB = color.b;
			}
		}

		public override void update(float dt)
		{
			ICCRGBAProtocol mPTarget = this.m_pTarget as ICCRGBAProtocol;
			if (mPTarget != null)
			{
				mPTarget.Color = new ccColor3B((byte)((float)this.m_fromR + (float)this.m_deltaR * dt), (byte)((float)this.m_fromG + (float)this.m_deltaG * dt), (byte)((float)this.m_fromB + (float)this.m_deltaB * dt));
			}
		}
	}
}