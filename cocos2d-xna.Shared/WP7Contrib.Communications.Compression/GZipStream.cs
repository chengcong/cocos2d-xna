using System;
using System.IO;
using System.Text;

namespace WP7Contrib.Communications.Compression
{
	internal class GZipStream : Stream
	{
		internal static DateTime _unixEpoch;

		internal static Encoding iso8859dash1;

		public DateTime? LastModified;

		internal ZlibBaseStream _baseStream;

		private bool _disposed;

		private bool _firstReadDone;

		private string _FileName;

		private string _Comment;

		private int _Crc32;

		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				if (this._baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 128)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer.", value));
				}
				this._baseStream._bufferSize = value;
			}
		}

		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanRead;
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
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._Comment = value;
			}
		}

		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._FileName = value;
				if (this._FileName == null)
				{
					return;
				}
				if (this._FileName.IndexOf("/") != -1)
				{
					this._FileName = this._FileName.Replace("/", "\\");
				}
				if (this._FileName.EndsWith("\\"))
				{
					throw new Exception("Illegal filename");
				}
				if (this._FileName.IndexOf("\\") == -1)
				{
					return;
				}
				this._FileName = Path.GetFileName(this._FileName);
			}
		}

		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		public override long Length
		{
			get
			{
				return this._baseStream.Length;
			}
		}

		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode != ZlibBaseStream.StreamMode.Reader)
				{
					return (long)0;
				}
				return this._baseStream._z.TotalBytesIn + (long)this._baseStream._gzipHeaderByteCount;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		static GZipStream()
		{
			GZipStream._unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			GZipStream.iso8859dash1 = Encoding.GetEncoding("iso-8859-1");
		}

		public GZipStream(Stream stream)
		{
			this._baseStream = new ZlibBaseStream(stream, ZlibStreamFlavor.GZIP, false);
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
						this._Crc32 = this._baseStream.Crc32;
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private int EmitHeader()
		{
			byte[] bytes;
			byte[] numArray;
			if (this.Comment == null)
			{
				bytes = null;
			}
			else
			{
				bytes = GZipStream.iso8859dash1.GetBytes(this.Comment);
			}
			byte[] numArray1 = bytes;
			if (this.FileName == null)
			{
				numArray = null;
			}
			else
			{
				numArray = GZipStream.iso8859dash1.GetBytes(this.FileName);
			}
			byte[] numArray2 = numArray;
			int num = (this.Comment == null ? 0 : (int)numArray1.Length + 1);
			int num1 = (this.FileName == null ? 0 : (int)numArray2.Length + 1);
			byte[] numArray3 = new byte[10 + num + num1];
			int num2 = 0;
			byte[] numArray4 = numArray3;
			int num3 = num2;
			int num4 = num3 + 1;
			numArray4[num3] = (byte)31;
			byte[] numArray5 = numArray3;
			int num5 = num4;
			int num6 = num5 + 1;
			numArray5[num5] = (byte)139;
			byte[] numArray6 = numArray3;
			int num7 = num6;
			int num8 = num7 + 1;
			numArray6[num7] = (byte)8;
			byte num9 = 0;
			if (this.Comment != null)
			{
				num9 = (byte)(num9 ^ 16);
			}
			if (this.FileName != null)
			{
				num9 = (byte)(num9 ^ 8);
			}
			byte[] numArray7 = numArray3;
			int num10 = num8;
			int num11 = num10 + 1;
			numArray7[num10] = (byte)num9;
			if (!this.LastModified.HasValue)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			TimeSpan value = this.LastModified.Value - GZipStream._unixEpoch;
			Array.Copy(BitConverter.GetBytes((int)value.TotalSeconds), 0, numArray3, num11, 4);
			int num12 = num11 + 4;
			byte[] numArray8 = numArray3;
			int num13 = num12;
			int num14 = num13 + 1;
			numArray8[num13] = (byte)0;
			byte[] numArray9 = numArray3;
			int num15 = num14;
			int num16 = num15 + 1;
			numArray9[num15] = (byte)255;
			if (num1 != 0)
			{
				Array.Copy(numArray2, 0, numArray3, num16, num1 - 1);
				int num17 = num16 + (num1 - 1);
				byte[] numArray10 = numArray3;
				int num18 = num17;
				num16 = num18 + 1;
				numArray10[num18] = (byte)0;
			}
			if (num != 0)
			{
				Array.Copy(numArray1, 0, numArray3, num16, num - 1);
				int num19 = num16 + (num - 1);
				byte[] numArray11 = numArray3;
				int num20 = num19;
				numArray11[num20] = (byte)0;
			}
			this._baseStream._stream.Write(numArray3, 0, (int)numArray3.Length);
			return (int)numArray3.Length;
		}

		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int num = this._baseStream.Read(buffer, offset, count);
			if (!this._firstReadDone)
			{
				this._firstReadDone = true;
				this.FileName = this._baseStream._GzipFileName;
				this.Comment = this._baseStream._GzipComment;
			}
			return num;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
	}
}