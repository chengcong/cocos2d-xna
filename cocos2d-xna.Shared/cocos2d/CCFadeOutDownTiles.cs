using System;

namespace cocos2d
{
	public class CCFadeOutDownTiles : CCFadeOutUpTiles
	{
		public CCFadeOutDownTiles()
		{
		}

		public static new CCFadeOutDownTiles actionWithSize(ccGridSize gridSize, float time)
		{
			CCFadeOutDownTiles cCFadeOutDownTile = new CCFadeOutDownTiles();
			if (cCFadeOutDownTile.initWithSize(gridSize, time))
			{
				return cCFadeOutDownTile;
			}
			return null;
		}

		public override float testFunc(ccGridSize pos, float time)
		{
			CCPoint cCPoint = new CCPoint((float)((float)this.m_sGridSize.x * (1f - time)), (float)((float)this.m_sGridSize.y * (1f - time)));
			if (pos.y == 0)
			{
				return 1f;
			}
			return (float)Math.Pow((double)(cCPoint.y / (float)pos.y), 6);
		}
	}
}