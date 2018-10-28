using System;

namespace ComponentAce.Compression.Libs.zlib
{
	internal sealed class Tree
	{
		private const int MAX_BITS = 15;

		private const int BL_CODES = 19;

		private const int D_CODES = 30;

		private const int LITERALS = 256;

		private const int LENGTH_CODES = 29;

		internal const int MAX_BL_BITS = 7;

		internal const int END_BLOCK = 256;

		internal const int REP_3_6 = 16;

		internal const int REPZ_3_10 = 17;

		internal const int REPZ_11_138 = 18;

		internal const int Buf_size = 16;

		internal const int DIST_CODE_LEN = 512;

		private readonly static int L_CODES;

		private readonly static int HEAP_SIZE;

		internal readonly static int[] extra_lbits;

		internal readonly static int[] extra_dbits;

		internal readonly static int[] extra_blbits;

		internal readonly static byte[] bl_order;

		internal readonly static byte[] _dist_code;

		internal readonly static byte[] _length_code;

		internal readonly static int[] base_length;

		internal readonly static int[] base_dist;

		internal short[] dyn_tree;

		internal int max_code;

		internal StaticTree stat_desc;

		static Tree()
		{
			Tree.L_CODES = 286;
			Tree.HEAP_SIZE = 2 * Tree.L_CODES + 1;
			Tree.extra_lbits = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0 };
			Tree.extra_dbits = new int[] { 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 };
			Tree.extra_blbits = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 7 };
			Tree.bl_order = new byte[] { 16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15 };
			Tree._dist_code = new byte[] { 0, 1, 2, 3, 4, 4, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 0, 0, 16, 17, 18, 18, 19, 19, 20, 20, 20, 20, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 23, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29 };
			Tree._length_code = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 12, 12, 13, 13, 13, 13, 14, 14, 14, 14, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16, 16, 16, 17, 17, 17, 17, 17, 17, 17, 17, 18, 18, 18, 18, 18, 18, 18, 18, 19, 19, 19, 19, 19, 19, 19, 19, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 28 };
			Tree.base_length = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 12, 14, 16, 20, 24, 28, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 0 };
			Tree.base_dist = new int[] { 0, 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 96, 128, 192, 256, 384, 512, 768, 1024, 1536, 2048, 3072, 4096, 6144, 8192, 12288, 16384, 24576 };
		}

		public Tree()
		{
		}

		internal static int bi_reverse(int code, int len)
		{
			int num;
			int num1 = 0;
			do
			{
				num1 = num1 | code & 1;
				code = SupportClass.URShift(code, 1);
				num1 = num1 << 1;
				num = len - 1;
				len = num;
			}
			while (num > 0);
			return SupportClass.URShift(num1, 1);
		}

		internal void build_tree(Deflate s)
		{
			int i;
			int num;
			int num1;
			short[] dynTree = this.dyn_tree;
			short[] staticTree = this.stat_desc.static_tree;
			int statDesc = this.stat_desc.elems;
			int num2 = -1;
			s.heap_len = 0;
			s.heap_max = Tree.HEAP_SIZE;
			for (i = 0; i < statDesc; i++)
			{
				if (dynTree[i * 2] == 0)
				{
					dynTree[i * 2 + 1] = 0;
				}
				else
				{
					Deflate deflate = s;
					int heapLen = deflate.heap_len + 1;
					int num3 = heapLen;
					deflate.heap_len = heapLen;
					int num4 = i;
					num2 = num4;
					s.heap[num3] = num4;
					s.depth[i] = 0;
				}
			}
			while (s.heap_len < 2)
			{
				int[] numArray = s.heap;
				Deflate deflate1 = s;
				int heapLen1 = deflate1.heap_len + 1;
				int num5 = heapLen1;
				deflate1.heap_len = heapLen1;
				int num6 = num5;
				if (num2 < 2)
				{
					num1 = num2 + 1;
					num2 = num1;
				}
				else
				{
					num1 = 0;
				}
				int num7 = num1;
				numArray[num6] = num1;
				num = num7;
				dynTree[num * 2] = 1;
				s.depth[num] = 0;
				Deflate optLen = s;
				optLen.opt_len = optLen.opt_len - 1;
				if (staticTree == null)
				{
					continue;
				}
				Deflate staticLen = s;
				staticLen.static_len = staticLen.static_len - staticTree[num * 2 + 1];
			}
			this.max_code = num2;
			for (i = s.heap_len / 2; i >= 1; i--)
			{
				s.pqdownheap(dynTree, i);
			}
			num = statDesc;
			do
			{
				i = s.heap[1];
				int[] numArray1 = s.heap;
				int[] numArray2 = s.heap;
				Deflate deflate2 = s;
				int heapLen2 = deflate2.heap_len;
				int num8 = heapLen2;
				deflate2.heap_len = heapLen2 - 1;
				numArray1[1] = numArray2[num8];
				s.pqdownheap(dynTree, 1);
				int num9 = s.heap[1];
				Deflate deflate3 = s;
				int heapMax = deflate3.heap_max - 1;
				int num10 = heapMax;
				deflate3.heap_max = heapMax;
				s.heap[num10] = i;
				Deflate deflate4 = s;
				int heapMax1 = deflate4.heap_max - 1;
				int num11 = heapMax1;
				deflate4.heap_max = heapMax1;
				s.heap[num11] = num9;
				dynTree[num * 2] = (short)(dynTree[i * 2] + dynTree[num9 * 2]);
				s.depth[num] = (byte)(Math.Max(s.depth[i], s.depth[num9]) + 1);
				short num12 = (short)num;
				short num13 = num12;
				dynTree[num9 * 2 + 1] = num12;
				dynTree[i * 2 + 1] = num13;
				int num14 = num;
				num = num14 + 1;
				s.heap[1] = num14;
				s.pqdownheap(dynTree, 1);
			}
			while (s.heap_len >= 2);
			Deflate deflate5 = s;
			int heapMax2 = deflate5.heap_max - 1;
			int num15 = heapMax2;
			deflate5.heap_max = heapMax2;
			s.heap[num15] = s.heap[1];
			this.gen_bitlen(s);
			Tree.gen_codes(dynTree, num2, s.bl_count);
		}

		internal static int d_code(int dist)
		{
			if (dist < 256)
			{
				return Tree._dist_code[dist];
			}
			return Tree._dist_code[256 + SupportClass.URShift(dist, 7)];
		}

		internal void gen_bitlen(Deflate s)
		{
			int j;
			int blCount;
			int i;
			short[] dynTree = this.dyn_tree;
			short[] staticTree = this.stat_desc.static_tree;
			int[] extraBits = this.stat_desc.extra_bits;
			int extraBase = this.stat_desc.extra_base;
			int maxLength = this.stat_desc.max_length;
			int num = 0;
			for (i = 0; i <= 15; i++)
			{
				s.bl_count[i] = 0;
			}
			dynTree[s.heap[s.heap_max] * 2 + 1] = 0;
			for (j = s.heap_max + 1; j < Tree.HEAP_SIZE; j++)
			{
				blCount = s.heap[j];
				i = dynTree[dynTree[blCount * 2 + 1] * 2 + 1] + 1;
				if (i > maxLength)
				{
					i = maxLength;
					num++;
				}
				dynTree[blCount * 2 + 1] = (short)i;
				if (blCount <= this.max_code)
				{
					s.bl_count[i] = (short)(s.bl_count[i] + 1);
					int num1 = 0;
					if (blCount >= extraBase)
					{
						num1 = extraBits[blCount - extraBase];
					}
					short num2 = dynTree[blCount * 2];
					Deflate optLen = s;
					optLen.opt_len = optLen.opt_len + num2 * (i + num1);
					if (staticTree != null)
					{
						Deflate staticLen = s;
						staticLen.static_len = staticLen.static_len + num2 * (staticTree[blCount * 2 + 1] + num1);
					}
				}
			}
			if (num == 0)
			{
				return;
			}
			do
			{
				i = maxLength - 1;
				while (s.bl_count[i] == 0)
				{
					i--;
				}
				s.bl_count[i] = (short)(s.bl_count[i] - 1);
				s.bl_count[i + 1] = (short)(s.bl_count[i + 1] + 2);
				s.bl_count[maxLength] = (short)(s.bl_count[maxLength] - 1);
				num = num - 2;
			}
			while (num > 0);
			for (i = maxLength; i != 0; i--)
			{
				blCount = s.bl_count[i];
				while (blCount != 0)
				{
					int num3 = j - 1;
					j = num3;
					int num4 = s.heap[num3];
					if (num4 > this.max_code)
					{
						continue;
					}
					if (dynTree[num4 * 2 + 1] != i)
					{
						s.opt_len = (int)((long)s.opt_len + ((long)i - (long)dynTree[num4 * 2 + 1]) * (long)dynTree[num4 * 2]);
						dynTree[num4 * 2 + 1] = (short)i;
					}
					blCount--;
				}
			}
		}

		internal static void gen_codes(short[] tree, int max_code, short[] bl_count)
		{
			short[] numArray = new short[16];
			short num = 0;
			for (int i = 1; i <= 15; i++)
			{
				short blCount = (short)(num + bl_count[i - 1] << 1);
				num = blCount;
				numArray[i] = blCount;
			}
			for (int j = 0; j <= max_code; j++)
			{
				int num1 = tree[j * 2 + 1];
				if (num1 != 0)
				{
					short num2 = numArray[num1];
					short num3 = num2;
					numArray[num1] = (short)(num2 + 1);
					tree[j * 2] = (short)Tree.bi_reverse(num3, num1);
				}
			}
		}
	}
}