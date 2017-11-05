using System;

namespace cocos2d
{
	public class CCFadeOutUpTiles : CCFadeOutTRTiles
	{
		public CCFadeOutUpTiles()
		{
		}

		public static new CCFadeOutUpTiles actionWithSize(ccGridSize gridSize, float time)
		{
			CCFadeOutUpTiles cCFadeOutUpTile = new CCFadeOutUpTiles();
			if (cCFadeOutUpTile.initWithSize(gridSize, time))
			{
				return cCFadeOutUpTile;
			}
			return null;
		}

		public override float testFunc(ccGridSize pos, float time)
		{
			CCPoint cCPoint = new CCPoint((float)((float)this.m_sGridSize.x * time), (float)((float)this.m_sGridSize.y * time));
			if (cCPoint.y == 0f)
			{
				return 1f;
			}
			return (float)Math.Pow((double)((float)pos.y / cCPoint.y), 6);
		}

		public override void transformTile(ccGridSize pos, float distance)
		{
			ccQuad3 _ccQuad3 = this.originalTile(pos);
			if (_ccQuad3 == null)
			{
				return;
			}
			CCPoint step = this.m_pTarget.Grid.Step;
			ccVertex3F _ccVertex3F = _ccQuad3.bl;
			_ccVertex3F.y = _ccVertex3F.y + step.y / 2f * (1f - distance);
			ccVertex3F _ccVertex3F1 = _ccQuad3.br;
			_ccVertex3F1.y = _ccVertex3F1.y + step.y / 2f * (1f - distance);
			ccVertex3F _ccVertex3F2 = _ccQuad3.tl;
			_ccVertex3F2.y = _ccVertex3F2.y - step.y / 2f * (1f - distance);
			ccVertex3F _ccVertex3F3 = _ccQuad3.tr;
			_ccVertex3F3.y = _ccVertex3F3.y - step.y / 2f * (1f - distance);
			this.setTile(pos, _ccQuad3);
		}
	}
}