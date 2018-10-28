using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cocos2d
{
	public class CCRenderTexture : CCNode
	{
		protected CCSprite m_pSprite;

		protected int m_uFBO;

		protected int m_nOldFBO;

		protected CCTexture2D m_pTexture;

		protected int m_ePixelFormat;

		private RenderTarget2D m_RenderTarget2D;

		public CCSprite Sprite
		{
			get
			{
				return this.m_pSprite;
			}
			set
			{
				this.m_pSprite = value;
			}
		}

		public CCRenderTexture()
		{
			this.m_ePixelFormat = 1;
		}

		public void begin()
		{
			CCSize contentSizeInPixels = this.m_pTexture.ContentSizeInPixels;
			CCSize cCSize = CCDirector.sharedDirector().displaySizeInPixels;
			float single = cCSize.width / contentSizeInPixels.width;
			float single1 = cCSize.height / contentSizeInPixels.height;
			CCApplication.sharedApplication().GraphicsDevice.SetRenderTarget(this.m_RenderTarget2D);
		}

		public void beginWithClear(float r, float g, float b, float a)
		{
			this.begin();
			CCApplication.sharedApplication().GraphicsDevice.Clear(new Color(r, g, b, a));
		}

		public void clear(float r, float g, float b, float a)
		{
			this.beginWithClear(r, g, b, a);
			this.end();
		}

		public void end(bool bIsTOCacheTexture)
		{
		}

		public void end()
		{
			CCApplication.sharedApplication().GraphicsDevice.SetRenderTarget(null);
		}

		public void endToLua()
		{
			this.end();
		}

		public CCData getUIImageAsDataFromBuffer(int format)
		{
			return null;
		}

		public bool getUIImageFromBuffer(CCTexture2D pImage, int x, int y, int nWidth, int nHeight)
		{
			if (pImage == null || this.m_pTexture == null)
			{
				return false;
			}
			CCSize contentSizeInPixels = this.m_pTexture.ContentSizeInPixels;
			int num = (int)contentSizeInPixels.width;
			int num1 = (int)contentSizeInPixels.height;
			if (x < 0 || x >= num || y < 0 || y >= num1)
			{
				return false;
			}
			if (nWidth < 0 || nHeight < 0 || nWidth == 0 && nHeight != 0 || nHeight == 0 && nWidth != 0)
			{
				return false;
			}
			int num2 = nWidth;
			int num3 = nHeight;
			if (nWidth == 0)
			{
				num2 = num;
			}
			if (nHeight == 0)
			{
				num3 = num1;
			}
			num2 = (x + num2 > num ? num - x : num2);
			num3 = (y + num3 > num1 ? num1 - y : num3);
			bool flag = false;
			while (new byte[num2 * num3 * 4] != null)
			{
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				num4 = (int)ccUtils.ccNextPOT((long)num);
				num5 = (int)ccUtils.ccNextPOT((long)num1);
				if (num4 == 0 || num5 == 0 || num4 > num6 || num5 > num6 || new byte[num4 * num5 * 4] == null)
				{
					break;
				}
				this.begin();
				this.end(false);
			}
			return flag;
		}

		public bool initWithWidthAndHeight(int w, int h, CCTexture2DPixelFormat eFormat)
		{
			if (CCConfiguration.sharedConfiguration().getGlesVersion() <= CCGlesVersion.GLES_VER_1_0)
			{
				return false;
			}
			w = w * (int)CCDirector.sharedDirector().ContentScaleFactor;
			h = h * (int)CCDirector.sharedDirector().ContentScaleFactor;
			ccUtils.ccNextPOT((long)w);
			ccUtils.ccNextPOT((long)h);
			this.m_pTexture = new CCTexture2D();
			CCApplication cCApplication = CCApplication.sharedApplication();
			this.m_RenderTarget2D = new RenderTarget2D(cCApplication.GraphicsDevice, w, h);
			cCApplication.GraphicsDevice.SetRenderTarget(this.m_RenderTarget2D);
			cCApplication.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
			this.m_pTexture.initWithTexture(this.m_RenderTarget2D);
			this.m_pSprite = CCSprite.spriteWithTexture(this.m_pTexture);
			this.addChild(this.m_pSprite);
			ccBlendFunc _ccBlendFunc = new ccBlendFunc()
			{
				src = 1,
				dst = 771
			};
			this.m_pSprite.BlendFunc = _ccBlendFunc;
			return true;
		}

		public static CCRenderTexture renderTextureWithWidthAndHeight(int w, int h, CCTexture2DPixelFormat eFormat)
		{
			CCRenderTexture cCRenderTexture = new CCRenderTexture();
			if (cCRenderTexture.initWithWidthAndHeight(w, h, eFormat))
			{
				return cCRenderTexture;
			}
			return null;
		}

		public static CCRenderTexture renderTextureWithWidthAndHeight(int w, int h)
		{
			CCRenderTexture cCRenderTexture = new CCRenderTexture();
			if (cCRenderTexture.initWithWidthAndHeight(w, h, CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGBA8888))
			{
				return cCRenderTexture;
			}
			return null;
		}

		public bool saveBuffer(string szFilePath, int x, int y, int nWidth, int nHeight)
		{
			throw new NotFiniteNumberException();
		}

		public bool saveBuffer(int format, string name, int x, int y, int nWidth, int nHeight)
		{
			throw new NotFiniteNumberException();
		}
	}
}