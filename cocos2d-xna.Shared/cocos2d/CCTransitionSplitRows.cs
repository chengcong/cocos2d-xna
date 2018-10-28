using System;

namespace cocos2d
{
	public class CCTransitionSplitRows : CCTransitionSplitCols
	{
		public CCTransitionSplitRows()
		{
		}

		public override CCActionInterval action()
		{
			return CCSplitRows.actionWithRows(3, this.m_fDuration / 2f);
		}

		public static new CCTransitionSplitRows transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionSplitRows cCTransitionSplitRow = new CCTransitionSplitRows();
			if (cCTransitionSplitRow.initWithDuration(t, scene))
			{
				return cCTransitionSplitRow;
			}
			return null;
		}
	}
}