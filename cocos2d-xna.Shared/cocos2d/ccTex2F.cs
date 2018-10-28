using Microsoft.Xna.Framework;
using System;

namespace cocos2d
{
	public class ccTex2F
	{
		public float u;

		public float v;

		public ccTex2F()
		{
			this.u = 0f;
			this.v = 0f;
		}

		public ccTex2F(float inu, float inv)
		{
			this.u = inu;
			this.v = inv;
		}

		public Vector2 ToVector2()
		{
			return new Vector2(this.u, this.v);
		}
	}
}