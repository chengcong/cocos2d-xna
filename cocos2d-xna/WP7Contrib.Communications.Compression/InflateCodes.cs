namespace WP7Contrib.Communications.Compression
{
    using System;

    internal sealed class InflateCodes
    {
        private const int BADCODE = 9;
        private const int COPY = 5;
        internal byte dbits;
        internal int dist;
        private const int DIST = 3;
        private const int DISTEXT = 4;
        internal int[] dtree;
        internal int dtree_index;
        private const int END = 8;
        internal int get_Renamed;
        private static readonly int[] inflate_mask = new int[] { 
            0, 1, 3, 7, 15, 0x1f, 0x3f, 0x7f, 0xff, 0x1ff, 0x3ff, 0x7ff, 0xfff, 0x1fff, 0x3fff, 0x7fff, 
            0xffff
         };
        internal byte lbits;
        internal int len;
        private const int LEN = 1;
        private const int LENEXT = 2;
        internal int lit;
        private const int LIT = 6;
        internal int[] ltree;
        internal int ltree_index;
        internal int mode;
        internal int need;
        private const int START = 0;
        internal int[] tree;
        internal int tree_index;
        private const int WASH = 7;

        internal InflateCodes()
        {
        }

        internal int InflateFast(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, InflateBlocks s, ZlibCodec z)
        {
            int num23;
            int nextIn = z.NextIn;
            int availableBytesIn = z.AvailableBytesIn;
            int bitb = s.bitb;
            int bitk = s.bitk;
            int write = s.write;
            int num6 = (write < s.read) ? ((s.read - write) - 1) : (s.end - write);
            int num7 = inflate_mask[bl];
            int num8 = inflate_mask[bd];
        Label_0085:
            while (bitk < 20)
            {
                availableBytesIn--;
                bitb |= (z.InputBuffer[nextIn++] & 0xff) << bitk;
                bitk += 8;
            }
            int num9 = bitb & num7;
            int[] numArray = tl;
            int num10 = tl_index;
            int index = (num10 + num9) * 3;
            int num12 = numArray[index];
            if (num12 == 0)
            {
                bitb = bitb >> numArray[index + 1];
                bitk -= numArray[index + 1];
                s.window[write++] = (byte)numArray[index + 2];
                num6--;
                goto Label_05F5;
            }
        Label_00E3:
            bitb = bitb >> numArray[index + 1];
            bitk -= numArray[index + 1];
            if ((num12 & 0x10) == 0)
            {
                if ((num12 & 0x40) != 0)
                {
                    if ((num12 & 0x20) != 0)
                    {
                        int num43 = z.AvailableBytesIn - availableBytesIn;
                        int num44 = ((bitk >> 3) < num43) ? (bitk >> 3) : num43;
                        int num45 = availableBytesIn + num44;
                        int num46 = nextIn - num44;
                        int num47 = bitk - (num44 << 3);
                        s.bitb = bitb;
                        s.bitk = num47;
                        z.AvailableBytesIn = num45;
                        z.TotalBytesIn += num46 - z.NextIn;
                        z.NextIn = num46;
                        s.write = write;
                        return 1;
                    }
                    z.Message = "invalid literal/length code";
                    int num48 = z.AvailableBytesIn - availableBytesIn;
                    int num49 = ((bitk >> 3) < num48) ? (bitk >> 3) : num48;
                    int num50 = availableBytesIn + num49;
                    int num51 = nextIn - num49;
                    int num52 = bitk - (num49 << 3);
                    s.bitb = bitb;
                    s.bitk = num52;
                    z.AvailableBytesIn = num50;
                    z.TotalBytesIn += num51 - z.NextIn;
                    z.NextIn = num51;
                    s.write = write;
                    return -3;
                }
                num9 = (num9 + numArray[index + 2]) + (bitb & inflate_mask[num12]);
                index = (num10 + num9) * 3;
                num12 = numArray[index];
                if (num12 == 0)
                {
                    bitb = bitb >> numArray[index + 1];
                    bitk -= numArray[index + 1];
                    s.window[write++] = (byte)numArray[index + 2];
                    num6--;
                    goto Label_05F5;
                }
                goto Label_00E3;
            }
            int num13 = num12 & 15;
            int length = numArray[index + 2] + (bitb & inflate_mask[num13]);
            int num15 = bitb >> num13;
            int num16 = bitk - num13;
            while (num16 < 15)
            {
                availableBytesIn--;
                num15 |= (z.InputBuffer[nextIn++] & 0xff) << num16;
                num16 += 8;
            }
            int num17 = num15 & num8;
            int[] numArray2 = td;
            int num18 = td_index;
            int num19 = (num18 + num17) * 3;
            int num20 = numArray2[num19];
            while (true)
            {
                num15 = num15 >> numArray2[num19 + 1];
                num16 -= numArray2[num19 + 1];
                if ((num20 & 0x10) != 0)
                {
                    break;
                }
                if ((num20 & 0x40) != 0)
                {
                    z.Message = "invalid distance code";
                    int num38 = z.AvailableBytesIn - availableBytesIn;
                    int num39 = ((num16 >> 3) < num38) ? (num16 >> 3) : num38;
                    int num40 = availableBytesIn + num39;
                    int num41 = nextIn - num39;
                    int num42 = num16 - (num39 << 3);
                    s.bitb = num15;
                    s.bitk = num42;
                    z.AvailableBytesIn = num40;
                    z.TotalBytesIn += num41 - z.NextIn;
                    z.NextIn = num41;
                    s.write = write;
                    return -3;
                }
                num17 = (num17 + numArray2[num19 + 2]) + (num15 & inflate_mask[num20]);
                num19 = (num18 + num17) * 3;
                num20 = numArray2[num19];
            }
            int num21 = num20 & 15;
            while (num16 < num21)
            {
                availableBytesIn--;
                num15 |= (z.InputBuffer[nextIn++] & 0xff) << num16;
                num16 += 8;
            }
            int num22 = numArray2[num19 + 2] + (num15 & inflate_mask[num21]);
            bitb = num15 >> num21;
            bitk = num16 - num21;
            num6 -= length;
            if (write >= num22)
            {
                int sourceIndex = write - num22;
                if (((write - sourceIndex) > 0) && (2 > (write - sourceIndex)))
                {
                    byte[] window = s.window;
                    int num25 = write;
                    int num26 = 1;
                    int num27 = num25 + num26;
                    byte[] buffer2 = s.window;
                    int num28 = sourceIndex;
                    int num29 = 1;
                    int num30 = num28 + num29;
                    int num31 = buffer2[num28];
                    window[num25] = (byte)num31;
                    byte[] buffer3 = s.window;
                    int num32 = num27;
                    int num33 = 1;
                    write = num32 + num33;
                    byte[] buffer4 = s.window;
                    int num34 = num30;
                    int num35 = 1;
                    num23 = num34 + num35;
                    int num36 = buffer4[num34];
                    buffer3[num32] = (byte)num36;
                    length -= 2;
                }
                else
                {
                    Array.Copy(s.window, sourceIndex, s.window, write, 2);
                    write += 2;
                    num23 = sourceIndex + 2;
                    length -= 2;
                }
            }
            else
            {
                num23 = write - num22;
                do
                {
                    num23 += s.end;
                }
                while (num23 < 0);
                int num37 = s.end - num23;
                if (length > num37)
                {
                    length -= num37;
                    if (((write - num23) > 0) && (num37 > (write - num23)))
                    {
                        do
                        {
                            s.window[write++] = s.window[num23++];
                        }
                        while (--num37 != 0);
                    }
                    else
                    {
                        Array.Copy(s.window, num23, s.window, write, num37);
                        write += num37;
                    }
                    num23 = 0;
                }
            }
            if (((write - num23) > 0) && (length > (write - num23)))
            {
                do
                {
                    s.window[write++] = s.window[num23++];
                }
                while (--length != 0);
            }
            else
            {
                Array.Copy(s.window, num23, s.window, write, length);
                write += length;
            }
        Label_05F5:
            if ((num6 >= 0x102) && (availableBytesIn >= 10))
            {
                goto Label_0085;
            }
            int num53 = z.AvailableBytesIn - availableBytesIn;
            int num54 = ((bitk >> 3) < num53) ? (bitk >> 3) : num53;
            int num55 = availableBytesIn + num54;
            int num56 = nextIn - num54;
            int num57 = bitk - (num54 << 3);
            s.bitb = bitb;
            s.bitk = num57;
            z.AvailableBytesIn = num55;
            z.TotalBytesIn += num56 - z.NextIn;
            z.NextIn = num56;
            s.write = write;
            return 0;
        }

        internal void Init(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index)
        {
            this.mode = 0;
            this.lbits = (byte)bl;
            this.dbits = (byte)bd;
            this.ltree = tl;
            this.ltree_index = tl_index;
            this.dtree = td;
            this.dtree_index = td_index;
            this.tree = null;
        }

        internal int Process(InflateBlocks blocks, int r)
        {
            int num7;
            int num11;
            int num15;
            ZlibCodec z = blocks._codec;
            int nextIn = z.NextIn;
            int availableBytesIn = z.AvailableBytesIn;
            int bitb = blocks.bitb;
            int bitk = blocks.bitk;
            int write = blocks.write;
            int num6 = (write < blocks.read) ? ((blocks.read - write) - 1) : (blocks.end - write);
        Label_004E:
            switch (this.mode)
            {
                case 0:
                    if ((num6 < 0x102) || (availableBytesIn < 10))
                    {
                        break;
                    }
                    blocks.bitb = bitb;
                    blocks.bitk = bitk;
                    z.AvailableBytesIn = availableBytesIn;
                    z.TotalBytesIn += nextIn - z.NextIn;
                    z.NextIn = nextIn;
                    blocks.write = write;
                    r = this.InflateFast(this.lbits, this.dbits, this.ltree, this.ltree_index, this.dtree, this.dtree_index, blocks, z);
                    nextIn = z.NextIn;
                    availableBytesIn = z.AvailableBytesIn;
                    bitb = blocks.bitb;
                    bitk = blocks.bitk;
                    write = blocks.write;
                    num6 = (write < blocks.read) ? ((blocks.read - write) - 1) : (blocks.end - write);
                    if (r == 0)
                    {
                        break;
                    }
                    this.mode = (r == 1) ? 7 : 9;
                    goto Label_004E;

                case 1:
                    goto Label_0190;

                case 2:
                    {
                        int num10 = this.get_Renamed;
                        while (bitk < num10)
                        {
                            if (availableBytesIn != 0)
                            {
                                r = 0;
                                availableBytesIn--;
                                bitb |= (z.InputBuffer[nextIn++] & 0xff) << bitk;
                                bitk += 8;
                            }
                            else
                            {
                                blocks.bitb = bitb;
                                blocks.bitk = bitk;
                                z.AvailableBytesIn = availableBytesIn;
                                z.TotalBytesIn += nextIn - z.NextIn;
                                z.NextIn = nextIn;
                                blocks.write = write;
                                return blocks.Flush(r);
                            }
                        }
                        this.len += bitb & inflate_mask[num10];
                        bitb = bitb >> num10;
                        bitk -= num10;
                        this.need = this.dbits;
                        this.tree = this.dtree;
                        this.tree_index = this.dtree_index;
                        this.mode = 3;
                        goto Label_03B3;
                    }
                case 3:
                    goto Label_03B3;

                case 4:
                    {
                        int num14 = this.get_Renamed;
                        while (bitk < num14)
                        {
                            if (availableBytesIn != 0)
                            {
                                r = 0;
                                availableBytesIn--;
                                bitb |= (z.InputBuffer[nextIn++] & 0xff) << bitk;
                                bitk += 8;
                            }
                            else
                            {
                                blocks.bitb = bitb;
                                blocks.bitk = bitk;
                                z.AvailableBytesIn = availableBytesIn;
                                z.TotalBytesIn += nextIn - z.NextIn;
                                z.NextIn = nextIn;
                                blocks.write = write;
                                return blocks.Flush(r);
                            }
                        }
                        this.dist += bitb & inflate_mask[num14];
                        bitb = bitb >> num14;
                        bitk -= num14;
                        this.mode = 5;
                        goto Label_057D;
                    }
                case 5:
                    goto Label_057D;

                case 6:
                    if (num6 == 0)
                    {
                        if ((write == blocks.end) && (blocks.read != 0))
                        {
                            write = 0;
                            num6 = (write < blocks.read) ? ((blocks.read - write) - 1) : (blocks.end - write);
                        }
                        if (num6 == 0)
                        {
                            blocks.write = write;
                            r = blocks.Flush(r);
                            write = blocks.write;
                            num6 = (write < blocks.read) ? ((blocks.read - write) - 1) : (blocks.end - write);
                            if ((write == blocks.end) && (blocks.read != 0))
                            {
                                write = 0;
                                num6 = (write < blocks.read) ? ((blocks.read - write) - 1) : (blocks.end - write);
                            }
                            if (num6 == 0)
                            {
                                blocks.bitb = bitb;
                                blocks.bitk = bitk;
                                z.AvailableBytesIn = availableBytesIn;
                                z.TotalBytesIn += nextIn - z.NextIn;
                                z.NextIn = nextIn;
                                blocks.write = write;
                                return blocks.Flush(r);
                            }
                        }
                    }
                    r = 0;
                    blocks.window[write++] = (byte)this.lit;
                    num6--;
                    this.mode = 0;
                    goto Label_004E;

                case 7:
                    if (bitk > 7)
                    {
                        bitk -= 8;
                        availableBytesIn++;
                        nextIn--;
                    }
                    blocks.write = write;
                    r = blocks.Flush(r);
                    write = blocks.write;
                    if (write >= blocks.read)
                    {
                        int end = blocks.end;
                    }
                    else
                    {
                        int read = blocks.read;
                    }
                    if (blocks.read != blocks.write)
                    {
                        blocks.bitb = bitb;
                        blocks.bitk = bitk;
                        z.AvailableBytesIn = availableBytesIn;
                        z.TotalBytesIn += nextIn - z.NextIn;
                        z.NextIn = nextIn;
                        blocks.write = write;
                        return blocks.Flush(r);
                    }
                    this.mode = 8;
                    goto Label_096B;

                case 8:
                    goto Label_096B;

                case 9:
                    r = -3;
                    blocks.bitb = bitb;
                    blocks.bitk = bitk;
                    z.AvailableBytesIn = availableBytesIn;
                    z.TotalBytesIn += nextIn - z.NextIn;
                    z.NextIn = nextIn;
                    blocks.write = write;
                    return blocks.Flush(r);

                default:
                    r = -2;
                    blocks.bitb = bitb;
                    blocks.bitk = bitk;
                    z.AvailableBytesIn = availableBytesIn;
                    z.TotalBytesIn += nextIn - z.NextIn;
                    z.NextIn = nextIn;
                    blocks.write = write;
                    return blocks.Flush(r);
            }
            this.need = this.lbits;
            this.tree = this.ltree;
            this.tree_index = this.ltree_index;
            this.mode = 1;
        Label_0190:
            num7 = this.need;
            while (bitk < num7)
            {
                if (availableBytesIn != 0)
                {
                    r = 0;
                    availableBytesIn--;
                    bitb |= (z.InputBuffer[nextIn++] & 0xff) << bitk;
                    bitk += 8;
                }
                else
                {
                    blocks.bitb = bitb;
                    blocks.bitk = bitk;
                    z.AvailableBytesIn = availableBytesIn;
                    z.TotalBytesIn += nextIn - z.NextIn;
                    z.NextIn = nextIn;
                    blocks.write = write;
                    return blocks.Flush(r);
                }
            }
            int index = (this.tree_index + (bitb & inflate_mask[num7])) * 3;
            bitb = SharedUtils.URShift(bitb, this.tree[index + 1]);
            bitk -= this.tree[index + 1];
            int num9 = this.tree[index];
            if (num9 == 0)
            {
                this.lit = this.tree[index + 2];
                this.mode = 6;
            }
            else if ((num9 & 0x10) != 0)
            {
                this.get_Renamed = num9 & 15;
                this.len = this.tree[index + 2];
                this.mode = 2;
            }
            else if ((num9 & 0x40) == 0)
            {
                this.need = num9;
                this.tree_index = (index / 3) + this.tree[index + 2];
            }
            else
            {
                if ((num9 & 0x20) == 0)
                {
                    this.mode = 9;
                    z.Message = "invalid literal/length code";
                    r = -3;
                    blocks.bitb = bitb;
                    blocks.bitk = bitk;
                    z.AvailableBytesIn = availableBytesIn;
                    z.TotalBytesIn += nextIn - z.NextIn;
                    z.NextIn = nextIn;
                    blocks.write = write;
                    return blocks.Flush(r);
                }
                this.mode = 7;
            }
            goto Label_004E;
        Label_03B3:
            num11 = this.need;
            while (bitk < num11)
            {
                if (availableBytesIn != 0)
                {
                    r = 0;
                    availableBytesIn--;
                    bitb |= (z.InputBuffer[nextIn++] & 0xff) << bitk;
                    bitk += 8;
                }
                else
                {
                    blocks.bitb = bitb;
                    blocks.bitk = bitk;
                    z.AvailableBytesIn = availableBytesIn;
                    z.TotalBytesIn += nextIn - z.NextIn;
                    z.NextIn = nextIn;
                    blocks.write = write;
                    return blocks.Flush(r);
                }
            }
            int num12 = (this.tree_index + (bitb & inflate_mask[num11])) * 3;
            bitb = bitb >> this.tree[num12 + 1];
            bitk -= this.tree[num12 + 1];
            int num13 = this.tree[num12];
            if ((num13 & 0x10) != 0)
            {
                this.get_Renamed = num13 & 15;
                this.dist = this.tree[num12 + 2];
                this.mode = 4;
            }
            else
            {
                if ((num13 & 0x40) != 0)
                {
                    this.mode = 9;
                    z.Message = "invalid distance code";
                    r = -3;
                    blocks.bitb = bitb;
                    blocks.bitk = bitk;
                    z.AvailableBytesIn = availableBytesIn;
                    z.TotalBytesIn += nextIn - z.NextIn;
                    z.NextIn = nextIn;
                    blocks.write = write;
                    return blocks.Flush(r);
                }
                this.need = num13;
                this.tree_index = (num12 / 3) + this.tree[num12 + 2];
            }
            goto Label_004E;
        Label_057D:
            num15 = write - this.dist;
            while (num15 < 0)
            {
                num15 += blocks.end;
            }
            while (this.len != 0)
            {
                if (num6 == 0)
                {
                    if ((write == blocks.end) && (blocks.read != 0))
                    {
                        write = 0;
                        num6 = (write < blocks.read) ? ((blocks.read - write) - 1) : (blocks.end - write);
                    }
                    if (num6 == 0)
                    {
                        blocks.write = write;
                        r = blocks.Flush(r);
                        write = blocks.write;
                        num6 = (write < blocks.read) ? ((blocks.read - write) - 1) : (blocks.end - write);
                        if ((write == blocks.end) && (blocks.read != 0))
                        {
                            write = 0;
                            num6 = (write < blocks.read) ? ((blocks.read - write) - 1) : (blocks.end - write);
                        }
                        if (num6 == 0)
                        {
                            blocks.bitb = bitb;
                            blocks.bitk = bitk;
                            z.AvailableBytesIn = availableBytesIn;
                            z.TotalBytesIn += nextIn - z.NextIn;
                            z.NextIn = nextIn;
                            blocks.write = write;
                            return blocks.Flush(r);
                        }
                    }
                }
                blocks.window[write++] = blocks.window[num15++];
                num6--;
                if (num15 == blocks.end)
                {
                    num15 = 0;
                }
                this.len--;
            }
            this.mode = 0;
            goto Label_004E;
        Label_096B:
            r = 1;
            blocks.bitb = bitb;
            blocks.bitk = bitk;
            z.AvailableBytesIn = availableBytesIn;
            z.TotalBytesIn += nextIn - z.NextIn;
            z.NextIn = nextIn;
            blocks.write = write;
            return blocks.Flush(r);
        }
    }
}
