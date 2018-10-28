using System;
using System.IO;

namespace ComponentAce.Compression.Libs.zlib
{
	public class ZOutputStream : Stream
	{
		protected internal ZStream z = new ZStream();

		protected internal int bufsize = 4096;

		protected internal int flush_Renamed_Field;

		protected internal byte[] buf;

		protected internal byte[] buf1 = new byte[1];

		protected internal bool compress;

		private Stream out_Renamed;

		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public virtual int FlushMode
		{
			get
			{
				return this.flush_Renamed_Field;
			}
			set
			{
				this.flush_Renamed_Field = value;
			}
		}

		public override long Length
		{
			get
			{
				return (long)0;
			}
		}

		public override long Position
		{
			get
			{
				return (long)0;
			}
			set
			{
			}
		}

		public virtual long TotalIn
		{
			get
			{
				return this.z.total_in;
			}
		}

		public virtual long TotalOut
		{
			get
			{
				return this.z.total_out;
			}
		}

		public ZOutputStream(Stream out_Renamed)
		{
			this.InitBlock();
			this.out_Renamed = out_Renamed;
			this.z.inflateInit();
			this.compress = false;
		}

		public ZOutputStream(Stream out_Renamed, int level)
		{
			this.InitBlock();
			this.out_Renamed = out_Renamed;
			this.z.deflateInit(level);
			this.compress = true;
		}

		public override void Close()
		{
			try
			{
				try
				{
					this.finish();
				}
				catch
				{
				}
			}
			finally
			{
				this.end();
				this.out_Renamed.Close();
				this.out_Renamed = null;
			}
		}

		public virtual void end()
		{
			if (!this.compress)
			{
				this.z.inflateEnd();
			}
			else
			{
				this.z.deflateEnd();
			}
			this.z.free();
			this.z = null;
		}

		public virtual void finish()
		{
			int num;
			do
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = this.bufsize;
				num = (!this.compress ? this.z.inflate(4) : this.z.deflate(4));
				if (num != 1 && num != 0)
				{
					throw new ZStreamException(string.Concat((this.compress ? "de" : "in"), "flating: ", this.z.msg));
				}
				if (this.bufsize - this.z.avail_out <= 0)
				{
					continue;
				}
				this.out_Renamed.Write(this.buf, 0, this.bufsize - this.z.avail_out);
			}
			while (this.z.avail_in > 0 || this.z.avail_out == 0);
			try
			{
				this.Flush();
			}
			catch
			{
			}
		}

		public override void Flush()
		{
			this.out_Renamed.Flush();
		}

		private void InitBlock()
		{
			this.flush_Renamed_Field = 0;
			this.buf = new byte[this.bufsize];
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return (long)0;
		}

		public override void SetLength(long value)
		{
		}

		public override void Write(byte[] b1, int off, int len)
		{
			int num;
			if (len == 0)
			{
				return;
			}
			byte[] numArray = new byte[(int)b1.Length];
			Array.Copy(b1, 0, numArray, 0, (int)b1.Length);
			this.z.next_in = numArray;
			this.z.next_in_index = off;
			this.z.avail_in = len;
			do
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = this.bufsize;
				num = (!this.compress ? this.z.inflate(this.flush_Renamed_Field) : this.z.deflate(this.flush_Renamed_Field));
				if (num != 0 && num != 1)
				{
					throw new ZStreamException(string.Concat((this.compress ? "de" : "in"), "flating: ", this.z.msg));
				}
				this.out_Renamed.Write(this.buf, 0, this.bufsize - this.z.avail_out);
			}
			while (this.z.avail_in > 0 || this.z.avail_out == 0);
		}

		public void WriteByte(int b)
		{
			this.buf1[0] = (byte)b;
			this.Write(this.buf1, 0, 1);
		}

		public override void WriteByte(byte b)
		{
			this.WriteByte((int)b);
		}
	}
}