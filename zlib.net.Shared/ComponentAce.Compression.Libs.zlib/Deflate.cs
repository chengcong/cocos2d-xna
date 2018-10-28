using System;

namespace ComponentAce.Compression.Libs.zlib
{
	public sealed class Deflate
	{
		private const int MAX_MEM_LEVEL = 9;

		private const int Z_DEFAULT_COMPRESSION = -1;

		private const int MAX_WBITS = 15;

		private const int DEF_MEM_LEVEL = 8;

		private const int STORED = 0;

		private const int FAST = 1;

		private const int SLOW = 2;

		private const int NeedMore = 0;

		private const int BlockDone = 1;

		private const int FinishStarted = 2;

		private const int FinishDone = 3;

		private const int PRESET_DICT = 32;

		private const int Z_FILTERED = 1;

		private const int Z_HUFFMAN_ONLY = 2;

		private const int Z_DEFAULT_STRATEGY = 0;

		private const int Z_NO_FLUSH = 0;

		private const int Z_PARTIAL_FLUSH = 1;

		private const int Z_SYNC_FLUSH = 2;

		private const int Z_FULL_FLUSH = 3;

		private const int Z_FINISH = 4;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private const int INIT_STATE = 42;

		private const int BUSY_STATE = 113;

		private const int FINISH_STATE = 666;

		private const int Z_DEFLATED = 8;

		private const int STORED_BLOCK = 0;

		private const int STATIC_TREES = 1;

		private const int DYN_TREES = 2;

		private const int Z_BINARY = 0;

		private const int Z_ASCII = 1;

		private const int Z_UNKNOWN = 2;

		private const int Buf_size = 16;

		private const int REP_3_6 = 16;

		private const int REPZ_3_10 = 17;

		private const int REPZ_11_138 = 18;

		private const int MIN_MATCH = 3;

		private const int MAX_MATCH = 258;

		private const int MAX_BITS = 15;

		private const int D_CODES = 30;

		private const int BL_CODES = 19;

		private const int LENGTH_CODES = 29;

		private const int LITERALS = 256;

		private const int END_BLOCK = 256;

		private static Deflate.Config[] config_table;

		private readonly static string[] z_errmsg;

		private readonly static int MIN_LOOKAHEAD;

		private readonly static int L_CODES;

		private readonly static int HEAP_SIZE;

		internal ZStream strm;

		internal int status;

		internal byte[] pending_buf;

		internal int pending_buf_size;

		internal int pending_out;

		internal int pending;

		internal int noheader;

		internal byte data_type;

		internal byte method;

		internal int last_flush;

		internal int w_size;

		internal int w_bits;

		internal int w_mask;

		internal byte[] window;

		internal int window_size;

		internal short[] prev;

		internal short[] head;

		internal int ins_h;

		internal int hash_size;

		internal int hash_bits;

		internal int hash_mask;

		internal int hash_shift;

		internal int block_start;

		internal int match_length;

		internal int prev_match;

		internal int match_available;

		internal int strstart;

		internal int match_start;

		internal int lookahead;

		internal int prev_length;

		internal int max_chain_length;

		internal int max_lazy_match;

		internal int level;

		internal int strategy;

		internal int good_match;

		internal int nice_match;

		internal short[] dyn_ltree;

		internal short[] dyn_dtree;

		internal short[] bl_tree;

		internal Tree l_desc = new Tree();

		internal Tree d_desc = new Tree();

		internal Tree bl_desc = new Tree();

		internal short[] bl_count = new short[16];

		internal int[] heap = new int[2 * Deflate.L_CODES + 1];

		internal int heap_len;

		internal int heap_max;

		internal byte[] depth = new byte[2 * Deflate.L_CODES + 1];

		internal int l_buf;

		internal int lit_bufsize;

		internal int last_lit;

		internal int d_buf;

		internal int opt_len;

		internal int static_len;

		internal int matches;

		internal int last_eob_len;

		internal short bi_buf;

		internal int bi_valid;

		static Deflate()
		{
			string[] strArrays = new string[] { "need dictionary", "stream end", "", "file error", "stream error", "data error", "insufficient memory", "buffer error", "incompatible version", "" };
			Deflate.z_errmsg = strArrays;
			Deflate.MIN_LOOKAHEAD = 262;
			Deflate.L_CODES = 286;
			Deflate.HEAP_SIZE = 2 * Deflate.L_CODES + 1;
			Deflate.config_table = new Deflate.Config[] { new Deflate.Config(0, 0, 0, 0, 0), new Deflate.Config(4, 4, 8, 4, 1), new Deflate.Config(4, 5, 16, 8, 1), new Deflate.Config(4, 6, 32, 32, 1), new Deflate.Config(4, 4, 16, 16, 2), new Deflate.Config(8, 16, 32, 32, 2), new Deflate.Config(8, 16, 128, 128, 2), new Deflate.Config(8, 32, 128, 256, 2), new Deflate.Config(32, 128, 258, 1024, 2), new Deflate.Config(32, 258, 258, 4096, 2) };
		}

		internal Deflate()
		{
			this.dyn_ltree = new short[Deflate.HEAP_SIZE * 2];
			this.dyn_dtree = new short[122];
			this.bl_tree = new short[78];
		}

