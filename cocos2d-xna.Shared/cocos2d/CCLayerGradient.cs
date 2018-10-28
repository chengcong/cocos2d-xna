using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cocos2d
{
	public class CCLayerGradient : CCLayerColor
	{
		private ccColor3B m_endColor;

		private byte m_cStartOpacity;

		private byte m_cEndOpacity;

		private CCPoint m_AlongVector;

		private bool m_bCompressedInterpolation;

		public ccColor3B EndColor
		{
			get
			{
				return this.m_endColor;
			}
			set
			{
				this.m_endColor = value;
				this.updateColor();
			}
		}

		public byte EndOpacity
		{
			get
			{
				return this.m_cEndOpacity;
			}
			set
			{
				this.m_cEndOpacity = value;
				this.updateColor();
			}
		}

		public bool IsCompressedInterpolation
		{
			get
			{
				return this.m_bCompressedInterpolation;
			}
			set
			{
				this.m_bCompressedInterpolation = value;
				this.updateColor();
			}
		}

		public ccColor3B StartColor
		{
			get
			{
				return this.m_tColor;
			}
			set
			{
				base.Color = value;
			}
		}

		public byte StartOpacity
		{
			get
			{
				return this.m_cStartOpacity;
			}
			set
			{
				this.m_cStartOpacity = value;
				this.updateColor();
			}
		}

		public CCPoint Vector
		{
			get
			{
				return this.m_AlongVector;
			}
			set
			{
				this.m_AlongVector = value;
				this.updateColor();
			}
		}

		public CCLayerGradient()
		{
		}

		public virtual bool initWithColor(ccColor4B start, ccColor4B end)
		{
			return this.initWithColor(start, end, new CCPoint(0f, -1f));
		}

		public virtual bool initWithColor(ccColor4B start, ccColor4B end, CCPoint v)
		{
			this.m_endColor = new ccColor3B()
			{
				r = end.r,
				g = end.g,
				b = end.b
			};
			this.m_cEndOpacity = end.a;
			this.m_cStartOpacity = start.a;
			this.m_AlongVector = v;
			this.m_bCompressedInterpolation = true;
			return base.initWithColor(new ccColor4B(start.r, start.g, start.b, 255));
		}

		public static CCLayerGradient layerWithColor(ccColor4B start, ccColor4B end)
		{
			CCLayerGradient cCLayerGradient = new CCLayerGradient();
			if (cCLayerGradient.initWithColor(start, end))
			{
				return cCLayerGradient;
			}
			return null;
		}

		public static CCLayerGradient layerWithColor(ccColor4B start, ccColor4B end, CCPoint v)
		{
			CCLayerGradient cCLayerGradient = new CCLayerGradient();
			if (cCLayerGradient.initWithColor(start, end, v))
			{
				return cCLayerGradient;
			}
			return null;
		}

		public static new CCLayerGradient node()
		{
			CCLayerGradient cCLayerGradient = new CCLayerGradient();
			if (cCLayerGradient.init())
			{
				return cCLayerGradient;
			}
			return null;
		}

		protected override void updateColor()
		{
			base.updateColor();
			float single = CCPointExtension.ccpLength(this.m_AlongVector);
			if (single == 0f)
			{
				return;
			}
			double num = Math.Sqrt(2);
			CCPoint cCPoint = new CCPoint(this.m_AlongVector.x / single, this.m_AlongVector.y / single);
			if (this.m_bCompressedInterpolation)
			{
				float single1 = 1f / (Math.Abs(cCPoint.x) + Math.Abs(cCPoint.y));
				cCPoint = CCPointExtension.ccpMult(cCPoint, single1 * (float)num);
			}
			float mCOpacity = (float)this.m_cOpacity / 255f;
			ccColor4B _ccColor4B = new ccColor4B()
			{
				r = this.m_tColor.r,
				g = this.m_tColor.g,
				b = this.m_tColor.b,
				a = (byte)((float)this.m_cStartOpacity * mCOpacity)
			};
			ccColor4B _ccColor4B1 = _ccColor4B;
			ccColor4B _ccColor4B2 = new ccColor4B()
			{
				r = this.m_endColor.r,
				g = this.m_endColor.g,
				b = this.m_endColor.b,
				a = (byte)((float)this.m_cEndOpacity * mCOpacity)
			};
			ccColor4B _ccColor4B3 = _ccColor4B2;
			this.m_pSquareColors[0].r = (byte)((double)_ccColor4B3.r + (double)(_ccColor4B1.r - _ccColor4B3.r) * ((num + (double)cCPoint.x + (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[0].g = (byte)((double)_ccColor4B3.g + (double)(_ccColor4B1.g - _ccColor4B3.g) * ((num + (double)cCPoint.x + (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[0].b = (byte)((double)_ccColor4B3.b + (double)(_ccColor4B1.b - _ccColor4B3.b) * ((num + (double)cCPoint.x + (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[0].a = (byte)((double)_ccColor4B3.a + (double)(_ccColor4B1.a - _ccColor4B3.a) * ((num + (double)cCPoint.x + (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[1].r = (byte)((double)_ccColor4B3.r + (double)(_ccColor4B1.r - _ccColor4B3.r) * ((num - (double)cCPoint.x + (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[1].g = (byte)((double)_ccColor4B3.g + (double)(_ccColor4B1.g - _ccColor4B3.g) * ((num - (double)cCPoint.x + (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[1].b = (byte)((double)_ccColor4B3.b + (double)(_ccColor4B1.b - _ccColor4B3.b) * ((num - (double)cCPoint.x + (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[1].a = (byte)((double)_ccColor4B3.a + (double)(_ccColor4B1.a - _ccColor4B3.a) * ((num - (double)cCPoint.x + (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[2].r = (byte)((double)_ccColor4B3.r + (double)(_ccColor4B1.r - _ccColor4B3.r) * ((num + (double)cCPoint.x - (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[2].g = (byte)((double)_ccColor4B3.g + (double)(_ccColor4B1.g - _ccColor4B3.g) * ((num + (double)cCPoint.x - (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[2].b = (byte)((double)_ccColor4B3.b + (double)(_ccColor4B1.b - _ccColor4B3.b) * ((num + (double)cCPoint.x - (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[2].a = (byte)((double)_ccColor4B3.a + (double)(_ccColor4B1.a - _ccColor4B3.a) * ((num + (double)cCPoint.x - (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[3].r = (byte)((double)_ccColor4B3.r + (double)(_ccColor4B1.r - _ccColor4B3.r) * ((num - (double)cCPoint.x - (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[3].g = (byte)((double)_ccColor4B3.g + (double)(_ccColor4B1.g - _ccColor4B3.g) * ((num - (double)cCPoint.x - (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[3].b = (byte)((double)_ccColor4B3.b + (double)(_ccColor4B1.b - _ccColor4B3.b) * ((num - (double)cCPoint.x - (double)cCPoint.y) / (2 * num)));
			this.m_pSquareColors[3].a = (byte)((double)_ccColor4B3.a + (double)(_ccColor4B1.a - _ccColor4B3.a) * ((num - (double)cCPoint.x - (double)cCPoint.y) / (2 * num)));
			this.vertices[0].Color = new Microsoft.Xna.Framework.Color((int)this.m_pSquareColors[0].r, (int)this.m_pSquareColors[0].g, (int)this.m_pSquareColors[0].b, (int)this.m_pSquareColors[0].a);
			this.vertices[1].Color = new Microsoft.Xna.Framework.Color((int)this.m_pSquareColors[1].r, (int)this.m_pSquareColors[1].g, (int)this.m_pSquareColors[1].b, (int)this.m_pSquareColors[1].a);
			this.vertices[2].Color = new Microsoft.Xna.Framework.Color((int)this.m_pSquareColors[2].r, (int)this.m_pSquareColors[2].g, (int)this.m_pSquareColors[2].b, (int)this.m_pSquareColors[2].a);
			this.vertices[3].Color = new Microsoft.Xna.Framework.Color((int)this.m_pSquareColors[3].r, (int)this.m_pSquareColors[3].g, (int)this.m_pSquareColors[3].b, (int)this.m_pSquareColors[3].a);
		}
	}
}