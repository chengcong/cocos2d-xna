using System;
using System.Collections.Generic;
using System.Linq;

namespace cocos2d
{
	public class CCLabelBMFont : CCSpriteBatchNode, ICCLabelProtocol, ICCRGBAProtocol
	{
		private const uint kCCBMFontMaxChars = 2048;

		public static Dictionary<string, CCBMFontConfiguration> configurations;

		private byte m_cOpacity;

		private ccColor3B m_tColor;

		private bool m_bIsOpacityModifyRGB;

		protected string m_sString = "";

		protected CCBMFontConfiguration m_pConfiguration;

		public override CCPoint anchorPoint
		{
			get
			{
				return base.anchorPoint;
			}
			set
			{
				if (!CCPoint.CCPointEqualToPoint(value, this.m_tAnchorPoint))
				{
					base.anchorPoint = value;
					this.createFontChars();
				}
			}
		}

		public ccColor3B Color
		{
			get
			{
				return this.m_tColor;
			}
			set
			{
				this.m_tColor.r = value.r;
				this.m_tColor.g = value.g;
				this.m_tColor.b = value.b;
				this.createFontChars();
			}
		}

		public bool IsOpacityModifyRGB
		{
			get
			{
				return this.m_bIsOpacityModifyRGB;
			}
			set
			{
				this.m_bIsOpacityModifyRGB = value;
			}
		}

		public byte Opacity
		{
			get
			{
				return this.m_cOpacity;
			}
			set
			{
				this.m_cOpacity = value;
			}
		}

		public CCLabelBMFont()
		{
		}

		private char atlasNameFromFntFile(string fntFile)
		{
			throw new NotImplementedException();
		}

		public virtual ICCLabelProtocol convertToLabelProtocol()
		{
			return this;
		}

		public virtual ICCRGBAProtocol convertToRGBAProtocol()
		{
			return this;
		}

		public void createFontChars()
		{
			int item = 0;
			int mUCommonHeight = 0;
			int num = -1;
			int num1 = 0;
			CCSize cCSize = new CCSize(0f, 0f);
			int num2 = 0;
			int mUCommonHeight1 = 0;
			int num3 = 1;
			int length = this.m_sString.Length;
			if (length == 0)
			{
				return;
			}
			for (int i = 0; i < length - 1; i++)
			{
				if (this.m_sString[i] == 10)
				{
					num3++;
				}
			}
			mUCommonHeight1 = this.m_pConfiguration.m_uCommonHeight * num3;
			mUCommonHeight = -(this.m_pConfiguration.m_uCommonHeight - this.m_pConfiguration.m_uCommonHeight * num3);
			for (int j = 0; j < length; j++)
			{
				int mSString = this.m_sString[j];
				if ((long)mSString >= (long)2048)
				{
					object[] objArray = new object[] { "LabelBMFont: character ", this.m_sString[j], " outside of max font characters, which is ", (uint)2048 };
					throw new ArgumentException(string.Concat(objArray));
				}
				if (mSString != 10)
				{
					num1 = this.kerningAmountForFirst(num, mSString);
					if (!this.m_pConfiguration.m_pBitmapFontArray.ContainsKey(mSString))
					{
						throw new ArgumentException(string.Concat("Character ", mSString, " in LabelBMFont is not in the font definition."));
					}
					ccBMFontDef _ccBMFontDef = this.m_pConfiguration.m_pBitmapFontArray[mSString];
					CCRect cCRect = _ccBMFontDef.rect;
					CCSprite childByTag = (CCSprite)base.getChildByTag(j);
					if (childByTag != null)
					{
						childByTag.setTextureRectInPixels(cCRect, false, cCRect.size);
						childByTag.visible = true;
						childByTag.Opacity = 255;
					}
					else
					{
						childByTag = new CCSprite();
						childByTag.initWithBatchNodeRectInPixels(this, cCRect);
						this.addChild(childByTag, 0, j);
					}
					float single = (float)(this.m_pConfiguration.m_uCommonHeight - _ccBMFontDef.yOffset);
					childByTag.positionInPixels = new CCPoint((float)(item + _ccBMFontDef.xOffset) + _ccBMFontDef.rect.size.width / 2f + (float)num1, (float)mUCommonHeight + single - cCRect.size.height / 2f);
					item = item + this.m_pConfiguration.m_pBitmapFontArray[mSString].xAdvance + num1;
					num = mSString;
					childByTag.IsOpacityModifyRGB = this.m_bIsOpacityModifyRGB;
					childByTag.Color = this.m_tColor;
					if (this.m_cOpacity != 255)
					{
						childByTag.Opacity = this.m_cOpacity;
					}
					if (num2 < item)
					{
						num2 = item;
					}
				}
				else
				{
					item = 0;
					mUCommonHeight = mUCommonHeight - this.m_pConfiguration.m_uCommonHeight;
				}
			}
			cCSize.width = (float)num2;
			cCSize.height = (float)mUCommonHeight1;
			base.contentSizeInPixels = cCSize;
		}

