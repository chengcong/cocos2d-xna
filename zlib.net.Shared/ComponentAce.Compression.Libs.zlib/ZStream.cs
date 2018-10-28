using System;

namespace ComponentAce.Compression.Libs.zlib
{
	public sealed class ZStream
	{
		private const int MAX_WBITS = 15;

		private const int Z_NO_FLUSH = 0;

		private const int Z_PARTIAL_FLUSH = 1;

		private const int Z_SYNC_FLUSH = 2;

		private const int Z_FULL_FLUSH = 3;

		private const int Z_FINISH = 4;

		private const int MAX_MEM_LEVEL = 9;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private readonly static int DEF_WBITS;

		public byte[] next_in;

		public int next_in_index;

		public int avail_in;

		public long total_in;

		public byte[] next_out;

		public int next_out_index;

		public int avail_out;

		public long total_out;

		public string msg;

		internal Deflate dstate;

		internal Inflate istate;

		internal int data_type;

		public long adler;

		internal Adler32 _adler = new Adler32();

		static ZStream()
		{
			ZStream.DEF_WBITS = 15;
		}

		public ZStream()
		{
		}

		public int deflate(int flush)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflate(this, flush);
		}

		public int deflateEnd()
		{
			if (this.dstate == null)
			{
				return -2;
			}
			int num = this.dstate.deflateEnd();
			this.dstate = null;
			return num;
		}

		public int deflateInit(int level)
		{
			return this.deflateInit(level, 15);
		}

		public int deflateInit(int level, int bits)
		{
			this.dstate = new Deflate();
			return this.dstate.deflateInit(this, level, bits);
		}

		public int deflateParams(int level, int strategy)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateParams(this, level, strategy);
		}

		public int deflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateSetDictionary(this, dictionary, dictLength);
		}

		internal void flush_pending()
		{
			int availOut = this.dstate.pending;
			if (availOut > this.avail_out)
			{
				availOut = this.avail_out;
			}
			if (availOut == 0)
			{
				return;
			}
			if ((int)this.dstate.pending_buf.Length > this.dstate.pending_out && (int)this.next_out.Length > this.next_out_index && (int)this.dstate.pending_buf.Length >= this.dstate.pending_out + availOut)
			{
				int length = (int)this.next_out.Length;
			}
			Array.Copy(this.dstate.pending_buf, this.dstate.pending_out, this.next_out, this.next_out_index, availOut);
			ZStream nextOutIndex = this;
			nextOutIndex.next_out_index = nextOutIndex.next_out_index + availOut;
			Deflate pendingOut = this.dstate;
			pendingOut.pending_out = pendingOut.pending_out + availOut;
			ZStream totalOut = this;
			totalOut.total_out = totalOut.total_out + (long)availOut;
			ZStream zStream = this;
			zStream.avail_out = zStream.avail_out - availOut;
			Deflate deflate = this.dstate;
			deflate.pending = deflate.pending - availOut;
			if (this.dstate.pending == 0)
			{
				this.dstate.pending_out = 0;
			}
		}

		public void free()
		{
			this.next_in = null;
			this.next_out = null;
			this.msg = null;
			this._adler = null;
		}

		public int inflate(int f)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflate(this, f);
		}

		public int inflateEnd()
		{
			if (this.istate == null)
			{
				return -2;
			}
			int num = this.istate.inflateEnd(this);
			this.istate = null;
			return num;
		}

		public int inflateInit()
		{
			return this.inflateInit(ZStream.DEF_WBITS);
		}

		public int inflateInit(int w)
		{
			this.istate = new Inflate();
			return this.istate.inflateInit(this, w);
		}

		public int inflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSetDictionary(this, dictionary, dictLength);
		}

		public int inflateSync()
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSync(this);
		}

		internal int read_buf(byte[] buf, int start, int size)
		{
			int availIn = this.avail_in;
			if (availIn > size)
			{
				availIn = size;
			}
			if (availIn == 0)
			{
				return 0;
			}
			ZStream zStream = this;
			zStream.avail_in = zStream.avail_in - availIn;
			if (this.dstate.noheader == 0)
			{
				this.adler = this._adler.adler32(this.adler, this.next_in, this.next_in_index, availIn);
			}
			Array.Copy(this.next_in, this.next_in_index, buf, start, availIn);
			ZStream nextInIndex = this;
			nextInIndex.next_in_index = nextInIndex.next_in_index + availIn;
			ZStream totalIn = this;
			totalIn.total_in = totalIn.total_in + (long)availIn;
			return availIn;
		}
	}
}