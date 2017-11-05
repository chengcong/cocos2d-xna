using System;

namespace cocos2d
{
	public class ccGridSize
	{
		public int x;

		public int y;

		public ccGridSize()
		{
			this.x = 0;
			this.y = 0;
		}

		public ccGridSize(int inx, int iny)
		{
			this.x = inx;
			this.y = iny;
		}

		public void @set(int inx, int iny)
		{
			this.x = inx;
			this.y = iny;
		}
	}
}