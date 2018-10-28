using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCAnimation : CCObject
	{
		protected string m_nameStr;

		protected float m_fDelay;

		protected List<CCSpriteFrame> m_pobFrames;

		public CCAnimation()
		{
			this.m_nameStr = null;
			this.m_pobFrames = new List<CCSpriteFrame>();
		}

		public void addFrame(CCSpriteFrame pFrame)
		{
			this.m_pobFrames.Add(pFrame);
		}

		public void addFrameWithFileName(string pszFileName)
		{
			CCTexture2D cCTexture2D = CCTextureCache.sharedTextureCache().addImage(pszFileName);
			CCRect cCRect = new CCRect(0f, 0f, 0f, 0f)
			{
				size = cCTexture2D.getContentSize()
			};
			CCSpriteFrame cCSpriteFrame = CCSpriteFrame.frameWithTexture(cCTexture2D, cCRect);
			this.m_pobFrames.Add(cCSpriteFrame);
		}

		public void addFrameWithTexture(CCTexture2D pobTexture, CCRect rect)
		{
			CCSpriteFrame cCSpriteFrame = CCSpriteFrame.frameWithTexture(pobTexture, rect);
			this.m_pobFrames.Add(cCSpriteFrame);
#if WINDOWS_UWP
            throw new Exception();
#else
            throw new NotFiniteNumberException();
#endif
        }

		public static CCAnimation animation()
		{
			CCAnimation cCAnimation = new CCAnimation();
			cCAnimation.init();
			return cCAnimation;
		}

		public static CCAnimation animationWithFrames(List<CCSpriteFrame> frames)
		{
			CCAnimation cCAnimation = new CCAnimation();
			cCAnimation.initWithFrames(frames);
			return cCAnimation;
		}

		public static CCAnimation animationWithFrames(List<CCSpriteFrame> frames, float delay)
		{
			CCAnimation cCAnimation = new CCAnimation();
			cCAnimation.initWithFrames(frames, delay);
			return cCAnimation;
		}

		public float getDelay()
		{
			return this.m_fDelay;
		}

		public List<CCSpriteFrame> getFrames()
		{
			return this.m_pobFrames;
		}

		public string getName()
		{
			return this.m_nameStr;
		}

		public bool init()
		{
			return this.initWithFrames(new List<CCSpriteFrame>(), 0f);
		}

		public bool initWithFrames(List<CCSpriteFrame> pFrames)
		{
			return this.initWithFrames(pFrames, 0f);
		}

		public bool initWithFrames(List<CCSpriteFrame> pFrames, float delay)
		{
			this.m_fDelay = delay;
			this.m_pobFrames = pFrames;
			return true;
		}

		public void setDelay(float fDelay)
		{
			this.m_fDelay = fDelay;
		}

		public void setFrames(List<CCSpriteFrame> pFrames)
		{
			this.m_pobFrames = pFrames;
		}

		public void setName(string pszName)
		{
			this.m_nameStr = pszName;
		}
	}
}