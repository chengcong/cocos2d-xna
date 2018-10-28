using System;
using System.Collections.Generic;
using System.Linq;

namespace cocos2d
{
	public class CCSpriteFrameCache
	{
		protected Dictionary<string, CCSpriteFrame> m_pSpriteFrames;

		protected Dictionary<string, string> m_pSpriteFramesAliases;

		public static CCSpriteFrameCache pSharedSpriteFrameCache;

		static CCSpriteFrameCache()
		{
		}

		public CCSpriteFrameCache()
		{
		}

		public void addSpriteFrame(CCSpriteFrame pobFrame, string pszFrameName)
		{
			this.m_pSpriteFrames.Add(pszFrameName, pobFrame);
		}

		public void addSpriteFramesWithDictionary(Dictionary<string, object> pobDictionary, CCTexture2D pobTexture)
		{
			Dictionary<string, object> item = null;
			if (pobDictionary.Keys.Contains<string>("metadata"))
			{
				item = (Dictionary<string, object>)pobDictionary["metadata"];
			}
			Dictionary<string, object> strs = null;
			if (pobDictionary.Keys.Contains<string>("frames"))
			{
				strs = (Dictionary<string, object>)pobDictionary["frames"];
			}
			int num = 0;
			if (item != null)
			{
				num = ccUtils.ccParseInt(item["format"].ToString());
			}
			foreach (string key in strs.Keys)
			{
				Dictionary<string, object> item1 = strs[key] as Dictionary<string, object>;
				CCSpriteFrame cCSpriteFrame = new CCSpriteFrame();
				if (num == 0)
				{
					float single = ccUtils.ccParseFloat(item1["x"].ToString());
					float single1 = ccUtils.ccParseFloat(item1["y"].ToString());
					float single2 = ccUtils.ccParseFloat(item1["width"].ToString());
					float single3 = ccUtils.ccParseFloat(item1["height"].ToString());
					float single4 = ccUtils.ccParseFloat(item1["offsetX"].ToString());
					float single5 = ccUtils.ccParseFloat(item1["offsetY"].ToString());
					int num1 = ccUtils.ccParseInt(item1["originalWidth"].ToString());
					int num2 = ccUtils.ccParseInt(item1["originalHeight"].ToString());
					if (num1 == 0 || num2 == 0)
					{
						CCLog.Log("cocos2d: WARNING: originalWidth/Height not found on the CCSpriteFrame. AnchorPoint won't work as expected. Regenrate the .plist");
					}
					num1 = Math.Abs(num1);
					num2 = Math.Abs(num2);
					cCSpriteFrame = new CCSpriteFrame();
					cCSpriteFrame.initWithTexture(pobTexture, new CCRect(single, single1, single2, single3), false, new CCPoint(single4, single5), new CCSize((float)num1, (float)num2));
				}
				else if (num == 1 || num == 2)
				{
					CCRect cCRect = CCNS.CCRectFromString(item1["frame"].ToString());
					bool flag = false;
					if (num == 2 && item1.Keys.Contains<string>("rotated"))
					{
						flag = (ccUtils.ccParseInt(this.valueForKey("rotated", item1)) == 0 ? false : true);
					}
					CCPoint cCPoint = CCNS.CCPointFromString(this.valueForKey("offset", item1));
					CCSize cCSize = CCNS.CCSizeFromString(this.valueForKey("sourceSize", item1));
					cCSpriteFrame = new CCSpriteFrame();
					cCSpriteFrame.initWithTexture(pobTexture, cCRect, flag, cCPoint, cCSize);
				}
				else if (num == 3)
				{
					CCSize cCSize1 = CCNS.CCSizeFromString(this.valueForKey("spriteSize", item1));
					CCPoint cCPoint1 = CCNS.CCPointFromString(this.valueForKey("spriteOffset", item1));
					CCSize cCSize2 = CCNS.CCSizeFromString(this.valueForKey("spriteSourceSize", item1));
					CCRect cCRect1 = CCNS.CCRectFromString(this.valueForKey("textureRect", item1));
					bool flag1 = false;
					if (item1.Keys.Contains<string>("textureRotated"))
					{
						flag1 = (ccUtils.ccParseInt(this.valueForKey("textureRotated", item1)) == 0 ? false : true);
					}
					object obj = item1["aliases"];
					List<object> objs = item1["aliases"] as List<object>;
					string str = key;
					foreach (object obj1 in objs)
					{
						string str1 = obj1.ToString();
						if (this.m_pSpriteFramesAliases.Keys.Contains<string>(str1) && this.m_pSpriteFramesAliases[str1] != null)
						{
							CCLog.Log("cocos2d: WARNING: an alias with name {0} already exists", new object[] { str1 });
						}
						if (this.m_pSpriteFramesAliases.Keys.Contains<string>(str))
						{
							continue;
						}
						this.m_pSpriteFramesAliases.Add(str, str1);
					}
					cCSpriteFrame = new CCSpriteFrame();
					cCSpriteFrame.initWithTexture(pobTexture, new CCRect(cCRect1.origin.x, cCRect1.origin.y, cCSize1.width, cCSize1.height), flag1, cCPoint1, cCSize2);
				}
				if (this.m_pSpriteFrames.Keys.Contains<string>(key))
				{
					continue;
				}
				this.m_pSpriteFrames.Add(key, cCSpriteFrame);
			}
		}

