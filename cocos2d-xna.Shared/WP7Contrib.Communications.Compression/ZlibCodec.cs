using System;

namespace WP7Contrib.Communications.Compression
{
	internal sealed class ZlibCodec
	{
		public int WindowBits = 15;

		public byte[] InputBuffer;

		public int NextIn;

		public int AvailableBytesIn;

		public long TotalBytesIn;

		public byte[] OutputBuffer;

		public int NextOut;

		public int AvailableBytesOut;

		public long TotalBytesOut;

		public string Message;

		internal InflateManager istate;

		internal long _Adler32;

		public long Adler32
		{
			get
			{
				return this._Adler32;
			}
		}

		public ZlibCodec()
		{
			if (this.InitializeInflate() != 0)
			{
				throw new ZlibException("Cannot initialize for inflate.");
			}
		}

		public int EndInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			int num = this.istate.End();
			this.istate = null;
			return num;
		}

		public int Inflate(FlushType flush)
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			this.WindowBits = windowBits;
			this.istate = new InflateManager(expectRfc1950Header);
			return this.istate.Initialize(this, windowBits);
		}

		public int SetDictionary(byte[] dictionary)
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate state!");
			}
			return this.istate.SetDictionary(dictionary);
		}

		public int SyncInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}
	}
}