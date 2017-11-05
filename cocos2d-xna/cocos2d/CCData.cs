using System;

namespace cocos2d
{
	public class CCData : CCObject
	{
		private byte[] m_pData;

		public CCData()
		{
		}

		public byte[] bytes()
		{
			return this.m_pData;
		}

		public static CCData dataWithBytes(byte[] pBytes, int size)
		{
			return null;
		}

		public static CCData dataWithContentsOfFile(string strPath)
		{
			CCFileData cCFileDatum = new CCFileData(strPath, "rb");
			ulong size = cCFileDatum.Size;
			byte[] buffer = cCFileDatum.Buffer;
			if (buffer == null)
			{
				return null;
			}
			return new CCData()
			{
				m_pData = buffer
			};
		}
	}
}