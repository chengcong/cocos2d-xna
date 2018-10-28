using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cocos2d
{
	public class CCTextureAtlas : CCObject
	{
		protected short[] m_pIndices;

		private int[] m_pBuffersVBO;

		private bool m_bDirty;

		private int m_uTotalQuads;

		protected int m_uCapacity;

		protected CCTexture2D m_pTexture;

		protected ccV3F_C4B_T2F_Quad[] m_pQuads;

		public int Capacity
		{
			get
			{
				return this.m_uCapacity;
			}
			set
			{
				this.m_uCapacity = value;
			}
		}

		public ccV3F_C4B_T2F_Quad[] Quads
		{
			get
			{
				return this.m_pQuads;
			}
			set
			{
				this.m_pQuads = value;
			}
		}

		public CCTexture2D Texture
		{
			get
			{
				return this.m_pTexture;
			}
			set
			{
				this.m_pTexture = value;
			}
		}

		public int TotalQuads
		{
			get
			{
				return this.m_uTotalQuads;
			}
		}

		public CCTextureAtlas()
		{
		}

		public string description()
		{
			return string.Format("<CCTextureAtlas | totalQuads = {0}>", this.m_uTotalQuads);
		}

		public void drawNumberOfQuads(int n)
		{
			this.drawNumberOfQuads(n, 0);
		}

		public void drawNumberOfQuads(int n, int start)
		{
			if (n == start)
			{
				return;
			}
			if (this.m_pQuads == null || (int)this.m_pQuads.Length < 1)
			{
				return;
			}
			CCApplication texture2D = CCApplication.sharedApplication();
			CCDirector.sharedDirector().getWinSize();
			texture2D.basicEffect.Texture = this.Texture.getTexture2D();
			texture2D.basicEffect.TextureEnabled = true;
			texture2D.GraphicsDevice.BlendState = BlendState.AlphaBlend;
			texture2D.basicEffect.VertexColorEnabled = true;
			List<VertexPositionColorTexture> vertexPositionColorTextures = new List<VertexPositionColorTexture>();
			short[] numArray = new short[n * 6];
			for (int i = start; i < start + n; i++)
			{
				ccV3F_C4B_T2F_Quad mPQuads = this.m_pQuads[i];
				if (mPQuads != null)
				{
					vertexPositionColorTextures.AddRange(mPQuads.getVertices(ccDirectorProjection.kCCDirectorProjection3D).ToList<VertexPositionColorTexture>());
					numArray[i * 6] = (short)(i * 4);
					numArray[i * 6 + 1] = (short)(i * 4 + 1);
					numArray[i * 6 + 2] = (short)(i * 4 + 2);
					numArray[i * 6 + 3] = (short)(i * 4 + 2);
					numArray[i * 6 + 4] = (short)(i * 4 + 1);
					numArray[i * 6 + 5] = (short)(i * 4 + 3);
				}
			}
			VertexElement[] vertexElement = new VertexElement[] { new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0), new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Color, 0), new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0) };
			VertexDeclaration vertexDeclaration = new VertexDeclaration(vertexElement);
			foreach (EffectPass pass in texture2D.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				texture2D.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, vertexPositionColorTextures.ToArray(), 0, vertexPositionColorTextures.Count, numArray, 0, vertexPositionColorTextures.Count / 2);
			}
		}

		public void drawQuads()
		{
			this.drawNumberOfQuads(this.m_uTotalQuads, 0);
		}

		private void initIndices()
		{
			for (int i = 0; i < this.m_uCapacity; i++)
			{
				this.m_pIndices[i * 6] = (short)(i * 4);
				this.m_pIndices[i * 6 + 1] = (short)(i * 4 + 1);
				this.m_pIndices[i * 6 + 2] = (short)(i * 4 + 2);
				this.m_pIndices[i * 6 + 3] = (short)(i * 4 + 3);
				this.m_pIndices[i * 6 + 4] = (short)(i * 4 + 2);
				this.m_pIndices[i * 6 + 5] = (short)(i * 4 + 1);
			}
		}

		public bool initWithFile(string file, int capacity)
		{
			CCTexture2D cCTexture2D = CCTextureCache.sharedTextureCache().addImage(file);
			if (cCTexture2D == null)
			{
				return false;
			}
			return this.initWithTexture(cCTexture2D, capacity);
		}

		public bool initWithTexture(CCTexture2D texture, int capacity)
		{
			this.m_uCapacity = capacity;
			this.m_uTotalQuads = 0;
			this.m_pTexture = texture;
			this.m_pQuads = new ccV3F_C4B_T2F_Quad[this.m_uCapacity];
			this.m_pIndices = new short[this.m_uCapacity * 6];
			this.m_bDirty = true;
			this.initIndices();
			return true;
		}

		public void insertQuad(ccV3F_C4B_T2F_Quad quad, int index)
		{
			CCTextureAtlas mUTotalQuads = this;
			mUTotalQuads.m_uTotalQuads = mUTotalQuads.m_uTotalQuads + 1;
			int num = this.m_uTotalQuads - 1 - index;
			if (num > 0)
			{
				Array.Copy(this.m_pQuads, index, this.m_pQuads, index + 1, num);
			}
			this.m_pQuads[index] = quad;
			this.m_bDirty = true;
		}

		public void insertQuadFromIndex(int oldIndex, int newIndex)
		{
			if (oldIndex == newIndex)
			{
				return;
			}
			int num = (oldIndex - newIndex > 0 ? oldIndex - newIndex : newIndex - oldIndex);
			ccV3F_C4B_T2F_Quad mPQuads = this.m_pQuads[oldIndex];
			if (oldIndex <= newIndex)
			{
				Array.Copy(this.m_pQuads, newIndex + 1, this.m_pQuads, newIndex, num);
				this.m_pQuads[newIndex] = mPQuads;
			}
			else
			{
				Array.Copy(this.m_pQuads, newIndex, this.m_pQuads, newIndex + 1, num);
				this.m_pQuads[newIndex] = mPQuads;
			}
			this.m_bDirty = true;
		}

		public void removeAllQuads()
		{
			this.m_pQuads = null;
			this.m_uTotalQuads = 0;
		}

		public void removeQuadAtIndex(int index)
		{
			ccV3F_C4B_T2F_Quad[] ccV3FC4BT2FQuadArray = new ccV3F_C4B_T2F_Quad[this.m_uCapacity];
			Array.Copy(this.m_pQuads, ccV3FC4BT2FQuadArray, index);
			Array.Copy(this.m_pQuads, index + 1, ccV3FC4BT2FQuadArray, index, this.m_uTotalQuads - index - 1);
			this.m_pQuads = ccV3FC4BT2FQuadArray;
			CCTextureAtlas mUTotalQuads = this;
			mUTotalQuads.m_uTotalQuads = mUTotalQuads.m_uTotalQuads - 1;
		}

		public bool resizeCapacity(int newCapacity)
		{
			if (newCapacity == this.m_uCapacity)
			{
				return true;
			}
			this.m_uTotalQuads = Math.Min(this.m_uTotalQuads, newCapacity);
			this.m_uCapacity = newCapacity;
			ccV3F_C4B_T2F_Quad[] ccV3FC4BT2FQuadArray = new ccV3F_C4B_T2F_Quad[newCapacity];
			Array.Copy(this.m_pQuads, ccV3FC4BT2FQuadArray, (int)this.m_pQuads.Length);
			this.m_pQuads = ccV3FC4BT2FQuadArray;
			this.m_pIndices = new short[this.m_uCapacity * 6];
			this.initIndices();
			this.m_bDirty = true;
			return true;
		}

		public static CCTextureAtlas textureAtlasWithFile(string file, int capacity)
		{
			CCTextureAtlas cCTextureAtla = new CCTextureAtlas();
			if (cCTextureAtla.initWithFile(file, capacity))
			{
				return cCTextureAtla;
			}
			return null;
		}

		public static CCTextureAtlas textureAtlasWithTexture(CCTexture2D texture, int capacity)
		{
			CCTextureAtlas cCTextureAtla = new CCTextureAtlas();
			if (cCTextureAtla.initWithTexture(texture, capacity))
			{
				return cCTextureAtla;
			}
			return null;
		}

		public void updateQuad(ccV3F_C4B_T2F_Quad quad, int index)
		{
			this.m_uTotalQuads = Math.Max(index + 1, this.m_uTotalQuads);
			this.m_pQuads[index] = quad;
			this.m_bDirty = true;
		}
	}
}