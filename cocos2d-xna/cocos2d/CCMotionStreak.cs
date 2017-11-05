using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCMotionStreak : CCNode, ICCTextureProtocol, ICCBlendProtocol, ICCRGBAProtocol
	{
		protected bool m_bFastMode;

		protected bool m_bStartingPositionInitialized;

		private CCTexture2D m_pTexture;

		private ccBlendFunc m_tBlendFunc;

		private CCPoint m_tPositionR;

		private ccColor3B m_tColor;

		private float m_fStroke;

		private float m_fFadeDelta;

		private float m_fMinSeg;

		private int m_uMaxPoints;

		private int m_uNuPoints;

		private int m_uPreviousNuPoints;

		private CCPoint[] m_pPointVertexes;

		private float[] m_pPointState;

		private ccVertex2F[] m_pVertices;

		private ccColor4B[] m_pColor;

		private ccTex2F[] m_pTexCoords;

		private byte m_tOpacity;

		private VertexPositionColorTexture[] m_pVerticesPCT;

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

		public ccColor3B Color
		{
			get
			{
				return this.m_tColor.copy();
			}
			set
			{
				this.m_tColor = value.copy();
			}
		}

		public bool IsOpacityModifyRGB
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public byte Opacity
		{
			get
			{
				return this.m_tOpacity;
			}
			set
			{
				this.m_tOpacity = value;
			}
		}

		public override CCPoint position
		{
			set
			{
				this.m_bStartingPositionInitialized = true;
				base.position = value;
				this.m_tPositionR = value;
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
				if (this.m_pTexture != value)
				{
					this.m_pTexture = value;
				}
			}
		}

		public CCMotionStreak()
		{
			this.m_tBlendFunc = new ccBlendFunc()
			{
				src = 0,
				dst = 771
			};
			this.m_tOpacity = 255;
		}

		public static CCMotionStreak create(float fade, float minSeg, float stroke, ccColor3B color, string path)
		{
			CCMotionStreak cCMotionStreak = new CCMotionStreak();
			if (cCMotionStreak.initWithFade(fade, minSeg, stroke, color, path))
			{
				return cCMotionStreak;
			}
			return null;
		}

		public static CCMotionStreak create(float fade, float minSeg, float stroke, ccColor3B color, CCTexture2D texture)
		{
			CCMotionStreak cCMotionStreak = new CCMotionStreak();
			if (cCMotionStreak.initWithFade(fade, minSeg, stroke, color, texture))
			{
				return cCMotionStreak;
			}
			return null;
		}

		public override void draw()
		{
			base.draw();
			if (this.m_uNuPoints <= 1)
			{
				return;
			}
			CCApplication opacity = CCApplication.sharedApplication();
			float alpha = opacity.basicEffect.Alpha;
			CCDirector.sharedDirector().getWinSize();
			opacity.basicEffect.VertexColorEnabled = true;
			opacity.basicEffect.TextureEnabled = true;
			opacity.basicEffect.Alpha = (float)this.Opacity / 255f;
			opacity.basicEffect.Texture = this.Texture.Texture;
			foreach (EffectPass pass in opacity.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				opacity.GraphicsDevice.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleStrip, this.m_pVerticesPCT, 0, this.m_uNuPoints * 2);
			}
			opacity.basicEffect.Alpha = alpha;
		}

		public bool initWithFade(float fade, float minSeg, float stroke, ccColor3B color, string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(path, "Path is required to create the texture2d.");
			}
			CCTexture2D cCTexture2D = CCTextureCache.sharedTextureCache().addImage(path);
			return this.initWithFade(fade, minSeg, stroke, color, cCTexture2D);
		}

		public bool initWithFade(float fade, float minSeg, float stroke, ccColor3B color, CCTexture2D texture)
		{
			this.position = CCPoint.CCPointZero;
			this.anchorPoint = CCPoint.CCPointZero;
			this.isRelativeAnchorPoint = false;
			this.m_bStartingPositionInitialized = false;
			this.m_tPositionR = CCPoint.CCPointZero;
			this.m_bFastMode = true;
			this.m_fMinSeg = (minSeg == -1f ? stroke / 5f : minSeg);
			CCMotionStreak mFMinSeg = this;
			mFMinSeg.m_fMinSeg = mFMinSeg.m_fMinSeg * this.m_fMinSeg;
			this.m_fStroke = stroke;
			this.m_fFadeDelta = 1f / fade;
			this.m_uMaxPoints = (int)(fade * 60f) + 2;
			this.m_uNuPoints = 0;
			this.m_pPointState = new float[this.m_uMaxPoints];
			this.m_pPointVertexes = new CCPoint[this.m_uMaxPoints];
			this.m_pVertices = new ccVertex2F[this.m_uMaxPoints * 2];
			this.m_pTexCoords = new ccTex2F[this.m_uMaxPoints * 2];
			this.m_pColor = new ccColor4B[this.m_uMaxPoints * 2];
			this.m_tBlendFunc.src = 770;
			this.m_tBlendFunc.dst = 771;
			this.Texture = texture;
			this.Color = color;
			CCMotionStreak cCMotionStreak = this;
			base.schedule(new SEL_SCHEDULE(cCMotionStreak.update));
			return true;
		}

		public bool isFastMode()
		{
			return this.m_bFastMode;
		}

		public bool isStartingPositionInitialized()
		{
			return this.m_bStartingPositionInitialized;
		}

		private void reset()
		{
			this.m_uNuPoints = 0;
		}

		public void setFastMode(bool bFastMode)
		{
			this.m_bFastMode = bFastMode;
		}

		public void setStartingPositionInitialized(bool bStartingPositionInitialized)
		{
			this.m_bStartingPositionInitialized = bStartingPositionInitialized;
		}

		public static CCMotionStreak streakWithFade(float fade, float minSeg, float stroke, ccColor3B color, string path)
		{
			return CCMotionStreak.create(fade, minSeg, stroke, color, path);
		}

		public static CCMotionStreak streakWithFade(float fade, float minSeg, float stroke, ccColor3B color, CCTexture2D texture)
		{
			return CCMotionStreak.create(fade, minSeg, stroke, color, texture);
		}

		public void tintWithColor(ccColor3B colors)
		{
			this.Color = colors;
			for (int i = 0; i < this.m_uNuPoints * 2; i++)
			{
				this.m_pColor[i] = new ccColor4B(colors);
			}
		}

		public override void update(float delta)
		{
			int num;
			int i;
			VertexPositionColorTexture vertexPositionColorTexture;
			VertexPositionColorTexture vertexPositionColorTexture1;
			if (!this.m_bStartingPositionInitialized)
			{
				return;
			}
			if (this.m_pVerticesPCT == null)
			{
				this.m_pVerticesPCT = new VertexPositionColorTexture[(this.m_uNuPoints + 1) * 2];
			}
			if (this.m_uNuPoints * 2 > (int)this.m_pVerticesPCT.Length)
			{
				VertexPositionColorTexture[] vertexPositionColorTextureArray = new VertexPositionColorTexture[(this.m_uNuPoints + 1) * 2];
				this.m_pVerticesPCT.CopyTo(vertexPositionColorTextureArray, 0);
				this.m_pVerticesPCT = vertexPositionColorTextureArray;
			}
			delta = delta * this.m_fFadeDelta;
			int num1 = 0;
			for (i = 0; i < this.m_uNuPoints; i++)
			{
				VertexPositionColorTexture mPVerticesPCT = this.m_pVerticesPCT[i];
				VertexPositionColorTexture mPVerticesPCT1 = this.m_pVerticesPCT[i * 2];
				this.m_pPointState[i] = this.m_pPointState[i] - delta;
				if (this.m_pPointState[i] > 0f)
				{
					int num2 = i - num1;
					if (num1 <= 0)
					{
						num = num2 * 2;
					}
					else
					{
						this.m_pPointState[num2] = this.m_pPointState[i];
						this.m_pPointVertexes[num2] = new CCPoint(this.m_pPointVertexes[i]);
						int num3 = i * 2;
						num = num2 * 2;
						this.m_pVertices[num] = new ccVertex2F(this.m_pVertices[num3]);
						this.m_pVertices[num + 1] = new ccVertex2F(this.m_pVertices[num3 + 1]);
						this.m_pColor[num] = new ccColor4B(this.m_pColor[num3]);
						this.m_pColor[num + 1] = new ccColor4B(this.m_pColor[num3 + 1]);
						this.m_pVerticesPCT[num] = this.m_pVerticesPCT[num3];
						this.m_pVerticesPCT[num + 1] = this.m_pVerticesPCT[num3 + 1];
					}
					byte mPPointState = (byte)(this.m_pPointState[num2] * 255f);
					this.m_pColor[num].a = mPPointState;
					this.m_pColor[num + 1].a = mPPointState;
					if (this.m_pVertices[num] != null)
					{
						vertexPositionColorTexture = new VertexPositionColorTexture(this.m_pVertices[num].ToVector3(), this.m_pColor[num].XNAColor, this.m_pTexCoords[num].ToVector2());
						this.m_pVerticesPCT[num] = vertexPositionColorTexture;
					}
					if (this.m_pVertices[num + 1] != null)
					{
						vertexPositionColorTexture = new VertexPositionColorTexture(this.m_pVertices[num + 1].ToVector3(), this.m_pColor[num + 1].XNAColor, this.m_pTexCoords[num + 1].ToVector2());
						this.m_pVerticesPCT[num + 1] = vertexPositionColorTexture;
					}
				}
				else
				{
					num1++;
				}
			}
			CCMotionStreak mUNuPoints = this;
			mUNuPoints.m_uNuPoints = mUNuPoints.m_uNuPoints - num1;
			bool flag = true;
			if (this.m_uNuPoints >= this.m_uMaxPoints)
			{
				flag = false;
			}
			else if (this.m_uNuPoints > 0)
			{
				bool flag1 = this.m_pPointVertexes[this.m_uNuPoints - 1].DistanceSQ(this.m_tPositionR) < this.m_fMinSeg;
				if (flag1 || (this.m_uNuPoints == 1 ? false : this.m_pPointVertexes[this.m_uNuPoints - 2].DistanceSQ(this.m_tPositionR) < this.m_fMinSeg * 2f))
				{
					flag = false;
				}
			}
			if (flag)
			{
				this.m_pPointVertexes[this.m_uNuPoints] = new CCPoint(this.m_tPositionR);
				this.m_pPointState[this.m_uNuPoints] = 1f;
				int mUNuPoints1 = this.m_uNuPoints * 2;
				this.m_pColor[mUNuPoints1] = new ccColor4B(this.m_tColor);
				this.m_pColor[mUNuPoints1 + 1] = new ccColor4B(this.m_tColor);
				this.m_pColor[mUNuPoints1].a = 255;
				this.m_pColor[mUNuPoints1 + 1].a = 255;
				if (this.m_uNuPoints > 0 && this.m_bFastMode)
				{
					if (this.m_uNuPoints <= 1)
					{
						CCVertex.LineToPolygon(this.m_pPointVertexes, this.m_fStroke, this.m_pVertices, 0, 2);
					}
					else
					{
						CCVertex.LineToPolygon(this.m_pPointVertexes, this.m_fStroke, this.m_pVertices, this.m_uNuPoints, 1);
					}
				}
				CCMotionStreak cCMotionStreak = this;
				cCMotionStreak.m_uNuPoints = cCMotionStreak.m_uNuPoints + 1;
			}
			if (!this.m_bFastMode)
			{
				CCVertex.LineToPolygon(this.m_pPointVertexes, this.m_fStroke, this.m_pVertices, 0, this.m_uNuPoints);
			}
			if (this.m_uPreviousNuPoints != this.m_uNuPoints)
			{
				if (this.m_uNuPoints > this.m_uPreviousNuPoints)
				{
					int length = (this.m_uNuPoints + 1) * 2;
					if (length < (int)this.m_pVerticesPCT.Length)
					{
						length = (int)this.m_pVerticesPCT.Length;
					}
					VertexPositionColorTexture[] vertexPositionColorTextureArray1 = new VertexPositionColorTexture[length];
					this.m_pVerticesPCT.CopyTo(vertexPositionColorTextureArray1, 0);
					this.m_pVerticesPCT = vertexPositionColorTextureArray1;
				}
				float single = 1f / (float)this.m_uNuPoints;
				for (i = 0; i < this.m_uNuPoints; i++)
				{
					this.m_pTexCoords[i * 2] = new ccTex2F(0f, single * (float)i);
					this.m_pTexCoords[i * 2 + 1] = new ccTex2F(1f, single * (float)i);
					if (this.m_pVertices[i * 2] != null)
					{
						vertexPositionColorTexture1 = new VertexPositionColorTexture(this.m_pVertices[i * 2].ToVector3(), this.m_pColor[i * 2].XNAColor, this.m_pTexCoords[i * 2].ToVector2());
						this.m_pVerticesPCT[i * 2] = vertexPositionColorTexture1;
					}
					if (this.m_pVertices[i * 2 + 1] != null)
					{
						vertexPositionColorTexture1 = new VertexPositionColorTexture(this.m_pVertices[i * 2 + 1].ToVector3(), this.m_pColor[i * 2 + 1].XNAColor, this.m_pTexCoords[i * 2 + 1].ToVector2());
						this.m_pVerticesPCT[i * 2 + 1] = vertexPositionColorTexture1;
					}
				}
				this.m_uPreviousNuPoints = this.m_uNuPoints;
			}
		}
	}
}