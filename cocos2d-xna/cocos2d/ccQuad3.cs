using System;

namespace cocos2d
{
	public class ccQuad3
	{
		public ccVertex3F bl;

		public ccVertex3F br;

		public ccVertex3F tl;

		public ccVertex3F tr;

		public ccQuad3()
		{
			this.tl = new ccVertex3F();
			this.tr = new ccVertex3F();
			this.bl = new ccVertex3F();
			this.br = new ccVertex3F();
		}
	}
}