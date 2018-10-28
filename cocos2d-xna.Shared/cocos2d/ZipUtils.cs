using System;

namespace cocos2d
{
	public class ZipUtils
	{
		public ZipUtils()
		{
		}

		public static int ccInflateCCZFile(string filename, byte[] parameterout)
		{
			throw new NotImplementedException();
		}

		public static int ccInflateGZipFile(char filename, byte[] parameterout)
		{
			throw new NotImplementedException();
		}

		public static int ccInflateMemory(byte[] parameterin, uint inLength, byte[] parameterout)
		{
			throw new NotImplementedException();
		}

		public static int ccInflateMemoryWithHint(byte[] parameterin, int inLength, byte[] parameterout, int outLenghtHint)
		{
			throw new NotImplementedException();
		}

		private static int ccInflateMemoryWithHint(byte[] parameterin, uint inLength, byte[] parameterout, uint[] outLength, uint outLenghtHint)
		{
			throw new NotImplementedException();
		}
	}
}