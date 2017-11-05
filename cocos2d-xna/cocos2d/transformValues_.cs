using System;

namespace cocos2d
{
	public class transformValues_
	{
		public CCPoint pos;

		public CCPoint scale;

		public float rotation;

		public CCPoint skew;

		public CCPoint ap;

		public bool visible;

		public transformValues_()
		{
			this.pos = new CCPoint();
			this.scale = new CCPoint();
			this.skew = new CCPoint();
			this.ap = new CCPoint();
		}
	}
}