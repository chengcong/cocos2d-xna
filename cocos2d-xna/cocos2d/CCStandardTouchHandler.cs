using System;

namespace cocos2d
{
	public class CCStandardTouchHandler : CCTouchHandler
	{
		public CCStandardTouchHandler()
		{
		}

		public static CCStandardTouchHandler handlerWithDelegate(ICCStandardTouchDelegate pDelegate, int nPriority)
		{
			CCStandardTouchHandler cCStandardTouchHandler = new CCStandardTouchHandler();
			cCStandardTouchHandler.initWithDelegate(pDelegate, nPriority);
			return cCStandardTouchHandler;
		}

		public virtual bool initWithDelegate(ICCStandardTouchDelegate pDelegate, int nPriority)
		{
			return base.initWithDelegate(pDelegate, nPriority);
		}
	}
}