using System;

namespace cocos2d
{
	public class CCFlipY : CCActionInstant
	{
		private bool m_bFlipY;

		public CCFlipY()
		{
			this.m_bFlipY = false;
		}

		public static CCFlipY actionWithFlipY(bool y)
		{
			CCFlipY cCFlipY = new CCFlipY();
			if (cCFlipY != null && cCFlipY.initWithFlipY(y))
			{
				return cCFlipY;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCFlipY cCFlipY = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCFlipY = new CCFlipY();
				pZone = new CCZone(cCFlipY);
			}
			else
			{
				cCFlipY = (CCFlipY)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCFlipY.initWithFlipY(this.m_bFlipY);
			return cCFlipY;
		}

		~CCFlipY()
		{
		}

		public bool initWithFlipY(bool y)
		{
			this.m_bFlipY = y;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCFlipY.actionWithFlipY(!this.m_bFlipY);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			((CCSprite)pTarget).IsFlipY = this.m_bFlipY;
		}
	}
}