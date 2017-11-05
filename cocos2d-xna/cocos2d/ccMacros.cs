namespace cocos2d
{
    using System;

    public class ccMacros
    {
        public static readonly int CC_BLEND_DST = 0x303;
        public static readonly int CC_BLEND_SRC = 1;
        public static readonly float CC_DIRECTOR_FPS_INTERVAL = 0.5f;
        public static readonly string CC_RETINA_DISPLAY_FILENAME_SUFFIX = "-hd";
        public static readonly int CCSpriteIndexNotInitialized = 0x1312d000;
        public static readonly float FLT_EPSILON = 1.192093E-07f;
        private static Random rand = new Random();

        public static int CC_CONTENT_SCALE_FACTOR()
        {
            return 1;
        }

        public static float CC_DEGREES_TO_RADIANS(float angle)
        {
            return (angle * 0.01745329f);
        }

        public static bool CC_HOST_IS_BIG_ENDIAN()
        {
            return !BitConverter.IsLittleEndian;
        }

        public static float CC_RADIANS_TO_DEGREES(float angle)
        {
            return (angle * 57.29578f);
        }

        public static CCRect CC_RECT_PIXELS_TO_POINTS(CCRect pixels)
        {
            return pixels;
        }

        public static CCRect CC_RECT_POINTS_TO_PIXELS(CCRect points)
        {
            return points;
        }

        public static void CC_SWAP<T>(ref T x, ref T y)
        {
            T local = x;
            x = y;
            y = local;
        }

        public static ushort CC_SWAP_INT16_BIG_TO_HOST(ushort i)
        {
            if (!CC_HOST_IS_BIG_ENDIAN())
            {
                return CC_SWAP16(i);
            }
            return i;
        }

        public static ushort CC_SWAP_INT16_LITTLE_TO_HOST(ushort i)
        {
            if (!CC_HOST_IS_BIG_ENDIAN())
            {
                return i;
            }
            return CC_SWAP16(i);
        }

        public static uint CC_SWAP_INT32_BIG_TO_HOST(uint i)
        {
            if (!CC_HOST_IS_BIG_ENDIAN())
            {
                return CC_SWAP32(i);
            }
            return i;
        }

        public static uint CC_SWAP_INT32_LITTLE_TO_HOST(uint i)
        {
            if (!CC_HOST_IS_BIG_ENDIAN())
            {
                return i;
            }
            return CC_SWAP32(i);
        }

        public static ushort CC_SWAP16(ushort i)
        {
            return (ushort)(((i & 0xff) << 8) | ((i & 0xff00) >> 8));
        }

        public static uint CC_SWAP32(uint i)
        {
            return (uint)(((((i & 0xff) << 0x18) | ((i & 0xff00) << 8)) | ((i & 0xff0000) >> 8)) | ((i & -16777216) >> 0x18));
        }

        public static CCPoint CCPointMake(float x, float y)
        {
            return new CCPoint(x, y);
        }

        public static float CCRANDOM_0_1()
        {
            return (((float)rand.Next()) / 2.147484E+09f);
        }

        public static float CCRANDOM_MINUS1_1()
        {
            return ((2f * (((float)rand.Next()) / 2.147484E+09f)) - 1f);
        }

        public static CCRect CCRectMake(float x, float y, float width, float height)
        {
            return new CCRect(x, y, width, height);
        }

        public static CCSize CCSizeMake(float width, float height)
        {
            return new CCSize(width, height);
        }
    }
}
