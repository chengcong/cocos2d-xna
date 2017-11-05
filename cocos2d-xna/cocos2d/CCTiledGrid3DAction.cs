using System;

namespace cocos2d
{
	public class CCTiledGrid3DAction : CCGridAction
	{
		public CCTiledGrid3DAction()
		{
		}

		public static new CCTiledGrid3DAction actionWithSize(ccGridSize gridSize, float duration)
		{
			CCTiledGrid3DAction cCTiledGrid3DAction = new CCTiledGrid3DAction();
			cCTiledGrid3DAction.initWithSize(gridSize, duration);
			return cCTiledGrid3DAction;
		}

		public override CCGridBase getGrid()
		{
			return CCTiledGrid3D.gridWithSize(this.m_sGridSize);
		}

		public virtual ccQuad3 originalTile(ccGridSize pos)
		{
			return this.originalTile(pos.x, pos.y);
		}

		public virtual ccQuad3 originalTile(int x, int y)
		{
			CCTiledGrid3D grid = (CCTiledGrid3D)this.m_pTarget.Grid;
			if (grid == null)
			{
				return null;
			}
			return grid.originalTile(x, y);
		}

		public virtual void setTile(ccGridSize pos, ccQuad3 coords)
		{
			this.setTile(pos.x, pos.y, coords);
		}

		public virtual void setTile(int x, int y, ccQuad3 coords)
		{
			if (coords == null)
			{
				return;
			}
			CCTiledGrid3D grid = (CCTiledGrid3D)this.m_pTarget.Grid;
			if (grid != null)
			{
				grid.setTile(x, y, coords);
			}
		}

		public virtual ccQuad3 tile(ccGridSize pos)
		{
			return this.tile(pos.x, pos.y);
		}

		public virtual ccQuad3 tile(int x, int y)
		{
			CCTiledGrid3D grid = (CCTiledGrid3D)this.m_pTarget.Grid;
			if (grid == null)
			{
				return null;
			}
			return grid.tile(x, y);
		}
	}
}