namespace WP7Contrib.Communications.Compression
{
    using System;

    internal sealed class InflateBlocks
    {
        internal ZlibCodec _codec;
        private const int BAD = 9;
        internal int[] bb = new int[1];
        internal int bitb;
        internal int bitk;
        internal int[] blens;
        internal static readonly int[] border = new int[] { 
            0x10, 0x11, 0x12, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 
            14, 1, 15
         };
        private const int BTREE = 4;
        internal long check;
        internal object checkfn;
        internal InflateCodes codes = new InflateCodes();
        private const int CODES = 6;
        private const int DONE = 8;
        private const int DRY = 7;
        private const int DTREE = 5;
        internal int end;
        internal int[] hufts;
        internal int index;
        private static readonly int[] inflate_mask = new int[] { 
            0, 1, 3, 7, 15, 0x1f, 0x3f, 0x7f, 0xff, 0x1ff, 0x3ff, 0x7ff, 0xfff, 0x1fff, 0x3fff, 0x7fff, 
            0xffff
         };
        internal InfTree inftree = new InfTree();
        internal int last;
        internal int left;
        private const int LENS = 1;
        private const int MANY = 0x5a0;
        internal int mode;
        internal int read;
        private const int STORED = 2;
        internal int table;
        private const int TABLE = 3;
        internal int[] tb = new int[1];
        private const int TYPE = 0;
        internal byte[] window;
        internal int write;

        internal InflateBlocks(ZlibCodec codec, object checkfn, int w)
        {
            this._codec = codec;
            this.hufts = new int[0x10e0];
            this.window = new byte[w];
            this.end = w;
            this.checkfn = checkfn;
            this.mode = 0;
            this.Reset(null);
        }

        internal int Flush(int r)
        {
            int nextOut = this._codec.NextOut;
            int read = this.read;
            int len = ((read <= this.write) ? this.write : this.end) - read;
            if (len > this._codec.AvailableBytesOut)
            {
                len = this._codec.AvailableBytesOut;
            }
            if ((len != 0) && (r == -5))
            {
                r = 0;
            }
            this._codec.AvailableBytesOut -= len;
            this._codec.TotalBytesOut += len;
            if (this.checkfn != null)
            {
                this._codec._Adler32 = this.check = Adler.Adler32(this.check, this.window, read, len);
            }
            Array.Copy(this.window, read, this._codec.OutputBuffer, nextOut, len);
            int destinationIndex = nextOut + len;
            int num5 = read + len;
            if (num5 == this.end)
            {
                int index = 0;
                if (this.write == this.end)
                {
                    this.write = 0;
                }
                int availableBytesOut = this.write - index;
                if (availableBytesOut > this._codec.AvailableBytesOut)
                {
                    availableBytesOut = this._codec.AvailableBytesOut;
                }
                if ((availableBytesOut != 0) && (r == -5))
                {
                    r = 0;
                }
                this._codec.AvailableBytesOut -= availableBytesOut;
                this._codec.TotalBytesOut += availableBytesOut;
                if (this.checkfn != null)
                {
                    this._codec._Adler32 = this.check = Adler.Adler32(this.check, this.window, index, availableBytesOut);
                }
                Array.Copy(this.window, index, this._codec.OutputBuffer, destinationIndex, availableBytesOut);
                destinationIndex += availableBytesOut;
                num5 = index + availableBytesOut;
            }
            this._codec.NextOut = destinationIndex;
            this.read = num5;
            return r;
        }

        internal void Free()
        {
            this.Reset(null);
            this.window = null;
            this.hufts = null;
        }

