using System;

namespace ComponentAce.Compression.Libs.zlib
{
	internal sealed class Adler32
	{
		private const int BASE = 65521;

		private const int NMAX = 5552;

		public Adler32()
		{
		}

		internal long adler32(long adler, byte[] buf, int index, int len)
		{
			int num;
			if (buf == null)
			{
				return (long)1;
			}
			long num1 = adler & (long)65535;
			long num2 = adler >> 16 & (long)65535;
			while (len > 0)
			{
				int num3 = (len < 5552 ? len : 5552);
				len = len - num3;
				while (num3 >= 16)
				{
					int num4 = index;
					index = num4 + 1;
					num1 = num1 + (long)(buf[num4] & 255);
					num2 = num2 + num1;
					int num5 = index;
					index = num5 + 1;
					num1 = num1 + (long)(buf[num5] & 255);
					num2 = num2 + num1;
					int num6 = index;
					index = num6 + 1;
					num1 = num1 + (long)(buf[num6] & 255);
					num2 = num2 + num1;
					int num7 = index;
					index = num7 + 1;
					num1 = num1 + (long)(buf[num7] & 255);
					num2 = num2 + num1;
					int num8 = index;
					index = num8 + 1;
					num1 = num1 + (long)(buf[num8] & 255);
					num2 = num2 + num1;
					int num9 = index;
					index = num9 + 1;
					num1 = num1 + (long)(buf[num9] & 255);
					num2 = num2 + num1;
					int num10 = index;
					index = num10 + 1;
					num1 = num1 + (long)(buf[num10] & 255);
					num2 = num2 + num1;
					int num11 = index;
					index = num11 + 1;
					num1 = num1 + (long)(buf[num11] & 255);
					num2 = num2 + num1;
					int num12 = index;
					index = num12 + 1;
					num1 = num1 + (long)(buf[num12] & 255);
					num2 = num2 + num1;
					int num13 = index;
					index = num13 + 1;
					num1 = num1 + (long)(buf[num13] & 255);
					num2 = num2 + num1;
					int num14 = index;
					index = num14 + 1;
					num1 = num1 + (long)(buf[num14] & 255);
					num2 = num2 + num1;
					int num15 = index;
					index = num15 + 1;
					num1 = num1 + (long)(buf[num15] & 255);
					num2 = num2 + num1;
					int num16 = index;
					index = num16 + 1;
					num1 = num1 + (long)(buf[num16] & 255);
					num2 = num2 + num1;
					int num17 = index;
					index = num17 + 1;
					num1 = num1 + (long)(buf[num17] & 255);
					num2 = num2 + num1;
					int num18 = index;
					index = num18 + 1;
					num1 = num1 + (long)(buf[num18] & 255);
					num2 = num2 + num1;
					int num19 = index;
					index = num19 + 1;
					num1 = num1 + (long)(buf[num19] & 255);
					num2 = num2 + num1;
					num3 = num3 - 16;
				}
				if (num3 != 0)
				{
					do
					{
						int num20 = index;
						index = num20 + 1;
						num1 = num1 + (long)(buf[num20] & 255);
						num2 = num2 + num1;
						num = num3 - 1;
						num3 = num;
					}
					while (num != 0);
				}
				num1 = num1 % (long)65521;
				num2 = num2 % (long)65521;
			}
			return num2 << 16 | num1;
		}
	}
}