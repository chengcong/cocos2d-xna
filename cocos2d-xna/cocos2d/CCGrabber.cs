using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cocos2d
{
	public class CCGrabber : CCObject
	{
		protected uint m_fbo;

		protected int m_oldFBO;

		protected CCGlesVersion m_eGlesVersion;

		protected RenderTarget2D m_RenderTarget2D;

		public CCGrabber()
		{
			this.m_eGlesVersion = CCConfiguration.sharedConfiguration().getGlesVersion();
			CCGlesVersion mEGlesVersion = this.m_eGlesVersion;
		}

		public void afterRender(ref CCTexture2D pTexture)
		{
			if (this.m_eGlesVersion <= CCGlesVersion.GLES_VER_1_0)
			{
				return;
			}
			CCApplication.sharedApplication().GraphicsDevice.SetRenderTarget(null);
			pTexture.Texture = this.m_RenderTarget2D;
		}

		public void beforeRender(ref CCTexture2D pTexture)
		{
			if (this.m_eGlesVersion <= CCGlesVersion.GLES_VER_1_0)
			{
				return;
			}
			CCApplication.sharedApplication().GraphicsDevice.SetRenderTarget(this.m_RenderTarget2D);
		}

		public void grab(ref CCTexture2D pTexture)
		{
			if (this.m_eGlesVersion <= CCGlesVersion.GLES_VER_1_0)
			{
				return;
			}
			this.m_RenderTarget2D = new RenderTarget2D(CCApplication.sharedApplication().GraphicsDevice, (int)pTexture.ContentSizeInPixels.width, (int)pTexture.ContentSizeInPixels.height);
			pTexture.Texture = this.m_RenderTarget2D;
		}
	}
}