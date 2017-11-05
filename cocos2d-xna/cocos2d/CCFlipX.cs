using System;

namespace cocos2d
{
	public class CCFlipX : CCActionInstant
	{
		private bool m_bFlipX;

		public CCFlipX()
		{
			this.m_bFlipX = false;
		}

		public static CCFlipX actionWithFlipX(bool x)
		{
			CCFlipX cCFlipX = new CCFlipX();
			if (cCFlipX != null && cCFlipX.initWithFlipX(x))
			{
				return cCFlipX;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCFlipX cCFlipX = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCFlipX = new CCFlipX();
				pZone = new CCZone(cCFlipX);
			}
			else
			{
				cCFlipX = (CCFlipX)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCFlipX.initWithFlipX(this.m_bFlipX);
			return cCFlipX;
		}

		~CCFlipX()
		{
		}

		public bool initWithFlipX(bool x)
		{
			this.m_bFlipX = x;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCFlipX.actionWithFlipX(!this.m_bFlipX);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			((CCSprite)pTarget).IsFlipX = this.m_bFlipX;
		}
	}
}