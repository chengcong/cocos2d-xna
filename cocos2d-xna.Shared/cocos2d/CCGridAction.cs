using System;

namespace cocos2d
{
	public class CCGridAction : CCActionInterval
	{
		protected ccGridSize m_sGridSize;

		public CCGridAction()
		{
		}

		public static CCGridAction actionWithSize(ccGridSize gridSize, float duration)
		{
			CCGridAction cCGridAction = new CCGridAction();
			if (cCGridAction.initWithSize(gridSize, duration))
			{
				return cCGridAction;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCZone cCZone = null;
			CCGridAction cCGridAction = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCGridAction = new CCGridAction();
				CCZone cCZone1 = new CCZone(cCGridAction);
				cCZone = cCZone1;
				pZone = cCZone1;
			}
			else
			{
				cCGridAction = (CCGridAction)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCGridAction.initWithSize(this.m_sGridSize, this.m_fDuration);
			if (cCZone != null)
			{
				cCZone = null;
			}
			return cCGridAction;
		}

		public virtual CCGridBase getGrid()
		{
			return null;
		}

		public virtual bool initWithSize(ccGridSize gridSize, float duration)
		{
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.m_sGridSize = gridSize;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCReverseTime.actionWithAction(this);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			CCGridBase grid = this.getGrid();
			CCNode mPTarget = this.m_pTarget;
			CCGridBase cCGridBase = mPTarget.Grid;
			if (cCGridBase == null || cCGridBase.ReuseGrid <= 0)
			{
				if (cCGridBase != null && cCGridBase.Active)
				{
					cCGridBase.Active = false;
				}
				mPTarget.Grid = grid;
				mPTarget.Grid.Active = true;
			}
			else if (cCGridBase.Active && cCGridBase.GridSize.x == this.m_sGridSize.x && cCGridBase.GridSize.y == this.m_sGridSize.y)
			{
				cCGridBase.reuse();
				return;
			}
		}
	}
}