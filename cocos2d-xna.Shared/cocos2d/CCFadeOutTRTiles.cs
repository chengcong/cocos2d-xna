using System;

namespace cocos2d
{
	public class CCFadeOutTRTiles : CCTiledGrid3DAction
	{
		public CCFadeOutTRTiles()
		{
		}

		public static new CCFadeOutTRTiles actionWithSize(ccGridSize gridSize, float time)
		{
			CCFadeOutTRTiles cCFadeOutTRTile = new CCFadeOutTRTiles();
			if (cCFadeOutTRTile.initWithSize(gridSize, time))
			{
				return cCFadeOutTRTile;
			}
			return null;
		}

		public virtual float testFunc(ccGridSize pos, float time)
		{
			CCPoint cCPoint = new CCPoint((float)((float)this.m_sGridSize.x * time), (float)((float)this.m_sGridSize.y * time));
			if (cCPoint.x + cCPoint.y == 0f)
			{
				return 1f;
			}
			return (float)Math.Pow((double)((float)(pos.x + pos.y) / (cCPoint.x + cCPoint.y)), 6);
		}

		public virtual void transformTile(ccGridSize pos, float distance)
		{
			ccQuad3 _ccQuad3 = this.originalTile(pos);
			if (_ccQuad3 == null)
			{
				return;
			}
			CCPoint step = this.m_pTarget.Grid.Step;
			ccVertex3F _ccVertex3F = _ccQuad3.bl;
			_ccVertex3F.x = _ccVertex3F.x + step.x / 2f * (1f - distance);
			ccVertex3F _ccVertex3F1 = _ccQuad3.bl;
			_ccVertex3F1.y = _ccVertex3F1.y + step.y / 2f * (1f - distance);
			ccVertex3F _ccVertex3F2 = _ccQuad3.br;
			_ccVertex3F2.x = _ccVertex3F2.x - step.x / 2f * (1f - distance);
			ccVertex3F _ccVertex3F3 = _ccQuad3.br;
			_ccVertex3F3.y = _ccVertex3F3.y + step.y / 2f * (1f - distance);
			ccVertex3F _ccVertex3F4 = _ccQuad3.tl;
			_ccVertex3F4.x = _ccVertex3F4.x + step.x / 2f * (1f - distance);
			ccVertex3F _ccVertex3F5 = _ccQuad3.tl;
			_ccVertex3F5.y = _ccVertex3F5.y - step.y / 2f * (1f - distance);
			ccVertex3F _ccVertex3F6 = _ccQuad3.tr;
			_ccVertex3F6.x = _ccVertex3F6.x - step.x / 2f * (1f - distance);
			ccVertex3F _ccVertex3F7 = _ccQuad3.tr;
			_ccVertex3F7.y = _ccVertex3F7.y - step.y / 2f * (1f - distance);
			this.setTile(pos, _ccQuad3);
		}

		public void turnOffTile(ccGridSize pos)
		{
			this.setTile(pos, new ccQuad3());
		}

		public void turnOnTile(ccGridSize pos)
		{
			this.setTile(pos, this.originalTile(pos));
		}

		public override void update(float time)
		{
			for (int i = 0; i < this.m_sGridSize.x; i++)
			{
				for (int j = 0; j < this.m_sGridSize.y; j++)
				{
					float single = this.testFunc(new ccGridSize(i, j), time);
					if (single == 0f)
					{
						this.turnOffTile(new ccGridSize(i, j));
					}
					else if (single >= 1f)
					{
						this.turnOnTile(new ccGridSize(i, j));
					}
					else
					{
						this.transformTile(new ccGridSize(i, j), single);
					}
				}
			}
		}
	}
}