using System;

namespace cocos2d
{
	public class CCShuffleTiles : CCTiledGrid3DAction
	{
		protected int m_nSeed;

		protected int m_nTilesCount;

		protected int[] m_pTilesOrder;

		protected Tile[] m_pTiles;

		public CCShuffleTiles()
		{
		}

		public static CCShuffleTiles actionWithSeed(int s, ccGridSize gridSize, float duration)
		{
			CCShuffleTiles cCShuffleTile = new CCShuffleTiles();
			if (cCShuffleTile.initWithSeed(s, gridSize, duration))
			{
				return cCShuffleTile;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCShuffleTiles cCShuffleTile = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCShuffleTile = new CCShuffleTiles();
				pZone = new CCZone(cCShuffleTile);
			}
			else
			{
				cCShuffleTile = (CCShuffleTiles)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCShuffleTile.initWithSeed(this.m_nSeed, this.m_sGridSize, this.m_fDuration);
			return cCShuffleTile;
		}

		public ccGridSize getDelta(int x, int y)
		{
			int num = x * this.m_sGridSize.y + y;
			float mPTilesOrder = (float)(this.m_pTilesOrder[num] / this.m_sGridSize.y);
			float single = (float)(this.m_pTilesOrder[num] % this.m_sGridSize.y);
			return new ccGridSize((int)(mPTilesOrder - (float)x), (int)(single - (float)y));
		}

		public ccGridSize getDelta(ccGridSize pos)
		{
			return this.getDelta(pos.x, pos.y);
		}

		public bool initWithSeed(int s, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_nSeed = s;
			this.m_pTilesOrder = null;
			this.m_pTiles = null;
			return true;
		}

		public void placeTile(int x, int y, Tile t)
		{
			ccQuad3 _ccQuad3 = this.originalTile(x, y);
			if (_ccQuad3 == null)
			{
				return;
			}
			CCPoint step = this.m_pTarget.Grid.Step;
			ccVertex3F _ccVertex3F = _ccQuad3.bl;
			_ccVertex3F.x = _ccVertex3F.x + (float)((int)(t.position.x * step.x));
			ccVertex3F _ccVertex3F1 = _ccQuad3.bl;
			_ccVertex3F1.y = _ccVertex3F1.y + (float)((int)(t.position.y * step.y));
			ccVertex3F _ccVertex3F2 = _ccQuad3.br;
			_ccVertex3F2.x = _ccVertex3F2.x + (float)((int)(t.position.x * step.x));
			ccVertex3F _ccVertex3F3 = _ccQuad3.br;
			_ccVertex3F3.y = _ccVertex3F3.y + (float)((int)(t.position.y * step.y));
			ccVertex3F _ccVertex3F4 = _ccQuad3.tl;
			_ccVertex3F4.x = _ccVertex3F4.x + (float)((int)(t.position.x * step.x));
			ccVertex3F _ccVertex3F5 = _ccQuad3.tl;
			_ccVertex3F5.y = _ccVertex3F5.y + (float)((int)(t.position.y * step.y));
			ccVertex3F _ccVertex3F6 = _ccQuad3.tr;
			_ccVertex3F6.x = _ccVertex3F6.x + (float)((int)(t.position.x * step.x));
			ccVertex3F _ccVertex3F7 = _ccQuad3.tr;
			_ccVertex3F7.y = _ccVertex3F7.y + (float)((int)(t.position.y * step.y));
			this.setTile(x, y, _ccQuad3);
		}

		public void placeTile(ccGridSize pos, Tile t)
		{
			this.placeTile(pos.x, pos.y, t);
		}

		public void shuffle(ref int[] pArray, int nLen)
		{
			Random random = new Random();
			for (int i = nLen - 1; i >= 0; i--)
			{
				int num = random.Next() % (i + 1);
				int num1 = pArray[i];
				pArray[i] = pArray[num];
				pArray[num] = num1;
			}
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			if (this.m_nSeed != -1)
			{
				this.m_nSeed = (new Random()).Next();
			}
			this.m_nTilesCount = this.m_sGridSize.x * this.m_sGridSize.y;
			this.m_pTilesOrder = new int[this.m_nTilesCount];
			for (int i = 0; i < this.m_nTilesCount; i++)
			{
				this.m_pTilesOrder[i] = i;
			}
			this.shuffle(ref this.m_pTilesOrder, this.m_nTilesCount);
			this.m_pTiles = new Tile[this.m_nTilesCount];
			int num = 0;
			for (int j = 0; j < this.m_sGridSize.x; j++)
			{
				for (int k = 0; k < this.m_sGridSize.y; k++)
				{
					Tile tile = new Tile()
					{
						position = new CCPoint((float)j, (float)k),
						startPosition = new CCPoint((float)j, (float)k),
						delta = this.getDelta(j, k)
					};
					this.m_pTiles[num] = tile;
					num++;
				}
			}
		}

		public override void update(float time)
		{
			int num = 0;
			for (int i = 0; i < this.m_sGridSize.x; i++)
			{
				for (int j = 0; j < this.m_sGridSize.y; j++)
				{
					Tile mPTiles = this.m_pTiles[num];
					if (mPTiles != null)
					{
						mPTiles.position = new CCPoint((float)((float)mPTiles.delta.x * time), (float)((float)mPTiles.delta.y * time));
						this.placeTile(i, j, mPTiles);
						num++;
					}
				}
			}
		}
	}
}