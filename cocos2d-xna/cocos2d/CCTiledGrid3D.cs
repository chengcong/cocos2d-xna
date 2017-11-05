using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCTiledGrid3D : CCGridBase
	{
		protected ccQuad2[] m_pTexCoordinates;

		protected ccQuad3[] m_pVertices;

		protected ccQuad3[] m_pOriginalVertices;

		protected short[] m_pIndices;

		public CCTiledGrid3D()
		{
		}

		public override void blit()
		{
			int mSGridSize = this.m_sGridSize.x * this.m_sGridSize.y;
			CCApplication texture2D = CCApplication.sharedApplication();
			CCDirector.sharedDirector().getWinSize();
			texture2D.basicEffect.Texture = this.m_pTexture.getTexture2D();
			texture2D.basicEffect.TextureEnabled = true;
			texture2D.GraphicsDevice.BlendState = BlendState.AlphaBlend;
			texture2D.basicEffect.VertexColorEnabled = true;
			List<VertexPositionColorTexture> vertexPositionColorTextures = new List<VertexPositionColorTexture>();
			for (int i = 0; i < mSGridSize; i++)
			{
				ccQuad3 mPVertices = this.m_pVertices[i];
				ccQuad2 mPTexCoordinates = this.m_pTexCoordinates[i];
				if (mPVertices != null)
				{
					VertexPositionColorTexture vertexPositionColorTexture = new VertexPositionColorTexture()
					{
						Position = new Vector3(mPVertices.bl.x, mPVertices.bl.y, mPVertices.bl.z),
						Color = Color.White,
						TextureCoordinate = new Vector2(mPTexCoordinates.bl.x, mPTexCoordinates.bl.y)
					};
					vertexPositionColorTextures.Add(vertexPositionColorTexture);
					vertexPositionColorTexture = new VertexPositionColorTexture()
					{
						Position = new Vector3(mPVertices.br.x, mPVertices.br.y, mPVertices.br.z),
						Color = Color.White,
						TextureCoordinate = new Vector2(mPTexCoordinates.br.x, mPTexCoordinates.br.y)
					};
					vertexPositionColorTextures.Add(vertexPositionColorTexture);
					vertexPositionColorTexture = new VertexPositionColorTexture()
					{
						Position = new Vector3(mPVertices.tl.x, mPVertices.tl.y, mPVertices.tl.z),
						Color = Color.White,
						TextureCoordinate = new Vector2(mPTexCoordinates.tl.x, mPTexCoordinates.tl.y)
					};
					vertexPositionColorTextures.Add(vertexPositionColorTexture);
					vertexPositionColorTexture = new VertexPositionColorTexture()
					{
						Position = new Vector3(mPVertices.tr.x, mPVertices.tr.y, mPVertices.tr.z),
						Color = Color.White,
						TextureCoordinate = new Vector2(mPTexCoordinates.tr.x, mPTexCoordinates.tr.y)
					};
					vertexPositionColorTextures.Add(vertexPositionColorTexture);
				}
			}
			foreach (EffectPass pass in texture2D.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				texture2D.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, vertexPositionColorTextures.ToArray(), 0, vertexPositionColorTextures.Count, this.m_pIndices, 0, (int)this.m_pIndices.Length / 3);
			}
		}

		public override void calculateVertexPoints()
		{
			int i;
			float contentSizeInPixels = this.m_pTexture.ContentSizeInPixels.width;
			float single = this.m_pTexture.ContentSizeInPixels.height;
			float contentSizeInPixels1 = this.m_pTexture.ContentSizeInPixels.height;
			int mSGridSize = this.m_sGridSize.x * this.m_sGridSize.y;
			this.m_pVertices = new ccQuad3[mSGridSize];
			this.m_pOriginalVertices = new ccQuad3[mSGridSize];
			this.m_pTexCoordinates = new ccQuad2[mSGridSize];
			this.m_pIndices = new short[mSGridSize * 6];
			ccQuad3[] mPVertices = this.m_pVertices;
			ccQuad2[] mPTexCoordinates = this.m_pTexCoordinates;
			short[] mPIndices = this.m_pIndices;
			int num = 0;
			for (i = 0; i < this.m_sGridSize.x; i++)
			{
				for (int j = 0; j < this.m_sGridSize.y; j++)
				{
					float mObStep = (float)i * this.m_obStep.x;
					float mObStep1 = mObStep + this.m_obStep.x;
					float single1 = (float)j * this.m_obStep.y;
					float mObStep2 = single1 + this.m_obStep.y;
					mPVertices[num] = new ccQuad3();
					mPVertices[num].bl = new ccVertex3F(mObStep, single1, 0f);
					mPVertices[num].br = new ccVertex3F(mObStep1, single1, 0f);
					mPVertices[num].tl = new ccVertex3F(mObStep, mObStep2, 0f);
					mPVertices[num].tr = new ccVertex3F(mObStep1, mObStep2, 0f);
					float single2 = single1;
					float single3 = mObStep2;
					if (!this.m_bIsTextureFlipped)
					{
						single2 = contentSizeInPixels1 - single1;
						single3 = contentSizeInPixels1 - mObStep2;
					}
					mPTexCoordinates[num] = new ccQuad2();
					mPTexCoordinates[num].bl = new ccVertex2F(mObStep / contentSizeInPixels, single2 / single);
					mPTexCoordinates[num].br = new ccVertex2F(mObStep1 / contentSizeInPixels, single2 / single);
					mPTexCoordinates[num].tl = new ccVertex2F(mObStep / contentSizeInPixels, single3 / single);
					mPTexCoordinates[num].tr = new ccVertex2F(mObStep1 / contentSizeInPixels, single3 / single);
					num++;
				}
			}
			for (i = 0; i < mSGridSize; i++)
			{
				mPIndices[i * 6] = (short)(i * 4);
				mPIndices[i * 6 + 1] = (short)(i * 4 + 2);
				mPIndices[i * 6 + 2] = (short)(i * 4 + 1);
				mPIndices[i * 6 + 3] = (short)(i * 4 + 1);
				mPIndices[i * 6 + 4] = (short)(i * 4 + 2);
				mPIndices[i * 6 + 5] = (short)(i * 4 + 3);
			}
			Array.Copy(this.m_pVertices, this.m_pOriginalVertices, mSGridSize);
		}

		public static new CCTiledGrid3D gridWithSize(ccGridSize gridSize, CCTexture2D pTexture, bool bFlipped)
		{
			CCTiledGrid3D cCTiledGrid3D = new CCTiledGrid3D();
			if (cCTiledGrid3D.initWithSize(gridSize, pTexture, bFlipped))
			{
				return cCTiledGrid3D;
			}
			return null;
		}

		public static new CCTiledGrid3D gridWithSize(ccGridSize gridSize)
		{
			CCTiledGrid3D cCTiledGrid3D = new CCTiledGrid3D();
			if (cCTiledGrid3D.initWithSize(gridSize))
			{
				return cCTiledGrid3D;
			}
			return null;
		}

		public virtual ccQuad3 originalTile(int x, int y)
		{
			int mSGridSize = this.m_sGridSize.y * x + y;
			ccQuad3[] mPOriginalVertices = this.m_pOriginalVertices;
			ccQuad3 _ccQuad3 = new ccQuad3()
			{
				bl = new ccVertex3F(mPOriginalVertices[mSGridSize].bl.x, mPOriginalVertices[mSGridSize].bl.y, mPOriginalVertices[mSGridSize].bl.z),
				br = new ccVertex3F(mPOriginalVertices[mSGridSize].br.x, mPOriginalVertices[mSGridSize].br.y, mPOriginalVertices[mSGridSize].br.z),
				tl = new ccVertex3F(mPOriginalVertices[mSGridSize].tl.x, mPOriginalVertices[mSGridSize].tl.y, mPOriginalVertices[mSGridSize].tl.z),
				tr = new ccVertex3F(mPOriginalVertices[mSGridSize].tr.x, mPOriginalVertices[mSGridSize].tr.y, mPOriginalVertices[mSGridSize].tr.z)
			};
			return _ccQuad3;
		}

		public virtual ccQuad3 originalTile(ccGridSize pos)
		{
			return this.originalTile(pos.x, pos.y);
		}

		public override void reuse()
		{
			if (this.m_nReuseGrid > 0)
			{
				int mSGridSize = this.m_sGridSize.x * this.m_sGridSize.y;
				Array.Copy(this.m_pVertices, this.m_pOriginalVertices, mSGridSize);
				this.m_pOriginalVertices = this.m_pVertices;
				CCTiledGrid3D mNReuseGrid = this;
				mNReuseGrid.m_nReuseGrid = mNReuseGrid.m_nReuseGrid - 1;
			}
		}

		public virtual void setTile(ccGridSize pos, ccQuad3 coords)
		{
			int mSGridSize = this.m_sGridSize.y * pos.x + pos.y;
			this.m_pVertices[mSGridSize] = coords;
		}

		public virtual void setTile(int x, int y, ccQuad3 coords)
		{
			int mSGridSize = this.m_sGridSize.y * x + y;
			this.m_pVertices[mSGridSize] = coords;
		}

		public virtual ccQuad3 tile(ccGridSize pos)
		{
			return this.tile(pos.x, pos.y);
		}

		public virtual ccQuad3 tile(int x, int y)
		{
			int mSGridSize = this.m_sGridSize.y * x + y;
			ccQuad3[] mPVertices = this.m_pVertices;
			ccQuad3 _ccQuad3 = new ccQuad3()
			{
				bl = new ccVertex3F(mPVertices[mSGridSize].bl.x, mPVertices[mSGridSize].bl.y, mPVertices[mSGridSize].bl.z),
				br = new ccVertex3F(mPVertices[mSGridSize].br.x, mPVertices[mSGridSize].br.y, mPVertices[mSGridSize].br.z),
				tl = new ccVertex3F(mPVertices[mSGridSize].tl.x, mPVertices[mSGridSize].tl.y, mPVertices[mSGridSize].tl.z),
				tr = new ccVertex3F(mPVertices[mSGridSize].tr.x, mPVertices[mSGridSize].tr.y, mPVertices[mSGridSize].tr.z)
			};
			return _ccQuad3;
		}
	}
}