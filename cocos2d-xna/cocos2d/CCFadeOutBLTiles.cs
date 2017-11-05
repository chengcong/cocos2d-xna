using System;

namespace cocos2d
{
	public class CCFadeOutBLTiles : CCFadeOutTRTiles
	{
		public CCFadeOutBLTiles()
		{
		}

		public static new CCFadeOutBLTiles actionWithSize(ccGridSize gridSize, float time)
		{
			CCFadeOutBLTiles cCFadeOutBLTile = new CCFadeOutBLTiles();
			if (cCFadeOutBLTile.initWithSize(gridSize, time))
			{
				return cCFadeOutBLTile;
			}
			return null;
		}

		public override float testFunc(ccGridSize pos, float time)
		{
			CCPoint cCPoint = new CCPoint((float)((float)this.m_sGridSize.x * (1f - time)), (float)((float)this.m_sGridSize.y * (1f - time)));
			if (pos.x + pos.y == 0)
			{
				return 1f;
			}
			return (float)Math.Pow((double)((cCPoint.x + cCPoint.y) / (float)(pos.x + pos.y)), 6);
		}
	}
}