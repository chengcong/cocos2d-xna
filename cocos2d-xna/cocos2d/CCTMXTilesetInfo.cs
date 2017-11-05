using System;

namespace cocos2d
{
	public class CCTMXTilesetInfo : CCObject
	{
		public string m_sName;

		public int m_uFirstGid;

		public CCSize m_tTileSize;

		public int m_uSpacing;

		public int m_uMargin;

		public string m_sSourceImage;

		public CCSize m_tImageSize;

		public CCTMXTilesetInfo()
		{
		}

		public CCRect rectForGID(int gid)
		{
			CCRect cCRect = new CCRect()
			{
				size = this.m_tTileSize
			};
			gid = gid - this.m_uFirstGid;
			int mTImageSize = (int)((this.m_tImageSize.width - (float)(this.m_uMargin * 2) + (float)this.m_uSpacing) / (this.m_tTileSize.width + (float)this.m_uSpacing));
			cCRect.origin.x = (float)(gid % mTImageSize) * (this.m_tTileSize.width + (float)this.m_uSpacing) + (float)this.m_uMargin;
			cCRect.origin.y = (float)(gid / mTImageSize) * (this.m_tTileSize.height + (float)this.m_uSpacing) + (float)this.m_uMargin;
			return cCRect;
		}
	}
}