		public void addSpriteFramesWithFile(string pszPlist)
		{
			Dictionary<string, object> item;
			string str = CCFileUtils.fullPathFromRelativePath(pszPlist);
			Dictionary<string, object> strs = CCFileUtils.dictionaryWithContentsOfFile(str);
			string str1 = "";
			if (strs.Keys.Contains<string>("metadata"))
			{
				item = (Dictionary<string, object>)strs["metadata"];
			}
			else
			{
				item = null;
			}
			Dictionary<string, object> strs1 = item;
			if (strs1 != null && strs1.Keys.Contains<string>("textureFileName"))
			{
				str1 = this.valueForKey("textureFileName", strs1);
			}
			if (string.IsNullOrEmpty(str1))
			{
				str1 = str;
				int num = str.IndexOf("/");
				if (num < 0)
				{
					num = str.IndexOf("\\");
				}
				if (num > 0)
				{
					str1 = string.Concat(str.Substring(0, num), "/images", str.Substring(num));
				}
				CCLog.Log("cocos2d: CCSpriteFrameCache: Trying to use file {0} as texture", new object[] { str1 });
			}
			else
			{
				str1 = CCFileUtils.fullPathFromRelativeFile(str1, str);
			}
			CCTexture2D cCTexture2D = CCTextureCache.sharedTextureCache().addImage(str1);
			if (cCTexture2D == null)
			{
				CCLog.Log("cocos2d: CCSpriteFrameCache: Couldn't load texture");
				return;
			}
			this.addSpriteFramesWithDictionary(strs, cCTexture2D);
		}

		public void addSpriteFramesWithFile(string plist, string textureFileName)
		{
			CCTexture2D cCTexture2D = CCTextureCache.sharedTextureCache().addImage(textureFileName);
			if (cCTexture2D != null)
			{
				this.addSpriteFramesWithFile(plist, cCTexture2D);
				return;
			}
			CCLog.Log("cocos2d: CCSpriteFrameCache: couldn't load texture file. File not found {0}", new object[] { textureFileName });
		}

		public void addSpriteFramesWithFile(string pszPlist, CCTexture2D pobTexture)
		{
			string str = CCFileUtils.fullPathFromRelativePath(pszPlist);
			this.addSpriteFramesWithDictionary(CCFileUtils.dictionaryWithContentsOfFile(str), pobTexture);
		}

		public bool init()
		{
			this.m_pSpriteFrames = new Dictionary<string, CCSpriteFrame>();
			this.m_pSpriteFramesAliases = new Dictionary<string, string>();
			return true;
		}

		public static void purgeSharedSpriteFrameCache()
		{
			CCSpriteFrameCache.pSharedSpriteFrameCache = null;
		}

		public void removeSpriteFrameByName(string pszName)
		{
			if (string.IsNullOrEmpty(pszName))
			{
				return;
			}
			string item = this.m_pSpriteFramesAliases[pszName];
			if (string.IsNullOrEmpty(item))
			{
				this.m_pSpriteFrames.Remove(pszName);
				return;
			}
			this.m_pSpriteFrames.Remove(item);
			this.m_pSpriteFramesAliases.Remove(item);
		}

		public void removeSpriteFrames()
		{
			this.m_pSpriteFrames.Clear();
			this.m_pSpriteFramesAliases.Clear();
		}

		public void removeSpriteFramesFromDictionary(Dictionary<string, object> dictionary)
		{
			Dictionary<string, object> item = (Dictionary<string, object>)dictionary["frames"];
			List<string> strs = new List<string>();
			foreach (string key in item.Keys)
			{
				if (!this.m_pSpriteFrames.ContainsKey(key))
				{
					continue;
				}
				strs.Remove(key);
			}
			foreach (string str in strs)
			{
				this.m_pSpriteFrames.Remove(str);
			}
		}

		public void removeSpriteFramesFromFile(string plist)
		{
			this.removeSpriteFramesFromDictionary(CCFileUtils.dictionaryWithContentsOfFile(CCFileUtils.fullPathFromRelativePath(plist)));
		}

		public void removeSpriteFramesFromTexture(CCTexture2D texture)
		{
			List<string> strs = new List<string>();
			foreach (string key in this.m_pSpriteFrames.Keys)
			{
				CCSpriteFrame item = this.m_pSpriteFrames[key];
				if (item == null || item.Texture.Name != texture.Name)
				{
					continue;
				}
				strs.Add(key);
			}
			foreach (string str in strs)
			{
				this.m_pSpriteFrames.Remove(str);
			}
		}

		public void removeUnusedSpriteFrames()
		{
		}

		public static CCSpriteFrameCache sharedSpriteFrameCache()
		{
			if (CCSpriteFrameCache.pSharedSpriteFrameCache == null)
			{
				CCSpriteFrameCache.pSharedSpriteFrameCache = new CCSpriteFrameCache();
				CCSpriteFrameCache.pSharedSpriteFrameCache.init();
			}
			return CCSpriteFrameCache.pSharedSpriteFrameCache;
		}

		public CCSpriteFrame spriteFrameByName(string pszName)
		{
			if (pszName == null)
			{
				return null;
			}
			CCSpriteFrame item = null;
			if (this.m_pSpriteFrames.ContainsKey(pszName))
			{
				item = this.m_pSpriteFrames[pszName];
			}
			if (item == null)
			{
				string str = null;
				if (this.m_pSpriteFramesAliases.ContainsKey(pszName))
				{
					str = this.m_pSpriteFramesAliases[pszName];
				}
				if (str != null)
				{
					if (this.m_pSpriteFrames.ContainsKey(str))
					{
						item = this.m_pSpriteFrames[str];
					}
					if (item == null)
					{
						CCLog.Log("cocos2d: CCSpriteFrameCahce: Frame '{0}' not found", new object[] { pszName });
					}
				}
			}
			return item;
		}

		private string valueForKey(string key, Dictionary<string, object> dict)
		{
			if (dict == null || !dict.Keys.Contains<string>(key))
			{
				return "";
			}
			string item = (string)dict[key];
			if (item == null)
			{
				return "";
			}
			return item;
		}
	}
}