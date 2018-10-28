namespace WP7Contrib.Communications.Compression
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class ZlibBaseStream : Stream
    {
        protected internal byte[] _buf1 = new byte[1];
        protected internal int _bufferSize = 0x2000;
        protected internal ZlibStreamFlavor _flavor;
        protected internal FlushType _flushMode = FlushType.None;
        protected internal string _GzipComment;
        protected internal string _GzipFileName;
        protected internal int _gzipHeaderByteCount;
        protected internal DateTime _GzipMtime;
        protected internal bool _leaveOpen;
        protected internal Stream _stream;
        protected internal StreamMode _streamMode = StreamMode.Undefined;
        protected internal byte[] _workingBuffer;
        protected internal ZlibCodec _z;
        private WP7Contrib.Communications.Compression.Crc32 crc;
        private bool nomoreinput;

        public ZlibBaseStream(Stream stream, ZlibStreamFlavor flavor, bool leaveOpen)
        {
            this._stream = stream;
            this._leaveOpen = leaveOpen;
            this._flavor = flavor;
            if (flavor == ZlibStreamFlavor.GZIP)
            {
                this.crc = new WP7Contrib.Communications.Compression.Crc32();
            }
        }

        private int _ReadAndValidateGzipHeader()
        {
            int num = 0;
            byte[] buffer = new byte[10];
            int num2 = this._stream.Read(buffer, 0, buffer.Length);
            switch (num2)
            {
                case 0:
                    return 0;

                case 10:
                    {
                        if (((buffer[0] != 0x1f) || (buffer[1] != 0x8b)) || (buffer[2] != 8))
                        {
                            throw new ZlibException("Bad GZIP header.");
                        }
                        int num3 = BitConverter.ToInt32(buffer, 4);
                        this._GzipMtime = GZipStream._unixEpoch.AddSeconds((double)num3);
                        int num4 = num + num2;
                        if ((buffer[3] & 4) == 4)
                        {
                            int num5 = this._stream.Read(buffer, 0, 2);
                            int num6 = num4 + num5;
                            short num7 = (short)(buffer[0] + (buffer[1] * 0x100));
                            byte[] buffer2 = new byte[num7];
                            int num8 = this._stream.Read(buffer2, 0, buffer2.Length);
                            if (num8 != num7)
                            {
                                throw new ZlibException("Unexpected end-of-file reading GZIP header.");
                            }
                            num4 = num6 + num8;
                        }
                        if ((buffer[3] & 8) == 8)
                        {
                            this._GzipFileName = this.ReadZeroTerminatedString();
                        }
                        if ((buffer[3] & 0x10) == 0x10)
                        {
                            this._GzipComment = this.ReadZeroTerminatedString();
                        }
                        if ((buffer[3] & 2) == 2)
                        {
                            this.Read(this._buf1, 0, 1);
                        }
                        return num4;
                    }
            }
            throw new ZlibException("Not a valid GZIP stream.");
        }

        public override void Close()
        {
            if (this._stream != null)
            {
                try
                {
                    this.finish();
                }
                finally
                {
                    this.end();
                    if (!this._leaveOpen)
                    {
                        this._stream.Close();
                    }
                    this._stream = null;
                }
            }
        }

        private void end()
        {
            if (this.z != null)
            {
                this._z.EndInflate();
                this._z = null;
            }
        }

        private void finish()
        {
            if (this._z != null)
            {
                if (this._streamMode == StreamMode.Writer)
                {
                    bool flag;
                    do
                    {
                        this._z.OutputBuffer = this.workingBuffer;
                        this._z.NextOut = 0;
                        this._z.AvailableBytesOut = this._workingBuffer.Length;
                        int num = this._z.Inflate(FlushType.Finish);
                        if ((num != 1) && (num != 0))
                        {
                            throw new ZlibException("inflating: " + this._z.Message);
                        }
                        if ((this._workingBuffer.Length - this._z.AvailableBytesOut) > 0)
                        {
                            this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
                        }
                        flag = (this._z.AvailableBytesIn == 0) && (this._z.AvailableBytesOut != 0);
                        if (this._flavor == ZlibStreamFlavor.GZIP)
                        {
                            flag = (this._z.AvailableBytesIn == 8) && (this._z.AvailableBytesOut != 0);
                        }
                    }
                    while (!flag);
                    this.Flush();
                    if (this._flavor == ZlibStreamFlavor.GZIP)
                    {
                        throw new ZlibException("Writing with decompression is not supported.");
                    }
                }
                else if (((this._streamMode == StreamMode.Reader) && (this._flavor == ZlibStreamFlavor.GZIP)) && (this._z.TotalBytesOut != 0L))
                {
                    byte[] destinationArray = new byte[8];
                    if (this._z.AvailableBytesIn != 8)
                    {
                        throw new ZlibException(string.Format("Protocol error. AvailableBytesIn={0}, expected 8", this._z.AvailableBytesIn));
                    }
                    Array.Copy(this._z.InputBuffer, this._z.NextIn, destinationArray, 0, destinationArray.Length);
                    int num2 = BitConverter.ToInt32(destinationArray, 0);
                    int num3 = this.crc.Crc32Result;
                    int num4 = BitConverter.ToInt32(destinationArray, 4);
                    int num5 = (int)(((ulong)this._z.TotalBytesOut) & 0xffffffffL);
                    if (num3 != num2)
                    {
                        throw new ZlibException(string.Format("Bad CRC32 in GZIP stream. (actual({0:X8})!=expected({1:X8}))", num3, num2));
                    }
                    if (num5 != num4)
                    {
                        throw new ZlibException(string.Format("Bad size in GZIP stream. (actual({0})!=expected({1}))", num5, num4));
                    }
                }
            }
        }

        public override void Flush()
        {
            this._stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int num;
            if (this._streamMode == StreamMode.Undefined)
            {
                if (!this._stream.CanRead)
                {
                    throw new ZlibException("The stream is not readable.");
                }
                this._streamMode = StreamMode.Reader;
                this.z.AvailableBytesIn = 0;
                if (this._flavor == ZlibStreamFlavor.GZIP)
                {
                    this._gzipHeaderByteCount = this._ReadAndValidateGzipHeader();
                    if (this._gzipHeaderByteCount == 0)
                    {
                        return 0;
                    }
                }
            }
            if (this._streamMode != StreamMode.Reader)
            {
                throw new ZlibException("Cannot Read after Writing.");
            }
            if (count == 0)
            {
                return 0;
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (offset < buffer.GetLowerBound(0))
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if ((offset + count) > buffer.GetLength(0))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            this._z.OutputBuffer = buffer;
            this._z.NextOut = offset;
            this._z.AvailableBytesOut = count;
            this._z.InputBuffer = this.workingBuffer;
            do
            {
                if ((this._z.AvailableBytesIn == 0) && !this.nomoreinput)
                {
                    this._z.NextIn = 0;
                    this._z.AvailableBytesIn = this._stream.Read(this._workingBuffer, 0, this._workingBuffer.Length);
                    if (this._z.AvailableBytesIn == 0)
                    {
                        this.nomoreinput = true;
                    }
                }
                num = this._z.Inflate(this._flushMode);
                if (this.nomoreinput && (num == -5))
                {
                    return 0;
                }
                if ((num != 0) && (num != 1))
                {
                    throw new ZlibException(string.Format("inflating:  rc={0}  msg={1}", num, this._z.Message));
                }
            }
            while (((!this.nomoreinput && (num != 1)) || (this._z.AvailableBytesOut != count)) && (((this._z.AvailableBytesOut > 0) && !this.nomoreinput) && (num == 0)));
            if (this._z.AvailableBytesOut > 0)
            {
                if (num == 0)
                {
                    int availableBytesIn = this._z.AvailableBytesIn;
                }
                bool nomoreinput = this.nomoreinput;
            }
            int num2 = count - this._z.AvailableBytesOut;
            if (this.crc != null)
            {
                this.crc.SlurpBlock(buffer, offset, num2);
            }
            return num2;
        }

        private string ReadZeroTerminatedString()
        {
            List<byte> list = new List<byte>();
            bool flag = false;
            while (this._stream.Read(this._buf1, 0, 1) == 1)
            {
                if (this._buf1[0] == 0)
                {
                    flag = true;
                }
                else
                {
                    list.Add(this._buf1[0]);
                }
                if (flag)
                {
                    byte[] bytes = list.ToArray();
                    return GZipStream.iso8859dash1.GetString(bytes, 0, bytes.Length);
                }
            }
            throw new ZlibException("Unexpected EOF reading GZIP header.");
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            this._stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.crc != null)
            {
                this.crc.SlurpBlock(buffer, offset, count);
            }
            if (this._streamMode == StreamMode.Undefined)
            {
                this._streamMode = StreamMode.Writer;
            }
            else if (this._streamMode != StreamMode.Writer)
            {
                throw new ZlibException("Cannot Write after Reading.");
            }
            if (count != 0)
            {
                bool flag;
                this.z.InputBuffer = buffer;
                this._z.NextIn = offset;
                this._z.AvailableBytesIn = count;
                do
                {
                    this._z.OutputBuffer = this.workingBuffer;
                    this._z.NextOut = 0;
                    this._z.AvailableBytesOut = this._workingBuffer.Length;
                    int num = this._z.Inflate(this._flushMode);
                    if ((num != 0) && (num != 1))
                    {
                        throw new ZlibException("inflating: " + this._z.Message);
                    }
                    this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
                    flag = (this._z.AvailableBytesIn == 0) && (this._z.AvailableBytesOut != 0);
                    if (this._flavor == ZlibStreamFlavor.GZIP)
                    {
                        flag = (this._z.AvailableBytesIn == 8) && (this._z.AvailableBytesOut != 0);
                    }
                }
                while (!flag);
            }
        }

        public override bool CanRead
        {
            get
            {
                return this._stream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return this._stream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return this._stream.CanWrite;
            }
        }

        internal int Crc32
        {
            get
            {
                if (this.crc == null)
                {
                    return 0;
                }
                return this.crc.Crc32Result;
            }
        }

        public override long Length
        {
            get
            {
                return this._stream.Length;
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private byte[] workingBuffer
        {
            get
            {
                if (this._workingBuffer == null)
                {
                    this._workingBuffer = new byte[this._bufferSize];
                }
                return this._workingBuffer;
            }
        }

        private ZlibCodec z
        {
            get
            {
                if (this._z == null)
                {
                    bool flag = this._flavor == ZlibStreamFlavor.ZLIB;
                    this._z = new ZlibCodec();
                    this._z.InitializeInflate(flag);
                }
                return this._z;
            }
        }

        internal enum StreamMode
        {
            Writer,
            Reader,
            Undefined
        }
    }
}
