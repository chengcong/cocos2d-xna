using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCLayerColor : CCLayer, ICCRGBAProtocol, ICCBlendProtocol
	{
		protected ccVertex2F[] m_pSquareVertices = new ccVertex2F[4];

		protected ccColor4B[] m_pSquareColors = new ccColor4B[4];

		protected VertexPositionColor[] vertices = new VertexPositionColor[4];

		private short[] indexes = new short[6];

		protected byte m_cOpacity;

		protected ccColor3B m_tColor;

		protected ccBlendFunc m_tBlendFunc;

		public virtual ccBlendFunc BlendFunc
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

		public virtual ccColor3B Color
		{
			get
			{
				return this.m_tColor;
			}
			set
			{
				this.m_tColor = value;
				this.updateColor();
			}
		}

		public override CCSize contentSize
		{
			get
			{
				return base.contentSize;
			}
			set
			{
				float contentScaleFactor = CCDirector.sharedDirector().ContentScaleFactor;
				this.m_pSquareVertices[1].x = value.width * contentScaleFactor;
				this.m_pSquareVertices[2].y = value.height * contentScaleFactor;
				this.m_pSquareVertices[3].x = value.width * contentScaleFactor;
				this.m_pSquareVertices[3].y = value.height * contentScaleFactor;
				this.vertices[0].Position = new Vector3(0f, value.height * contentScaleFactor, 0f);
				this.vertices[1].Position = new Vector3(value.width * contentScaleFactor, value.height * contentScaleFactor, 0f);
				this.vertices[2].Position = new Vector3(0f, 0f, 0f);
				this.vertices[3].Position = new Vector3(value.width * contentScaleFactor, 0f, 0f);
				base.contentSize = value;
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

		public virtual byte Opacity
		{
			get
			{
				return this.m_cOpacity;
			}
			set
			{
				this.m_cOpacity = value;
				this.updateColor();
			}
		}

		public CCLayerColor()
		{
			this.m_cOpacity = 0;
			this.m_tColor = new ccColor3B(0, 0, 0);
			this.m_tBlendFunc = new ccBlendFunc()
			{
				src = 1,
				dst = 771
			};
		}

		public void changeHeight(float h)
		{
			this.contentSize = new CCSize(this.m_tContentSize.width, h);
		}

		public void changeWidth(float w)
		{
			this.contentSize = new CCSize(w, this.m_tContentSize.height);
		}

		public void changeWidthAndHeight(float w, float h)
		{
			this.contentSize = new CCSize(w, h);
		}

		public virtual ICCRGBAProtocol convertToRGBAProtocol()
		{
			return this;
		}

		public override void draw()
		{
			base.draw();
			CCApplication mCOpacity = CCApplication.sharedApplication();
			CCDirector.sharedDirector().getWinSize();
			mCOpacity.basicEffect.VertexColorEnabled = true;
			mCOpacity.basicEffect.TextureEnabled = false;
			mCOpacity.basicEffect.Alpha = (float)this.m_cOpacity / 255f;
			VertexElement[] vertexElement = new VertexElement[] { new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0), new VertexElement(12, VertexElementFormat.Vector4, VertexElementUsage.Color, 0) };
			VertexDeclaration vertexDeclaration = new VertexDeclaration(vertexElement);
			foreach (EffectPass pass in mCOpacity.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				mCOpacity.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, this.vertices, 0, 2);
			}
			mCOpacity.basicEffect.Alpha = 1f;
		}

		public virtual bool initWithColor(ccColor4B color)
		{
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.initWithColorWidthHeight(color, winSize.width, winSize.height);
			return true;
		}

		public virtual bool initWithColorWidthHeight(ccColor4B color, float width, float height)
		{
			this.m_tBlendFunc.src = 1;
			this.m_tBlendFunc.dst = 771;
			this.m_tColor.r = color.r;
			this.m_tColor.g = color.g;
			this.m_tColor.b = color.b;
			this.m_cOpacity = color.a;
			for (int i = 0; i < (int)this.m_pSquareVertices.Length; i++)
			{
				this.m_pSquareVertices[i] = new ccVertex2F();
				this.m_pSquareVertices[i].x = 0f;
				this.m_pSquareVertices[i].y = 0f;
				this.vertices[i] = new VertexPositionColor();
			}
			this.indexes[0] = 0;
			this.indexes[0] = 1;
			this.indexes[0] = 2;
			this.indexes[0] = 2;
			this.indexes[0] = 1;
			this.indexes[0] = 3;
			this.updateColor();
			this.contentSize = new CCSize(width, height);
			return true;
		}

		public static CCLayerColor layerWithColor(ccColor4B color)
		{
			CCLayerColor cCLayerColor = new CCLayerColor();
			if (cCLayerColor.initWithColor(color))
			{
				return cCLayerColor;
			}
			return null;
		}

		public static CCLayerColor layerWithColorWidthHeight(ccColor4B color, float width, float height)
		{
			CCLayerColor cCLayerColor = new CCLayerColor();
			if (cCLayerColor.initWithColorWidthHeight(color, width, height))
			{
				return cCLayerColor;
			}
			return null;
		}

		public static new CCLayerColor node()
		{
			CCLayerColor cCLayerColor = new CCLayerColor();
			if (cCLayerColor.init())
			{
				return cCLayerColor;
			}
			return null;
		}

		protected virtual void updateColor()
		{
			for (int i = 0; i < 4; i++)
			{
				this.m_pSquareColors[i] = new ccColor4B();
				this.m_pSquareColors[i].r = this.m_tColor.r;
				this.m_pSquareColors[i].g = this.m_tColor.g;
				this.m_pSquareColors[i].b = this.m_tColor.b;
				this.m_pSquareColors[i].a = this.m_cOpacity;
				this.vertices[i].Color = new Microsoft.Xna.Framework.Color((int)this.m_tColor.r, (int)this.m_tColor.g, (int)this.m_tColor.b, (int)this.m_cOpacity);
			}
		}
	}
}