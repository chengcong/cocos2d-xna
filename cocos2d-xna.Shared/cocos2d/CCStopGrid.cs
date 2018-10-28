using System;

namespace cocos2d
{
	public class CCStopGrid : CCActionInstant
	{
		public CCStopGrid()
		{
		}

		public static new CCStopGrid action()
		{
			return new CCStopGrid();
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			CCGridBase grid = this.m_pTarget.Grid;
			if (grid != null && grid.Active)
			{
				grid.Active = false;
			}
		}
	}
}