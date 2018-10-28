using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCProgressTimer : CCNode
	{
		private const int kProgressTextureCoordsCount = 4;

		private const int kProgressTextureCoords = 30;

		protected CCProgressTimerType m_eType;

		protected float m_fPercentage;

		protected CCSprite m_pSprite;

		protected int m_nVertexDataCount;

		protected ccV2F_C4B_T2F[] m_pVertexData;

		protected VertexPositionColorTexture[] vertices;

		protected short[] indexes;

		public float Percentage
		{
			get
			{
				return this.m_fPercentage;
			}
			set
			{
				if (this.m_fPercentage != value)
				{
					this.m_fPercentage = CCPointExtension.clampf(value, 0f, 100f);
					this.updateProgress();
				}
			}
		}

		public CCSprite Sprite
		{
			get
			{
				return this.m_pSprite;
			}
			set
			{
				if (this.m_pSprite != value)
				{
					this.m_pSprite = value;
					this.contentSize = this.m_pSprite.contentSize;
					if (this.m_pVertexData != null)
					{
						this.m_pVertexData = null;
						this.m_nVertexDataCount = 0;
					}
				}
			}
		}

		public CCProgressTimerType Type
		{
			get
			{
				return this.m_eType;
			}
			set
			{
				if (value != this.m_eType)
				{
					if (this.m_pVertexData == null)
					{
						this.m_pVertexData = null;
						this.m_nVertexDataCount = 0;
					}
					this.m_eType = value;
				}
			}
		}

		public CCProgressTimer()
		{
		}

		protected CCPoint boundaryTexCoord(int index)
		{
			if (index < 4)
			{
				switch (this.m_eType)
				{
					case CCProgressTimerType.kCCProgressTimerTypeRadialCCW:
					{
						return new CCPoint((float)(30 >> (7 - (index << 1) & 31) & 1), (float)(30 >> (7 - ((index << 1) + 1) & 31) & 1));
					}
					case CCProgressTimerType.kCCProgressTimerTypeRadialCW:
					{
						return new CCPoint((float)(30 >> ((index << 1) + 1 & 31) & 1), (float)(30 >> (index << 1 & 31) & 1));
					}
				}
			}
			return new CCPoint(0f, 0f);
		}

		public override void draw()
		{
			base.draw();
			if (this.m_pVertexData == null)
			{
				return;
			}
			if (this.m_pSprite == null)
			{
				return;
			}
			this.vertices = new VertexPositionColorTexture[(int)this.m_pVertexData.Length];
			this.getIndexes();
			for (int i = 0; i < (int)this.m_pVertexData.Length; i++)
			{
				ccV2F_C4B_T2F mPVertexData = this.m_pVertexData[i];
				this.vertices[i] = new VertexPositionColorTexture(new Vector3(mPVertexData.vertices.x, mPVertexData.vertices.y, 0f), new Color((int)mPVertexData.colors.r, (int)mPVertexData.colors.g, (int)mPVertexData.colors.b, (int)mPVertexData.colors.a), new Vector2(mPVertexData.texCoords.u, mPVertexData.texCoords.v));
			}
			CCApplication texture2D = CCApplication.sharedApplication();
			texture2D.basicEffect.Texture = this.m_pSprite.Texture.getTexture2D();
			texture2D.basicEffect.TextureEnabled = true;
			VertexElement[] vertexElement = new VertexElement[] { new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0), new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Color, 0), new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0) };
			VertexDeclaration vertexDeclaration = new VertexDeclaration(vertexElement);
			foreach (EffectPass pass in texture2D.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				texture2D.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, this.vertices, 0, this.m_nVertexDataCount, this.indexes, 0, this.m_nVertexDataCount - 2);
			}
		}

		private void getIndexes()
		{
			if (this.indexes == null)
			{
				switch (this.m_eType)
				{
					case CCProgressTimerType.kCCProgressTimerTypeRadialCCW:
					{
						this.indexes = new short[] { 1, 0, 2, 2, 0, 3, 3, 0, 4, 4, 0, 5, 5, 0, 6 };
						return;
					}
					case CCProgressTimerType.kCCProgressTimerTypeRadialCW:
					{
						this.indexes = new short[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6 };
						return;
					}
					case CCProgressTimerType.kCCProgressTimerTypeHorizontalBarLR:
					{
						this.indexes = new short[] { 1, 0, 2, 1, 2, 3 };
						return;
					}
					case CCProgressTimerType.kCCProgressTimerTypeHorizontalBarRL:
					{
						this.indexes = new short[] { 0, 1, 2, 2, 1, 3 };
						return;
					}
					case CCProgressTimerType.kCCProgressTimerTypeVerticalBarBT:
					{
						this.indexes = new short[] { 1, 0, 2, 1, 2, 3 };
						return;
					}
					case CCProgressTimerType.kCCProgressTimerTypeVerticalBarTB:
					{
						this.indexes = new short[] { 1, 0, 2, 1, 2, 3 };
						break;
					}
					default:
					{
						return;
					}
				}
			}
		}

		public bool initWithFile(string pszFileName)
		{
			return this.initWithTexture(CCTextureCache.sharedTextureCache().addImage(pszFileName));
		}

		public bool initWithTexture(CCTexture2D pTexture)
		{
			this.m_pSprite = CCSprite.spriteWithTexture(pTexture);
			this.m_fPercentage = 0f;
			this.m_pVertexData = null;
			this.m_nVertexDataCount = 0;
			this.anchorPoint = new CCPoint(0.5f, 0.5f);
			this.contentSize = this.m_pSprite.contentSize;
			this.m_eType = CCProgressTimerType.kCCProgressTimerTypeRadialCCW;
			return true;
		}

		public static CCProgressTimer progressWithFile(string pszFileName)
		{
			CCProgressTimer cCProgressTimer = new CCProgressTimer();
			if (cCProgressTimer.initWithFile(pszFileName))
			{
				return cCProgressTimer;
			}
			return null;
		}

		public static CCProgressTimer progressWithTexture(CCTexture2D pTexture)
		{
			CCProgressTimer cCProgressTimer = new CCProgressTimer();
			if (cCProgressTimer.initWithTexture(pTexture))
			{
				return cCProgressTimer;
			}
			return null;
		}

		protected void updateBar()
		{
			float mFPercentage = this.m_fPercentage / 100f;
			float single = Math.Max(this.m_pSprite.quad.br.texCoords.u, this.m_pSprite.quad.bl.texCoords.u);
			float single1 = Math.Min(this.m_pSprite.quad.br.texCoords.u, this.m_pSprite.quad.bl.texCoords.u);
			float single2 = Math.Max(this.m_pSprite.quad.tl.texCoords.v, this.m_pSprite.quad.bl.texCoords.v);
			float single3 = Math.Min(this.m_pSprite.quad.tl.texCoords.v, this.m_pSprite.quad.bl.texCoords.v);
			CCPoint cCPoint = new CCPoint(single, single2);
			CCPoint cCPoint1 = new CCPoint(single1, single3);
			int[] numArray = new int[2];
			int num = 0;
			if (this.m_pVertexData == null)
			{
				this.m_nVertexDataCount = 4;
				this.m_pVertexData = new ccV2F_C4B_T2F[this.m_nVertexDataCount];
				for (int i = 0; i < this.m_nVertexDataCount; i++)
				{
					this.m_pVertexData[i] = new ccV2F_C4B_T2F();
				}
				if (this.m_eType == CCProgressTimerType.kCCProgressTimerTypeHorizontalBarLR)
				{
					int num1 = 0;
					int num2 = num1;
					numArray[0] = num1;
					this.m_pVertexData[num2].texCoords = new ccTex2F(cCPoint1.x, cCPoint1.y);
					int num3 = 1;
					int num4 = num3;
					numArray[1] = num3;
					this.m_pVertexData[num4].texCoords = new ccTex2F(cCPoint1.x, cCPoint.y);
				}
				else if (this.m_eType == CCProgressTimerType.kCCProgressTimerTypeHorizontalBarRL)
				{
					int num5 = 2;
					int num6 = num5;
					numArray[0] = num5;
					this.m_pVertexData[num6].texCoords = new ccTex2F(cCPoint.x, cCPoint.y);
					int num7 = 3;
					int num8 = num7;
					numArray[1] = num7;
					this.m_pVertexData[num8].texCoords = new ccTex2F(cCPoint.x, cCPoint1.y);
				}
				else if (this.m_eType == CCProgressTimerType.kCCProgressTimerTypeVerticalBarBT)
				{
					int num9 = 1;
					int num10 = num9;
					numArray[0] = num9;
					this.m_pVertexData[num10].texCoords = new ccTex2F(cCPoint1.x, cCPoint.y);
					int num11 = 3;
					int num12 = num11;
					numArray[1] = num11;
					this.m_pVertexData[num12].texCoords = new ccTex2F(cCPoint.x, cCPoint.y);
				}
				else if (this.m_eType == CCProgressTimerType.kCCProgressTimerTypeVerticalBarTB)
				{
					int num13 = 0;
					int num14 = num13;
					numArray[0] = num13;
					this.m_pVertexData[num14].texCoords = new ccTex2F(cCPoint1.x, cCPoint1.y);
					int num15 = 2;
					int num16 = num15;
					numArray[1] = num15;
					this.m_pVertexData[num16].texCoords = new ccTex2F(cCPoint.x, cCPoint1.y);
				}
				num = numArray[0];
				this.m_pVertexData[num].vertices = this.vertexFromTexCoord(new CCPoint(this.m_pVertexData[num].texCoords.u, this.m_pVertexData[num].texCoords.v));
				num = numArray[1];
				this.m_pVertexData[num].vertices = this.vertexFromTexCoord(new CCPoint(this.m_pVertexData[num].texCoords.u, this.m_pVertexData[num].texCoords.v));
				if (this.m_pSprite.IsFlipY || this.m_pSprite.IsFlipX)
				{
					if (this.m_pSprite.IsFlipX)
					{
						num = numArray[0];
						this.m_pVertexData[num].texCoords.u = cCPoint1.x + cCPoint.x - this.m_pVertexData[num].texCoords.u;
						num = numArray[1];
						this.m_pVertexData[num].texCoords.u = cCPoint1.x + cCPoint.x - this.m_pVertexData[num].texCoords.u;
					}
					if (this.m_pSprite.IsFlipY)
					{
						num = numArray[0];
						this.m_pVertexData[num].texCoords.v = cCPoint1.y + cCPoint.y - this.m_pVertexData[num].texCoords.v;
						num = numArray[1];
						this.m_pVertexData[num].texCoords.v = cCPoint1.y + cCPoint.y - this.m_pVertexData[num].texCoords.v;
					}
				}
				this.updateColor();
			}
			if (this.m_eType == CCProgressTimerType.kCCProgressTimerTypeHorizontalBarLR)
			{
				int num17 = 3;
				int num18 = num17;
				numArray[0] = num17;
				this.m_pVertexData[num18].texCoords = new ccTex2F(cCPoint1.x + (cCPoint.x - cCPoint1.x) * mFPercentage, cCPoint.y);
				int num19 = 2;
				int num20 = num19;
				numArray[1] = num19;
				this.m_pVertexData[num20].texCoords = new ccTex2F(cCPoint1.x + (cCPoint.x - cCPoint1.x) * mFPercentage, cCPoint1.y);
			}
			else if (this.m_eType == CCProgressTimerType.kCCProgressTimerTypeHorizontalBarRL)
			{
				int num21 = 1;
				int num22 = num21;
				numArray[0] = num21;
				this.m_pVertexData[num22].texCoords = new ccTex2F(cCPoint1.x + (cCPoint.x - cCPoint1.x) * (1f - mFPercentage), cCPoint1.y);
				int num23 = 0;
				int num24 = num23;
				numArray[1] = num23;
				this.m_pVertexData[num24].texCoords = new ccTex2F(cCPoint1.x + (cCPoint.x - cCPoint1.x) * (1f - mFPercentage), cCPoint.y);
			}
			else if (this.m_eType == CCProgressTimerType.kCCProgressTimerTypeVerticalBarBT)
			{
				int num25 = 0;
				int num26 = num25;
				numArray[0] = num25;
				this.m_pVertexData[num26].texCoords = new ccTex2F(cCPoint1.x, cCPoint1.y + (cCPoint.y - cCPoint1.y) * (1f - mFPercentage));
				int num27 = 2;
				int num28 = num27;
				numArray[1] = num27;
				this.m_pVertexData[num28].texCoords = new ccTex2F(cCPoint.x, cCPoint1.y + (cCPoint.y - cCPoint1.y) * (1f - mFPercentage));
			}
			else if (this.m_eType == CCProgressTimerType.kCCProgressTimerTypeVerticalBarTB)
			{
				int num29 = 1;
				int num30 = num29;
				numArray[0] = num29;
				this.m_pVertexData[num30].texCoords = new ccTex2F(cCPoint1.x, cCPoint1.y + (cCPoint.y - cCPoint1.y) * mFPercentage);
				int num31 = 3;
				int num32 = num31;
				numArray[1] = num31;
				this.m_pVertexData[num32].texCoords = new ccTex2F(cCPoint.x, cCPoint1.y + (cCPoint.y - cCPoint1.y) * mFPercentage);
			}
			num = numArray[0];
			this.m_pVertexData[num].vertices = this.vertexFromTexCoord(new CCPoint(this.m_pVertexData[num].texCoords.u, this.m_pVertexData[num].texCoords.v));
			num = numArray[1];
			this.m_pVertexData[num].vertices = this.vertexFromTexCoord(new CCPoint(this.m_pVertexData[num].texCoords.u, this.m_pVertexData[num].texCoords.v));
			if (this.m_pSprite.IsFlipY || this.m_pSprite.IsFlipX)
			{
				if (this.m_pSprite.IsFlipX)
				{
					num = numArray[0];
					this.m_pVertexData[num].texCoords.u = cCPoint1.x + cCPoint.x - this.m_pVertexData[num].texCoords.u;
					num = numArray[1];
					this.m_pVertexData[num].texCoords.u = cCPoint1.x + cCPoint.x - this.m_pVertexData[num].texCoords.u;
				}
				if (this.m_pSprite.IsFlipY)
				{
					num = numArray[0];
					this.m_pVertexData[num].texCoords.v = cCPoint1.y + cCPoint.y - this.m_pVertexData[num].texCoords.v;
					num = numArray[1];
					this.m_pVertexData[num].texCoords.v = cCPoint1.y + cCPoint.y - this.m_pVertexData[num].texCoords.v;
				}
			}
		}

		protected void updateColor()
		{
			byte opacity = this.m_pSprite.Opacity;
			ccColor3B color = this.m_pSprite.Color;
			ccColor4B _ccColor4B = new ccColor4B()
			{
				r = color.r,
				g = color.g,
				b = color.b,
				a = opacity
			};
			ccColor4B _ccColor4B1 = _ccColor4B;
			if (this.m_pSprite.Texture.HasPremultipliedAlpha)
			{
				ccColor4B _ccColor4B2 = _ccColor4B1;
				_ccColor4B2.r = (byte)(_ccColor4B2.r * (byte)(opacity / 255));
				ccColor4B _ccColor4B3 = _ccColor4B1;
				_ccColor4B3.g = (byte)(_ccColor4B3.g * (byte)(opacity / 255));
				ccColor4B _ccColor4B4 = _ccColor4B1;
				_ccColor4B4.b = (byte)(_ccColor4B4.b * (byte)(opacity / 255));
			}
			if (this.m_pVertexData != null)
			{
				for (int i = 0; i < this.m_nVertexDataCount; i++)
				{
					this.m_pVertexData[i].colors = _ccColor4B1;
				}
			}
		}

		protected void updateProgress()
		{
			switch (this.m_eType)
			{
				case CCProgressTimerType.kCCProgressTimerTypeRadialCCW:
				case CCProgressTimerType.kCCProgressTimerTypeRadialCW:
				{
					this.updateRadial();
					return;
				}
				case CCProgressTimerType.kCCProgressTimerTypeHorizontalBarLR:
				case CCProgressTimerType.kCCProgressTimerTypeHorizontalBarRL:
				case CCProgressTimerType.kCCProgressTimerTypeVerticalBarBT:
				case CCProgressTimerType.kCCProgressTimerTypeVerticalBarTB:
				{
					this.updateBar();
					return;
				}
				default:
				{
					return;
				}
			}
		}

		protected void updateRadial()
		{
			float single = Math.Max(this.m_pSprite.quad.br.texCoords.u, this.m_pSprite.quad.bl.texCoords.u);
			float single1 = Math.Min(this.m_pSprite.quad.br.texCoords.u, this.m_pSprite.quad.bl.texCoords.u);
			float single2 = Math.Max(this.m_pSprite.quad.tl.texCoords.v, this.m_pSprite.quad.bl.texCoords.v);
			float single3 = Math.Min(this.m_pSprite.quad.tl.texCoords.v, this.m_pSprite.quad.bl.texCoords.v);
			CCPoint cCPoint = new CCPoint(single, single2);
			CCPoint cCPoint1 = new CCPoint(single1, single3);
			CCPoint cCPoint2 = CCPointExtension.ccpAdd(cCPoint1, CCPointExtension.ccpCompMult(this.m_tAnchorPoint, CCPointExtension.ccpSub(cCPoint, cCPoint1)));
			float mFPercentage = this.m_fPercentage / 100f;
			float single4 = 6.28318548f * (this.m_eType == CCProgressTimerType.kCCProgressTimerTypeRadialCW ? mFPercentage : 1f - mFPercentage);
			CCPoint cCPoint3 = new CCPoint(cCPoint2.x, cCPoint1.y);
			CCPoint cCPoint4 = CCPointExtension.ccpRotateByAngle(cCPoint3, cCPoint2, single4);
			int num = 0;
			CCPoint cCPoint5 = new CCPoint();
			if (mFPercentage == 0f)
			{
				cCPoint5 = cCPoint3;
				num = 0;
			}
			else if (mFPercentage != 1f)
			{
				float single5 = float.MaxValue;
				for (int i = 0; i <= 4; i++)
				{
					int num1 = (i + 3) % 4;
					CCPoint cCPoint6 = CCPointExtension.ccpAdd(cCPoint1, CCPointExtension.ccpCompMult(this.boundaryTexCoord(i % 4), CCPointExtension.ccpSub(cCPoint, cCPoint1)));
					CCPoint cCPoint7 = CCPointExtension.ccpAdd(cCPoint1, CCPointExtension.ccpCompMult(this.boundaryTexCoord(num1), CCPointExtension.ccpSub(cCPoint, cCPoint1)));
					if (i == 0)
					{
						cCPoint7 = CCPointExtension.ccpLerp(cCPoint6, cCPoint7, 0.5f);
					}
					else if (i == 4)
					{
						cCPoint6 = CCPointExtension.ccpLerp(cCPoint6, cCPoint7, 0.5f);
					}
					float single6 = 0f;
					float single7 = 0f;
					if (CCPointExtension.ccpLineIntersect(cCPoint6, cCPoint7, cCPoint2, cCPoint4, ref single6, ref single7) && (i != 0 && i != 4 || 0f <= single6 && single6 <= 1f) && single7 >= 0f && single7 < single5)
					{
						single5 = single7;
						num = i;
					}
				}
				cCPoint5 = CCPointExtension.ccpAdd(cCPoint2, CCPointExtension.ccpMult(CCPointExtension.ccpSub(cCPoint4, cCPoint2), single5));
			}
			else
			{
				cCPoint5 = cCPoint3;
				num = 4;
			}
			bool flag = true;
			if (this.m_nVertexDataCount != num + 3)
			{
				flag = false;
				if (this.m_pVertexData != null)
				{
					this.m_pVertexData = null;
					this.m_nVertexDataCount = 0;
				}
			}
			if (this.m_pVertexData == null)
			{
				this.m_nVertexDataCount = num + 3;
				this.m_pVertexData = new ccV2F_C4B_T2F[this.m_nVertexDataCount];
				for (int j = 0; j < this.m_nVertexDataCount; j++)
				{
					this.m_pVertexData[j] = new ccV2F_C4B_T2F();
				}
				this.updateColor();
			}
			if (!flag)
			{
				this.m_pVertexData[0].texCoords = new ccTex2F(cCPoint2.x, cCPoint2.y);
				this.m_pVertexData[0].vertices = this.vertexFromTexCoord(cCPoint2);
				this.m_pVertexData[1].texCoords = new ccTex2F(cCPoint2.x, cCPoint1.y);
				this.m_pVertexData[1].vertices = this.vertexFromTexCoord(new CCPoint(cCPoint2.x, cCPoint1.y));
				for (int k = 0; k < num; k++)
				{
					CCPoint cCPoint8 = CCPointExtension.ccpAdd(cCPoint1, CCPointExtension.ccpCompMult(this.boundaryTexCoord(k), CCPointExtension.ccpSub(cCPoint, cCPoint1)));
					this.m_pVertexData[k + 2].texCoords = new ccTex2F(cCPoint8.x, cCPoint8.y);
					this.m_pVertexData[k + 2].vertices = this.vertexFromTexCoord(cCPoint8);
				}
				if (this.m_pSprite.IsFlipX || this.m_pSprite.IsFlipY)
				{
					for (int l = 0; l < this.m_nVertexDataCount - 1; l++)
					{
						if (this.m_pSprite.IsFlipX)
						{
							this.m_pVertexData[l].texCoords.u = cCPoint1.x + cCPoint.x - this.m_pVertexData[l].texCoords.u;
						}
						if (this.m_pSprite.IsFlipY)
						{
							this.m_pVertexData[l].texCoords.v = cCPoint1.y + cCPoint.y - this.m_pVertexData[l].texCoords.v;
						}
					}
				}
			}
			this.m_pVertexData[this.m_nVertexDataCount - 1].texCoords = new ccTex2F(cCPoint5.x, cCPoint5.y);
			this.m_pVertexData[this.m_nVertexDataCount - 1].vertices = this.vertexFromTexCoord(cCPoint5);
			if (this.m_pSprite.IsFlipX || this.m_pSprite.IsFlipY)
			{
				if (this.m_pSprite.IsFlipX)
				{
					this.m_pVertexData[this.m_nVertexDataCount - 1].texCoords.u = cCPoint1.x + cCPoint.x - this.m_pVertexData[this.m_nVertexDataCount - 1].texCoords.u;
				}
				if (this.m_pSprite.IsFlipY)
				{
					this.m_pVertexData[this.m_nVertexDataCount - 1].texCoords.v = cCPoint1.y + cCPoint.y - this.m_pVertexData[this.m_nVertexDataCount - 1].texCoords.v;
				}
			}
		}

		protected ccVertex2F vertexFromTexCoord(CCPoint texCoord)
		{
			CCPoint cCPoint;
			ccVertex2F _ccVertex2F = new ccVertex2F();
			if (this.m_pSprite.Texture == null)
			{
				cCPoint = new CCPoint(0f, 0f);
			}
			else
			{
				float single = Math.Max(this.m_pSprite.quad.br.texCoords.u, this.m_pSprite.quad.bl.texCoords.u);
				float single1 = Math.Min(this.m_pSprite.quad.br.texCoords.u, this.m_pSprite.quad.bl.texCoords.u);
				float single2 = Math.Max(this.m_pSprite.quad.tl.texCoords.v, this.m_pSprite.quad.bl.texCoords.v);
				float single3 = Math.Min(this.m_pSprite.quad.tl.texCoords.v, this.m_pSprite.quad.bl.texCoords.v);
				CCPoint cCPoint1 = new CCPoint(single, single2);
				CCPoint cCPoint2 = new CCPoint(single1, single3);
				CCSize cCSize = new CCSize(this.m_pSprite.quad.br.vertices.x - this.m_pSprite.quad.bl.vertices.x, this.m_pSprite.quad.tl.vertices.y - this.m_pSprite.quad.bl.vertices.y);
				cCPoint = new CCPoint(cCSize.width * (texCoord.x - cCPoint2.x) / (cCPoint1.x - cCPoint2.x), cCSize.height * (1f - (texCoord.y - cCPoint2.y) / (cCPoint1.y - cCPoint2.y)));
			}
			_ccVertex2F.x = cCPoint.x;
			_ccVertex2F.y = cCPoint.y;
			return _ccVertex2F;
		}
	}
}