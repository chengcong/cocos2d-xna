using System;

namespace cocos2d
{
	public class CCConfiguration
	{
		protected int m_nMaxTextureSize = 3379;

		protected int m_nMaxModelviewStackDepth;

		protected bool m_bSupportsPVRTC;

		protected bool m_bSupportsNPOT;

		protected bool m_bSupportsBGRA8888;

		protected bool m_bSupportsDiscardFramebuffer;

		protected bool m_bInited;

		protected uint m_uOSVersion;

		protected int m_nMaxSamplesAllowed;

		protected string m_pGlExtensions;

		private static CCConfiguration m_sharedConfiguration;

		public bool IsSupportsBGRA8888
		{
			get
			{
				return this.m_bSupportsBGRA8888;
			}
		}

		public bool IsSupportsDiscardFramebuffer
		{
			get
			{
				return this.m_bSupportsDiscardFramebuffer;
			}
		}

		public bool IsSupportsNPOT
		{
			get
			{
				return this.m_bSupportsNPOT;
			}
		}

		public bool IsSupportsPVRTC
		{
			get
			{
				return this.m_bSupportsPVRTC;
			}
		}

		public int MaxModelviewStackDepth
		{
			get
			{
				return this.m_nMaxModelviewStackDepth;
			}
		}

		public int MaxTextureSize
		{
			get
			{
				return this.m_nMaxTextureSize;
			}
		}

		public uint OSVersion
		{
			get
			{
				return this.m_uOSVersion;
			}
		}

		static CCConfiguration()
		{
			CCConfiguration.m_sharedConfiguration = new CCConfiguration();
		}

		private CCConfiguration()
		{
		}

		public bool checkForGLExtension(string searchName)
		{
			throw new NotImplementedException();
		}

		public CCGlesVersion getGlesVersion()
		{
			return CCGlesVersion.GLES_VER_2_0;
		}

		public bool init()
		{
			return true;
		}

		public static CCConfiguration sharedConfiguration()
		{
			if (!CCConfiguration.m_sharedConfiguration.m_bInited)
			{
				CCConfiguration.m_sharedConfiguration.init();
				CCConfiguration.m_sharedConfiguration.m_bInited = true;
			}
			return CCConfiguration.m_sharedConfiguration;
		}
	}
}