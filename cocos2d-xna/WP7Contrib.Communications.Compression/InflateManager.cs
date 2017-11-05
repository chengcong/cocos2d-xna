namespace WP7Contrib.Communications.Compression
{
    using System;

    internal sealed class InflateManager
    {
        internal ZlibCodec _codec;
        private bool _handleRfc1950HeaderBytes;
        private const int BAD = 13;
        internal InflateBlocks blocks;
        private const int BLOCKS = 7;
        private const int CHECK1 = 11;
        private const int CHECK2 = 10;
        private const int CHECK3 = 9;
        private const int CHECK4 = 8;
        private const int DICT0 = 6;
        private const int DICT1 = 5;
        private const int DICT2 = 4;
        private const int DICT3 = 3;
        private const int DICT4 = 2;
        private const int DONE = 12;
        private const int FLAG = 1;
        private static byte[] mark;
        internal int marker;
        internal int method;
        private const int METHOD = 0;
        internal int mode;
        internal long need;
        private const int PRESET_DICT = 0x20;
        internal long[] was;
        internal int wbits;
        private const int Z_DEFLATED = 8;

        static InflateManager()
        {
            byte[] buffer = new byte[4];
            buffer[2] = 0xff;
            buffer[3] = 0xff;
            mark = buffer;
        }

        public InflateManager()
        {
            this.was = new long[1];
            this._handleRfc1950HeaderBytes = true;
        }

        public InflateManager(bool expectRfc1950HeaderBytes)
        {
            this.was = new long[1];
            this._handleRfc1950HeaderBytes = true;
            this._handleRfc1950HeaderBytes = expectRfc1950HeaderBytes;
        }

        internal int End()
        {
            if (this.blocks != null)
            {
                this.blocks.Free();
            }
            this.blocks = null;
            return 0;
        }

        internal int Inflate(FlushType flush)
        {
            int num = (int)flush;
            if (this._codec.InputBuffer == null)
            {
                throw new ZlibException("InputBuffer is null. ");
            }
            int num2 = (num == 4) ? -5 : 0;
            int r = -5;
        Label_0027:
            switch (this.mode)
            {
                case 0:
                    {
                        int num6;
                        if (this._codec.AvailableBytesIn == 0)
                        {
                            return r;
                        }
                        r = num2;
                        this._codec.AvailableBytesIn--;
                        this._codec.TotalBytesIn += 1L;
                        InflateManager manager = this;
                        byte[] inputBuffer = this._codec.InputBuffer;
                        int index = this._codec.NextIn++;
                        manager.method = num6 = inputBuffer[index];
                        if ((num6 & 15) == 8)
                        {
                            if (((this.method >> 4) + 8) > this.wbits)
                            {
                                this.mode = 13;
                                this._codec.Message = string.Format("invalid window size ({0})", (this.method >> 4) + 8);
                                this.marker = 5;
                                goto Label_0027;
                            }
                            this.mode = 1;
                            break;
                        }
                        this.mode = 13;
                        this._codec.Message = string.Format("unknown compression method (0x{0:X2})", this.method);
                        this.marker = 5;
                        goto Label_0027;
                    }
                case 1:
                    break;

                case 2:
                    goto Label_04BA;

                case 3:
                    goto Label_0533;

                case 4:
                    goto Label_05B4;

                case 5:
                    goto Label_0634;

                case 6:
                    this.mode = 13;
                    this._codec.Message = "need dictionary";
                    this.marker = 0;
                    return -2;

                case 7:
                    r = this.blocks.Process(r);
                    if (r != -3)
                    {
                        if (r == 0)
                        {
                            r = num2;
                        }
                        if (r != 1)
                        {
                            return r;
                        }
                        r = num2;
                        this.blocks.Reset(this.was);
                        if (!this.HandleRfc1950HeaderBytes)
                        {
                            this.mode = 12;
                            goto Label_0027;
                        }
                        this.mode = 8;
                        goto Label_0284;
                    }
                    this.mode = 13;
                    this.marker = 0;
                    goto Label_0027;

                case 8:
                    goto Label_0284;

                case 9:
                    goto Label_02FF;

                case 10:
                    goto Label_0382;

                case 11:
                    goto Label_0404;

                case 12:
                    goto Label_06F1;

                case 13:
                    throw new ZlibException(string.Format("Bad state ({0})", this._codec.Message));

                default:
                    throw new ZlibException("Stream error.");
            }
            if (this._codec.AvailableBytesIn == 0)
            {
                return r;
            }
            r = num2;
            this._codec.AvailableBytesIn--;
            this._codec.TotalBytesIn += 1L;
            int num7 = this._codec.InputBuffer[this._codec.NextIn++] & 0xff;
            if ((((this.method << 8) + num7) % 0x1f) != 0)
            {
                this.mode = 13;
                this._codec.Message = "incorrect header check";
                this.marker = 5;
            }
            else
            {
                if ((num7 & 0x20) != 0)
                {
                    this.mode = 2;
                    goto Label_04BA;
                }
                this.mode = 7;
            }
            goto Label_0027;
        Label_0284:
            if (this._codec.AvailableBytesIn == 0)
            {
                return r;
            }
            r = num2;
            this._codec.AvailableBytesIn--;
            this._codec.TotalBytesIn += 1L;
            this.need = ((this._codec.InputBuffer[this._codec.NextIn++] & 0xff) << 0x18) & -16777216;
            this.mode = 9;
        Label_02FF:
            if (this._codec.AvailableBytesIn == 0)
            {
                return r;
            }
            r = num2;
            this._codec.AvailableBytesIn--;
            this._codec.TotalBytesIn += 1L;
            this.need += ((this._codec.InputBuffer[this._codec.NextIn++] & 0xff) << 0x10) & 0xff0000L;
            this.mode = 10;
        Label_0382:
            if (this._codec.AvailableBytesIn == 0)
            {
                return r;
            }
            r = num2;
            this._codec.AvailableBytesIn--;
            this._codec.TotalBytesIn += 1L;
            this.need += ((this._codec.InputBuffer[this._codec.NextIn++] & 0xff) << 8) & 0xff00L;
            this.mode = 11;
        Label_0404:
            if (this._codec.AvailableBytesIn == 0)
            {
                return r;
            }
            r = num2;
            this._codec.AvailableBytesIn--;
            this._codec.TotalBytesIn += 1L;
            this.need += this._codec.InputBuffer[this._codec.NextIn++] & 0xffL;
            if (((int)this.was[0]) == ((int)this.need))
            {
                this.mode = 12;
                goto Label_06F1;
            }
            this.mode = 13;
            this._codec.Message = "incorrect data check";
            this.marker = 5;
            goto Label_0027;
        Label_04BA:
            if (this._codec.AvailableBytesIn == 0)
            {
                return r;
            }
            r = num2;
            this._codec.AvailableBytesIn--;
            this._codec.TotalBytesIn += 1L;
            this.need = ((this._codec.InputBuffer[this._codec.NextIn++] & 0xff) << 0x18) & -16777216;
            this.mode = 3;
        Label_0533:
            if (this._codec.AvailableBytesIn == 0)
            {
                return r;
            }
            r = num2;
            this._codec.AvailableBytesIn--;
            this._codec.TotalBytesIn += 1L;
            this.need += ((this._codec.InputBuffer[this._codec.NextIn++] & 0xff) << 0x10) & 0xff0000L;
            this.mode = 4;
        Label_05B4:
            if (this._codec.AvailableBytesIn == 0)
            {
                return r;
            }
            r = num2;
            this._codec.AvailableBytesIn--;
            this._codec.TotalBytesIn += 1L;
            this.need += ((this._codec.InputBuffer[this._codec.NextIn++] & 0xff) << 8) & 0xff00L;
            this.mode = 5;
        Label_0634:
            if (this._codec.AvailableBytesIn == 0)
            {
                return r;
            }
            this._codec.AvailableBytesIn--;
            this._codec.TotalBytesIn += 1L;
            this.need += this._codec.InputBuffer[this._codec.NextIn++] & 0xffL;
            this._codec._Adler32 = this.need;
            this.mode = 6;
            return 2;
        Label_06F1:
            return 1;
        }

        internal int Initialize(ZlibCodec codec, int w)
        {
            this._codec = codec;
            this._codec.Message = null;
            this.blocks = null;
            if ((w < 8) || (w > 15))
            {
                this.End();
                throw new ZlibException("Bad window size.");
            }
            this.wbits = w;
            this.blocks = new InflateBlocks(codec, this.HandleRfc1950HeaderBytes ? this : null, ((int)1) << w);
            this.Reset();
            return 0;
        }

        internal int Reset()
        {
            this._codec.TotalBytesIn = this._codec.TotalBytesOut = 0L;
            this._codec.Message = null;
            this.mode = this.HandleRfc1950HeaderBytes ? 0 : 7;
            this.blocks.Reset(null);
            return 0;
        }

        internal int SetDictionary(byte[] dictionary)
        {
            int start = 0;
            int length = dictionary.Length;
            if (this.mode != 6)
            {
                throw new ZlibException("Stream error.");
            }
            if (Adler.Adler32(1L, dictionary, 0, dictionary.Length) != this._codec._Adler32)
            {
                return -3;
            }
            this._codec._Adler32 = Adler.Adler32(0L, null, 0, 0);
            if (length >= (((int)1) << this.wbits))
            {
                length = (((int)1) << this.wbits) - 1;
                start = dictionary.Length - length;
            }
            this.blocks.SetDictionary(dictionary, start, length);
            this.mode = 7;
            return 0;
        }

        internal int Sync()
        {
            if (this.mode != 13)
            {
                this.mode = 13;
                this.marker = 0;
            }
            int availableBytesIn = this._codec.AvailableBytesIn;
            if (availableBytesIn == 0)
            {
                return -5;
            }
            int nextIn = this._codec.NextIn;
            int marker = this.marker;
            while ((availableBytesIn != 0) && (marker < 4))
            {
                if (this._codec.InputBuffer[nextIn] == mark[marker])
                {
                    marker++;
                }
                else
                {
                    marker = (this._codec.InputBuffer[nextIn] == 0) ? (4 - marker) : 0;
                }
                nextIn++;
                availableBytesIn--;
            }
            this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
            this._codec.NextIn = nextIn;
            this._codec.AvailableBytesIn = availableBytesIn;
            this.marker = marker;
            if (marker != 4)
            {
                return -3;
            }
            long totalBytesIn = this._codec.TotalBytesIn;
            long totalBytesOut = this._codec.TotalBytesOut;
            this.Reset();
            this._codec.TotalBytesIn = totalBytesIn;
            this._codec.TotalBytesOut = totalBytesOut;
            this.mode = 7;
            return 0;
        }

        internal int SyncPoint(ZlibCodec z)
        {
            return this.blocks.SyncPoint();
        }

        internal bool HandleRfc1950HeaderBytes
        {
            get
            {
                return this._handleRfc1950HeaderBytes;
            }
            set
            {
                this._handleRfc1950HeaderBytes = value;
            }
        }
    }
}
