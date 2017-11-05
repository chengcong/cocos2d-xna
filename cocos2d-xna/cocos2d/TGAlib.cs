using System;

namespace cocos2d
{
	public class TGAlib
	{
		public TGAlib()
		{
		}

		public static void tgaDestroy(tImageTGA psInfo)
		{
			if (psInfo != null)
			{
				if (psInfo.imageData != null)
				{
					psInfo.imageData = null;
				}
				psInfo = null;
			}
		}

		public static tImageTGA tgaLoad(string pszFilename)
		{
			tImageTGA _tImageTGA = null;
			CCFileData cCFileDatum = new CCFileData(pszFilename, "rb");
			ulong size = cCFileDatum.Size;
			byte[] buffer = cCFileDatum.Buffer;
			return _tImageTGA;
		}

		public static bool tgaLoadHeader(byte[] Buffer, ulong bufSize, tImageTGA psInfo)
		{
			return false;
		}

		public static bool tgaLoadImageData(byte[] Buffer, ulong bufSize, tImageTGA psInfo)
		{
			return false;
		}

		private bool tgaLoadRLEImageData(byte[] Buffer, ulong bufSize, tImageTGA psInfo)
		{
			throw new NotImplementedException();
		}

		public static void tgaRGBtogreyscale(tImageTGA psInfo)
		{
			throw new NotImplementedException();
		}
	}
}