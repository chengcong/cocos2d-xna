using System;

namespace cocos2d
{
	public class CCAtlasNode : CCNode, ICCRGBAProtocol, ICCTextureProtocol, ICCBlendProtocol
	{
		protected int m_uItemsPerRow;

		protected int m_uItemsPerColumn;

		protected int m_uItemWidth;

		protected int m_uItemHeight;

		protected ccColor3B m_tColorUnmodified;

		protected CCTextureAtlas m_pTextureAtlas;

		protected bool m_bIsOpacityModifyRGB;

		protected ccBlendFunc m_tBlendFunc;

		protected byte m_cOpacity;

		protected ccColor3B m_tColor;

		protected int m_uQuadsToDraw;

		public ccBlendFunc BlendFunc
		{
			get
			{
				return this.m_tBlendFunc;
			}
			set
			{
				this.m_tBlendFunc = value;
			}
		}

		public bool ccIsOpacityModifyRGB
		{
			get
			{
				return this.m_bIsOpacityModifyRGB;
			}
			set
			{
				ccColor3B mTColor = this.m_tColor;
				this.m_bIsOpacityModifyRGB = value;
				this.m_tColor = mTColor;
			}
		}

		public int ccQuadsToDraw
		{
			get
			{
				return this.m_uQuadsToDraw;
			}
			set
			{
				this.m_uQuadsToDraw = value;
			}
		}

		public CCTextureAtlas ccTextureAtlas
		{
			get
			{
				return this.m_pTextureAtlas;
			}
			set
			{
				this.m_pTextureAtlas = value;
			}
		}

		public ccColor3B Color
		{
			get
			{
				if (this.m_bIsOpacityModifyRGB)
				{
					return this.m_tColorUnmodified;
				}
				return this.m_tColor;
			}
			set
			{
				this.m_tColor = new ccColor3B(value.r, value.g, value.b);
				this.m_tColorUnmodified = new ccColor3B(value.r, value.g, value.b);
				if (this.m_bIsOpacityModifyRGB)
				{
					this.m_tColor.r = (byte)(value.r * this.m_cOpacity / 255);
					this.m_tColor.g = (byte)(value.g * this.m_cOpacity / 255);
					this.m_tColor.b = (byte)(value.b * this.m_cOpacity / 255);
				}
				this.updateAtlasValues();
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
				ccColor3B mTColor = this.m_tColor;
				this.m_bIsOpacityModifyRGB = value;
				this.m_tColor = mTColor;
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
				if (this.m_bIsOpacityModifyRGB)
				{
					this.Color = this.m_tColorUnmodified;
				}
			}
		}

		public int QuadsToDraw
		{
			get
			{
				return this.m_uQuadsToDraw;
			}
			set
			{
				this.m_uQuadsToDraw = value;
			}
		}

		public virtual CCTexture2D Texture
		{
			get
			{
				return this.m_pTextureAtlas.Texture;
			}
			set
			{
				this.m_pTextureAtlas.Texture = value;
				this.updateBlendFunc();
				this.updateOpacityModifyRGB();
			}
		}

		public CCTextureAtlas TextureAtlas
		{
			get
			{
				return this.m_pTextureAtlas;
			}
			set
			{
				this.m_pTextureAtlas = value;
			}
		}

		public CCAtlasNode()
		{
			this.m_tBlendFunc = new ccBlendFunc();
		}

		public static CCAtlasNode atlasWithTileFile(string tile, int tileWidth, int tileHeight, int itemsToRender)
		{
			CCAtlasNode cCAtlasNode = new CCAtlasNode();
			if (cCAtlasNode.initWithTileFile(tile, tileWidth, tileHeight, itemsToRender))
			{
				return cCAtlasNode;
			}
			return null;
		}

		private void calculateMaxItems()
		{
			CCSize contentSizeInPixels = this.m_pTextureAtlas.Texture.ContentSizeInPixels;
			this.m_uItemsPerColumn = (int)(contentSizeInPixels.height / (float)this.m_uItemHeight);
			this.m_uItemsPerRow = (int)(contentSizeInPixels.width / (float)this.m_uItemWidth);
		}

		public virtual ICCRGBAProtocol convertToRGBAProtocol()
		{
			return this;
		}

		public override void draw()
		{
			base.draw();
			this.m_pTextureAtlas.drawNumberOfQuads(this.m_uQuadsToDraw, 0);
		}

		public bool initWithTileFile(string tile, int tileWidth, int tileHeight, int itemsToRender)
		{
			this.m_uItemWidth = tileWidth;
			this.m_uItemHeight = tileHeight;
			this.m_cOpacity = 255;
			ccColor3B _ccColor3B = ccTypes.ccWHITE;
			ccColor3B _ccColor3B1 = _ccColor3B;
			this.m_tColorUnmodified = _ccColor3B;
			this.m_tColor = _ccColor3B1;
			this.m_bIsOpacityModifyRGB = true;
			this.m_tBlendFunc.src = ccMacros.CC_BLEND_SRC;
			this.m_tBlendFunc.dst = ccMacros.CC_BLEND_DST;
			this.m_pTextureAtlas = new CCTextureAtlas();
			this.m_pTextureAtlas.initWithFile(tile, itemsToRender);
			if (this.m_pTextureAtlas == null)
			{
				CCLog.Log("cocos2d: Could not initialize CCAtlasNode. Invalid Texture.");
				return false;
			}
			this.updateBlendFunc();
			this.updateOpacityModifyRGB();
			this.calculateMaxItems();
			this.m_uQuadsToDraw = itemsToRender;
			return true;
		}

		public virtual void updateAtlasValues()
		{
		}

		private void updateBlendFunc()
		{
			if (!this.m_pTextureAtlas.Texture.HasPremultipliedAlpha)
			{
				this.m_tBlendFunc.src = 770;
				this.m_tBlendFunc.dst = 771;
			}
		}

		private void updateOpacityModifyRGB()
		{
			this.m_bIsOpacityModifyRGB = this.m_pTextureAtlas.Texture.HasPremultipliedAlpha;
		}
	}
}