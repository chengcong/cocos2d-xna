using System;

namespace cocos2d
{
	public class CCMenuItemImage : CCMenuItemSprite
	{
		public CCMenuItemImage()
		{
		}

		private bool initFromNormalImage(string normalImage, string selectedImage, string disabledImage, SelectorProtocol target, SEL_MenuHandler selector)
		{
			CCNode cCNode = CCSprite.spriteWithFile(normalImage);
			CCNode cCNode1 = null;
			CCNode cCNode2 = null;
			if (selectedImage != null && !string.IsNullOrEmpty(selectedImage))
			{
				cCNode1 = CCSprite.spriteWithFile(selectedImage);
			}
			if (disabledImage != null && !string.IsNullOrEmpty(disabledImage))
			{
				cCNode2 = CCSprite.spriteWithFile(disabledImage);
			}
			return base.initFromNormalSprite(cCNode, cCNode1, cCNode2, target, selector);
		}

		public static CCMenuItemImage itemFromNormalImage(string normalImage, string selectedImage)
		{
			return CCMenuItemImage.itemFromNormalImage(normalImage, selectedImage, null, null, null);
		}

		public static CCMenuItemImage itemFromNormalImage(string normalImage, string selectedImage, SelectorProtocol target, SEL_MenuHandler selector)
		{
			return CCMenuItemImage.itemFromNormalImage(normalImage, selectedImage, null, target, selector);
		}

		public static CCMenuItemImage itemFromNormalImage(string normalImage, string selectedImage, string disabledImage, SelectorProtocol target, SEL_MenuHandler selector)
		{
			CCMenuItemImage cCMenuItemImage = new CCMenuItemImage();
			if (cCMenuItemImage.initFromNormalImage(normalImage, selectedImage, disabledImage, target, selector))
			{
				return cCMenuItemImage;
			}
			return null;
		}

		public static CCMenuItemImage itemFromNormalImage(string normalImage, string selectedImage, string disabledImage)
		{
			CCMenuItemImage cCMenuItemImage = new CCMenuItemImage();
			if (cCMenuItemImage != null && cCMenuItemImage.initFromNormalImage(normalImage, selectedImage, disabledImage, null, null))
			{
				return cCMenuItemImage;
			}
			return null;
		}
	}
}