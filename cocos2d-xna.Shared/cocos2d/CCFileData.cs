using System;

namespace cocos2d
{
	public class CCFileData
	{
		protected byte[] m_pBuffer;

		protected ulong m_uSize;

		public byte[] Buffer
		{
			get
			{
				return this.m_pBuffer;
			}
		}

		public ulong Size
		{
			get
			{
				return this.m_uSize;
			}
		}

		public CCFileData(string pszFileName, string pszMode)
		{
		}

		public bool reset(string pszFileName, string pszMode)
		{
			this.m_pBuffer = null;
			this.m_uSize = (ulong)0;
			if (this.m_pBuffer == null)
			{
				return false;
			}
			return true;
		}
	}
}