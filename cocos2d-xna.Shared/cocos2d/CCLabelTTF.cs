using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cocos2d
{
	public class CCLabelTTF : CCSprite, ICCLabelProtocol
	{
		protected SpriteFont spriteFont;

		protected CCSize m_tDimensions = new CCSize();

		protected CCTextAlignment m_eAlignment;

		protected string m_pFontName;

		protected float m_fFontSize;

		protected string m_pString;

		protected Microsoft.Xna.Framework.Color m_fgColor = Microsoft.Xna.Framework.Color.White;

		protected Microsoft.Xna.Framework.Color m_bgColor = Microsoft.Xna.Framework.Color.Transparent;

		public CCLabelTTF()
		{
			this.m_eAlignment = CCTextAlignment.CCTextAlignmentCenter;
			this.m_pFontName = string.Empty;
			this.m_fFontSize = 0f;
		}

		public virtual ICCLabelProtocol convertToLabelProtocol()
		{
			return this;
		}

		public string description()
		{
			return string.Format("<CCLabelTTF | FontName = {0}, FontSize = {1}>", this.m_pFontName, this.m_fFontSize);
		}

		public string getString()
		{
			return this.m_pString.ToString();
		}

		public bool initWithString(string label, CCSize dimensions, CCTextAlignment alignment, string fontName, float fontSize)
		{
			if (!this.init())
			{
				return false;
			}
			this.m_tDimensions = new CCSize(dimensions.width * CCDirector.sharedDirector().ContentScaleFactor, dimensions.height * CCDirector.sharedDirector().ContentScaleFactor);
			this.m_eAlignment = alignment;
			this.m_pFontName = fontName;
			this.m_fFontSize = fontSize * CCDirector.sharedDirector().ContentScaleFactor;
			this.setString(label);
			return true;
		}

		public bool initWithString(string label, string fontName, float fontSize)
		{
			if (!base.init())
			{
				return false;
			}
			this.m_tDimensions = new CCSize(0f, 0f);
			this.m_pFontName = fontName;
			this.m_fFontSize = fontSize * CCDirector.sharedDirector().ContentScaleFactor;
			this.setString(label);
			return true;
		}

		public bool initWithString(string label, string fontName, float fontSize, Microsoft.Xna.Framework.Color fgColor, Microsoft.Xna.Framework.Color bgColor)
		{
			if (!base.init())
			{
				return false;
			}
			this.m_tDimensions = new CCSize(0f, 0f);
			this.m_pFontName = fontName;
			this.m_fFontSize = fontSize * CCDirector.sharedDirector().ContentScaleFactor;
			this.m_fgColor = fgColor;
			this.m_bgColor = bgColor;
			this.setString(label);
			return true;
		}

		public static CCLabelTTF labelWithString(string label, CCSize dimensions, CCTextAlignment alignment, string fontName, float fontSize)
		{
			CCLabelTTF cCLabelTTF = new CCLabelTTF();
			if (cCLabelTTF != null && cCLabelTTF.initWithString(label, dimensions, alignment, fontName, fontSize))
			{
				return cCLabelTTF;
			}
			return null;
		}

		public static CCLabelTTF labelWithString(string label, string fontName, float fontSize)
		{
			CCLabelTTF cCLabelTTF = new CCLabelTTF();
			if (cCLabelTTF.initWithString(label, fontName, fontSize))
			{
				return cCLabelTTF;
			}
			return null;
		}

		public static CCLabelTTF labelWithString(string label, string fontName, float fontSize, Microsoft.Xna.Framework.Color fgColor, Microsoft.Xna.Framework.Color bgColor)
		{
			CCLabelTTF cCLabelTTF = new CCLabelTTF();
			if (cCLabelTTF.initWithString(label, fontName, fontSize, fgColor, bgColor))
			{
				return cCLabelTTF;
			}
			return null;
		}

		public void setString(string label)
		{
			CCTexture2D cCTexture2D;
			this.m_pString = label;
			if (!CCSize.CCSizeEqualToSize(this.m_tDimensions, new CCSize(0f, 0f)))
			{
				cCTexture2D = new CCTexture2D();
				cCTexture2D.initWithString(label, this.m_tDimensions, this.m_eAlignment, this.m_pFontName.ToString(), this.m_fFontSize, this.m_fgColor, this.m_bgColor);
			}
			else
			{
				cCTexture2D = new CCTexture2D();
				cCTexture2D.initWithString(label, this.m_pFontName.ToString(), this.m_fFontSize, this.m_fgColor, this.m_bgColor);
			}
			this.Texture = cCTexture2D;
			CCRect cCRect = new CCRect(0f, 0f, 0f, 0f)
			{
				size = this.m_pobTexture.getContentSize()
			};
			base.setTextureRect(cCRect);
		}
	}
}