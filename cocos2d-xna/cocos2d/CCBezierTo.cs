using System;

namespace cocos2d
{
	public class CCBezierTo : CCBezierBy
	{
		public CCBezierTo()
		{
		}

		public static new CCBezierTo actionWithDuration(float t, ccBezierConfig c)
		{
			CCBezierTo cCBezierTo = new CCBezierTo();
			cCBezierTo.initWithDuration(t, c);
			return cCBezierTo;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCBezierTo cCBezierTo = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCBezierTo = new CCBezierTo();
				cCZone = new CCZone(cCBezierTo);
			}
			else
			{
				cCBezierTo = cCZone.m_pCopyObject as CCBezierTo;
				if (cCBezierTo == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCBezierTo.initWithDuration(this.m_fDuration, this.m_sConfig);
			return cCBezierTo;
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_sConfig.controlPoint_1 = CCPointExtension.ccpSub(this.m_sConfig.controlPoint_1, this.m_startPosition);
			this.m_sConfig.controlPoint_2 = CCPointExtension.ccpSub(this.m_sConfig.controlPoint_2, this.m_startPosition);
			this.m_sConfig.endPosition = CCPointExtension.ccpSub(this.m_sConfig.endPosition, this.m_startPosition);
		}
	}
}