using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCTMXLayer : CCSpriteBatchNode
	{
		protected CCSize m_tLayerSize;

		protected CCSize m_tMapTileSize;

		protected int[] m_pTiles;

		protected CCTMXTilesetInfo m_pTileSet;

		protected CCTMXOrientatio m_uLayerOrientation;

		protected Dictionary<string, string> m_pProperties;

		protected string m_sLayerName;

		protected byte m_cOpacity;

		protected int m_uMinGID;

		protected int m_uMaxGID;

		protected int m_nVertexZvalue;

		protected bool m_bUseAutomaticVertexZ;

		protected float m_fAlphaFuncValue;

		protected CCSprite m_pReusedTile;

		protected ccCArray m_pAtlasIndexArray;

		protected float m_fContentScaleFactor;

		public string LayerName
		{
			get
			{
				return this.m_sLayerName;
			}
			set
			{
				this.m_sLayerName = value;
			}
		}

		public CCTMXOrientatio LayerOrientation
		{
			get
			{
				return this.m_uLayerOrientation;
			}
			set
			{
				this.m_uLayerOrientation = value;
			}
		}

		public CCSize LayerSize
		{
			get
			{
				return this.m_tLayerSize;
			}
			set
			{
				this.m_tLayerSize = value;
			}
		}

		public CCSize MapTileSize
		{
			get
			{
				return this.m_tMapTileSize;
			}
			set
			{
				this.m_tMapTileSize = value;
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

		public int[] Tiles
		{
			get
			{
				return this.m_pTiles;
			}
			set
			{
				this.m_pTiles = value;
			}
		}

		public virtual CCTMXTilesetInfo TileSet
		{
			get
			{
				return this.m_pTileSet;
			}
			set
			{
				this.m_pTileSet = value;
			}
		}

		public CCTMXLayer()
		{
		}

		public override void addChild(CCNode child, int zOrder, int tag)
		{
			base.addChild(child, zOrder, tag);
		}

		private CCSprite appendTileForGID(int gid, CCPoint pos)
		{
			CCRect cCRect = this.m_pTileSet.rectForGID(gid);
			cCRect = new CCRect(cCRect.origin.x / this.m_fContentScaleFactor, cCRect.origin.y / this.m_fContentScaleFactor, cCRect.size.width / this.m_fContentScaleFactor, cCRect.size.height / this.m_fContentScaleFactor);
			int num = (int)(pos.x + pos.y * this.m_tLayerSize.width);
			if (this.m_pReusedTile != null)
			{
				this.m_pReusedTile = new CCSprite();
				this.m_pReusedTile.initWithBatchNode(this, cCRect);
			}
			else
			{
				this.m_pReusedTile = new CCSprite();
				this.m_pReusedTile.initWithBatchNode(this, cCRect);
			}
			this.m_pReusedTile.position = this.positionAt(pos);
			this.m_pReusedTile.vertexZ = (float)this.vertexZForPos(pos);
			this.m_pReusedTile.anchorPoint = new CCPoint(0f, 0f);
			this.m_pReusedTile.Opacity = 255;
			int mPAtlasIndexArray = this.m_pAtlasIndexArray.num;
			base.addQuadFromSprite(this.m_pReusedTile, mPAtlasIndexArray);
			ccCArray.ccCArrayInsertValueAtIndex(this.m_pAtlasIndexArray, num, mPAtlasIndexArray);
			return this.m_pReusedTile;
		}

		private int atlasIndexForExistantZ(int z)
		{
			return Array.IndexOf<int>(this.m_pAtlasIndexArray.arr, z);
		}

		private int atlasIndexForNewZ(int z)
		{
			int num = 0;
			num = 0;
			while (num < this.m_pAtlasIndexArray.num && z >= this.m_pAtlasIndexArray.arr[num])
			{
				num++;
			}
			return num;
		}

		private CCPoint calculateLayerOffset(CCPoint pos)
		{
			CCPoint cCPoint = new CCPoint(0f, 0f);
			switch (this.m_uLayerOrientation)
			{
				case CCTMXOrientatio.CCTMXOrientationOrtho:
				{
					cCPoint = new CCPoint(pos.x * this.m_tMapTileSize.width, -pos.y * this.m_tMapTileSize.height);
					return cCPoint;
				}
				case CCTMXOrientatio.CCTMXOrientationHex:
				{
					return cCPoint;
				}
				case CCTMXOrientatio.CCTMXOrientationIso:
				{
					cCPoint = new CCPoint(this.m_tMapTileSize.width / 2f * (pos.x - pos.y), this.m_tMapTileSize.height / 2f * (-pos.x - pos.y));
					return cCPoint;
				}
				default:
				{
					return cCPoint;
				}
			}
		}

		private int compareInts(object a, object b)
		{
			return (int)a - (int)b;
		}

		public bool initWithTilesetInfo(CCTMXTilesetInfo tilesetInfo, CCTMXLayerInfo layerInfo, CCTMXMapInfo mapInfo)
		{
			CCSize mTLayerSize = layerInfo.m_tLayerSize;
			float single = mTLayerSize.width * mTLayerSize.height;
			float single1 = single * 0.35f + 1f;
			CCTexture2D cCTexture2D = null;
			if (tilesetInfo != null)
			{
				cCTexture2D = CCTextureCache.sharedTextureCache().addImage(tilesetInfo.m_sSourceImage);
			}
			if (!base.initWithTexture(cCTexture2D, (int)single1))
			{
				return false;
			}
			this.m_sLayerName = layerInfo.m_sName;
			this.m_tLayerSize = layerInfo.m_tLayerSize;
			this.m_pTiles = layerInfo.m_pTiles;
			this.m_uMinGID = layerInfo.m_uMinGID;
			this.m_uMaxGID = layerInfo.m_uMaxGID;
			this.m_cOpacity = layerInfo.m_cOpacity;
			this.m_pProperties = layerInfo.Properties;
			this.m_fContentScaleFactor = CCDirector.sharedDirector().ContentScaleFactor;
			this.m_pTileSet = tilesetInfo;
			this.m_tMapTileSize = mapInfo.TileSize;
			this.m_uLayerOrientation = (CCTMXOrientatio)mapInfo.Orientation;
			this.position = this.calculateLayerOffset(layerInfo.m_tOffset);
			this.m_pAtlasIndexArray = ccCArray.ccCArrayNew((int)single);
			base.contentSizeInPixels = new CCSize(this.m_tLayerSize.width * this.m_tMapTileSize.width, this.m_tLayerSize.height * this.m_tMapTileSize.height);
			CCSize mTMapTileSize = this.m_tMapTileSize;
			mTMapTileSize.width = mTMapTileSize.width / this.m_fContentScaleFactor;
			CCSize mFContentScaleFactor = this.m_tMapTileSize;
			mFContentScaleFactor.height = mFContentScaleFactor.height / this.m_fContentScaleFactor;
			this.m_bUseAutomaticVertexZ = false;
			this.m_nVertexZvalue = 0;
			this.m_fAlphaFuncValue = 0f;
			return true;
		}

		private CCSprite insertTileForGID(int gid, CCPoint pos)
		{
			CCRect cCRect = this.m_pTileSet.rectForGID(gid);
			cCRect = new CCRect(cCRect.origin.x / this.m_fContentScaleFactor, cCRect.origin.y / this.m_fContentScaleFactor, cCRect.size.width / this.m_fContentScaleFactor, cCRect.size.height / this.m_fContentScaleFactor);
			int num = (int)(pos.x + pos.y * this.m_tLayerSize.width);
			if (this.m_pReusedTile != null)
			{
				this.m_pReusedTile = new CCSprite();
				this.m_pReusedTile.initWithBatchNode(this, cCRect);
			}
			else
			{
				this.m_pReusedTile = new CCSprite();
				this.m_pReusedTile.initWithBatchNode(this, cCRect);
			}
			this.m_pReusedTile.positionInPixels = this.positionAt(pos);
			this.m_pReusedTile.vertexZ = (float)this.vertexZForPos(pos);
			this.m_pReusedTile.anchorPoint = new CCPoint(0f, 0f);
			this.m_pReusedTile.Opacity = this.m_cOpacity;
			int num1 = this.atlasIndexForNewZ(num);
			base.addQuadFromSprite(this.m_pReusedTile, num1);
			ccCArray.ccCArrayInsertValueAtIndex(this.m_pAtlasIndexArray, num, num1);
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				CCObject cCObject = null;
				for (int i = 0; i < this.m_pChildren.Count; i++)
				{
					CCSprite cCSprite = (CCSprite)cCObject;
					if (cCSprite != null)
					{
						int num2 = cCSprite.atlasIndex;
						if (num2 >= num1)
						{
							cCSprite.atlasIndex = num2 + 1;
						}
					}
				}
			}
			this.m_pTiles[num] = gid;
			return this.m_pReusedTile;
		}

		public static CCTMXLayer layerWithTilesetInfo(CCTMXTilesetInfo tilesetInfo, CCTMXLayerInfo layerInfo, CCTMXMapInfo mapInfo)
		{
			CCTMXLayer cCTMXLayer = new CCTMXLayer();
			if (cCTMXLayer.initWithTilesetInfo(tilesetInfo, layerInfo, mapInfo))
			{
				return cCTMXLayer;
			}
			return null;
		}

		private void parseInternalProperties()
		{
			string str = this.propertyNamed("cc_vertexz");
			if (str != null)
			{
				if (str != "automatic")
				{
					this.m_nVertexZvalue = ccUtils.ccParseInt(str);
				}
				else
				{
					this.m_bUseAutomaticVertexZ = true;
				}
			}
			string str1 = this.propertyNamed("cc_alpha_func");
			if (str1 != null)
			{
				this.m_fAlphaFuncValue = ccUtils.ccParseFloat(str1);
			}
		}

		public CCPoint positionAt(CCPoint pos)
		{
			CCPoint cCPoint = new CCPoint(0f, 0f);
			switch (this.m_uLayerOrientation)
			{
				case CCTMXOrientatio.CCTMXOrientationOrtho:
				{
					cCPoint = this.positionForOrthoAt(pos);
					break;
				}
				case CCTMXOrientatio.CCTMXOrientationHex:
				{
					cCPoint = this.positionForHexAt(pos);
					break;
				}
				case CCTMXOrientatio.CCTMXOrientationIso:
				{
					cCPoint = this.positionForIsoAt(pos);
					break;
				}
			}
			return cCPoint;
		}

		private CCPoint positionForHexAt(CCPoint pos)
		{
			float mTMapTileSize = 0f;
			if ((int)pos.x % 2 == 1)
			{
				mTMapTileSize = -this.m_tMapTileSize.height / 2f;
			}
			CCPoint cCPoint = new CCPoint(pos.x * this.m_tMapTileSize.width * 3f / 4f, (this.m_tLayerSize.height - pos.y - 1f) * this.m_tMapTileSize.height + mTMapTileSize);
			return cCPoint;
		}

		private CCPoint positionForIsoAt(CCPoint pos)
		{
			CCPoint cCPoint = new CCPoint(this.m_tMapTileSize.width / 2f * (this.m_tLayerSize.width + pos.x - pos.y - 1f), this.m_tMapTileSize.height / 2f * (this.m_tLayerSize.height * 2f - pos.x - pos.y - 2f));
			return cCPoint;
		}

		private CCPoint positionForOrthoAt(CCPoint pos)
		{
			CCPoint cCPoint = new CCPoint(pos.x * this.m_tMapTileSize.width, (this.m_tLayerSize.height - pos.y - 1f) * this.m_tMapTileSize.height);
			return cCPoint;
		}

		public string propertyNamed(string propertyName)
		{
			return this.m_pProperties[propertyName];
		}

		public void releaseMap()
		{
			if (this.m_pTiles != null)
			{
				this.m_pTiles = null;
			}
			if (this.m_pAtlasIndexArray != null)
			{
				this.m_pAtlasIndexArray = null;
			}
		}

		public override void removeChild(CCNode child, bool cleanup)
		{
			CCSprite cCSprite = child as CCSprite;
			if (cCSprite == null)
			{
				return;
			}
			int num = cCSprite.atlasIndex;
			int mPAtlasIndexArray = this.m_pAtlasIndexArray.arr[num];
			this.m_pTiles[mPAtlasIndexArray] = 0;
			ccCArray.ccCArrayRemoveValueAtIndex(this.m_pAtlasIndexArray, num);
		}

		public void removeTileAt(CCPoint pos)
		{
			if (this.tileGIDAt(pos) != 0)
			{
				int num = (int)(pos.x + pos.y * this.m_tLayerSize.width);
				int num1 = this.atlasIndexForExistantZ(num);
				this.m_pTiles[num] = 0;
				ccCArray.ccCArrayRemoveValueAtIndex(this.m_pAtlasIndexArray, num1);
				CCSprite childByTag = (CCSprite)base.getChildByTag(num);
				if (childByTag != null)
				{
					base.removeChild(childByTag, true);
					return;
				}
				this.m_pobTextureAtlas.removeQuadAtIndex(num1);
				if (this.m_pChildren != null && this.m_pChildren.Count > 0)
				{
					CCObject cCObject = null;
					for (int i = 0; i < this.m_pChildren.Count; i++)
					{
						CCSprite cCSprite = (CCSprite)cCObject;
						if (cCSprite != null)
						{
							int num2 = cCSprite.atlasIndex;
							if (num2 >= num1)
							{
								cCSprite.atlasIndex = num2 - 1;
							}
						}
					}
				}
			}
		}

		public void setTileGID(int gid, CCPoint pos)
		{
			int num = this.tileGIDAt(pos);
			if (num != gid)
			{
				if (gid == 0)
				{
					this.removeTileAt(pos);
					return;
				}
				if (num == 0)
				{
					this.insertTileForGID(gid, pos);
					return;
				}
				int num1 = (int)(pos.x + pos.y * this.m_tLayerSize.width);
				CCSprite childByTag = (CCSprite)base.getChildByTag(num1);
				if (childByTag != null)
				{
					CCRect cCRect = this.m_pTileSet.rectForGID(gid);
					cCRect = new CCRect(cCRect.origin.x / this.m_fContentScaleFactor, cCRect.origin.y / this.m_fContentScaleFactor, cCRect.size.width / this.m_fContentScaleFactor, cCRect.size.height / this.m_fContentScaleFactor);
					childByTag.setTextureRectInPixels(cCRect, false, cCRect.size);
					this.m_pTiles[num1] = gid;
					return;
				}
				this.updateTileForGID(gid, pos);
			}
		}

		public void setupTiles()
		{
			this.m_pTileSet.m_tImageSize = this.m_pobTextureAtlas.Texture.ContentSizeInPixels;
			for (int i = 0; (float)i < this.m_tLayerSize.height; i++)
			{
				for (int j = 0; (float)j < this.m_tLayerSize.width; j++)
				{
					int mTLayerSize = (int)((float)j + this.m_tLayerSize.width * (float)i);
					int mPTiles = this.m_pTiles[mTLayerSize];
					if (mPTiles != 0)
					{
						this.appendTileForGID(mPTiles, new CCPoint((float)j, (float)i));
						this.m_uMinGID = Math.Min(mPTiles, this.m_uMinGID);
						this.m_uMaxGID = Math.Max(mPTiles, this.m_uMaxGID);
					}
				}
			}
		}

		public CCSprite tileAt(CCPoint pos)
		{
			CCSprite childByTag = null;
			int num = this.tileGIDAt(pos);
			if (num != 0)
			{
				int num1 = (int)(pos.x + pos.y * this.m_tLayerSize.width);
				childByTag = (CCSprite)base.getChildByTag(num1);
				if (childByTag == null)
				{
					CCRect cCRect = this.m_pTileSet.rectForGID(num);
					cCRect = new CCRect(cCRect.origin.x / this.m_fContentScaleFactor, cCRect.origin.y / this.m_fContentScaleFactor, cCRect.size.width / this.m_fContentScaleFactor, cCRect.size.height / this.m_fContentScaleFactor);
					childByTag = new CCSprite();
					childByTag.initWithBatchNode(this, cCRect);
					childByTag.position = this.positionAt(pos);
					childByTag.vertexZ = (float)this.vertexZForPos(pos);
					childByTag.anchorPoint = new CCPoint(0f, 0f);
					childByTag.Opacity = this.m_cOpacity;
					int num2 = this.atlasIndexForExistantZ(num1);
					base.addSpriteWithoutQuad(childByTag, num2, num1);
				}
			}
			return childByTag;
		}

		public int tileGIDAt(CCPoint pos)
		{
			int num = (int)(pos.x + pos.y * this.m_tLayerSize.width);
			return this.m_pTiles[num];
		}

		private CCSprite updateTileForGID(int gid, CCPoint pos)
		{
			CCRect cCRect = this.m_pTileSet.rectForGID(gid);
			cCRect = new CCRect(cCRect.origin.x / this.m_fContentScaleFactor, cCRect.origin.y / this.m_fContentScaleFactor, cCRect.size.width / this.m_fContentScaleFactor, cCRect.size.height / this.m_fContentScaleFactor);
			int num = (int)(pos.x + pos.y * this.m_tLayerSize.width);
			if (this.m_pReusedTile != null)
			{
				this.m_pReusedTile = new CCSprite();
				this.m_pReusedTile.initWithBatchNode(this, cCRect);
			}
			else
			{
				this.m_pReusedTile = new CCSprite();
				this.m_pReusedTile.initWithBatchNode(this, cCRect);
			}
			this.m_pReusedTile.positionInPixels = this.positionAt(pos);
			this.m_pReusedTile.vertexZ = (float)this.vertexZForPos(pos);
			this.m_pReusedTile.anchorPoint = new CCPoint(0f, 0f);
			this.m_pReusedTile.Opacity = this.m_cOpacity;
			int num1 = this.atlasIndexForExistantZ(num);
			this.m_pReusedTile.atlasIndex = num1;
			this.m_pReusedTile.dirty = true;
			this.m_pReusedTile.updateTransform();
			this.m_pTiles[num] = gid;
			return this.m_pReusedTile;
		}

		private int vertexZForPos(CCPoint pos)
		{
			int mNVertexZvalue = 0;
			uint mTLayerSize = 0;
			if (!this.m_bUseAutomaticVertexZ)
			{
				mNVertexZvalue = this.m_nVertexZvalue;
			}
			else
			{
				switch (this.m_uLayerOrientation)
				{
					case CCTMXOrientatio.CCTMXOrientationOrtho:
					{
						mNVertexZvalue = (int)(-(this.m_tLayerSize.height - pos.y));
						break;
					}
					case CCTMXOrientatio.CCTMXOrientationIso:
					{
						mTLayerSize = (uint)(this.m_tLayerSize.width + this.m_tLayerSize.height);
						mNVertexZvalue = (int)(-((float)((float)mTLayerSize) - (pos.x + pos.y)));
						break;
					}
				}
			}
			return mNVertexZvalue;
		}
	}
}