using ComponentAce.Compression.Libs.zlib;
using ICSharpCode.SharpZipLib.GZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cocos2d
{
	public class CCTMXMapInfo : CCObject, ICCSAXDelegator
	{
		protected int m_nOrientation;

		protected CCSize m_tMapSize;

		protected CCSize m_tTileSize;

		protected List<CCTMXLayerInfo> m_pLayers;

		protected List<CCTMXTilesetInfo> m_pTilesets;

		protected List<CCTMXObjectGroup> m_pObjectGroups;

		protected int m_nParentElement;

		protected int m_uParentGID;

		protected int m_nLayerAttribs;

		protected bool m_bStoringCharacters;

		protected Dictionary<string, string> m_pProperties;

		protected string m_sTMXFileName;

		protected byte[] m_sCurrentString;

		protected Dictionary<int, Dictionary<string, string>> m_pTileProperties;

		public byte[] CurrentString
		{
			get
			{
				return this.m_sCurrentString;
			}
			set
			{
				this.m_sCurrentString = value;
			}
		}

		public int LayerAttribs
		{
			get
			{
				return this.m_nLayerAttribs;
			}
			set
			{
				this.m_nLayerAttribs = value;
			}
		}

		public virtual List<CCTMXLayerInfo> Layers
		{
			get
			{
				return this.m_pLayers;
			}
			set
			{
				this.m_pLayers = value;
			}
		}

		public CCSize MapSize
		{
			get
			{
				return this.m_tMapSize;
			}
			set
			{
				this.m_tMapSize = value;
			}
		}

		public virtual List<CCTMXObjectGroup> ObjectGroups
		{
			get
			{
				return this.m_pObjectGroups;
			}
			set
			{
				this.m_pObjectGroups = value;
			}
		}

		public int Orientation
		{
			get
			{
				return this.m_nOrientation;
			}
			set
			{
				this.m_nOrientation = value;
			}
		}

		public int ParentElement
		{
			get
			{
				return this.m_nParentElement;
			}
			set
			{
				this.m_nParentElement = value;
			}
		}

		public int ParentGID
		{
			get
			{
				return this.m_uParentGID;
			}
			set
			{
				this.m_uParentGID = value;
			}
		}

		public Dictionary<string, string> Properties
		{
			get
			{
				return this.m_pProperties;
			}
			set
			{
				this.m_pProperties = value;
			}
		}

		public bool StoringCharacters
		{
			get
			{
				return this.m_bStoringCharacters;
			}
			set
			{
				this.m_bStoringCharacters = value;
			}
		}

		public Dictionary<int, Dictionary<string, string>> TileProperties
		{
			get
			{
				return this.m_pTileProperties;
			}
			set
			{
				this.m_pTileProperties = value;
			}
		}

		public virtual List<CCTMXTilesetInfo> Tilesets
		{
			get
			{
				return this.m_pTilesets;
			}
			set
			{
				this.m_pTilesets = value;
			}
		}

		public CCSize TileSize
		{
			get
			{
				return this.m_tTileSize;
			}
			set
			{
				this.m_tTileSize = value;
			}
		}

		public string TMXFileName
		{
			get
			{
				return this.m_sTMXFileName;
			}
			set
			{
				this.m_sTMXFileName = value;
			}
		}

		public CCTMXMapInfo()
		{
		}

		public void endElement(object ctx, string elementName)
		{
			CCTMXMapInfo cCTMXMapInfo = this;
			byte[] currentString = cCTMXMapInfo.CurrentString;
			if (!(elementName == "data") || (cCTMXMapInfo.LayerAttribs & 2) == 0)
			{
				if (elementName == "map")
				{
					cCTMXMapInfo.ParentElement = 0;
					return;
				}
				if (elementName == "layer")
				{
					cCTMXMapInfo.ParentElement = 0;
					return;
				}
				if (elementName == "objectgroup")
				{
					cCTMXMapInfo.ParentElement = 0;
					return;
				}
				if (elementName == "object")
				{
					cCTMXMapInfo.ParentElement = 0;
				}
				return;
			}
			cCTMXMapInfo.StoringCharacters = false;
			CCTMXLayerInfo cCTMXLayerInfo = cCTMXMapInfo.Layers.LastOrDefault<CCTMXLayerInfo>();
			if ((cCTMXMapInfo.LayerAttribs & 12) == 0)
			{
				for (int i = 0; i < (int)cCTMXLayerInfo.m_pTiles.Length; i++)
				{
					cCTMXLayerInfo.m_pTiles[i] = currentString[i * 4];
				}
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream(currentString, false))
				{
					if ((cCTMXMapInfo.LayerAttribs & 4) != 0)
					{
						using (GZipInputStream gZipInputStream = new GZipInputStream(memoryStream))
						{
							using (BinaryReader binaryReader = new BinaryReader(gZipInputStream))
							{
								for (int j = 0; j < (int)cCTMXLayerInfo.m_pTiles.Length; j++)
								{
									cCTMXLayerInfo.m_pTiles[j] = binaryReader.ReadInt32();
								}
							}
						}
					}
					if ((cCTMXMapInfo.LayerAttribs & 8) != 0)
					{
						using (ZInputStream zInputStream = new ZInputStream(memoryStream))
						{
							for (int k = 0; k < (int)cCTMXLayerInfo.m_pTiles.Length; k++)
							{
								cCTMXLayerInfo.m_pTiles[k] = zInputStream.Read();
								zInputStream.Read();
								zInputStream.Read();
								zInputStream.Read();
							}
						}
					}
				}
			}
			cCTMXMapInfo.CurrentString = null;
		}

		public static CCTMXMapInfo formatWithTMXFile(string tmxFile)
		{
			CCTMXMapInfo cCTMXMapInfo = new CCTMXMapInfo();
			if (cCTMXMapInfo.initWithTMXFile(tmxFile))
			{
				return cCTMXMapInfo;
			}
			return null;
		}

		public bool initWithTMXFile(string tmxFile)
		{
			this.m_pTilesets = new List<CCTMXTilesetInfo>();
			this.m_pLayers = new List<CCTMXLayerInfo>();
			this.m_sTMXFileName = CCFileUtils.fullPathFromRelativePath(tmxFile);
			this.m_pObjectGroups = new List<CCTMXObjectGroup>();
			this.m_pProperties = new Dictionary<string, string>();
			this.m_pTileProperties = new Dictionary<int, Dictionary<string, string>>();
			this.m_sCurrentString = null;
			this.m_bStoringCharacters = false;
			this.m_nLayerAttribs = 1;
			this.m_nParentElement = 0;
			return this.parseXMLFile(this.m_sTMXFileName);
		}

		public bool parseXMLFile(string xmlFilename)
		{
			CCSAXParser cCSAXParser = new CCSAXParser();
			if (!cCSAXParser.init("UTF-8"))
			{
				return false;
			}
			cCSAXParser.setDelegator(this);
			return cCSAXParser.parse(xmlFilename);
		}

		public void startElement(object ctx, string name, string[] atts)
		{
			CCTMXMapInfo mUFirstGid = this;
			string str = name;
			Dictionary<string, string> strs = new Dictionary<string, string>();
			if (atts != null && atts[0] != null)
			{
				for (int i = 0; i + 1 < (int)atts.Length; i = i + 2)
				{
					string str1 = atts[i];
					strs.Add(str1, atts[i + 1]);
				}
			}
			if (str == "map")
			{
				string item = strs["version"];
				if (item != "1.0")
				{
					CCLog.Log("cocos2d: TMXFormat: Unsupported TMX version: {0}", new object[] { item });
				}
				string item1 = strs["orientation"];
				if (item1 == "orthogonal")
				{
					mUFirstGid.Orientation = 0;
				}
				else if (item1 == "isometric")
				{
					mUFirstGid.Orientation = 2;
				}
				else if (item1 != "hexagonal")
				{
					object[] orientation = new object[] { mUFirstGid.Orientation };
					CCLog.Log("cocos2d: TMXFomat: Unsupported orientation: {0}", orientation);
				}
				else
				{
					mUFirstGid.Orientation = 1;
				}
				CCSize cCSize = new CCSize()
				{
					width = ccUtils.ccParseFloat(strs["width"]),
					height = ccUtils.ccParseFloat(strs["height"])
				};
				mUFirstGid.MapSize = cCSize;
				CCSize cCSize1 = new CCSize()
				{
					width = ccUtils.ccParseFloat(strs["tilewidth"]),
					height = ccUtils.ccParseFloat(strs["tileheight"])
				};
				mUFirstGid.TileSize = cCSize1;
				mUFirstGid.ParentElement = 1;
			}
			else if (str == "tileset")
			{
				if (!strs.Keys.Contains<string>("source"))
				{
					CCTMXTilesetInfo cCTMXTilesetInfo = new CCTMXTilesetInfo()
					{
						m_sName = strs["name"],
						m_uFirstGid = ccUtils.ccParseInt(strs["firstgid"])
					};
					if (strs.Keys.Contains<string>("spacing"))
					{
						cCTMXTilesetInfo.m_uSpacing = ccUtils.ccParseInt(strs["spacing"]);
					}
					if (strs.Keys.Contains<string>("margin"))
					{
						cCTMXTilesetInfo.m_uMargin = ccUtils.ccParseInt(strs["margin"]);
					}
					CCSize cCSize2 = new CCSize()
					{
						width = ccUtils.ccParseFloat(strs["tilewidth"]),
						height = ccUtils.ccParseFloat(strs["tileheight"])
					};
					cCTMXTilesetInfo.m_tTileSize = cCSize2;
					mUFirstGid.Tilesets.Add(cCTMXTilesetInfo);
				}
				else
				{
					string item2 = strs["source"];
					item2 = CCFileUtils.fullPathFromRelativeFile(item2, mUFirstGid.TMXFileName);
					mUFirstGid.parseXMLFile(item2);
				}
			}
			else if (str == "tile")
			{
				CCTMXTilesetInfo cCTMXTilesetInfo1 = mUFirstGid.Tilesets.LastOrDefault<CCTMXTilesetInfo>();
				Dictionary<string, string> strs1 = new Dictionary<string, string>();
				mUFirstGid.ParentGID = cCTMXTilesetInfo1.m_uFirstGid + ccUtils.ccParseInt(strs["id"]);
				mUFirstGid.TileProperties.Add(mUFirstGid.ParentGID, strs1);
				mUFirstGid.ParentElement = 5;
			}
			else if (str == "layer")
			{
				CCTMXLayerInfo cCTMXLayerInfo = new CCTMXLayerInfo()
				{
					m_sName = strs["name"]
				};
				CCSize cCSize3 = new CCSize()
				{
					width = ccUtils.ccParseFloat(strs["width"]),
					height = ccUtils.ccParseFloat(strs["height"])
				};
				cCTMXLayerInfo.m_tLayerSize = cCSize3;
				cCTMXLayerInfo.m_pTiles = new int[(int)cCSize3.width * (int)cCSize3.height];
				if (!strs.Keys.Contains<string>("visible"))
				{
					cCTMXLayerInfo.m_bVisible = true;
				}
				else
				{
					cCTMXLayerInfo.m_bVisible = !(strs["visible"] == "0");
				}
				if (!strs.Keys.Contains<string>("opacity"))
				{
					cCTMXLayerInfo.m_cOpacity = 255;
				}
				else
				{
					string str2 = strs["opacity"];
					cCTMXLayerInfo.m_cOpacity = (byte)(255f * ccUtils.ccParseFloat(str2));
				}
				float single = (strs.Keys.Contains<string>("x") ? ccUtils.ccParseFloat(strs["x"]) : 0f);
				cCTMXLayerInfo.m_tOffset = new CCPoint(single, (strs.Keys.Contains<string>("y") ? ccUtils.ccParseFloat(strs["y"]) : 0f));
				mUFirstGid.Layers.Add(cCTMXLayerInfo);
				mUFirstGid.ParentElement = 2;
			}
			else if (str == "objectgroup")
			{
				CCTMXObjectGroup cCTMXObjectGroup = new CCTMXObjectGroup()
				{
					GroupName = strs["name"]
				};
				CCPoint cCPoint = new CCPoint();
				if (strs.ContainsKey("x"))
				{
					cCPoint.x = ccUtils.ccParseFloat(strs["x"]) * mUFirstGid.TileSize.width;
				}
				if (strs.ContainsKey("y"))
				{
					cCPoint.y = ccUtils.ccParseFloat(strs["y"]) * mUFirstGid.TileSize.height;
				}
				cCTMXObjectGroup.PositionOffset = cCPoint;
				mUFirstGid.ObjectGroups.Add(cCTMXObjectGroup);
				mUFirstGid.ParentElement = 3;
			}
			else if (str == "image")
			{
				CCTMXTilesetInfo cCTMXTilesetInfo2 = mUFirstGid.Tilesets.LastOrDefault<CCTMXTilesetInfo>();
				string item3 = strs["source"];
				cCTMXTilesetInfo2.m_sSourceImage = CCFileUtils.fullPathFromRelativeFile(item3, mUFirstGid.TMXFileName);
			}
			else if (str == "data")
			{
				string str3 = (strs.ContainsKey("encoding") ? strs["encoding"] : "");
				string str4 = (strs.ContainsKey("compression") ? strs["compression"] : "");
				if (str3 == "base64")
				{
					mUFirstGid.LayerAttribs = mUFirstGid.LayerAttribs | 2;
					mUFirstGid.StoringCharacters = true;
					if (str4 == "gzip")
					{
						mUFirstGid.LayerAttribs = mUFirstGid.LayerAttribs | 4;
					}
					else if (str4 == "zlib")
					{
						mUFirstGid.LayerAttribs = mUFirstGid.LayerAttribs | 8;
					}
				}
			}
			else if (str == "object")
			{
				CCTMXObjectGroup cCTMXObjectGroup1 = mUFirstGid.ObjectGroups.LastOrDefault<CCTMXObjectGroup>();
				Dictionary<string, string> strs2 = new Dictionary<string, string>();
				string str5 = "name";
				strs2.Add(str5, (strs.ContainsKey("name") ? strs["name"] : ""));
				str5 = "type";
				strs2.Add(str5, (strs.ContainsKey("type") ? strs["type"] : ""));
				int num = ccUtils.ccParseInt(strs["x"]) + (int)cCTMXObjectGroup1.PositionOffset.x;
				str5 = "x";
				strs2.Add(str5, num.ToString());
				int mapSize = ccUtils.ccParseInt(strs["y"]) + (int)cCTMXObjectGroup1.PositionOffset.y;
				mapSize = (int)(mUFirstGid.MapSize.height * mUFirstGid.TileSize.height) - mapSize - (strs.ContainsKey("height") ? ccUtils.ccParseInt(strs["height"]) : 0);
				str5 = "y";
				strs2.Add(str5, mapSize.ToString());
				str5 = "width";
				strs2.Add(str5, (strs.ContainsKey("width") ? strs["width"] : ""));
				str5 = "height";
				strs2.Add(str5, (strs.ContainsKey("height") ? strs["height"] : ""));
				cCTMXObjectGroup1.Objects.Add(strs2);
				mUFirstGid.ParentElement = 4;
			}
			else if (str == "property")
			{
				if (mUFirstGid.ParentElement == 0)
				{
					object[] objArray = new object[] { strs["name"], strs["value"] };
					CCLog.Log("TMX tile map: Parent element is unsupported. Cannot add property named '{0}' with value '{1}'", objArray);
				}
				else if (mUFirstGid.ParentElement == 1)
				{
					string item4 = strs["value"];
					string item5 = strs["name"];
					mUFirstGid.Properties.Add(item5, item4);
				}
				else if (mUFirstGid.ParentElement == 2)
				{
					CCTMXLayerInfo cCTMXLayerInfo1 = mUFirstGid.Layers.LastOrDefault<CCTMXLayerInfo>();
					string item6 = strs["value"];
					string str6 = strs["name"];
					cCTMXLayerInfo1.Properties.Add(str6, item6);
				}
				else if (mUFirstGid.ParentElement == 3)
				{
					CCTMXObjectGroup cCTMXObjectGroup2 = mUFirstGid.ObjectGroups.LastOrDefault<CCTMXObjectGroup>();
					string item7 = strs["value"];
					string str7 = strs["name"];
					cCTMXObjectGroup2.Properties.Add(str7, item7);
				}
				else if (mUFirstGid.ParentElement == 4)
				{
					CCTMXObjectGroup cCTMXObjectGroup3 = mUFirstGid.ObjectGroups.LastOrDefault<CCTMXObjectGroup>();
					Dictionary<string, string> strs3 = cCTMXObjectGroup3.Objects.LastOrDefault<Dictionary<string, string>>();
					strs3.Add(strs["name"], strs["value"]);
				}
				else if (mUFirstGid.ParentElement == 5)
				{
					Dictionary<string, string> strs4 = mUFirstGid.TileProperties[mUFirstGid.ParentGID];
					strs4.Add(strs["name"], strs["value"]);
				}
			}
			if (strs != null)
			{
				strs = null;
			}
		}

		public void textHandler(object ctx, byte[] ch, int len)
		{
			CCTMXMapInfo cCTMXMapInfo = this;
			if (cCTMXMapInfo.StoringCharacters)
			{
				cCTMXMapInfo.CurrentString = ch;
			}
		}
	}
}