using System;

namespace cocos2d
{
	public class CCSpriteFrame : CCObject
	{
		protected CCRect m_obRectInPixels;

		protected bool m_bRotated;

		protected CCRect m_obRect;

		protected CCPoint m_obOffsetInPixels;

		protected CCSize m_obOriginalSizeInPixels;

		protected CCTexture2D m_pobTexture;

		public bool IsRotated
		{
			get
			{
				return this.m_bRotated;
			}
			set
			{
				this.m_bRotated = value;
			}
		}

		public CCPoint OffsetInPixels
		{
			get
			{
				return this.m_obOffsetInPixels;
			}
			set
			{
				this.m_obOffsetInPixels = value;
			}
		}

		public CCSize OriginalSizeInPixels
		{
			get
			{
				return this.m_obOriginalSizeInPixels;
			}
			set
			{
				this.m_obOriginalSizeInPixels = value;
			}
		}

		public CCRect Rect
		{
			get
			{
				return this.m_obRect;
			}
			set
			{
				this.m_obRect = value;
				this.m_obRectInPixels = ccMacros.CC_RECT_POINTS_TO_PIXELS(this.m_obRect);
			}
		}

		public CCRect RectInPixels
		{
			get
			{
				return this.m_obRectInPixels;
			}
			set
			{
				this.m_obRectInPixels = value;
				this.m_obRect = ccMacros.CC_RECT_PIXELS_TO_POINTS(this.m_obRectInPixels);
			}
		}

		public CCTexture2D Texture
		{
			get
			{
				return this.m_pobTexture;
			}
			set
			{
				this.m_pobTexture = value;
			}
		}

		public CCSpriteFrame()
		{
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCSpriteFrame cCSpriteFrame = new CCSpriteFrame();
			cCSpriteFrame.initWithTexture(this.m_pobTexture, this.m_obRectInPixels, this.m_bRotated, this.m_obOffsetInPixels, this.m_obOriginalSizeInPixels);
			return cCSpriteFrame;
		}

		public static CCSpriteFrame frameWithTexture(CCTexture2D pobTexture, CCRect rect)
		{
			CCSpriteFrame cCSpriteFrame = new CCSpriteFrame();
			cCSpriteFrame.initWithTexture(pobTexture, rect);
			return cCSpriteFrame;
		}

		public static CCSpriteFrame frameWithTexture(CCTexture2D pobTexture, CCRect rect, bool rotated, CCPoint offset, CCSize originalSize)
		{
			CCSpriteFrame cCSpriteFrame = new CCSpriteFrame();
			cCSpriteFrame.initWithTexture(pobTexture, rect, rotated, offset, originalSize);
			return cCSpriteFrame;
		}

		public bool initWithTexture(CCTexture2D pobTexture, CCRect rect)
		{
			CCRect cCRect = ccMacros.CC_RECT_POINTS_TO_PIXELS(rect);
			return this.initWithTexture(pobTexture, cCRect, false, new CCPoint(0f, 0f), cCRect.size);
		}

		public bool initWithTexture(CCTexture2D pobTexture, CCRect rect, bool rotated, CCPoint offset, CCSize originalSize)
		{
			this.m_pobTexture = pobTexture;
			this.m_obRectInPixels = rect;
			this.m_obRect = ccMacros.CC_RECT_PIXELS_TO_POINTS(rect);
			this.m_bRotated = rotated;
			this.m_obOffsetInPixels = offset;
			this.m_obOriginalSizeInPixels = originalSize;
			return true;
		}
	}
}