		internal void _tr_align()
		{
			this.send_bits(2, 3);
			this.send_code(256, StaticTree.static_ltree);
			this.bi_flush();
			if (1 + this.last_eob_len + 10 - this.bi_valid < 9)
			{
				this.send_bits(2, 3);
				this.send_code(256, StaticTree.static_ltree);
				this.bi_flush();
			}
			this.last_eob_len = 7;
		}

		internal void _tr_flush_block(int buf, int stored_len, bool eof)
		{
			int num;
			int num1;
			int num2 = 0;
			if (this.level <= 0)
			{
				int storedLen = stored_len + 5;
				num1 = storedLen;
				num = storedLen;
			}
			else
			{
				if (this.data_type == 2)
				{
					this.set_data_type();
				}
				this.l_desc.build_tree(this);
				this.d_desc.build_tree(this);
				num2 = this.build_bl_tree();
				num = SupportClass.URShift(this.opt_len + 3 + 7, 3);
				num1 = SupportClass.URShift(this.static_len + 3 + 7, 3);
				if (num1 <= num)
				{
					num = num1;
				}
			}
			if (stored_len + 4 <= num && buf != -1)
			{
				this._tr_stored_block(buf, stored_len, eof);
			}
			else if (num1 != num)
			{
				this.send_bits(4 + (eof ? 1 : 0), 3);
				this.send_all_trees(this.l_desc.max_code + 1, this.d_desc.max_code + 1, num2 + 1);
				this.compress_block(this.dyn_ltree, this.dyn_dtree);
			}
			else
			{
				this.send_bits(2 + (eof ? 1 : 0), 3);
				this.compress_block(StaticTree.static_ltree, StaticTree.static_dtree);
			}
			this.init_block();
			if (eof)
			{
				this.bi_windup();
			}
		}

		internal void _tr_stored_block(int buf, int stored_len, bool eof)
		{
			this.send_bits((eof ? 1 : 0), 3);
			this.copy_block(buf, stored_len, true);
		}

		internal bool _tr_tally(int dist, int lc)
		{
			this.pending_buf[this.d_buf + this.last_lit * 2] = (byte)SupportClass.URShift(dist, 8);
			this.pending_buf[this.d_buf + this.last_lit * 2 + 1] = (byte)dist;
			this.pending_buf[this.l_buf + this.last_lit] = (byte)lc;
			Deflate lastLit = this;
			lastLit.last_lit = lastLit.last_lit + 1;
			if (dist != 0)
			{
				Deflate deflate = this;
				deflate.matches = deflate.matches + 1;
				dist--;
				this.dyn_ltree[(Tree._length_code[lc] + 256 + 1) * 2] = (short)(this.dyn_ltree[(Tree._length_code[lc] + 256 + 1) * 2] + 1);
				this.dyn_dtree[Tree.d_code(dist) * 2] = (short)(this.dyn_dtree[Tree.d_code(dist) * 2] + 1);
			}
			else
			{
				this.dyn_ltree[lc * 2] = (short)(this.dyn_ltree[lc * 2] + 1);
			}
			if ((this.last_lit & 8191) == 0 && this.level > 2)
			{
				int dynDtree = this.last_lit * 8;
				int blockStart = this.strstart - this.block_start;
				for (int i = 0; i < 30; i++)
				{
					dynDtree = (int)((long)dynDtree + (long)this.dyn_dtree[i * 2] * ((long)5 + (long)Tree.extra_dbits[i]));
				}
				dynDtree = SupportClass.URShift(dynDtree, 3);
				if (this.matches < this.last_lit / 2 && dynDtree < blockStart / 2)
				{
					return true;
				}
			}
			return this.last_lit == this.lit_bufsize - 1;
		}

		internal void bi_flush()
		{
			if (this.bi_valid == 16)
			{
				this.put_short(this.bi_buf);
				this.bi_buf = 0;
				this.bi_valid = 0;
				return;
			}
			if (this.bi_valid >= 8)
			{
				this.put_byte((byte)this.bi_buf);
				this.bi_buf = (short)SupportClass.URShift(this.bi_buf, 8);
				Deflate biValid = this;
				biValid.bi_valid = biValid.bi_valid - 8;
			}
		}

		internal void bi_windup()
		{
			if (this.bi_valid > 8)
			{
				this.put_short(this.bi_buf);
			}
			else if (this.bi_valid > 0)
			{
				this.put_byte((byte)this.bi_buf);
			}
			this.bi_buf = 0;
			this.bi_valid = 0;
		}

		internal int build_bl_tree()
		{
			this.scan_tree(this.dyn_ltree, this.l_desc.max_code);
			this.scan_tree(this.dyn_dtree, this.d_desc.max_code);
			this.bl_desc.build_tree(this);
			int num = 18;
			while (num >= 3 && this.bl_tree[Tree.bl_order[num] * 2 + 1] == 0)
			{
				num--;
			}
			Deflate optLen = this;
			optLen.opt_len = optLen.opt_len + 3 * (num + 1) + 5 + 5 + 4;
			return num;
		}

