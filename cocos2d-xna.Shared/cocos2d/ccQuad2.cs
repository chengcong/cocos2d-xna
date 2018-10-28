using System;

namespace cocos2d
{
	public class ccQuad2
	{
		public ccVertex2F tl;

		public ccVertex2F tr;

		public ccVertex2F bl;

		public ccVertex2F br;

		public ccQuad2()
		{
			this.tl = new ccVertex2F();
			this.tr = new ccVertex2F();
			this.bl = new ccVertex2F();
			this.br = new ccVertex2F();
		}
	}
}