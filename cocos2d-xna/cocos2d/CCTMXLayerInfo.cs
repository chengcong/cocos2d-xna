using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCTMXLayerInfo : CCObject
	{
		protected Dictionary<string, string> m_pProperties;

		public string m_sName;

		public CCSize m_tLayerSize;

		public int[] m_pTiles;

		public bool m_bVisible;

		public byte m_cOpacity;

		public bool m_bOwnTiles;

		public int m_uMinGID;

		public int m_uMaxGID;

		public CCPoint m_tOffset;

		public virtual Dictionary<string, string> Properties
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

		public CCTMXLayerInfo()
		{
			this.m_sName = "";
			this.m_pTiles = null;
			this.m_bOwnTiles = true;
			this.m_uMinGID = 100000;
			this.m_uMaxGID = 0;
			this.m_tOffset = new CCPoint(0f, 0f);
			this.m_pProperties = new Dictionary<string, string>();
		}
	}
}