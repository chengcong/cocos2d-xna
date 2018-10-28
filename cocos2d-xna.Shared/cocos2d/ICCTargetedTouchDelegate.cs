using System;

namespace cocos2d
{
	public interface ICCTargetedTouchDelegate : ICCTouchDelegate
	{
		bool ccTouchBegan(CCTouch pTouch, CCEvent pEvent);

		void ccTouchCancelled(CCTouch pTouch, CCEvent pEvent);

		void ccTouchEnded(CCTouch pTouch, CCEvent pEvent);

		void ccTouchMoved(CCTouch pTouch, CCEvent pEvent);
	}
}