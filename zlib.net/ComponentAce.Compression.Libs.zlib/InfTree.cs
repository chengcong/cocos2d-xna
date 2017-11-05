using System;

namespace ComponentAce.Compression.Libs.zlib
{
	internal sealed class InfTree
	{
		private const int MANY = 1440;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		internal const int fixed_bl = 9;

		internal const int fixed_bd = 5;

		internal const int BMAX = 15;

		internal readonly static int[] fixed_tl;

		internal readonly static int[] fixed_td;

		internal readonly static int[] cplens;

		internal readonly static int[] cplext;

		internal readonly static int[] cpdist;

		internal readonly static int[] cpdext;

		static InfTree()
		{
			InfTree.fixed_tl = new int[] { 96, 7, 256, 0, 8, 80, 0, 8, 16, 84, 8, 115, 82, 7, 31, 0, 8, 112, 0, 8, 48, 0, 9, 192, 80, 7, 10, 0, 8, 96, 0, 8, 32, 0, 9, 160, 0, 8, 0, 0, 8, 128, 0, 8, 64, 0, 9, 224, 80, 7, 6, 0, 8, 88, 0, 8, 24, 0, 9, 144, 83, 7, 59, 0, 8, 120, 0, 8, 56, 0, 9, 208, 81, 7, 17, 0, 8, 104, 0, 8, 40, 0, 9, 176, 0, 8, 8, 0, 8, 136, 0, 8, 72, 0, 9, 240, 80, 7, 4, 0, 8, 84, 0, 8, 20, 85, 8, 227, 83, 7, 43, 0, 8, 116, 0, 8, 52, 0, 9, 200, 81, 7, 13, 0, 8, 100, 0, 8, 36, 0, 9, 168, 0, 8, 4, 0, 8, 132, 0, 8, 68, 0, 9, 232, 80, 7, 8, 0, 8, 92, 0, 8, 28, 0, 9, 152, 84, 7, 83, 0, 8, 124, 0, 8, 60, 0, 9, 216, 82, 7, 23, 0, 8, 108, 0, 8, 44, 0, 9, 184, 0, 8, 12, 0, 8, 140, 0, 8, 76, 0, 9, 248, 80, 7, 3, 0, 8, 82, 0, 8, 18, 85, 8, 163, 83, 7, 35, 0, 8, 114, 0, 8, 50, 0, 9, 196, 81, 7, 11, 0, 8, 98, 0, 8, 34, 0, 9, 164, 0, 8, 2, 0, 8, 130, 0, 8, 66, 0, 9, 228, 80, 7, 7, 0, 8, 90, 0, 8, 26, 0, 9, 148, 84, 7, 67, 0, 8, 122, 0, 8, 58, 0, 9, 212, 82, 7, 19, 0, 8, 106, 0, 8, 42, 0, 9, 180, 0, 8, 10, 0, 8, 138, 0, 8, 74, 0, 9, 244, 80, 7, 5, 0, 8, 86, 0, 8, 22, 192, 8, 0, 83, 7, 51, 0, 8, 118, 0, 8, 54, 0, 9, 204, 81, 7, 15, 0, 8, 102, 0, 8, 38, 0, 9, 172, 0, 8, 6, 0, 8, 134, 0, 8, 70, 0, 9, 236, 80, 7, 9, 0, 8, 94, 0, 8, 30, 0, 9, 156, 84, 7, 99, 0, 8, 126, 0, 8, 62, 0, 9, 220, 82, 7, 27, 0, 8, 110, 0, 8, 46, 0, 9, 188, 0, 8, 14, 0, 8, 142, 0, 8, 78, 0, 9, 252, 96, 7, 256, 0, 8, 81, 0, 8, 17, 85, 8, 131, 82, 7, 31, 0, 8, 113, 0, 8, 49, 0, 9, 194, 80, 7, 10, 0, 8, 97, 0, 8, 33, 0, 9, 162, 0, 8, 1, 0, 8, 129, 0, 8, 65, 0, 9, 226, 80, 7, 6, 0, 8, 89, 0, 8, 25, 0, 9, 146, 83, 7, 59, 0, 8, 121, 0, 8, 57, 0, 9, 210, 81, 7, 17, 0, 8, 105, 0, 8, 41, 0, 9, 178, 0, 8, 9, 0, 8, 137, 0, 8, 73, 0, 9, 242, 80, 7, 4, 0, 8, 85, 0, 8, 21, 80, 8, 258, 83, 7, 43, 0, 8, 117, 0, 8, 53, 0, 9, 202, 81, 7, 13, 0, 8, 101, 0, 8, 37, 0, 9, 170, 0, 8, 5, 0, 8, 133, 0, 8, 69, 0, 9, 234, 80, 7, 8, 0, 8, 93, 0, 8, 29, 0, 9, 154, 84, 7, 83, 0, 8, 125, 0, 8, 61, 0, 9, 218, 82, 7, 23, 0, 8, 109, 0, 8, 45, 0, 9, 186, 0, 8, 13, 0, 8, 141, 0, 8, 77, 0, 9, 250, 80, 7, 3, 0, 8, 83, 0, 8, 19, 85, 8, 195, 83, 7, 35, 0, 8, 115, 0, 8, 51, 0, 9, 198, 81, 7, 11, 0, 8, 99, 0, 8, 35, 0, 9, 166, 0, 8, 3, 0, 8, 131, 0, 8, 67, 0, 9, 230, 80, 7, 7, 0, 8, 91, 0, 8, 27, 0, 9, 150, 84, 7, 67, 0, 8, 123, 0, 8, 59, 0, 9, 214, 82, 7, 19, 0, 8, 107, 0, 8, 43, 0, 9, 182, 0, 8, 11, 0, 8, 139, 0, 8, 75, 0, 9, 246, 80, 7, 5, 0, 8, 87, 0, 8, 23, 192, 8, 0, 83, 7, 51, 0, 8, 119, 0, 8, 55, 0, 9, 206, 81, 7, 15, 0, 8, 103, 0, 8, 39, 0, 9, 174, 0, 8, 7, 0, 8, 135, 0, 8, 71, 0, 9, 238, 80, 7, 9, 0, 8, 95, 0, 8, 31, 0, 9, 158, 84, 7, 99, 0, 8, 127, 0, 8, 63, 0, 9, 222, 82, 7, 27, 0, 8, 111, 0, 8, 47, 0, 9, 190, 0, 8, 15, 0, 8, 143, 0, 8, 79, 0, 9, 254, 96, 7, 256, 0, 8, 80, 0, 8, 16, 84, 8, 115, 82, 7, 31, 0, 8, 112, 0, 8, 48, 0, 9, 193, 80, 7, 10, 0, 8, 96, 0, 8, 32, 0, 9, 161, 0, 8, 0, 0, 8, 128, 0, 8, 64, 0, 9, 225, 80, 7, 6, 0, 8, 88, 0, 8, 24, 0, 9, 145, 83, 7, 59, 0, 8, 120, 0, 8, 56, 0, 9, 209, 81, 7, 17, 0, 8, 104, 0, 8, 40, 0, 9, 177, 0, 8, 8, 0, 8, 136, 0, 8, 72, 0, 9, 241, 80, 7, 4, 0, 8, 84, 0, 8, 20, 85, 8, 227, 83, 7, 43, 0, 8, 116, 0, 8, 52, 0, 9, 201, 81, 7, 13, 0, 8, 100, 0, 8, 36, 0, 9, 169, 0, 8, 4, 0, 8, 132, 0, 8, 68, 0, 9, 233, 80, 7, 8, 0, 8, 92, 0, 8, 28, 0, 9, 153, 84, 7, 83, 0, 8, 124, 0, 8, 60, 0, 9, 217, 82, 7, 23, 0, 8, 108, 0, 8, 44, 0, 9, 185, 0, 8, 12, 0, 8, 140, 0, 8, 76, 0, 9, 249, 80, 7, 3, 0, 8, 82, 0, 8, 18, 85, 8, 163, 83, 7, 35, 0, 8, 114, 0, 8, 50, 0, 9, 197, 81, 7, 11, 0, 8, 98, 0, 8, 34, 0, 9, 165, 0, 8, 2, 0, 8, 130, 0, 8, 66, 0, 9, 229, 80, 7, 7, 0, 8, 90, 0, 8, 26, 0, 9, 149, 84, 7, 67, 0, 8, 122, 0, 8, 58, 0, 9, 213, 82, 7, 19, 0, 8, 106, 0, 8, 42, 0, 9, 181, 0, 8, 10, 0, 8, 138, 0, 8, 74, 0, 9, 245, 80, 7, 5, 0, 8, 86, 0, 8, 22, 192, 8, 0, 83, 7, 51, 0, 8, 118, 0, 8, 54, 0, 9, 205, 81, 7, 15, 0, 8, 102, 0, 8, 38, 0, 9, 173, 0, 8, 6, 0, 8, 134, 0, 8, 70, 0, 9, 237, 80, 7, 9, 0, 8, 94, 0, 8, 30, 0, 9, 157, 84, 7, 99, 0, 8, 126, 0, 8, 62, 0, 9, 221, 82, 7, 27, 0, 8, 110, 0, 8, 46, 0, 9, 189, 0, 8, 14, 0, 8, 142, 0, 8, 78, 0, 9, 253, 96, 7, 256, 0, 8, 81, 0, 8, 17, 85, 8, 131, 82, 7, 31, 0, 8, 113, 0, 8, 49, 0, 9, 195, 80, 7, 10, 0, 8, 97, 0, 8, 33, 0, 9, 163, 0, 8, 1, 0, 8, 129, 0, 8, 65, 0, 9, 227, 80, 7, 6, 0, 8, 89, 0, 8, 25, 0, 9, 147, 83, 7, 59, 0, 8, 121, 0, 8, 57, 0, 9, 211, 81, 7, 17, 0, 8, 105, 0, 8, 41, 0, 9, 179, 0, 8, 9, 0, 8, 137, 0, 8, 73, 0, 9, 243, 80, 7, 4, 0, 8, 85, 0, 8, 21, 80, 8, 258, 83, 7, 43, 0, 8, 117, 0, 8, 53, 0, 9, 203, 81, 7, 13, 0, 8, 101, 0, 8, 37, 0, 9, 171, 0, 8, 5, 0, 8, 133, 0, 8, 69, 0, 9, 235, 80, 7, 8, 0, 8, 93, 0, 8, 29, 0, 9, 155, 84, 7, 83, 0, 8, 125, 0, 8, 61, 0, 9, 219, 82, 7, 23, 0, 8, 109, 0, 8, 45, 0, 9, 187, 0, 8, 13, 0, 8, 141, 0, 8, 77, 0, 9, 251, 80, 7, 3, 0, 8, 83, 0, 8, 19, 85, 8, 195, 83, 7, 35, 0, 8, 115, 0, 8, 51, 0, 9, 199, 81, 7, 11, 0, 8, 99, 0, 8, 35, 0, 9, 167, 0, 8, 3, 0, 8, 131, 0, 8, 67, 0, 9, 231, 80, 7, 7, 0, 8, 91, 0, 8, 27, 0, 9, 151, 84, 7, 67, 0, 8, 123, 0, 8, 59, 0, 9, 215, 82, 7, 19, 0, 8, 107, 0, 8, 43, 0, 9, 183, 0, 8, 11, 0, 8, 139, 0, 8, 75, 0, 9, 247, 80, 7, 5, 0, 8, 87, 0, 8, 23, 192, 8, 0, 83, 7, 51, 0, 8, 119, 0, 8, 55, 0, 9, 207, 81, 7, 15, 0, 8, 103, 0, 8, 39, 0, 9, 175, 0, 8, 7, 0, 8, 135, 0, 8, 71, 0, 9, 239, 80, 7, 9, 0, 8, 95, 0, 8, 31, 0, 9, 159, 84, 7, 99, 0, 8, 127, 0, 8, 63, 0, 9, 223, 82, 7, 27, 0, 8, 111, 0, 8, 47, 0, 9, 191, 0, 8, 15, 0, 8, 143, 0, 8, 79, 0, 9, 255 };
			InfTree.fixed_td = new int[] { 80, 5, 1, 87, 5, 257, 83, 5, 17, 91, 5, 4097, 81, 5, 5, 89, 5, 1025, 85, 5, 65, 93, 5, 16385, 80, 5, 3, 88, 5, 513, 84, 5, 33, 92, 5, 8193, 82, 5, 9, 90, 5, 2049, 86, 5, 129, 192, 5, 24577, 80, 5, 2, 87, 5, 385, 83, 5, 25, 91, 5, 6145, 81, 5, 7, 89, 5, 1537, 85, 5, 97, 93, 5, 24577, 80, 5, 4, 88, 5, 769, 84, 5, 49, 92, 5, 12289, 82, 5, 13, 90, 5, 3073, 86, 5, 193, 192, 5, 24577 };
			InfTree.cplens = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258, 0, 0 };
			InfTree.cplext = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 112, 112 };
			InfTree.cpdist = new int[] { 1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577 };
			InfTree.cpdext = new int[] { 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 };
		}

		public InfTree()
		{
		}

		internal static int huft_build(int[] b, int bindex, int n, int s, int[] d, int[] e, int[] t, int[] m, int[] hp, int[] hn, int[] v)
		{
			int num;
			int num1;
			object obj;
			int[] numArray = new int[16];
			int[] numArray1 = new int[3];
			int[] numArray2 = new int[15];
			int[] numArray3 = new int[16];
			int num2 = 0;
			int num3 = n;
			do
			{
				numArray[b[bindex + num2]] = numArray[b[bindex + num2]] + 1;
				num2++;
				num3--;
			}
			while (num3 != 0);
			if (numArray[0] == n)
			{
				t[0] = -1;
				m[0] = 0;
				return 0;
			}
			int num4 = m[0];
			int i = 1;
			while (i <= 15 && numArray[i] == 0)
			{
				i++;
			}
			int num5 = i;
			if (num4 < i)
			{
				num4 = i;
			}
			num3 = 15;
			while (num3 != 0 && numArray[num3] == 0)
			{
				num3--;
			}
			int num6 = num3;
			if (num4 > num3)
			{
				num4 = num3;
			}
			m[0] = num4;
			int num7 = 1 << (i & 31);
			while (i < num3)
			{
				int num8 = num7 - numArray[i];
				num7 = num8;
				if (num8 < 0)
				{
					return -3;
				}
				i++;
				num7 = num7 << 1;
			}
			int num9 = num7 - numArray[num3];
			num7 = num9;
			if (num9 < 0)
			{
				return -3;
			}
			numArray[num3] = numArray[num3] + num7;
			int num10 = 0;
			i = num10;
			numArray3[1] = num10;
			num2 = 1;
			int num11 = 2;
			while (true)
			{
				int num12 = num3 - 1;
				num3 = num12;
				if (num12 == 0)
				{
					break;
				}
				int num13 = i + numArray[num2];
				i = num13;
				numArray3[num11] = num13;
				num11++;
				num2++;
			}
			num3 = 0;
			num2 = 0;
			do
			{
				int num14 = b[bindex + num2];
				i = num14;
				if (num14 != 0)
				{
					int num15 = numArray3[i];
					int num16 = num15;
					numArray3[i] = num15 + 1;
					v[num16] = num3;
				}
				num2++;
				num1 = num3 + 1;
				num3 = num1;
			}
			while (num1 < n);
			n = numArray3[num6];
			int num17 = 0;
			num3 = num17;
			numArray3[0] = num17;
			num2 = 0;
			int num18 = -1;
			int num19 = -num4;
			numArray2[0] = 0;
			int num20 = 0;
			int num21 = 0;
		Label1:
			if (num5 > num6)
			{
				if (num7 != 0 && num6 != 1)
				{
					return -5;
				}
				return 0;
			}
			int num22 = numArray[num5];
		Label2:
			int num23 = num22;
			num22 = num23 - 1;
			if (num23 != 0)
			{
				goto Label0;
			}
			num5++;
			goto Label1;
		Label0:
			while (num5 > num19 + num4)
			{
				num18++;
				num19 = num19 + num4;
				num21 = num6 - num19;
				num21 = (num21 > num4 ? num4 : num21);
				int num24 = num5 - num19;
				i = num24;
				int num25 = 1 << (num24 & 31);
				num = num25;
				if (num25 > num22 + 1)
				{
					num = num - (num22 + 1);
					num11 = num5;
					if (i < num21)
					{
						while (true)
						{
							int num26 = i + 1;
							i = num26;
							if (num26 >= num21)
							{
								break;
							}
							int num27 = num << 1;
							num = num27;
							int num28 = num11 + 1;
							num11 = num28;
							if (num27 <= numArray[num28])
							{
								break;
							}
							num = num - numArray[num11];
						}
					}
				}
				num21 = 1 << (i & 31);
				if (hn[0] + num21 > 1440)
				{
					return -3;
				}
				int num29 = hn[0];
				num20 = num29;
				numArray2[num18] = num29;
				hn[0] = hn[0] + num21;
				if (num18 == 0)
				{
					t[0] = num20;
				}
				else
				{
					numArray3[num18] = num3;
					numArray1[0] = (byte)i;
					numArray1[1] = (byte)num4;
					i = SupportClass.URShift(num3, num19 - num4);
					numArray1[2] = num20 - numArray2[num18 - 1] - i;
					Array.Copy(numArray1, 0, hp, (numArray2[num18 - 1] + i) * 3, 3);
				}
			}
			numArray1[1] = (byte)(num5 - num19);
			if (num2 >= n)
			{
				numArray1[0] = 192;
			}
			else if (v[num2] >= s)
			{
				numArray1[0] = (byte)(e[v[num2] - s] + 16 + 64);
				int num30 = num2;
				num2 = num30 + 1;
				numArray1[2] = d[v[num30] - s];
			}
			else
			{
				int[] numArray4 = numArray1;
				if (v[num2] < 256)
				{
					obj = null;
				}
				else
				{
					obj = 96;
				}
				numArray4[0] = (byte)obj;
				int num31 = num2;
				num2 = num31 + 1;
				numArray1[2] = v[num31];
			}
			num = 1 << (num5 - num19 & 31);
			for (i = SupportClass.URShift(num3, num19); i < num21; i = i + num)
			{
				Array.Copy(numArray1, 0, hp, (num20 + i) * 3, 3);
			}
			for (i = 1 << (num5 - 1 & 31); (num3 & i) != 0; i = SupportClass.URShift(i, 1))
			{
				num3 = num3 ^ i;
			}
			num3 = num3 ^ i;
			for (int j = (1 << (num19 & 31)) - 1; (num3 & j) != numArray3[num18]; j = (1 << (num19 & 31)) - 1)
			{
				num18--;
				num19 = num19 - num4;
			}
			goto Label2;
		}

		internal static int inflate_trees_bits(int[] c, int[] bb, int[] tb, int[] hp, ZStream z)
		{
			int[] numArray = new int[1];
			int[] numArray1 = new int[19];
			int num = InfTree.huft_build(c, 0, 19, 19, null, null, tb, bb, hp, numArray, numArray1);
			if (num == -3)
			{
				z.msg = "oversubscribed dynamic bit lengths tree";
			}
			else if (num == -5 || bb[0] == 0)
			{
				z.msg = "incomplete dynamic bit lengths tree";
				num = -3;
			}
			return num;
		}

		internal static int inflate_trees_dynamic(int nl, int nd, int[] c, int[] bl, int[] bd, int[] tl, int[] td, int[] hp, ZStream z)
		{
			int[] numArray = new int[1];
			int[] numArray1 = new int[288];
			int num = InfTree.huft_build(c, 0, nl, 257, InfTree.cplens, InfTree.cplext, tl, bl, hp, numArray, numArray1);
			if (num != 0 || bl[0] == 0)
			{
				if (num == -3)
				{
					z.msg = "oversubscribed literal/length tree";
				}
				else if (num != -4)
				{
					z.msg = "incomplete literal/length tree";
					num = -3;
				}
				return num;
			}
			num = InfTree.huft_build(c, nl, nd, 0, InfTree.cpdist, InfTree.cpdext, td, bd, hp, numArray, numArray1);
			if (num == 0 && (bd[0] != 0 || nl <= 257))
			{
				return 0;
			}
			if (num == -3)
			{
				z.msg = "oversubscribed distance tree";
			}
			else if (num == -5)
			{
				z.msg = "incomplete distance tree";
				num = -3;
			}
			else if (num != -4)
			{
				z.msg = "empty distance tree with lengths";
				num = -3;
			}
			return num;
		}

		internal static int inflate_trees_fixed(int[] bl, int[] bd, int[][] tl, int[][] td, ZStream z)
		{
			bl[0] = 9;
			bd[0] = 5;
			tl[0] = InfTree.fixed_tl;
			td[0] = InfTree.fixed_td;
			return 0;
		}
	}
}