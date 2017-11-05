using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCTileMapAtlas : CCAtlasNode
	{
		private tImageTGA m_pTGAInfo;

		protected Dictionary<string, int> m_pPosToAtlasIndex;

		protected int m_nItemsToRender;

		public tImageTGA TGAInfo
		{
			get
			{
				return this.m_pTGAInfo;
			}
			set
			{
				this.m_pTGAInfo = value;
			}
		}

		public CCTileMapAtlas()
		{
		}

		private void calculateItemsToRender()
		{
			this.m_nItemsToRender = 0;
			for (int i = 0; i < this.m_pTGAInfo.width; i++)
			{
				for (int j = 0; j < this.m_pTGAInfo.height; j++)
				{
					ccColor3B _ccColor3B = new ccColor3B()
					{
						r = this.m_pTGAInfo.imageData[0],
						g = this.m_pTGAInfo.imageData[1],
						b = this.m_pTGAInfo.imageData[2]
					};
				}
			}
		}

		public bool initWithTileFile(string tile, string mapFile, int tileWidth, int tileHeight)
		{
			this.loadTGAfile(mapFile);
			this.calculateItemsToRender();
			if (!base.initWithTileFile(tile, tileWidth, tileHeight, this.m_nItemsToRender))
			{
				return false;
			}
			this.m_pPosToAtlasIndex = new Dictionary<string, int>();
			this.updateAtlasValues();
			this.contentSize = new CCSize((float)(this.m_pTGAInfo.width * this.m_uItemWidth), (float)(this.m_pTGAInfo.height * this.m_uItemHeight));
			return true;
		}

		private void loadTGAfile(string file)
		{
		}

		public void releaseMap()
		{
			this.m_pTGAInfo = null;
			this.m_pPosToAtlasIndex = null;
		}

		private void setTile(ccColor3B tile, ccGridSize position)
		{
		}

		public ccColor3B tileAt(ccGridSize position)
		{
			throw new NotImplementedException();
		}

		public static CCTileMapAtlas tileMapAtlasWithTileFile(string tile, string mapFile, int tileWidth, int tileHeight)
		{
			CCTileMapAtlas cCTileMapAtla = new CCTileMapAtlas();
			if (cCTileMapAtla.initWithTileFile(tile, mapFile, tileWidth, tileHeight))
			{
				return cCTileMapAtla;
			}
			return null;
		}

		private void updateAtlasValueAt(ccGridSize pos, ccColor3B value, int index)
		{
		}

		private new void updateAtlasValues()
		{
		}
	}
}