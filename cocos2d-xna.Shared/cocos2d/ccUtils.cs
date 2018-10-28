using System;
using System.Globalization;

namespace cocos2d
{
	public class ccUtils
	{
		public ccUtils()
		{
		}

		public static CCPoint ccCardinalSplineAt(CCPoint p0, CCPoint p1, CCPoint p2, CCPoint p3, float tension, float t)
		{
			float single = t * t;
			float single1 = single * t;
			float single2 = (1f - tension) / 2f;
			float single3 = single2 * (-single1 + 2f * single - t);
			float single4 = single2 * (-single1 + single) + (2f * single1 - 3f * single + 1f);
			float single5 = single2 * (single1 - 2f * single + t) + (-2f * single1 + 3f * single);
			float single6 = single2 * (single1 - single);
			float single7 = p0.x * single3 + p1.x * single4 + p2.x * single5 + p3.x * single6;
			float single8 = p0.y * single3 + p1.y * single4 + p2.y * single5 + p3.y * single6;
			return new CCPoint(single7, single8);
		}

		public static long ccNextPOT(long x)
		{
			x = x - (long)1;
			x = x | x >> 1;
			x = x | x >> 2;
			x = x | x >> 4;
			x = x | x >> 8;
			x = x | x >> 16;
			return x + (long)1;
		}

		public static float ccParseFloat(string toParse)
		{
			return float.Parse(toParse, CultureInfo.InvariantCulture);
		}

		public static float ccParseFloat(string toParse, NumberStyles ns)
		{
			return float.Parse(toParse, ns, CultureInfo.InvariantCulture);
		}

		public static int ccParseInt(string toParse)
		{
			return int.Parse(toParse, CultureInfo.InvariantCulture);
		}

		public static int ccParseInt(string toParse, NumberStyles ns)
		{
			return int.Parse(toParse, ns, CultureInfo.InvariantCulture);
		}
	}
}