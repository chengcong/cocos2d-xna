using System;

namespace cocos2d
{
	public class ccPointSprite
	{
		public ccVertex2F pos;

		public ccColor4B color;

		public float size;

		public ccPointSprite()
		{
			this.pos = new ccVertex2F();
			this.color = new ccColor4B();
			this.size = 0f;
		}
	}
}