        internal int Process(int r)
        {
            int num17;
            int nextIn = this._codec.NextIn;
            int availableBytesIn = this._codec.AvailableBytesIn;
            int bitb = this.bitb;
            int bitk = this.bitk;
            int write = this.write;
            int num6 = (write < this.read) ? ((this.read - write) - 1) : (this.end - write);
        Label_0050:
            switch (this.mode)
            {
                case 0:
                    {
                        while (bitk < 3)
                        {
                            if (availableBytesIn != 0)
                            {
                                r = 0;
                                availableBytesIn--;
                                bitb |= (this._codec.InputBuffer[nextIn++] & 0xff) << bitk;
                                bitk += 8;
                            }
                            else
                            {
                                this.bitb = bitb;
                                this.bitk = bitk;
                                this._codec.AvailableBytesIn = availableBytesIn;
                                this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                                this._codec.NextIn = nextIn;
                                this.write = write;
                                return this.Flush(r);
                            }
                        }
                        int number = bitb & 7;
                        this.last = number & 1;
                        switch (SharedUtils.URShift(number, 1))
                        {
                            case 0:
                                {
                                    int num10 = SharedUtils.URShift(bitb, 3);
                                    int num11 = bitk - 3;
                                    int bits = num11 & 7;
                                    bitb = SharedUtils.URShift(num10, bits);
                                    bitk = num11 - bits;
                                    this.mode = 1;
                                    break;
                                }
                            case 1:
                                {
                                    int[] numArray = new int[1];
                                    int[] numArray2 = new int[1];
                                    int[][] numArray3 = new int[1][];
                                    int[][] numArray4 = new int[1][];
                                    InfTree.inflate_trees_fixed(numArray, numArray2, numArray3, numArray4, this._codec);
                                    this.codes.Init(numArray[0], numArray2[0], numArray3[0], 0, numArray4[0], 0);
                                    bitb = SharedUtils.URShift(bitb, 3);
                                    bitk -= 3;
                                    this.mode = 6;
                                    break;
                                }
                            case 2:
                                bitb = SharedUtils.URShift(bitb, 3);
                                bitk -= 3;
                                this.mode = 3;
                                break;

                            case 3:
                                {
                                    int num30 = SharedUtils.URShift(bitb, 3);
                                    int num31 = bitk - 3;
                                    this.mode = 9;
                                    this._codec.Message = "invalid block type";
                                    r = -3;
                                    this.bitb = num30;
                                    this.bitk = num31;
                                    this._codec.AvailableBytesIn = availableBytesIn;
                                    this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                                    this._codec.NextIn = nextIn;
                                    this.write = write;
                                    return this.Flush(r);
                                }
                        }
                        goto Label_0050;
                    }
                case 1:
                    while (bitk < 0x20)
                    {
                        if (availableBytesIn != 0)
                        {
                            r = 0;
                            availableBytesIn--;
                            bitb |= (this._codec.InputBuffer[nextIn++] & 0xff) << bitk;
                            bitk += 8;
                        }
                        else
                        {
                            this.bitb = bitb;
                            this.bitk = bitk;
                            this._codec.AvailableBytesIn = availableBytesIn;
                            this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                            this._codec.NextIn = nextIn;
                            this.write = write;
                            return this.Flush(r);
                        }
                    }
                    if ((SharedUtils.URShift(~bitb, 0x10) & 0xffff) != (bitb & 0xffff))
                    {
                        this.mode = 9;
                        this._codec.Message = "invalid stored block lengths";
                        r = -3;
                        this.bitb = bitb;
                        this.bitk = bitk;
                        this._codec.AvailableBytesIn = availableBytesIn;
                        this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                        this._codec.NextIn = nextIn;
                        this.write = write;
                        return this.Flush(r);
                    }
                    this.left = bitb & 0xffff;
                    bitb = bitk = 0;
                    this.mode = (this.left != 0) ? 2 : ((this.last != 0) ? 7 : 0);
                    goto Label_0050;

                case 2:
                    {
                        if (availableBytesIn == 0)
                        {
                            this.bitb = bitb;
                            this.bitk = bitk;
                            this._codec.AvailableBytesIn = availableBytesIn;
                            this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                            this._codec.NextIn = nextIn;
                            this.write = write;
                            return this.Flush(r);
                        }
                        if (num6 == 0)
                        {
                            if ((write == this.end) && (this.read != 0))
                            {
                                write = 0;
                                num6 = (write < this.read) ? ((this.read - write) - 1) : (this.end - write);
                            }
                            if (num6 == 0)
                            {
                                this.write = write;
                                r = this.Flush(r);
                                write = this.write;
                                num6 = (write < this.read) ? ((this.read - write) - 1) : (this.end - write);
                                if ((write == this.end) && (this.read != 0))
                                {
                                    write = 0;
                                    num6 = (write < this.read) ? ((this.read - write) - 1) : (this.end - write);
                                }
                                if (num6 == 0)
                                {
                                    this.bitb = bitb;
                                    this.bitk = bitk;
                                    this._codec.AvailableBytesIn = availableBytesIn;
                                    this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                                    this._codec.NextIn = nextIn;
                                    this.write = write;
                                    return this.Flush(r);
                                }
                            }
                        }
                        r = 0;
                        int left = this.left;
                        if (left > availableBytesIn)
                        {
                            left = availableBytesIn;
                        }
                        if (left > num6)
                        {
                            left = num6;
                        }
                        Array.Copy(this._codec.InputBuffer, nextIn, this.window, write, left);
                        nextIn += left;
                        availableBytesIn -= left;
                        write += left;
                        num6 -= left;
                        this.left -= left;
                        if (this.left == 0)
                        {
                            this.mode = (this.last != 0) ? 7 : 0;
                        }
                        goto Label_0050;
                    }
                case 3:
                    {
                        int num14;
                        while (bitk < 14)
                        {
                            if (availableBytesIn != 0)
                            {
                                r = 0;
                                availableBytesIn--;
                                bitb |= (this._codec.InputBuffer[nextIn++] & 0xff) << bitk;
                                bitk += 8;
                            }
                            else
                            {
                                this.bitb = bitb;
                                this.bitk = bitk;
                                this._codec.AvailableBytesIn = availableBytesIn;
                                this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                                this._codec.NextIn = nextIn;
                                this.write = write;
                                return this.Flush(r);
                            }
                        }
                        this.table = num14 = bitb & 0x3fff;
                        if (((num14 & 0x1f) > 0x1d) || (((num14 >> 5) & 0x1f) > 0x1d))
                        {
                            this.mode = 9;
                            this._codec.Message = "too many length or distance symbols";
                            r = -3;
                            this.bitb = bitb;
                            this.bitk = bitk;
                            this._codec.AvailableBytesIn = availableBytesIn;
                            this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                            this._codec.NextIn = nextIn;
                            this.write = write;
                            return this.Flush(r);
                        }
                        int num15 = (0x102 + (num14 & 0x1f)) + ((num14 >> 5) & 0x1f);
                        if ((this.blens == null) || (this.blens.Length < num15))
                        {
                            this.blens = new int[num15];
                        }
                        else
                        {
                            for (int i = 0; i < num15; i++)
                            {
                                this.blens[i] = 0;
                            }
                        }
                        bitb = SharedUtils.URShift(bitb, 14);
                        bitk -= 14;
                        this.index = 0;
                        this.mode = 4;
                        break;
                    }
                case 4:
                    break;

                case 5:
                    goto Label_068D;

                case 6:
                    goto Label_09EA;

                case 7:
                    goto Label_0E29;

                case 8:
                    goto Label_0EAD;

                case 9:
                    r = -3;
                    this.bitb = bitb;
                    this.bitk = bitk;
                    this._codec.AvailableBytesIn = availableBytesIn;
                    this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                    this._codec.NextIn = nextIn;
                    this.write = write;
                    return this.Flush(r);

                default:
                    r = -2;
                    this.bitb = bitb;
                    this.bitk = bitk;
                    this._codec.AvailableBytesIn = availableBytesIn;
                    this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                    this._codec.NextIn = nextIn;
                    this.write = write;
                    return this.Flush(r);
            }
            while (this.index < (4 + SharedUtils.URShift(this.table, 10)))
            {
                while (bitk < 3)
                {
                    if (availableBytesIn != 0)
                    {
                        r = 0;
                        availableBytesIn--;
                        bitb |= (this._codec.InputBuffer[nextIn++] & 0xff) << bitk;
                        bitk += 8;
                    }
                    else
                    {
                        this.bitb = bitb;
                        this.bitk = bitk;
                        this._codec.AvailableBytesIn = availableBytesIn;
                        this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                        this._codec.NextIn = nextIn;
                        this.write = write;
                        return this.Flush(r);
                    }
                }
                this.blens[border[this.index++]] = bitb & 7;
                bitb = SharedUtils.URShift(bitb, 3);
                bitk -= 3;
            }
            while (this.index < 0x13)
            {
                this.blens[border[this.index++]] = 0;
            }
            this.bb[0] = 7;
            int num7 = this.inftree.inflate_trees_bits(this.blens, this.bb, this.tb, this.hufts, this._codec);
            if (num7 != 0)
            {
                r = num7;
                if (r == -3)
                {
                    this.blens = null;
                    this.mode = 9;
                }
                this.bitb = bitb;
                this.bitk = bitk;
                this._codec.AvailableBytesIn = availableBytesIn;
                this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                this._codec.NextIn = nextIn;
                this.write = write;
                return this.Flush(r);
            }
            this.index = 0;
            this.mode = 5;
        Label_068D:
            num17 = this.table;
            if (this.index < ((0x102 + (num17 & 0x1f)) + ((num17 >> 5) & 0x1f)))
            {
                int index = this.bb[0];
                while (bitk < index)
                {
                    if (availableBytesIn != 0)
                    {
                        r = 0;
                        availableBytesIn--;
                        bitb |= (this._codec.InputBuffer[nextIn++] & 0xff) << bitk;
                        bitk += 8;
                    }
                    else
                    {
                        this.bitb = bitb;
                        this.bitk = bitk;
                        this._codec.AvailableBytesIn = availableBytesIn;
                        this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                        this._codec.NextIn = nextIn;
                        this.write = write;
                        return this.Flush(r);
                    }
                }
                int num1 = this.tb[0];
                int num19 = this.hufts[((this.tb[0] + (bitb & inflate_mask[index])) * 3) + 1];
                int num20 = this.hufts[((this.tb[0] + (bitb & inflate_mask[num19])) * 3) + 2];
                if (num20 < 0x10)
                {
                    bitb = SharedUtils.URShift(bitb, num19);
                    bitk -= num19;
                    this.blens[this.index++] = num20;
                }
                else
                {
                    int num21 = (num20 == 0x12) ? 7 : (num20 - 14);
                    int num22 = (num20 == 0x12) ? 11 : 3;
                    while (bitk < (num19 + num21))
                    {
                        if (availableBytesIn != 0)
                        {
                            r = 0;
                            availableBytesIn--;
                            bitb |= (this._codec.InputBuffer[nextIn++] & 0xff) << bitk;
                            bitk += 8;
                        }
                        else
                        {
                            this.bitb = bitb;
                            this.bitk = bitk;
                            this._codec.AvailableBytesIn = availableBytesIn;
                            this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                            this._codec.NextIn = nextIn;
                            this.write = write;
                            return this.Flush(r);
                        }
                    }
                    int num23 = SharedUtils.URShift(bitb, num19);
                    int num24 = bitk - num19;
                    int num25 = num22 + (num23 & inflate_mask[num21]);
                    bitb = SharedUtils.URShift(num23, num21);
                    bitk = num24 - num21;
                    int num26 = this.index;
                    int num27 = this.table;
                    if (((num26 + num25) > ((0x102 + (num27 & 0x1f)) + ((num27 >> 5) & 0x1f))) || ((num20 == 0x10) && (num26 < 1)))
                    {
                        this.blens = null;
                        this.mode = 9;
                        this._codec.Message = "invalid bit length repeat";
                        r = -3;
                        this.bitb = bitb;
                        this.bitk = bitk;
                        this._codec.AvailableBytesIn = availableBytesIn;
                        this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                        this._codec.NextIn = nextIn;
                        this.write = write;
                        return this.Flush(r);
                    }
                    int num28 = (num20 == 0x10) ? this.blens[num26 - 1] : 0;
                    do
                    {
                        this.blens[num26++] = num28;
                    }
                    while (--num25 != 0);
                    this.index = num26;
                }
                goto Label_068D;
            }
            this.tb[0] = -1;
            int[] bl = new int[] { 9 };
            int[] bd = new int[] { 6 };
            int[] tl = new int[1];
            int[] td = new int[1];
            int table = this.table;
            int num8 = this.inftree.inflate_trees_dynamic(0x101 + (table & 0x1f), 1 + ((table >> 5) & 0x1f), this.blens, bl, bd, tl, td, this.hufts, this._codec);
            int num38 = num8;
            if (num38 == -3)
            {
                this.blens = null;
                this.mode = 9;
                goto Label_0DC0;
            }
            if (num38 != 0)
            {
                goto Label_0DC0;
            }
            this.codes.Init(bl[0], bd[0], this.hufts, tl[0], this.hufts, td[0]);
            this.mode = 6;
        Label_09EA:
            this.bitb = bitb;
            this.bitk = bitk;
            this._codec.AvailableBytesIn = availableBytesIn;
            this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
            this._codec.NextIn = nextIn;
            this.write = write;
            if ((r = this.codes.Process(this, r)) != 1)
            {
                return this.Flush(r);
            }
            r = 0;
            nextIn = this._codec.NextIn;
            availableBytesIn = this._codec.AvailableBytesIn;
            bitb = this.bitb;
            bitk = this.bitk;
            write = this.write;
            num6 = (write < this.read) ? ((this.read - write) - 1) : (this.end - write);
            if (this.last != 0)
            {
                this.mode = 7;
                goto Label_0E29;
            }
            this.mode = 0;
            goto Label_0050;
        Label_0DC0:
            r = num8;
            this.bitb = bitb;
            this.bitk = bitk;
            this._codec.AvailableBytesIn = availableBytesIn;
            this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
            this._codec.NextIn = nextIn;
            this.write = write;
            return this.Flush(r);
        Label_0E29:
            this.write = write;
            r = this.Flush(r);
            write = this.write;
            if (this.read != this.write)
            {
                this.bitb = bitb;
                this.bitk = bitk;
                this._codec.AvailableBytesIn = availableBytesIn;
                this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
                this._codec.NextIn = nextIn;
                this.write = write;
                return this.Flush(r);
            }
            this.mode = 8;
        Label_0EAD:
            r = 1;
            this.bitb = bitb;
            this.bitk = bitk;
            this._codec.AvailableBytesIn = availableBytesIn;
            this._codec.TotalBytesIn += nextIn - this._codec.NextIn;
            this._codec.NextIn = nextIn;
            this.write = write;
            return this.Flush(r);
        }

        internal void Reset(long[] c)
        {
            if (c != null)
            {
                c[0] = this.check;
            }
            if (this.mode != 4)
            {
                int num1 = this.mode;
            }
            int mode = this.mode;
            this.mode = 0;
            this.bitk = 0;
            this.bitb = 0;
            this.read = this.write = 0;
            if (this.checkfn != null)
            {
                this._codec._Adler32 = this.check = Adler.Adler32(0L, null, 0, 0);
            }
        }

        internal void SetDictionary(byte[] d, int start, int n)
        {
            Array.Copy(d, start, this.window, 0, n);
            this.read = this.write = n;
        }

        internal int SyncPoint()
        {
            if (this.mode != 1)
            {
                return 0;
            }
            return 1;
        }
    }
}
