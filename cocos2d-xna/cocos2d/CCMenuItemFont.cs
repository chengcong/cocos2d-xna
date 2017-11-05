using System;

namespace cocos2d
{
	public class CCMenuItemFont : CCMenuItemLabel
	{
		protected uint m_uFontSize;

		protected string m_strFontName;

		public static string FontName
		{
			get
			{
				return CCMenuItem._fontName;
			}
			set
			{
				CCMenuItem._fontName = value;
			}
		}

		public string FontNameObj
		{
			get
			{
				return this.m_strFontName;
			}
			set
			{
				this.m_strFontName = value;
				this.recreateLabel();
			}
		}

		public static uint FontSize
		{
			get
			{
				return CCMenuItem._fontSize;
			}
			set
			{
				CCMenuItem._fontSize = value;
			}
		}

		public uint FontSizeObj
		{
			get
			{
				return this.m_uFontSize;
			}
			set
			{
				this.m_uFontSize = value;
				this.recreateLabel();
			}
		}

		public CCMenuItemFont()
		{
		}

		public bool initFromString(string value, SelectorProtocol target, SEL_MenuHandler selector)
		{
			this.m_strFontName = CCMenuItem._fontName;
			this.m_uFontSize = CCMenuItem._fontSize;
			CCLabelTTF cCLabelTTF = CCLabelTTF.labelWithString(value, this.m_strFontName, (float)((float)this.m_uFontSize));
			base.initWithLabel(cCLabelTTF, target, selector);
			return true;
		}

		public static CCMenuItemFont itemFromString(string value)
		{
			CCMenuItemFont cCMenuItemFont = new CCMenuItemFont();
			cCMenuItemFont.initFromString(value, null, null);
			return cCMenuItemFont;
		}

		public static CCMenuItemFont itemFromString(string value, SelectorProtocol target, SEL_MenuHandler selector)
		{
			CCMenuItemFont cCMenuItemFont = new CCMenuItemFont();
			cCMenuItemFont.initFromString(value, target, selector);
			return cCMenuItemFont;
		}

		protected void recreateLabel()
		{
		}
	}
}