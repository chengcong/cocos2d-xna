using System;

namespace cocos2d
{
	public class ccTypes
	{
		public readonly static ccColor3B ccWHITE;

		public readonly static ccColor3B ccYELLOW;

		public readonly static ccColor3B ccBLUE;

		public readonly static ccColor3B ccGREEN;

		public readonly static ccColor3B ccRED;

		public readonly static ccColor3B ccMAGENTA;

		public readonly static ccColor3B ccBLACK;

		public readonly static ccColor3B ccORANGE;

		public readonly static ccColor3B ccGRAY;

		static ccTypes()
		{
			ccTypes.ccWHITE = new ccColor3B(255, 255, 255);
			ccTypes.ccYELLOW = new ccColor3B(255, 255, 0);
			ccTypes.ccBLUE = new ccColor3B(0, 0, 255);
			ccTypes.ccGREEN = new ccColor3B(0, 255, 0);
			ccTypes.ccRED = new ccColor3B(255, 0, 0);
			ccTypes.ccMAGENTA = new ccColor3B(255, 0, 255);
			ccTypes.ccBLACK = new ccColor3B(0, 0, 0);
			ccTypes.ccORANGE = new ccColor3B(255, 127, 0);
			ccTypes.ccGRAY = new ccColor3B(166, 166, 166);
		}

		public ccTypes()
		{
		}

		public static ccColor3B ccc3(byte r, byte g, byte b)
		{
			return new ccColor3B(r, g, b);
		}

		public static ccColor4B ccc4(byte r, byte g, byte b, byte o)
		{
			return new ccColor4B(r, g, b, o);
		}

		public static bool ccc4FEqual(ccColor4F a, ccColor4F b)
		{
			if (a.r != b.r || a.g != b.g || a.b != b.b)
			{
				return false;
			}
			return a.a == b.a;
		}

		public static ccColor4F ccc4FFromccc3B(ccColor3B c)
		{
			ccColor4F _ccColor4F = new ccColor4F((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, 1f);
			return _ccColor4F;
		}

		public static ccColor4F ccc4FFromccc4B(ccColor4B c)
		{
			ccColor4F _ccColor4F = new ccColor4F((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
			return _ccColor4F;
		}

		public static ccGridSize ccg(int x, int y)
		{
			return new ccGridSize(x, y);
		}

		public static ccTex2F tex2(float u, float v)
		{
			return new ccTex2F(u, v);
		}

		public static ccVertex2F vertex2(float x, float y)
		{
			return new ccVertex2F(x, y);
		}

		public static ccVertex3F vertex3(float x, float y, float z)
		{
			return new ccVertex3F(x, y, z);
		}
	}
}