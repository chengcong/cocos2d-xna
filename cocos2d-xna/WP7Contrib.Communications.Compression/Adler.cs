using System;

namespace WP7Contrib.Communications.Compression
{
	internal sealed class Adler
	{
		private static int BASE;

		private static int NMAX;

		static Adler()
		{
			Adler.BASE = 65521;
			Adler.NMAX = 5552;
		}

		public Adler()
		{
		}

		internal static long Adler32(long adler, byte[] buf, int index, int len)
		{
			int num;
			if (buf == null)
			{
				return (long)1;
			}
			long bASE = adler & (long)65535;
			long bASE1 = adler >> 16 & (long)65535;
			while (len > 0)
			{
				int num1 = (len < Adler.NMAX ? len : Adler.NMAX);
				len = len - num1;
				while (num1 >= 16)
				{
					int num2 = index;
					index = num2 + 1;
					long num3 = bASE + (long)(buf[num2] & 255);
					long num4 = bASE1 + num3;
					int num5 = index;
					index = num5 + 1;
					long num6 = num3 + (long)(buf[num5] & 255);
					long num7 = num4 + num6;
					int num8 = index;
					index = num8 + 1;
					long num9 = num6 + (long)(buf[num8] & 255);
					long num10 = num7 + num9;
					int num11 = index;
					index = num11 + 1;
					long num12 = num9 + (long)(buf[num11] & 255);
					long num13 = num10 + num12;
					int num14 = index;
					index = num14 + 1;
					long num15 = num12 + (long)(buf[num14] & 255);
					long num16 = num13 + num15;
					int num17 = index;
					index = num17 + 1;
					long num18 = num15 + (long)(buf[num17] & 255);
					long num19 = num16 + num18;
					int num20 = index;
					index = num20 + 1;
					long num21 = num18 + (long)(buf[num20] & 255);
					long num22 = num19 + num21;
					int num23 = index;
					index = num23 + 1;
					long num24 = num21 + (long)(buf[num23] & 255);
					long num25 = num22 + num24;
					int num26 = index;
					index = num26 + 1;
					long num27 = num24 + (long)(buf[num26] & 255);
					long num28 = num25 + num27;
					int num29 = index;
					index = num29 + 1;
					long num30 = num27 + (long)(buf[num29] & 255);
					long num31 = num28 + num30;
					int num32 = index;
					index = num32 + 1;
					long num33 = num30 + (long)(buf[num32] & 255);
					long num34 = num31 + num33;
					int num35 = index;
					index = num35 + 1;
					long num36 = num33 + (long)(buf[num35] & 255);
					long num37 = num34 + num36;
					int num38 = index;
					index = num38 + 1;
					long num39 = num36 + (long)(buf[num38] & 255);
					long num40 = num37 + num39;
					int num41 = index;
					index = num41 + 1;
					long num42 = num39 + (long)(buf[num41] & 255);
					long num43 = num40 + num42;
					int num44 = index;
					index = num44 + 1;
					long num45 = num42 + (long)(buf[num44] & 255);
					long num46 = num43 + num45;
					int num47 = index;
					index = num47 + 1;
					bASE = num45 + (long)(buf[num47] & 255);
					bASE1 = num46 + bASE;
					num1 = num1 - 16;
				}
				if (num1 != 0)
				{
					do
					{
						int num48 = index;
						index = num48 + 1;
						bASE = bASE + (long)(buf[num48] & 255);
						bASE1 = bASE1 + bASE;
						num = num1 - 1;
						num1 = num;
					}
					while (num != 0);
				}
				bASE = bASE % (long)Adler.BASE;
				bASE1 = bASE1 % (long)Adler.BASE;
			}
			return bASE1 << 16 | bASE;
		}
	}
}