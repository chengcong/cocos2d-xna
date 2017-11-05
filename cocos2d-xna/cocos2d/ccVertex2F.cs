using Microsoft.Xna.Framework;
using System;

namespace cocos2d
{
	public class ccVertex2F
	{
		public float x;

		public float y;

		public ccVertex2F()
		{
			this.x = 0f;
			this.y = 0f;
		}

		public ccVertex2F(float inx, float iny)
		{
			this.x = inx;
			this.y = iny;
		}

		public ccVertex2F(ccVertex2F copy)
		{
			this.x = copy.x;
			this.y = copy.y;
		}

		public ccVertex2F copy()
		{
			return new ccVertex2F(this);
		}

		public Vector3 ToVector3()
		{
			return new Vector3(this.x, this.y, 0f);
		}
	}
}