		internal void compress_block(short[] ltree, short[] dtree)
		{
			int num = 0;
			if (this.last_lit != 0)
			{
				do
				{
					int pendingBuf = this.pending_buf[this.d_buf + num * 2] << 8 & 65280 | this.pending_buf[this.d_buf + num * 2 + 1] & 255;
					int baseLength = this.pending_buf[this.l_buf + num] & 255;
					num++;
					if (pendingBuf != 0)
					{
						int _lengthCode = Tree._length_code[baseLength];
						this.send_code(_lengthCode + 256 + 1, ltree);
						int extraLbits = Tree.extra_lbits[_lengthCode];
						if (extraLbits != 0)
						{
							baseLength = baseLength - Tree.base_length[_lengthCode];
							this.send_bits(baseLength, extraLbits);
						}
						pendingBuf--;
						_lengthCode = Tree.d_code(pendingBuf);
						this.send_code(_lengthCode, dtree);
						extraLbits = Tree.extra_dbits[_lengthCode];
						if (extraLbits == 0)
						{
							continue;
						}
						pendingBuf = pendingBuf - Tree.base_dist[_lengthCode];
						this.send_bits(pendingBuf, extraLbits);
					}
					else
					{
						this.send_code(baseLength, ltree);
					}
				}
				while (num < this.last_lit);
			}
			this.send_code(256, ltree);
			this.last_eob_len = ltree[513];
		}

		internal void copy_block(int buf, int len, bool header)
		{
			this.bi_windup();
			this.last_eob_len = 8;
			if (header)
			{
				this.put_short((short)len);
				this.put_short((short)(~len));
			}
			this.put_byte(this.window, buf, len);
		}

		internal int deflate(ZStream strm, int flush)
		{
			if (flush > 4 || flush < 0)
			{
				return -2;
			}
			if (strm.next_out == null || strm.next_in == null && strm.avail_in != 0 || this.status == 666 && flush != 4)
			{
				strm.msg = Deflate.z_errmsg[4];
				return -2;
			}
			if (strm.avail_out == 0)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			this.strm = strm;
			int lastFlush = this.last_flush;
			this.last_flush = flush;
			if (this.status == 42)
			{
				int wBits = 8 + (this.w_bits - 8 << 4) << 8;
				int num = (this.level - 1 & 255) >> 1;
				if (num > 3)
				{
					num = 3;
				}
				wBits = wBits | num << 6;
				if (this.strstart != 0)
				{
					wBits = wBits | 32;
				}
				wBits = wBits + (31 - wBits % 31);
				this.status = 113;
				this.putShortMSB(wBits);
				if (this.strstart != 0)
				{
					this.putShortMSB((int)SupportClass.URShift(strm.adler, 16));
					this.putShortMSB((int)(strm.adler & (long)65535));
				}
				strm.adler = strm._adler.adler32((long)0, null, 0, 0);
			}
			if (this.pending != 0)
			{
				strm.flush_pending();
				if (strm.avail_out == 0)
				{
					this.last_flush = -1;
					return 0;
				}
			}
			else if (strm.avail_in == 0 && flush <= lastFlush && flush != 4)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			if (this.status == 666 && strm.avail_in != 0)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			if (strm.avail_in != 0 || this.lookahead != 0 || flush != 0 && this.status != 666)
			{
				int num1 = -1;
				switch (Deflate.config_table[this.level].func)
				{
					case 0:
					{
						num1 = this.deflate_stored(flush);
						break;
					}
					case 1:
					{
						num1 = this.deflate_fast(flush);
						break;
					}
					case 2:
					{
						num1 = this.deflate_slow(flush);
						break;
					}
				}
				if (num1 == 2 || num1 == 3)
				{
					this.status = 666;
				}
				if (num1 == 0 || num1 == 2)
				{
					if (strm.avail_out == 0)
					{
						this.last_flush = -1;
					}
					return 0;
				}
				if (num1 == 1)
				{
					if (flush != 1)
					{
						this._tr_stored_block(0, 0, false);
						if (flush == 3)
						{
							for (int i = 0; i < this.hash_size; i++)
							{
								this.head[i] = 0;
							}
						}
					}
					else
					{
						this._tr_align();
					}
					strm.flush_pending();
					if (strm.avail_out == 0)
					{
						this.last_flush = -1;
						return 0;
					}
				}
			}
			if (flush != 4)
			{
				return 0;
			}
			if (this.noheader != 0)
			{
				return 1;
			}
			this.putShortMSB((int)SupportClass.URShift(strm.adler, 16));
			this.putShortMSB((int)(strm.adler & (long)65535));
			strm.flush_pending();
			this.noheader = -1;
			if (this.pending == 0)
			{
				return 1;
			}
			return 0;
		}

