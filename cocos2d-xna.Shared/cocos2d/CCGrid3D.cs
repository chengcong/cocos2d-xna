using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCGrid3D : CCGridBase
	{
		protected CCPoint[] m_pTexCoordinates;

		protected ccVertex3F[] m_pVertices;

		protected ccVertex3F[] m_pOriginalVertices;

		protected short[] m_pIndices;

		public CCGrid3D()
		{
		}

		public override void blit()
		{
			int mSGridSize = this.m_sGridSize.x;
			int num = this.m_sGridSize.y;
			CCApplication texture2D = CCApplication.sharedApplication();
			CCDirector.sharedDirector().getWinSize();
			texture2D.basicEffect.Texture = this.m_pTexture.getTexture2D();
			texture2D.basicEffect.TextureEnabled = true;
			texture2D.GraphicsDevice.BlendState = BlendState.AlphaBlend;
			texture2D.basicEffect.VertexColorEnabled = true;
			List<VertexPositionColorTexture> vertexPositionColorTextures = new List<VertexPositionColorTexture>();
			for (int i = 0; i < (this.m_sGridSize.x + 1) * (this.m_sGridSize.y + 1); i++)
			{
				VertexPositionColorTexture vertexPositionColorTexture = new VertexPositionColorTexture()
				{
					Position = new Vector3(this.m_pVertices[i].x, this.m_pVertices[i].y, this.m_pVertices[i].z),
					TextureCoordinate = new Vector2(this.m_pTexCoordinates[i].x, this.m_pTexCoordinates[i].y),
					Color = Color.White
				};
				vertexPositionColorTextures.Add(vertexPositionColorTexture);
			}
			short[] mPIndices = this.m_pIndices;
			foreach (EffectPass pass in texture2D.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				texture2D.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, vertexPositionColorTextures.ToArray(), 0, vertexPositionColorTextures.Count, mPIndices, 0, (int)mPIndices.Length / 3);
			}
		}

		public override void calculateVertexPoints()
		{
			float contentSizeInPixels = (float)this.m_pTexture.ContentSizeInPixels.width;
			float single = (float)this.m_pTexture.ContentSizeInPixels.height;
			float contentSizeInPixels1 = this.m_pTexture.ContentSizeInPixels.height;
			this.m_pVertices = new ccVertex3F[(this.m_sGridSize.x + 1) * (this.m_sGridSize.y + 1)];
			this.m_pOriginalVertices = new ccVertex3F[(this.m_sGridSize.x + 1) * (this.m_sGridSize.y + 1)];
			this.m_pTexCoordinates = new CCPoint[(this.m_sGridSize.x + 1) * (this.m_sGridSize.y + 1)];
			this.m_pIndices = new short[this.m_sGridSize.x * this.m_sGridSize.y * 6];
			ccVertex3F[] mPVertices = this.m_pVertices;
			CCPoint[] mPTexCoordinates = this.m_pTexCoordinates;
			short[] mPIndices = this.m_pIndices;
			for (int i = 0; i < this.m_sGridSize.x; i++)
			{
				for (int j = 0; j < this.m_sGridSize.y; j++)
				{
					int mSGridSize = j * this.m_sGridSize.x + i;
					float mObStep = (float)i * this.m_obStep.x;
					float mObStep1 = mObStep + this.m_obStep.x;
					float single1 = (float)j * this.m_obStep.y;
					float mObStep2 = single1 + this.m_obStep.y;
					int num = i * (this.m_sGridSize.y + 1) + j;
					int mSGridSize1 = (i + 1) * (this.m_sGridSize.y + 1) + j;
					int num1 = (i + 1) * (this.m_sGridSize.y + 1) + j + 1;
					int mSGridSize2 = i * (this.m_sGridSize.y + 1) + j + 1;
					short[] numArray = new short[] { (short)num, (short)mSGridSize2, (short)mSGridSize1, (short)mSGridSize1, (short)mSGridSize2, (short)num1 };
					short[] numArray1 = numArray;
					Array.Copy(numArray1, 0, mPIndices, 6 * mSGridSize, (int)numArray1.Length);
					int[] numArray2 = new int[] { num, mSGridSize1, num1, mSGridSize2 };
					ccVertex3F _ccVertex3F = new ccVertex3F(mObStep, single1, 0f);
					ccVertex3F _ccVertex3F1 = new ccVertex3F(mObStep1, single1, 0f);
					ccVertex3F _ccVertex3F2 = new ccVertex3F(mObStep1, mObStep2, 0f);
					ccVertex3F _ccVertex3F3 = new ccVertex3F(mObStep, mObStep2, 0f);
					ccVertex3F[] ccVertex3FArray = new ccVertex3F[] { _ccVertex3F, _ccVertex3F1, _ccVertex3F2, _ccVertex3F3 };
					int[] numArray3 = new int[] { num, mSGridSize1, num1, mSGridSize2 };
					CCPoint[] cCPoint = new CCPoint[] { new CCPoint(mObStep, single1), new CCPoint(mObStep1, single1), new CCPoint(mObStep1, mObStep2), new CCPoint(mObStep, mObStep2) };
					CCPoint[] cCPointArray = cCPoint;
					for (int k = 0; k < 4; k++)
					{
						mPVertices[numArray2[k]] = new ccVertex3F();
						mPVertices[numArray2[k]].x = ccVertex3FArray[k].x;
						mPVertices[numArray2[k]].y = ccVertex3FArray[k].y;
						mPVertices[numArray2[k]].z = ccVertex3FArray[k].z;
						mPTexCoordinates[numArray3[k]] = new CCPoint();
						mPTexCoordinates[numArray3[k]].x = cCPointArray[k].x / contentSizeInPixels;
						if (!this.m_bIsTextureFlipped)
						{
							mPTexCoordinates[numArray3[k]].y = (contentSizeInPixels1 - cCPointArray[k].y) / single;
						}
						else
						{
							mPTexCoordinates[numArray3[k]].y = cCPointArray[k].y / single;
						}
					}
				}
			}
			Array.Copy(this.m_pVertices, this.m_pOriginalVertices, (this.m_sGridSize.x + 1) * (this.m_sGridSize.y + 1));
		}

		public static new CCGrid3D gridWithSize(ccGridSize gridSize, CCTexture2D pTexture, bool bFlipped)
		{
			CCGrid3D cCGrid3D = new CCGrid3D();
			if (cCGrid3D.initWithSize(gridSize, pTexture, bFlipped))
			{
				return cCGrid3D;
			}
			return null;
		}

		public static new CCGrid3D gridWithSize(ccGridSize gridSize)
		{
			CCGrid3D cCGrid3D = new CCGrid3D();
			if (cCGrid3D.initWithSize(gridSize))
			{
				return cCGrid3D;
			}
			return null;
		}

		public ccVertex3F originalVertex(ccGridSize pos)
		{
			int num = pos.x * (this.m_sGridSize.y + 1) + pos.y;
			ccVertex3F[] mPOriginalVertices = this.m_pOriginalVertices;
			ccVertex3F _ccVertex3F = new ccVertex3F()
			{
				x = mPOriginalVertices[num].x,
				y = mPOriginalVertices[num].y,
				z = mPOriginalVertices[num].z
			};
			return _ccVertex3F;
		}

		public ccVertex3F originalVertex(int px, int py)
		{
			int num = px * (this.m_sGridSize.y + 1) + py;
			ccVertex3F[] mPOriginalVertices = this.m_pOriginalVertices;
			ccVertex3F _ccVertex3F = new ccVertex3F()
			{
				x = mPOriginalVertices[num].x,
				y = mPOriginalVertices[num].y,
				z = mPOriginalVertices[num].z
			};
			return _ccVertex3F;
		}

		public override void reuse()
		{
			if (this.m_nReuseGrid > 0)
			{
				Array.Copy(this.m_pVertices, this.m_pOriginalVertices, (this.m_sGridSize.x + 1) * (this.m_sGridSize.y + 1));
				CCGrid3D mNReuseGrid = this;
				mNReuseGrid.m_nReuseGrid = mNReuseGrid.m_nReuseGrid - 1;
			}
		}

		public void setVertex(ccGridSize pos, ccVertex3F vertex)
		{
			int num = pos.x * (this.m_sGridSize.y + 1) + pos.y;
			ccVertex3F[] mPVertices = this.m_pVertices;
			mPVertices[num].x = vertex.x;
			mPVertices[num].y = vertex.y;
			mPVertices[num].z = vertex.z;
		}

		public void setVertex(int px, int py, ccVertex3F vertex)
		{
			int num = px * (this.m_sGridSize.y + 1) + py;
			ccVertex3F[] mPVertices = this.m_pVertices;
			mPVertices[num].x = vertex.x;
			mPVertices[num].y = vertex.y;
			mPVertices[num].z = vertex.z;
		}

		public ccVertex3F vertex(ccGridSize pos)
		{
			int num = pos.x * (this.m_sGridSize.y + 1) + pos.y;
			ccVertex3F[] mPVertices = this.m_pVertices;
			ccVertex3F _ccVertex3F = new ccVertex3F()
			{
				x = mPVertices[num].x,
				y = mPVertices[num].y,
				z = mPVertices[num].z
			};
			return _ccVertex3F;
		}
	}
}