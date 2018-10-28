using System;

namespace cocos2d
{
	public class CCSkewTo : CCActionInterval
	{
		protected float m_fSkewX;

		protected float m_fSkewY;

		protected float m_fStartSkewX;

		protected float m_fStartSkewY;

		protected float m_fEndSkewX;

		protected float m_fEndSkewY;

		protected float m_fDeltaX;

		protected float m_fDeltaY;

		public CCSkewTo()
		{
		}

		public static CCSkewTo actionWithDuration(float t, float sx, float sy)
		{
			CCSkewTo cCSkewTo = new CCSkewTo();
			if (cCSkewTo != null)
			{
				cCSkewTo.initWithDuration(t, sx, sy);
			}
			return cCSkewTo;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCSkewTo cCSkewTo = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCSkewTo = new CCSkewTo();
				pZone = new CCZone(cCSkewTo);
			}
			else
			{
				cCSkewTo = (CCSkewTo)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCSkewTo.initWithDuration(this.m_fDuration, this.m_fEndSkewX, this.m_fEndSkewY);
			return cCSkewTo;
		}

		public virtual bool initWithDuration(float t, float sx, float sy)
		{
			bool flag = false;
			if (base.initWithDuration(t))
			{
				this.m_fEndSkewX = sx;
				this.m_fEndSkewY = sy;
				flag = true;
			}
			return flag;
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_fStartSkewX = pTarget.skewX;
			if (this.m_fStartSkewX <= 0f)
			{
				this.m_fStartSkewX = this.m_fStartSkewX % -180f;
			}
			else
			{
				this.m_fStartSkewX = this.m_fStartSkewX % 180f;
			}
			this.m_fDeltaX = this.m_fEndSkewX - this.m_fStartSkewX;
			if (this.m_fDeltaX > 180f)
			{
				CCSkewTo mFDeltaX = this;
				mFDeltaX.m_fDeltaX = mFDeltaX.m_fDeltaX - 360f;
			}
			if (this.m_fDeltaX < -180f)
			{
				CCSkewTo cCSkewTo = this;
				cCSkewTo.m_fDeltaX = cCSkewTo.m_fDeltaX + 360f;
			}
			this.m_fStartSkewY = pTarget.skewY;
			if (this.m_fStartSkewY <= 0f)
			{
				this.m_fStartSkewY = this.m_fStartSkewY % -360f;
			}
			else
			{
				this.m_fStartSkewY = this.m_fStartSkewY % 360f;
			}
			this.m_fDeltaY = this.m_fEndSkewY - this.m_fStartSkewY;
			if (this.m_fDeltaY > 180f)
			{
				CCSkewTo mFDeltaY = this;
				mFDeltaY.m_fDeltaY = mFDeltaY.m_fDeltaY - 360f;
			}
			if (this.m_fDeltaY < -180f)
			{
				CCSkewTo mFDeltaY1 = this;
				mFDeltaY1.m_fDeltaY = mFDeltaY1.m_fDeltaY + 360f;
			}
		}

		public override void update(float time)
		{
			this.m_pTarget.skewX = this.m_fStartSkewX + this.m_fDeltaX * time;
			this.m_pTarget.skewY = this.m_fStartSkewY + this.m_fDeltaY * time;
		}
	}
}