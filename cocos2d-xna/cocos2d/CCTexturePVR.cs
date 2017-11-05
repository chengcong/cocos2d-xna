using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCTexturePVR
	{
		private const int CC_PVRMIPMAP_MAX = 16;

		protected ulong m_uName;

		protected uint m_uWidth;

		protected uint m_uHeight;

		protected CCTexture2DPixelFormat m_eFormat;

		protected bool m_bHasAlpha;

		protected bool m_bRetainName;

		private uint m_uTableFormatIndex;

		private uint m_uNumberOfMipmaps;

		private List<CCPVRMipmap> m_asMipmaps;

		public CCTexture2DPixelFormat Format
		{
			get
			{
				return this.m_eFormat;
			}
		}

		public bool HasAlpha
		{
			get
			{
				return this.m_bHasAlpha;
			}
		}

		public uint Height
		{
			get
			{
				return this.m_uHeight;
			}
		}

		public ulong Name
		{
			get
			{
				return this.m_uName;
			}
		}

		public bool RetainName
		{
			get
			{
				return this.m_bRetainName;
			}
			set
			{
				this.m_bRetainName = value;
			}
		}

		public uint Width
		{
			get
			{
				return this.m_uWidth;
			}
		}

		public CCTexturePVR()
		{
		}

		protected bool createGLTexture()
		{
			throw new NotImplementedException();
		}

		public bool initWithContentsOfFile(string path)
		{
			throw new NotImplementedException();
		}

		public static CCTexturePVR pvrTextureWithContentsOfFile(string path)
		{
			throw new NotImplementedException();
		}

		protected bool unpackPVRData(string data, uint len)
		{
			throw new NotImplementedException();
		}
	}
}