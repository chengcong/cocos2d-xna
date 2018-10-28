using Microsoft.Xna.Framework;
using System;

namespace cocos2d
{
	public class ccColor3B
	{
		public byte r;

		public byte g;

		public byte b;

		public Color XNAColor
		{
			get
			{
				return new Color((int)this.r, (int)this.g, (int)this.b, 255);
			}
		}

		public ccColor3B()
		{
			this.r = 0;
			this.g = 0;
			this.b = 0;
		}

		public ccColor3B(ccColor3B copy)
		{
			this.r = copy.r;
			this.g = copy.g;
			this.b = copy.b;
		}

		public ccColor3B(byte inr, byte ing, byte inb)
		{
			this.r = inr;
			this.g = ing;
			this.b = inb;
		}

		public ccColor3B(Color color)
		{
			this.r = color.R;
			this.g = color.G;
			this.b = color.B;
		}

		public ccColor3B copy()
		{
			return new ccColor3B(this.r, this.g, this.b);
		}
	}
}