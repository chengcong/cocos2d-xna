using Microsoft.Xna.Framework;
using System;

namespace cocos2d
{
	public class ccColor4B
	{
		public byte r;

		public byte g;

		public byte b;

		public byte a;

		public Color XNAColor
		{
			get
			{
				return new Color((int)this.r, (int)this.g, (int)this.b, (int)this.a);
			}
		}

		public ccColor4B()
		{
			this.r = 0;
			this.g = 0;
			this.b = 0;
			this.a = 0;
		}

		public ccColor4B(ccColor4B copy)
		{
			this.r = copy.r;
			this.g = copy.g;
			this.b = copy.b;
			this.a = copy.a;
		}

		public ccColor4B(ccColor3B c3)
		{
			this.r = c3.r;
			this.g = c3.g;
			this.b = c3.b;
			this.a = 255;
		}

		public ccColor4B(byte inr, byte ing, byte inb, byte ina)
		{
			this.r = inr;
			this.g = ing;
			this.b = inb;
			this.a = ina;
		}

		public ccColor4B(Color color)
		{
			this.r = color.R;
			this.g = color.G;
			this.b = color.B;
			this.a = color.A;
		}

		public ccColor4B copy()
		{
			return new ccColor4B(this.r, this.g, this.b, this.a);
		}

		public static ccColor4B Parse(string s)
		{
			string[] strArrays = s.Split(new char[] { ',' });
			return new ccColor4B(byte.Parse(strArrays[0]), byte.Parse(strArrays[1]), byte.Parse(strArrays[2]), byte.Parse(strArrays[3]));
		}

		public new string ToString()
		{
			object[] objArray = new object[] { this.r, this.g, this.b, this.a };
			return string.Format("{0},{1},{2},{3}", objArray);
		}
	}
}