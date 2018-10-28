using cocos2d.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCBMFontConfiguration : CCObject
	{
		public Dictionary<int, ccBMFontDef> m_pBitmapFontArray = new Dictionary<int, ccBMFontDef>();

		public int m_uCommonHeight;

		public ccBMFontPadding m_tPadding = new ccBMFontPadding();

		public string m_sAtlasName;

		public Dictionary<int, tKerningHashElement> m_pKerningDictionary = new Dictionary<int, tKerningHashElement>();

		public string Description
		{
			get
			{
				return (new string[100]).ToString();
			}
		}

		public CCBMFontConfiguration()
		{
			this.purgeKerningDictionary();
			this.m_sAtlasName = "";
		}

		public static CCBMFontConfiguration configurationWithFNTFile(string FNTfile)
		{
			CCBMFontConfiguration cCBMFontConfiguration = new CCBMFontConfiguration();
			if (cCBMFontConfiguration.initWithFNTfile(FNTfile))
			{
				return cCBMFontConfiguration;
			}
			return null;
		}

		public bool initWithFNTfile(string FNTfile)
		{
			this.m_pKerningDictionary = new Dictionary<int, tKerningHashElement>();
			this.parseConfigFile(FNTfile);
			return true;
		}

		private void parseCharacterDefinition(string line, ccBMFontDef characterDefinition)
		{
			int num = line.IndexOf("id=");
			int num1 = line.IndexOf(' ', num);
			string str = line.Substring(num, num1 - num);
			characterDefinition.charID = ccUtils.ccParseInt(str.Replace("id=", ""));
			num = line.IndexOf("x=");
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			characterDefinition.rect.origin.x = ccUtils.ccParseFloat(str.Replace("x=", ""));
			num = line.IndexOf("y=");
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			characterDefinition.rect.origin.y = ccUtils.ccParseFloat(str.Replace("y=", ""));
			num = line.IndexOf("width=");
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			characterDefinition.rect.size.width = ccUtils.ccParseFloat(str.Replace("width=", ""));
			num = line.IndexOf("height=");
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			characterDefinition.rect.size.height = ccUtils.ccParseFloat(str.Replace("height=", ""));
			num = line.IndexOf("xoffset=");
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			characterDefinition.xOffset = ccUtils.ccParseInt(str.Replace("xoffset=", ""));
			num = line.IndexOf("yoffset=");
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			characterDefinition.yOffset = ccUtils.ccParseInt(str.Replace("yoffset=", ""));
			num = line.IndexOf("xadvance=");
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			characterDefinition.xAdvance = ccUtils.ccParseInt(str.Replace("xadvance=", ""));
		}

		private void parseCommonArguments(string line)
		{
			int num = line.IndexOf("lineHeight=");
			int num1 = line.IndexOf(' ', num);
			string str = line.Substring(num, num1 - num);
			this.m_uCommonHeight = ccUtils.ccParseInt(str.Replace("lineHeight=", ""));
			num = line.IndexOf("scaleW=") + "scaleW=".Length;
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			num = line.IndexOf("scaleH=") + "scaleH=".Length;
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			num = line.IndexOf("pages=") + "pages=".Length;
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
		}

		private void parseConfigFile(string controlFile)
		{
			string str;
			CCContent cCContent = CCApplication.sharedApplication().content.Load<CCContent>(controlFile);
			string content = cCContent.Content;
			int length = cCContent.Content.Length;
			if (string.IsNullOrEmpty(content))
			{
				return;
			}
			string str1 = content;
			if (str1.StartsWith("<?xml"))
			{
				throw new ArgumentException("FNT control file is XML, expecting it to be plain text.");
			}
			while (str1.Length > 0)
			{
				int num = str1.IndexOf('\n');
				if (num == -1)
				{
					str = str1;
					str1 = null;
				}
				else
				{
					str = str1.Substring(0, num);
					str1 = str1.Substring(num + 1);
				}
				if (str.StartsWith("info face"))
				{
					this.parseInfoArguments(str);
				}
				if (str.StartsWith("common lineHeight"))
				{
					this.parseCommonArguments(str);
				}
				if (str.StartsWith("page id"))
				{
					this.parseImageFileName(str, controlFile);
				}
				if (str.StartsWith("chars c"))
				{
					continue;
				}
				if (str.StartsWith("char"))
				{
					ccBMFontDef _ccBMFontDef = new ccBMFontDef();
					this.parseCharacterDefinition(str, _ccBMFontDef);
					this.m_pBitmapFontArray.Add(_ccBMFontDef.charID, _ccBMFontDef);
				}
				if (str.StartsWith("kernings count"))
				{
					this.parseKerningCapacity(str);
				}
				if (!str.StartsWith("kerning first"))
				{
					continue;
				}
				this.parseKerningEntry(str);
			}
		}

		private void parseImageFileName(string line, string fntFile)
		{
			int num = line.IndexOf('=') + 1;
			int num1 = line.IndexOf(' ', num);
			string str = line.Substring(num, num1 - num);
			num = line.IndexOf('\"') + 1;
			num1 = line.IndexOf('\"', num);
			str = line.Substring(num, num1 - num);
			string str1 = str.Substring(0, str.LastIndexOf('.'));
			this.m_sAtlasName = string.Concat(fntFile.Substring(0, fntFile.LastIndexOf("/")), "/images/", str1);
		}

		private void parseInfoArguments(string line)
		{
			int num = line.IndexOf("padding=");
			int num1 = line.IndexOf(' ', num);
			string str = line.Substring(num, num1 - num);
			str = str.Replace("padding=", "");
			string[] strArrays = str.Split(new char[] { ',' });
			this.m_tPadding.top = ccUtils.ccParseInt(strArrays[0]);
			this.m_tPadding.right = ccUtils.ccParseInt(strArrays[1]);
			this.m_tPadding.bottom = ccUtils.ccParseInt(strArrays[2]);
			this.m_tPadding.left = ccUtils.ccParseInt(strArrays[3]);
		}

		private void parseKerningCapacity(string line)
		{
		}

		private void parseKerningEntry(string line)
		{
			int num = line.IndexOf("first=");
			int num1 = line.IndexOf(' ', num);
			string str = line.Substring(num, num1 - num);
			int num2 = ccUtils.ccParseInt(str.Replace("first=", ""));
			num = line.IndexOf("second=");
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num, num1 - num);
			int num3 = ccUtils.ccParseInt(str.Replace("second=", ""));
			num = line.IndexOf("amount=");
			num1 = line.IndexOf(' ', num);
			str = line.Substring(num);
			int num4 = ccUtils.ccParseInt(str.Replace("amount=", ""));
			tKerningHashElement _tKerningHashElement = new tKerningHashElement()
			{
				amount = num4,
				key = num2 << 16 | num3 & 65535
			};
			this.m_pKerningDictionary.Add(_tKerningHashElement.key, _tKerningHashElement);
		}

		private void purgeKerningDictionary()
		{
			this.m_pKerningDictionary.Clear();
		}
	}
}