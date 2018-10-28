using System;
using System.Collections.Generic;

namespace cocos2d
{
	public interface ICCStandardTouchDelegate : ICCTouchDelegate
	{
		void ccTouchesBegan(List<CCTouch> pTouches, CCEvent pEvent);

		void ccTouchesCancelled(List<CCTouch> pTouches, CCEvent pEvent);

		void ccTouchesEnded(List<CCTouch> pTouches, CCEvent pEvent);

		void ccTouchesMoved(List<CCTouch> pTouches, CCEvent pEvent);
	}
}