		internal int deflate_fast(int flush)
		{
			bool flag;
			int num;
			int num1 = 0;
			do
			{
			Label0:
				if (this.lookahead < Deflate.MIN_LOOKAHEAD)
				{
					this.fill_window();
					if (this.lookahead < Deflate.MIN_LOOKAHEAD && flush == 0)
					{
						return 0;
					}
					if (this.lookahead == 0)
					{
						this.flush_block_only(flush == 4);
						if (this.strm.avail_out == 0)
						{
							if (flush == 4)
							{
								return 2;
							}
							return 0;
						}
						if (flush != 4)
						{
							return 1;
						}
						return 3;
					}
				}
				if (this.lookahead >= 3)
				{
					this.ins_h = (this.ins_h << (this.hash_shift & 31) ^ this.window[this.strstart + 2] & 255) & this.hash_mask;
					num1 = this.head[this.ins_h] & 65535;
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				if ((long)num1 != (long)0 && (this.strstart - num1 & 65535) <= this.w_size - Deflate.MIN_LOOKAHEAD && this.strategy != 2)
				{
					this.match_length = this.longest_match(num1);
				}
				if (this.match_length < 3)
				{
					flag = this._tr_tally(0, this.window[this.strstart] & 255);
					Deflate deflate = this;
					deflate.lookahead = deflate.lookahead - 1;
					Deflate deflate1 = this;
					deflate1.strstart = deflate1.strstart + 1;
				}
				else
				{
					flag = this._tr_tally(this.strstart - this.match_start, this.match_length - 3);
					Deflate matchLength = this;
					matchLength.lookahead = matchLength.lookahead - this.match_length;
					if (this.match_length > this.max_lazy_match || this.lookahead < 3)
					{
						Deflate matchLength1 = this;
						matchLength1.strstart = matchLength1.strstart + this.match_length;
						this.match_length = 0;
						this.ins_h = this.window[this.strstart] & 255;
						this.ins_h = (this.ins_h << (this.hash_shift & 31) ^ this.window[this.strstart + 1] & 255) & this.hash_mask;
					}
					else
					{
						Deflate matchLength2 = this;
						matchLength2.match_length = matchLength2.match_length - 1;
						do
						{
							Deflate deflate2 = this;
							deflate2.strstart = deflate2.strstart + 1;
							this.ins_h = (this.ins_h << (this.hash_shift & 31) ^ this.window[this.strstart + 2] & 255) & this.hash_mask;
							num1 = this.head[this.ins_h] & 65535;
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
							Deflate deflate3 = this;
							int num2 = deflate3.match_length - 1;
							num = num2;
							deflate3.match_length = num2;
						}
						while (num != 0);
						Deflate deflate4 = this;
						deflate4.strstart = deflate4.strstart + 1;
					}
				}
				if (flag)
				{
					this.flush_block_only(false);
				}
				else
				{
					goto Label0;
				}
			}
			while (this.strm.avail_out != 0);
			return 0;
		}

		internal int deflate_slow(int flush)
		{
			bool flag;
			int num;
			int num1 = 0;
			do
			{
			Label0:
				if (this.lookahead < Deflate.MIN_LOOKAHEAD)
				{
					this.fill_window();
					if (this.lookahead < Deflate.MIN_LOOKAHEAD && flush == 0)
					{
						return 0;
					}
					if (this.lookahead == 0)
					{
						if (this.match_available != 0)
						{
							flag = this._tr_tally(0, this.window[this.strstart - 1] & 255);
							this.match_available = 0;
						}
						this.flush_block_only(flush == 4);
						if (this.strm.avail_out == 0)
						{
							if (flush == 4)
							{
								return 2;
							}
							return 0;
						}
						if (flush != 4)
						{
							return 1;
						}
						return 3;
					}
				}
				if (this.lookahead >= 3)
				{
					this.ins_h = (this.ins_h << (this.hash_shift & 31) ^ this.window[this.strstart + 2] & 255) & this.hash_mask;
					num1 = this.head[this.ins_h] & 65535;
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				this.prev_length = this.match_length;
				this.prev_match = this.match_start;
				this.match_length = 2;
				if (num1 != 0 && this.prev_length < this.max_lazy_match && (this.strstart - num1 & 65535) <= this.w_size - Deflate.MIN_LOOKAHEAD)
				{
					if (this.strategy != 2)
					{
						this.match_length = this.longest_match(num1);
					}
					if (this.match_length <= 5 && (this.strategy == 1 || this.match_length == 3 && this.strstart - this.match_start > 4096))
					{
						this.match_length = 2;
					}
				}
				if (this.prev_length >= 3 && this.match_length <= this.prev_length)
				{
					int num2 = this.strstart + this.lookahead - 3;
					flag = this._tr_tally(this.strstart - 1 - this.prev_match, this.prev_length - 3);
					Deflate prevLength = this;
					prevLength.lookahead = prevLength.lookahead - (this.prev_length - 1);
					Deflate deflate = this;
					deflate.prev_length = deflate.prev_length - 2;
					do
					{
						Deflate deflate1 = this;
						int num3 = deflate1.strstart + 1;
						int num4 = num3;
						deflate1.strstart = num3;
						if (num4 <= num2)
						{
							this.ins_h = (this.ins_h << (this.hash_shift & 31) ^ this.window[this.strstart + 2] & 255) & this.hash_mask;
							num1 = this.head[this.ins_h] & 65535;
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
						}
						Deflate deflate2 = this;
						int prevLength1 = deflate2.prev_length - 1;
						num = prevLength1;
						deflate2.prev_length = prevLength1;
					}
					while (num != 0);
					this.match_available = 0;
					this.match_length = 2;
					Deflate deflate3 = this;
					deflate3.strstart = deflate3.strstart + 1;
					if (flag)
					{
						this.flush_block_only(false);
						if (this.strm.avail_out == 0)
						{
							return 0;
						}
						else
						{
							goto Label0;
						}
					}
					else
					{
						goto Label0;
					}
				}
				else if (this.match_available == 0)
				{
					this.match_available = 1;
					Deflate deflate4 = this;
					deflate4.strstart = deflate4.strstart + 1;
					Deflate deflate5 = this;
					deflate5.lookahead = deflate5.lookahead - 1;
					goto Label0;
				}
				else
				{
					flag = this._tr_tally(0, this.window[this.strstart - 1] & 255);
					if (flag)
					{
						this.flush_block_only(false);
					}
					Deflate deflate6 = this;
					deflate6.strstart = deflate6.strstart + 1;
					Deflate deflate7 = this;
					deflate7.lookahead = deflate7.lookahead - 1;
				}
			}
			while (this.strm.avail_out != 0);
			return 0;
		}

		internal int deflate_stored(int flush)
		{
			int pendingBufSize = 65535;
			if (pendingBufSize > this.pending_buf_size - 5)
			{
				pendingBufSize = this.pending_buf_size - 5;
			}
			do
			{
			Label0:
				if (this.lookahead <= 1)
				{
					this.fill_window();
					if (this.lookahead == 0 && flush == 0)
					{
						return 0;
					}
					if (this.lookahead == 0)
					{
						this.flush_block_only(flush == 4);
						if (this.strm.avail_out == 0)
						{
							if (flush != 4)
							{
								return 0;
							}
							return 2;
						}
						if (flush != 4)
						{
							return 1;
						}
						return 3;
					}
				}
				Deflate deflate = this;
				deflate.strstart = deflate.strstart + this.lookahead;
				this.lookahead = 0;
				int blockStart = this.block_start + pendingBufSize;
				if (this.strstart == 0 || this.strstart >= blockStart)
				{
					this.lookahead = this.strstart - blockStart;
					this.strstart = blockStart;
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
				if (this.strstart - this.block_start >= this.w_size - Deflate.MIN_LOOKAHEAD)
				{
					this.flush_block_only(false);
				}
				else
				{
					goto Label0;
				}
			}
			while (this.strm.avail_out != 0);
			return 0;
		}

		internal int deflateEnd()
		{
			if (this.status != 42 && this.status != 113 && this.status != 666)
			{
				return -2;
			}
			this.pending_buf = null;
			this.head = null;
			this.prev = null;
			this.window = null;
			if (this.status != 113)
			{
				return 0;
			}
			return -3;
		}

		internal int deflateInit(ZStream strm, int level, int bits)
		{
			return this.deflateInit2(strm, level, 8, bits, 8, 0);
		}

		internal int deflateInit(ZStream strm, int level)
		{
			return this.deflateInit(strm, level, 15);
		}

		internal int deflateInit2(ZStream strm, int level, int method, int windowBits, int memLevel, int strategy)
		{
			int num = 0;
			strm.msg = null;
			if (level == -1)
			{
				level = 6;
			}
			if (windowBits < 0)
			{
				num = 1;
				windowBits = -windowBits;
			}
			if (memLevel < 1 || memLevel > 9 || method != 8 || windowBits < 9 || windowBits > 15 || level < 0 || level > 9 || strategy < 0 || strategy > 2)
			{
				return -2;
			}
			strm.dstate = this;
			this.noheader = num;
			this.w_bits = windowBits;
			this.w_size = 1 << (this.w_bits & 31);
			this.w_mask = this.w_size - 1;
			this.hash_bits = memLevel + 7;
			this.hash_size = 1 << (this.hash_bits & 31);
			this.hash_mask = this.hash_size - 1;
			this.hash_shift = (this.hash_bits + 3 - 1) / 3;
			this.window = new byte[this.w_size * 2];
			this.prev = new short[this.w_size];
			this.head = new short[this.hash_size];
			this.lit_bufsize = 1 << (memLevel + 6 & 31);
			this.pending_buf = new byte[this.lit_bufsize * 4];
			this.pending_buf_size = this.lit_bufsize * 4;
			this.d_buf = this.lit_bufsize;
			this.l_buf = 3 * this.lit_bufsize;
			this.level = level;
			this.strategy = strategy;
			this.method = (byte)method;
			return this.deflateReset(strm);
		}

		internal int deflateParams(ZStream strm, int _level, int _strategy)
		{
			int num = 0;
			if (_level == -1)
			{
				_level = 6;
			}
			if (_level < 0 || _level > 9 || _strategy < 0 || _strategy > 2)
			{
				return -2;
			}
			if (Deflate.config_table[this.level].func != Deflate.config_table[_level].func && strm.total_in != (long)0)
			{
				num = strm.deflate(1);
			}
			if (this.level != _level)
			{
				this.level = _level;
				this.max_lazy_match = Deflate.config_table[this.level].max_lazy;
				this.good_match = Deflate.config_table[this.level].good_length;
				this.nice_match = Deflate.config_table[this.level].nice_length;
				this.max_chain_length = Deflate.config_table[this.level].max_chain;
			}
			this.strategy = _strategy;
			return num;
		}

		internal int deflateReset(ZStream strm)
		{
			long num = (long)0;
			long num1 = num;
			strm.total_out = num;
			strm.total_in = num1;
			strm.msg = null;
			strm.data_type = 2;
			this.pending = 0;
			this.pending_out = 0;
			if (this.noheader < 0)
			{
				this.noheader = 0;
			}
			this.status = (this.noheader != 0 ? 113 : 42);
			strm.adler = strm._adler.adler32((long)0, null, 0, 0);
			this.last_flush = 0;
			this.tr_init();
			this.lm_init();
			return 0;
		}

		internal int deflateSetDictionary(ZStream strm, byte[] dictionary, int dictLength)
		{
			int wSize = dictLength;
			int num = 0;
			if (dictionary == null || this.status != 42)
			{
				return -2;
			}
			strm.adler = strm._adler.adler32(strm.adler, dictionary, 0, dictLength);
			if (wSize < 3)
			{
				return 0;
			}
			if (wSize > this.w_size - Deflate.MIN_LOOKAHEAD)
			{
				wSize = this.w_size - Deflate.MIN_LOOKAHEAD;
				num = dictLength - wSize;
			}
			Array.Copy(dictionary, num, this.window, 0, wSize);
			this.strstart = wSize;
			this.block_start = wSize;
			this.ins_h = this.window[0] & 255;
			this.ins_h = (this.ins_h << (this.hash_shift & 31) ^ this.window[1] & 255) & this.hash_mask;
			for (int i = 0; i <= wSize - 3; i++)
			{
				this.ins_h = (this.ins_h << (this.hash_shift & 31) ^ this.window[i + 2] & 255) & this.hash_mask;
				this.prev[i & this.w_mask] = this.head[this.ins_h];
				this.head[this.ins_h] = (short)i;
			}
			return 0;
		}

		internal void fill_window()
		{
			int hashSize;
			int num;
			object wSize;
			int num1;
			object obj;
			int num2;
			do
			{
				int windowSize = this.window_size - this.lookahead - this.strstart;
				if (windowSize == 0 && this.strstart == 0 && this.lookahead == 0)
				{
					windowSize = this.w_size;
				}
				else if (windowSize == -1)
				{
					windowSize--;
				}
				else if (this.strstart >= this.w_size + this.w_size - Deflate.MIN_LOOKAHEAD)
				{
					Array.Copy(this.window, this.w_size, this.window, 0, this.w_size);
					Deflate matchStart = this;
					matchStart.match_start = matchStart.match_start - this.w_size;
					Deflate deflate = this;
					deflate.strstart = deflate.strstart - this.w_size;
					Deflate blockStart = this;
					blockStart.block_start = blockStart.block_start - this.w_size;
					hashSize = this.hash_size;
					int num3 = hashSize;
					do
					{
						int num4 = num3 - 1;
						num3 = num4;
						num = this.head[num4] & 65535;
						short[] numArray = this.head;
						int num5 = num3;
						if (num >= this.w_size)
						{
							wSize = num - this.w_size;
						}
						else
						{
							wSize = null;
						}
						numArray[num5] = (short)wSize;
						num1 = hashSize - 1;
						hashSize = num1;
					}
					while (num1 != 0);
					hashSize = this.w_size;
					num3 = hashSize;
					do
					{
						int num6 = num3 - 1;
						num3 = num6;
						num = this.prev[num6] & 65535;
						short[] numArray1 = this.prev;
						int num7 = num3;
						if (num >= this.w_size)
						{
							obj = num - this.w_size;
						}
						else
						{
							obj = null;
						}
						numArray1[num7] = (short)obj;
						num2 = hashSize - 1;
						hashSize = num2;
					}
					while (num2 != 0);
					windowSize = windowSize + this.w_size;
				}
				if (this.strm.avail_in == 0)
				{
					return;
				}
				hashSize = this.strm.read_buf(this.window, this.strstart + this.lookahead, windowSize);
				Deflate deflate1 = this;
				deflate1.lookahead = deflate1.lookahead + hashSize;
				if (this.lookahead < 3)
				{
					continue;
				}
				this.ins_h = this.window[this.strstart] & 255;
				this.ins_h = (this.ins_h << (this.hash_shift & 31) ^ this.window[this.strstart + 1] & 255) & this.hash_mask;
			}
			while (this.lookahead < Deflate.MIN_LOOKAHEAD && this.strm.avail_in != 0);
		}

		internal void flush_block_only(bool eof)
		{
			this._tr_flush_block((this.block_start >= 0 ? this.block_start : -1), this.strstart - this.block_start, eof);
			this.block_start = this.strstart;
			this.strm.flush_pending();
		}

		internal void init_block()
		{
			for (int i = 0; i < Deflate.L_CODES; i++)
			{
				this.dyn_ltree[i * 2] = 0;
			}
			for (int j = 0; j < 30; j++)
			{
				this.dyn_dtree[j * 2] = 0;
			}
			for (int k = 0; k < 19; k++)
			{
				this.bl_tree[k * 2] = 0;
			}
			this.dyn_ltree[512] = 1;
			int num = 0;
			int num1 = num;
			this.static_len = num;
			this.opt_len = num1;
			int num2 = 0;
			int num3 = num2;
			this.matches = num2;
			this.last_lit = num3;
		}

		internal void lm_init()
		{
			this.window_size = 2 * this.w_size;
			this.head[this.hash_size - 1] = 0;
			for (int i = 0; i < this.hash_size - 1; i++)
			{
				this.head[i] = 0;
			}
			this.max_lazy_match = Deflate.config_table[this.level].max_lazy;
			this.good_match = Deflate.config_table[this.level].good_length;
			this.nice_match = Deflate.config_table[this.level].nice_length;
			this.max_chain_length = Deflate.config_table[this.level].max_chain;
			this.strstart = 0;
			this.block_start = 0;
			this.lookahead = 0;
			int num = 2;
			int num1 = num;
			this.prev_length = num;
			this.match_length = num1;
			this.match_available = 0;
			this.ins_h = 0;
		}

		internal int longest_match(int cur_match)
		{
			int num;
			int num1;
			int num2;
			int maxChainLength = this.max_chain_length;
			int num3 = this.strstart;
			int prevLength = this.prev_length;
			int num4 = (this.strstart > this.w_size - Deflate.MIN_LOOKAHEAD ? this.strstart - (this.w_size - Deflate.MIN_LOOKAHEAD) : 0);
			int niceMatch = this.nice_match;
			int wMask = this.w_mask;
			int num5 = this.strstart + 258;
			byte num6 = this.window[num3 + prevLength - 1];
			byte num7 = this.window[num3 + prevLength];
			if (this.prev_length >= this.good_match)
			{
				maxChainLength = maxChainLength >> 2;
			}
			if (niceMatch > this.lookahead)
			{
				niceMatch = this.lookahead;
			}
			do
			{
				int curMatch = cur_match;
				if (this.window[curMatch + prevLength] == num7 && this.window[curMatch + prevLength - 1] == num6 && this.window[curMatch] == this.window[num3])
				{
					int num8 = curMatch + 1;
					curMatch = num8;
					if (this.window[num8] == this.window[num3 + 1])
					{
						num3 = num3 + 2;
						curMatch++;
						do
						{
							int num9 = num3 + 1;
							num3 = num9;
							int num10 = curMatch + 1;
							curMatch = num10;
							if (this.window[num9] != this.window[num10])
							{
								break;
							}
							int num11 = num3 + 1;
							num3 = num11;
							int num12 = curMatch + 1;
							curMatch = num12;
							if (this.window[num11] != this.window[num12])
							{
								break;
							}
							int num13 = num3 + 1;
							num3 = num13;
							int num14 = curMatch + 1;
							curMatch = num14;
							if (this.window[num13] != this.window[num14])
							{
								break;
							}
							int num15 = num3 + 1;
							num3 = num15;
							int num16 = curMatch + 1;
							curMatch = num16;
							if (this.window[num15] != this.window[num16])
							{
								break;
							}
							int num17 = num3 + 1;
							num3 = num17;
							int num18 = curMatch + 1;
							curMatch = num18;
							if (this.window[num17] != this.window[num18])
							{
								break;
							}
							int num19 = num3 + 1;
							num3 = num19;
							int num20 = curMatch + 1;
							curMatch = num20;
							if (this.window[num19] != this.window[num20])
							{
								break;
							}
							int num21 = num3 + 1;
							num3 = num21;
							int num22 = curMatch + 1;
							curMatch = num22;
							if (this.window[num21] != this.window[num22])
							{
								break;
							}
							num1 = num3 + 1;
							num3 = num1;
							num2 = curMatch + 1;
							curMatch = num2;
						}
						while (this.window[num1] == this.window[num2] && num3 < num5);
						int num23 = 258 - (num5 - num3);
						num3 = num5 - 258;
						if (num23 > prevLength)
						{
							this.match_start = cur_match;
							prevLength = num23;
							if (num23 >= niceMatch)
							{
								break;
							}
							num6 = this.window[num3 + prevLength - 1];
							num7 = this.window[num3 + prevLength];
						}
					}
				}
				int num24 = this.prev[cur_match & wMask] & 65535;
				cur_match = num24;
				if (num24 <= num4)
				{
					break;
				}
				num = maxChainLength - 1;
				maxChainLength = num;
			}
			while (num != 0);
			if (prevLength <= this.lookahead)
			{
				return prevLength;
			}
			return this.lookahead;
		}

		internal void pqdownheap(short[] tree, int k)
		{
			int num = this.heap[k];
			for (int i = k << 1; i <= this.heap_len; i = i << 1)
			{
				if (i < this.heap_len && Deflate.smaller(tree, this.heap[i + 1], this.heap[i], this.depth))
				{
					i++;
				}
				if (Deflate.smaller(tree, num, this.heap[i], this.depth))
				{
					break;
				}
				this.heap[k] = this.heap[i];
				k = i;
			}
			this.heap[k] = num;
		}

		internal void put_byte(byte[] p, int start, int len)
		{
			Array.Copy(p, start, this.pending_buf, this.pending, len);
			Deflate deflate = this;
			deflate.pending = deflate.pending + len;
		}

		internal void put_byte(byte c)
		{
			byte[] pendingBuf = this.pending_buf;
			Deflate deflate = this;
			int num = deflate.pending;
			int num1 = num;
			deflate.pending = num + 1;
			pendingBuf[num1] = c;
		}

		internal void put_short(int w)
		{
			this.put_byte((byte)w);
			this.put_byte((byte)SupportClass.URShift(w, 8));
		}

		internal void putShortMSB(int b)
		{
			this.put_byte((byte)(b >> 8));
			this.put_byte((byte)b);
		}

		internal void scan_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num1 = tree[1];
			int num2 = 0;
			int num3 = 7;
			int num4 = 4;
			if (num1 == 0)
			{
				num3 = 138;
				num4 = 3;
			}
			tree[(max_code + 1) * 2 + 1] = (short)SupportClass.Identity((long)65535);
			for (int i = 0; i <= max_code; i++)
			{
				int num5 = num1;
				num1 = tree[(i + 1) * 2 + 1];
				int num6 = num2 + 1;
				num2 = num6;
				if (num6 >= num3 || num5 != num1)
				{
					if (num2 < num4)
					{
						this.bl_tree[num5 * 2] = (short)(this.bl_tree[num5 * 2] + num2);
					}
					else if (num5 != 0)
					{
						if (num5 != num)
						{
							this.bl_tree[num5 * 2] = (short)(this.bl_tree[num5 * 2] + 1);
						}
						this.bl_tree[32] = (short)(this.bl_tree[32] + 1);
					}
					else if (num2 > 10)
					{
						this.bl_tree[36] = (short)(this.bl_tree[36] + 1);
					}
					else
					{
						this.bl_tree[34] = (short)(this.bl_tree[34] + 1);
					}
					num2 = 0;
					num = num5;
					if (num1 == 0)
					{
						num3 = 138;
						num4 = 3;
					}
					else if (num5 != num1)
					{
						num3 = 7;
						num4 = 4;
					}
					else
					{
						num3 = 6;
						num4 = 3;
					}
				}
			}
		}

		internal void send_all_trees(int lcodes, int dcodes, int blcodes)
		{
			this.send_bits(lcodes - 257, 5);
			this.send_bits(dcodes - 1, 5);
			this.send_bits(blcodes - 4, 4);
			for (int i = 0; i < blcodes; i++)
			{
				this.send_bits(this.bl_tree[Tree.bl_order[i] * 2 + 1], 3);
			}
			this.send_tree(this.dyn_ltree, lcodes - 1);
			this.send_tree(this.dyn_dtree, dcodes - 1);
		}

		internal void send_bits(int value_Renamed, int length)
		{
			int num = length;
			if (this.bi_valid <= 16 - num)
			{
				this.bi_buf = (short)((ushort)this.bi_buf | (ushort)(value_Renamed << (this.bi_valid & 31) & 65535));
				Deflate biValid = this;
				biValid.bi_valid = biValid.bi_valid + num;
				return;
			}
			int valueRenamed = value_Renamed;
			this.bi_buf = (short)((ushort)this.bi_buf | (ushort)(valueRenamed << (this.bi_valid & 31) & 65535));
			this.put_short(this.bi_buf);
			this.bi_buf = (short)SupportClass.URShift(valueRenamed, 16 - this.bi_valid);
			Deflate deflate = this;
			deflate.bi_valid = deflate.bi_valid + (num - 16);
		}

		internal void send_code(int c, short[] tree)
		{
			this.send_bits(tree[c * 2] & 65535, tree[c * 2 + 1] & 65535);
		}

		internal void send_tree(short[] tree, int max_code)
		{
			int num;
			int num1 = -1;
			int num2 = tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = tree[(i + 1) * 2 + 1];
				int num7 = num3 + 1;
				num3 = num7;
				if (num7 >= num4 || num6 != num2)
				{
					if (num3 < num5)
					{
						do
						{
							this.send_code(num6, this.bl_tree);
							num = num3 - 1;
							num3 = num;
						}
						while (num != 0);
					}
					else if (num6 != 0)
					{
						if (num6 != num1)
						{
							this.send_code(num6, this.bl_tree);
							num3--;
						}
						this.send_code(16, this.bl_tree);
						this.send_bits(num3 - 3, 2);
					}
					else if (num3 > 10)
					{
						this.send_code(18, this.bl_tree);
						this.send_bits(num3 - 11, 7);
					}
					else
					{
						this.send_code(17, this.bl_tree);
						this.send_bits(num3 - 3, 3);
					}
					num3 = 0;
					num1 = num6;
					if (num2 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else if (num6 != num2)
					{
						num4 = 7;
						num5 = 4;
					}
					else
					{
						num4 = 6;
						num5 = 3;
					}
				}
			}
		}

		internal void set_data_type()
		{
			object obj;
			int num = 0;
			int dynLtree = 0;
			int dynLtree1 = 0;
			while (num < 7)
			{
				dynLtree1 = dynLtree1 + this.dyn_ltree[num * 2];
				num++;
			}
			while (num < 128)
			{
				dynLtree = dynLtree + this.dyn_ltree[num * 2];
				num++;
			}
			while (num < 256)
			{
				dynLtree1 = dynLtree1 + this.dyn_ltree[num * 2];
				num++;
			}
			if (dynLtree1 > SupportClass.URShift(dynLtree, 2))
			{
				obj = null;
			}
			else
			{
				obj = 1;
			}
			this.data_type = (byte)obj;
		}

		internal static bool smaller(short[] tree, int n, int m, byte[] depth)
		{
			if (tree[n * 2] < tree[m * 2])
			{
				return true;
			}
			if (tree[n * 2] != tree[m * 2])
			{
				return false;
			}
			return depth[n] <= depth[m];
		}

		internal void tr_init()
		{
			this.l_desc.dyn_tree = this.dyn_ltree;
			this.l_desc.stat_desc = StaticTree.static_l_desc;
			this.d_desc.dyn_tree = this.dyn_dtree;
			this.d_desc.stat_desc = StaticTree.static_d_desc;
			this.bl_desc.dyn_tree = this.bl_tree;
			this.bl_desc.stat_desc = StaticTree.static_bl_desc;
			this.bi_buf = 0;
			this.bi_valid = 0;
			this.last_eob_len = 8;
			this.init_block();
		}

		internal class Config
		{
			internal int good_length;

			internal int max_lazy;

			internal int nice_length;

			internal int max_chain;

			internal int func;

			internal Config(int good_length, int max_lazy, int nice_length, int max_chain, int func)
			{
				this.good_length = good_length;
				this.max_lazy = max_lazy;
				this.nice_length = nice_length;
				this.max_chain = max_chain;
				this.func = func;
			}
		}
	}
}