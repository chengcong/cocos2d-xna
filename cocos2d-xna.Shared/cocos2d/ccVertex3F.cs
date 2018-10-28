using Microsoft.Xna.Framework;
using System;

namespace cocos2d
{
	public class ccVertex3F
	{
		public float x;

		public float y;

		public float z;

		public ccVertex3F()
		{
			this.x = 0f;
			this.y = 0f;
			this.z = 0f;
		}

		public ccVertex3F(float inx, float iny, float inz)
		{
			this.x = inx;
			this.y = iny;
			this.z = inz;
		}

		public Vector3 ToVector3()
		{
			return new Vector3(this.x, this.y, this.z);
		}
	}
}