using System;

namespace cocos2d
{
	public class CCGrid3DAction : CCGridAction
	{
		public CCGrid3DAction()
		{
		}

		public static new CCGrid3DAction actionWithSize(ccGridSize gridSize, float duration)
		{
			throw new NotImplementedException("win32 is not implemented");
		}

		public override CCGridBase getGrid()
		{
			return CCGrid3D.gridWithSize(this.m_sGridSize);
		}

		public ccVertex3F originalVertex(ccGridSize pos)
		{
			return ((CCGrid3D)this.m_pTarget.Grid).originalVertex(pos);
		}

		public ccVertex3F originalVertex(int i, int j)
		{
			return ((CCGrid3D)this.m_pTarget.Grid).originalVertex(i, j);
		}

		public void setVertex(ccGridSize pos, ccVertex3F vertex)
		{
			((CCGrid3D)this.m_pTarget.Grid).setVertex(pos, vertex);
		}

		public void setVertex(int i, int j, ccVertex3F vertex)
		{
			((CCGrid3D)this.m_pTarget.Grid).setVertex(i, j, vertex);
		}

		public ccVertex3F vertex(ccGridSize pos)
		{
			return ((CCGrid3D)this.m_pTarget.Grid).vertex(pos);
		}
	}
}