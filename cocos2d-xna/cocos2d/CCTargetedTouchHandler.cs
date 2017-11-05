using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCTargetedTouchHandler : CCTouchHandler
	{
		protected bool m_bSwallowsTouches;

		protected List<CCTouch> m_pClaimedTouches;

		public List<CCTouch> ClaimedTouches
		{
			get
			{
				return this.m_pClaimedTouches;
			}
		}

		public bool IsSwallowsTouches
		{
			get
			{
				return this.m_bSwallowsTouches;
			}
			set
			{
				this.m_bSwallowsTouches = value;
			}
		}

		public CCTargetedTouchHandler()
		{
		}

		public static CCTargetedTouchHandler handlerWithDelegate(ICCTargetedTouchDelegate pDelegate, int nPriority, bool bSwallow)
		{
			CCTargetedTouchHandler cCTargetedTouchHandler = new CCTargetedTouchHandler();
			cCTargetedTouchHandler.initWithDelegate(pDelegate, nPriority, bSwallow);
			return cCTargetedTouchHandler;
		}

		public bool initWithDelegate(ICCTargetedTouchDelegate pDelegate, int nPriority, bool bSwallow)
		{
			if (!base.initWithDelegate(pDelegate, nPriority))
			{
				return false;
			}
			this.m_pClaimedTouches = new List<CCTouch>();
			this.m_bSwallowsTouches = bSwallow;
			return true;
		}
	}
}