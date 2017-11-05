using System;

namespace cocos2d
{
	public class CCPlace : CCActionInstant
	{
		private CCPoint m_tPosition;

		public CCPlace()
		{
			this.m_tPosition = new CCPoint();
		}

		public static CCPlace actionWithPosition(CCPoint pos)
		{
			CCPlace cCPlace = new CCPlace();
			if (cCPlace != null && cCPlace.initWithPosition(pos))
			{
				return cCPlace;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCPlace cCPlace = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCPlace = new CCPlace();
				pZone = new CCZone(cCPlace);
			}
			else
			{
				cCPlace = (CCPlace)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCPlace.initWithPosition(this.m_tPosition);
			return cCPlace;
		}

		~CCPlace()
		{
		}

		public bool initWithPosition(CCPoint pos)
		{
			this.m_tPosition = pos;
			return true;
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_pTarget.position = this.m_tPosition;
		}
	}
}