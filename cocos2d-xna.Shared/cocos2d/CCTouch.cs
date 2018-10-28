using System;

namespace cocos2d
{
	public class CCTouch : CCObject
	{
		private int m_nViewId;

		private CCPoint m_point;

		private CCPoint m_prevPoint;

		public CCTouch() : this(0, 0f, 0f)
		{
		}

		public CCTouch(int nViewId, float x, float y)
		{
			this.m_nViewId = nViewId;
			this.m_point = new CCPoint(x, y);
			this.m_prevPoint = new CCPoint(x, y);
		}

		public CCPoint locationInView(int nViewId)
		{
			return this.m_point;
		}

		public CCPoint previousLocationInView(int nViewId)
		{
			return this.m_prevPoint;
		}

		public void SetTouchInfo(int nViewId, float x, float y)
		{
			this.m_nViewId = nViewId;
			this.m_prevPoint = new CCPoint(this.m_point.x, this.m_point.y);
			this.m_point.x = x;
			this.m_point.y = y;
		}

		public int view()
		{
			return this.m_nViewId;
		}
	}
}