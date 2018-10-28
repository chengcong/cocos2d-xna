using cocos2d;
using System;

namespace cocos2d.menu_nodes
{
	public class CCMenuItemAtlasFont : CCMenuItemLabel
	{
		public CCMenuItemAtlasFont()
		{
		}

		public bool initFromString(string value, string charMapFile, int itemWidth, int itemHeight, char startCharMap, SelectorProtocol target, SEL_MenuHandler selector)
		{
			CCLabelAtlas cCLabelAtla = new CCLabelAtlas();
			cCLabelAtla.initWithString(value, charMapFile, itemWidth, itemHeight, startCharMap);
			base.initWithLabel(cCLabelAtla, target, selector);
			return true;
		}

		public static CCMenuItemAtlasFont itemFromString(string value, string charMapFile, int itemWidth, int itemHeight, char startCharMap)
		{
			return CCMenuItemAtlasFont.itemFromString(value, charMapFile, itemWidth, itemHeight, startCharMap, null, null);
		}

		public static CCMenuItemAtlasFont itemFromString(string value, string charMapFile, int itemWidth, int itemHeight, char startCharMap, SelectorProtocol target, SEL_MenuHandler selector)
		{
			CCMenuItemAtlasFont cCMenuItemAtlasFont = new CCMenuItemAtlasFont();
			cCMenuItemAtlasFont.initFromString(value, charMapFile, itemWidth, itemHeight, startCharMap, target, selector);
			return cCMenuItemAtlasFont;
		}
	}
}