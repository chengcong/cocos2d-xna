using System;

namespace cocos2d
{
	public class CCGridBase : CCObject
	{
		protected CCTexture2D m_pTexture;

		protected CCGrabber m_pGrabber;

		protected bool m_bActive;

		protected int m_nReuseGrid;

		protected ccGridSize m_sGridSize;

		protected CCPoint m_obStep;

		protected bool m_bIsTextureFlipped;

		public bool Active
		{
			get
			{
				return this.m_bActive;
			}
			set
			{
				this.m_bActive = value;
			}
		}

		public ccGridSize GridSize
		{
			get
			{
				return this.m_sGridSize;
			}
			set
			{
				this.m_sGridSize = value;
			}
		}

		public int ReuseGrid
		{
			get
			{
				return this.m_nReuseGrid;
			}
			set
			{
				this.m_nReuseGrid = value;
			}
		}

		public CCPoint Step
		{
			get
			{
				return this.m_obStep;
			}
			set
			{
				this.m_obStep = value;
			}
		}

		public bool TextureFlipped
		{
			get
			{
				return this.m_bIsTextureFlipped;
			}
			set
			{
				this.m_bIsTextureFlipped = value;
			}
		}

		public CCGridBase()
		{
		}

		public void afterDraw(CCNode pTarget)
		{
			this.m_pGrabber.afterRender(ref this.m_pTexture);
			this.blit();
		}

		protected void applyLandscape()
		{
			CCDirector cCDirector = CCDirector.sharedDirector();
			CCSize cCSize = cCDirector.displaySizeInPixels;
			float single = cCSize.width / 2f;
			float single1 = cCSize.height / 2f;
			switch (cCDirector.deviceOrientation)
			{
				default:
				{
					return;
				}
			}
		}

		public void beforeDraw()
		{
			this.m_pGrabber.beforeRender(ref this.m_pTexture);
		}

		public virtual void blit()
		{
		}

		public virtual void calculateVertexPoints()
		{
		}

		public ulong ccNextPOT(ulong x)
		{
			x = x - (long)1;
			x = x | x >> 1;
			x = x | x >> 2;
			x = x | x >> 4;
			x = x | x >> 8;
			x = x | x >> 16;
			return x + (long)1;
		}

		public static CCGridBase gridWithSize(ccGridSize gridSize, CCTexture2D texture, bool flipped)
		{
			CCGridBase cCGridBase = new CCGridBase();
			if (cCGridBase.initWithSize(gridSize, texture, flipped))
			{
				return cCGridBase;
			}
			return null;
		}

		public static CCGridBase gridWithSize(ccGridSize gridSize)
		{
			CCGridBase cCGridBase = new CCGridBase();
			if (cCGridBase.initWithSize(gridSize))
			{
				return cCGridBase;
			}
			return null;
		}

		public bool initWithSize(ccGridSize gridSize, CCTexture2D pTexture, bool bFlipped)
		{
			bool flag = true;
			this.m_bActive = false;
			this.m_nReuseGrid = 0;
			this.m_sGridSize = gridSize;
			this.m_pTexture = pTexture;
			this.m_bIsTextureFlipped = bFlipped;
			CCSize contentSizeInPixels = this.m_pTexture.ContentSizeInPixels;
			this.m_obStep = new CCPoint()
			{
				x = contentSizeInPixels.width / (float)this.m_sGridSize.x,
				y = contentSizeInPixels.height / (float)this.m_sGridSize.y
			};
			this.m_pGrabber = new CCGrabber();
			if (this.m_pGrabber == null)
			{
				flag = false;
			}
			else
			{
				this.m_pGrabber.grab(ref this.m_pTexture);
			}
			this.calculateVertexPoints();
			return flag;
		}

		public bool initWithSize(ccGridSize gridSize)
		{
			CCSize cCSize = CCDirector.sharedDirector().winSizeInPixels;
			ulong num = this.ccNextPOT((ulong)((uint)cCSize.width));
			ulong num1 = this.ccNextPOT((ulong)((uint)cCSize.height));
			CCTexture2DPixelFormat cCTexture2DPixelFormat = CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGBA8888;
			CCTexture2D cCTexture2D = new CCTexture2D();
			cCTexture2D.initWithData(null, cCTexture2DPixelFormat, (uint)num, (uint)num1, cCSize);
			if (cCTexture2D == null)
			{
				CCLog.Log("cocos2d: CCGrid: error creating texture");
				return false;
			}
			this.initWithSize(gridSize, cCTexture2D, false);
			return true;
		}

		public virtual void reuse()
		{
		}

		public void set2DProjection()
		{
			CCSize cCSize = CCDirector.sharedDirector().winSizeInPixels;
		}

		public void set3DProjection()
		{
			CCSize cCSize = CCDirector.sharedDirector().displaySizeInPixels;
		}
	}
}