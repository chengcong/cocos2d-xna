using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCTMXTiledMap : CCNode
	{
		protected CCSize m_tMapSize;

		protected CCSize m_tTileSize;

		protected int m_nMapOrientation;

		protected List<CCTMXObjectGroup> m_pObjectGroups;

		protected Dictionary<string, string> m_pProperties;

		protected Dictionary<int, Dictionary<string, string>> m_pTileProperties;

		protected Dictionary<string, CCTMXLayer> m_pTMXLayers;

		public int MapOrientation
		{
			get
			{
				return this.m_nMapOrientation;
			}
			set
			{
				this.m_nMapOrientation = value;
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

		public List<CCTMXObjectGroup> ObjectGroups
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

		public CCTMXTiledMap()
		{
		}

		public bool initWithTMXFile(string tmxFile)
		{
			this.contentSize = new CCSize(0f, 0f);
			CCTMXMapInfo cCTMXMapInfo = CCTMXMapInfo.formatWithTMXFile(tmxFile);
			if (cCTMXMapInfo == null)
			{
				return false;
			}
			this.m_tMapSize = cCTMXMapInfo.MapSize;
			this.m_tTileSize = cCTMXMapInfo.TileSize;
			this.m_nMapOrientation = cCTMXMapInfo.Orientation;
			this.ObjectGroups = cCTMXMapInfo.ObjectGroups;
			this.Properties = cCTMXMapInfo.Properties;
			this.m_pTileProperties = cCTMXMapInfo.TileProperties;
			int num = 0;
			List<CCTMXLayerInfo> layers = cCTMXMapInfo.Layers;
			if (layers != null && layers.Count > 0)
			{
				if (this.m_pTMXLayers == null)
				{
					this.m_pTMXLayers = new Dictionary<string, CCTMXLayer>();
				}
				for (int i = 0; i < layers.Count; i++)
				{
					CCTMXLayerInfo item = layers[i];
					if (item != null && item.m_bVisible)
					{
						CCTMXLayer cCTMXLayer = this.parseLayer(item, cCTMXMapInfo);
						this.addChild(cCTMXLayer, num, num);
						string layerName = cCTMXLayer.LayerName;
						this.m_pTMXLayers.Add(layerName, cCTMXLayer);
						CCSize cCSize = cCTMXLayer.contentSize;
						CCSize cCSize1 = this.contentSize;
						cCSize1.width = Math.Max(cCSize1.width, cCSize.width);
						cCSize1.height = Math.Max(cCSize1.height, cCSize.height);
						this.contentSize = cCSize1;
						num++;
					}
				}
			}
			return true;
		}

		public CCTMXLayer layerNamed(string layerName)
		{
			return this.m_pTMXLayers[layerName];
		}

		public CCTMXObjectGroup objectGroupNamed(string groupName)
		{
			if (this.m_pObjectGroups != null && this.m_pObjectGroups.Count > 0)
			{
				for (int i = 0; i < this.m_pObjectGroups.Count; i++)
				{
					CCTMXObjectGroup item = this.m_pObjectGroups[i];
					if (item != null && item.GroupName == groupName)
					{
						return item;
					}
				}
			}
			return null;
		}

		private CCTMXLayer parseLayer(CCTMXLayerInfo layerInfo, CCTMXMapInfo mapInfo)
		{
			CCTMXTilesetInfo cCTMXTilesetInfo = this.tilesetForLayer(layerInfo, mapInfo);
			CCTMXLayer cCTMXLayer = CCTMXLayer.layerWithTilesetInfo(cCTMXTilesetInfo, layerInfo, mapInfo);
			layerInfo.m_bOwnTiles = false;
			cCTMXLayer.setupTiles();
			return cCTMXLayer;
		}

		public Dictionary<string, string> propertiesForGID(int GID)
		{
			return this.m_pTileProperties[GID];
		}

		public string propertyNamed(string propertyName)
		{
			return this.m_pProperties[propertyName];
		}

		public static CCTMXTiledMap tiledMapWithTMXFile(string tmxFile)
		{
			CCTMXTiledMap cCTMXTiledMap = new CCTMXTiledMap();
			if (cCTMXTiledMap.initWithTMXFile(tmxFile))
			{
				return cCTMXTiledMap;
			}
			return null;
		}

		private CCTMXTilesetInfo tilesetForLayer(CCTMXLayerInfo layerInfo, CCTMXMapInfo mapInfo)
		{
			CCSize mTLayerSize = layerInfo.m_tLayerSize;
			List<CCTMXTilesetInfo> tilesets = mapInfo.Tilesets;
			if (tilesets != null && tilesets.Count > 0)
			{
				for (int i = 0; i < tilesets.Count; i++)
				{
					CCTMXTilesetInfo item = tilesets[i];
					if (item != null)
					{
						for (int j = 0; (float)j < mTLayerSize.height; j++)
						{
							for (int k = 0; (float)k < mTLayerSize.width; k++)
							{
								int num = (int)((float)k + mTLayerSize.width * (float)j);
								int mPTiles = layerInfo.m_pTiles[num];
								if (mPTiles != 0 && mPTiles >= item.m_uFirstGid)
								{
									mapInfo.Tilesets.Reverse();
									return item;
								}
							}
						}
					}
				}
			}
			CCLog.Log("cocos2d: Warning: TMX Layer '{0}' has no tiles", new object[] { layerInfo.m_sName });
			return null;
		}
	}
}