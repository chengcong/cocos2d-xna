using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCLabelAtlas : CCAtlasNode, ICCLabelProtocol
	{
		protected string m_sString;

		protected char m_cMapStartChar;

		public CCLabelAtlas()
		{
			this.m_sString = "";
		}

		public virtual ICCLabelProtocol convertToLabelProtocol()
		{
			return this;
		}

		public string getString()
		{
			return this.m_sString;
		}

		public bool initWithString(string label, string charMapFile, int itemWidth, int itemHeight, char startCharMap)
		{
			if (!base.initWithTileFile(charMapFile, itemWidth, itemHeight, label.Length))
			{
				return false;
			}
			this.m_cMapStartChar = startCharMap;
			this.setString(label);
			return true;
		}

		public static CCLabelAtlas labelWithString(string label, string charMapFile, int itemWidth, int itemHeight, char startCharMap)
		{
			CCLabelAtlas cCLabelAtla = new CCLabelAtlas();
			if (cCLabelAtla != null && cCLabelAtla.initWithString(label, charMapFile, itemWidth, itemHeight, startCharMap))
			{
				return cCLabelAtla;
			}
			return null;
		}

		public void setString(string label)
		{
			int length = label.Length;
			if (length > this.m_pTextureAtlas.TotalQuads)
			{
				this.m_pTextureAtlas.resizeCapacity(length);
			}
			base.children.Clear();
			this.m_sString = label;
			this.updateAtlasValues();
			CCSize cCSize = new CCSize()
			{
				width = (float)(length * this.m_uItemWidth),
				height = (float)this.m_uItemHeight
			};
			base.contentSizeInPixels = cCSize;
			this.m_uQuadsToDraw = length;
		}

		public override void updateAtlasValues()
		{
			char[] charArray = this.m_sString.ToCharArray();
			CCTexture2D texture = this.m_pTextureAtlas.Texture;
			float pixelsWide = (float)texture.PixelsWide;
			float pixelsHigh = (float)texture.PixelsHigh;
			for (int i = 0; i < this.m_sString.Length; i++)
			{
				ccV3F_C4B_T2F_Quad ccV3FC4BT2FQuad = new ccV3F_C4B_T2F_Quad();
				char mCMapStartChar = (char)(charArray[i] - this.m_cMapStartChar);
				float mUItemsPerRow = (float)(mCMapStartChar % (char)this.m_uItemsPerRow);
				float single = (float)(mCMapStartChar / (char)this.m_uItemsPerRow);
				float mUItemWidth = mUItemsPerRow * (float)this.m_uItemWidth / pixelsWide;
				float mUItemWidth1 = mUItemWidth + (float)this.m_uItemWidth / pixelsWide;
				float mUItemHeight = single * (float)this.m_uItemHeight / pixelsHigh;
				float mUItemHeight1 = mUItemHeight + (float)this.m_uItemHeight / pixelsHigh;
				ccV3FC4BT2FQuad.tl.texCoords.u = mUItemWidth;
				ccV3FC4BT2FQuad.tl.texCoords.v = mUItemHeight;
				ccV3FC4BT2FQuad.tr.texCoords.u = mUItemWidth1;
				ccV3FC4BT2FQuad.tr.texCoords.v = mUItemHeight;
				ccV3FC4BT2FQuad.bl.texCoords.u = mUItemWidth;
				ccV3FC4BT2FQuad.bl.texCoords.v = mUItemHeight1;
				ccV3FC4BT2FQuad.br.texCoords.u = mUItemWidth1;
				ccV3FC4BT2FQuad.br.texCoords.v = mUItemHeight1;
				ccV3F_C4B_T2F ccV3FC4BT2F = ccV3FC4BT2FQuad.tl;
				ccV3F_C4B_T2F ccV3FC4BT2F1 = ccV3FC4BT2FQuad.tr;
				ccV3F_C4B_T2F ccV3FC4BT2F2 = ccV3FC4BT2FQuad.bl;
				ccV3F_C4B_T2F ccV3FC4BT2F3 = ccV3FC4BT2FQuad.br;
				ccColor4B _ccColor4B = new ccColor4B(this.m_tColor.r, this.m_tColor.g, this.m_tColor.b, this.m_cOpacity);
				ccColor4B _ccColor4B1 = _ccColor4B;
				ccV3FC4BT2F3.colors = _ccColor4B;
				ccColor4B _ccColor4B2 = _ccColor4B1;
				ccColor4B _ccColor4B3 = _ccColor4B2;
				ccV3FC4BT2F2.colors = _ccColor4B2;
				ccColor4B _ccColor4B4 = _ccColor4B3;
				ccColor4B _ccColor4B5 = _ccColor4B4;
				ccV3FC4BT2F1.colors = _ccColor4B4;
				ccV3FC4BT2F.colors = _ccColor4B5;
				ccV3FC4BT2FQuad.bl.vertices.x = (float)(i * this.m_uItemWidth);
				ccV3FC4BT2FQuad.bl.vertices.y = 0f;
				ccV3FC4BT2FQuad.bl.vertices.z = 0f;
				ccV3FC4BT2FQuad.br.vertices.x = (float)(i * this.m_uItemWidth + this.m_uItemWidth);
				ccV3FC4BT2FQuad.br.vertices.y = 0f;
				ccV3FC4BT2FQuad.br.vertices.z = 0f;
				ccV3FC4BT2FQuad.tl.vertices.x = (float)(i * this.m_uItemWidth);
				ccV3FC4BT2FQuad.tl.vertices.y = (float)this.m_uItemHeight;
				ccV3FC4BT2FQuad.tl.vertices.z = 0f;
				ccV3FC4BT2FQuad.tr.vertices.x = (float)(i * this.m_uItemWidth + this.m_uItemWidth);
				ccV3FC4BT2FQuad.tr.vertices.y = (float)this.m_uItemHeight;
				ccV3FC4BT2FQuad.tr.vertices.z = 0f;
				this.m_pTextureAtlas.updateQuad(ccV3FC4BT2FQuad, i);
			}
		}
	}
}