		private static CCBMFontConfiguration FNTConfigLoadFile(string file)
		{
			CCBMFontConfiguration item = null;
			if (CCLabelBMFont.configurations == null)
			{
				CCLabelBMFont.configurations = new Dictionary<string, CCBMFontConfiguration>();
			}
			if (CCLabelBMFont.configurations.Keys.Contains<string>(file))
			{
				item = CCLabelBMFont.configurations[file];
			}
			else
			{
				item = CCBMFontConfiguration.configurationWithFNTFile(file);
				CCLabelBMFont.configurations.Add(file, item);
			}
			return item;
		}

		public static void FNTConfigRemoveCache()
		{
			throw new NotImplementedException();
		}

		public virtual string getString()
		{
			return this.m_sString;
		}

		public bool initWithString(string theString, string fntFile)
		{
			this.m_pConfiguration = CCLabelBMFont.FNTConfigLoadFile(fntFile);
			if (!base.initWithFile(this.m_pConfiguration.m_sAtlasName, theString.Length))
			{
				return false;
			}
			this.m_cOpacity = 255;
			this.m_tColor = new ccColor3B(255, 255, 255);
			this.m_tContentSize = new CCSize(0f, 0f);
			this.m_bIsOpacityModifyRGB = this.m_pobTextureAtlas.Texture.HasPremultipliedAlpha;
			this.anchorPoint = new CCPoint(0.5f, 0.5f);
			this.setString(theString);
			return true;
		}

		private int kerningAmountForFirst(int first, int second)
		{
			int num = 0;
			int num1 = first << 16 | second & 65535;
			if (this.m_pConfiguration.m_pKerningDictionary != null && this.m_pConfiguration.m_pKerningDictionary.ContainsKey(num1))
			{
				tKerningHashElement item = this.m_pConfiguration.m_pKerningDictionary[num1];
				if (item != null)
				{
					num = item.amount;
				}
			}
			return num;
		}

		public static CCLabelBMFont labelWithString(string str, string fntFile)
		{
			CCLabelBMFont cCLabelBMFont = new CCLabelBMFont();
			if (cCLabelBMFont.initWithString(str, fntFile))
			{
				return cCLabelBMFont;
			}
			return null;
		}

		public static void purgeCachedData()
		{
			CCLabelBMFont.FNTConfigRemoveCache();
		}

		public virtual void setString(string label)
		{
			this.m_sString = label;
			if (this.m_pChildren != null && this.m_pChildren.Count != 0)
			{
				for (int i = 0; i < this.m_pChildren.Count; i++)
				{
					CCNode item = this.m_pChildren[i];
					if (item != null)
					{
						item.visible = false;
					}
				}
			}
			this.createFontChars();
		}
	}
}