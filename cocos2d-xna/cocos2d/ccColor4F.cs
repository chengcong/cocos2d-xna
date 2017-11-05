using System;

namespace cocos2d
{
	public class ccColor4F
	{
		public float r;

		public float g;

		public float b;

		public float a;

		public ccColor4F()
		{
			this.r = 0f;
			this.g = 0f;
			this.b = 0f;
			this.a = 0f;
		}

		public ccColor4F(float inr, float ing, float inb, float ina)
		{
			this.r = inr;
			this.g = ing;
			this.b = inb;
			this.a = ina;
		}

		public ccColor4F copy()
		{
			return new ccColor4F(this.r, this.g, this.b, this.a);
		}

		public static ccColor4F Parse(string s)
		{
			string[] strArrays = s.Split(new char[] { ',' });
			return new ccColor4F(float.Parse(strArrays[0]), float.Parse(strArrays[1]), float.Parse(strArrays[2]), float.Parse(strArrays[3]));
		}

		public new string ToString()
		{
			object[] objArray = new object[] { this.r, this.g, this.b, this.a };
			return string.Format("{0},{1},{2},{3}", objArray);
		}
	}
}