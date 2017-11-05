using System;

namespace cocos2d
{
	public class CCReuseGrid : CCActionInstant
	{
		protected int m_nTimes;

		public CCReuseGrid()
		{
		}

		public static CCReuseGrid actionWithTimes(int times)
		{
			CCReuseGrid cCReuseGrid = new CCReuseGrid();
			if (cCReuseGrid != null)
			{
				cCReuseGrid.initWithTimes(times);
			}
			return cCReuseGrid;
		}

		public bool initWithTimes(int times)
		{
			this.m_nTimes = times;
			return true;
		}

		public virtual new void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			if (this.m_pTarget.Grid != null)
			{
				bool active = this.m_pTarget.Grid.Active;
				this.m_pTarget.Grid.ReuseGrid = this.m_pTarget.Grid.ReuseGrid + this.m_nTimes;
			}
		}
	}
}