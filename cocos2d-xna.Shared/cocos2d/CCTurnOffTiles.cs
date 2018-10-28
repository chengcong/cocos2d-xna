using System;

namespace cocos2d
{
	public class CCTurnOffTiles : CCTiledGrid3DAction
	{
		private Random random = new Random();

		protected int m_nSeed;

		protected int m_nTilesCount;

		protected int[] m_pTilesOrder;

		public CCTurnOffTiles()
		{
		}

		public static CCTurnOffTiles actionWithSeed(int s, ccGridSize gridSize, float duration)
		{
			CCTurnOffTiles cCTurnOffTile = new CCTurnOffTiles();
			if (cCTurnOffTile.initWithSeed(s, gridSize, duration))
			{
				return cCTurnOffTile;
			}
			return null;
		}

		public static new CCTurnOffTiles actionWithSize(ccGridSize size, float d)
		{
			CCTurnOffTiles cCTurnOffTile = new CCTurnOffTiles();
			if (cCTurnOffTile.initWithSize(size, d))
			{
				return cCTurnOffTile;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCTurnOffTiles cCTurnOffTile = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCTurnOffTile = new CCTurnOffTiles();
				pZone = new CCZone(cCTurnOffTile);
			}
			else
			{
				cCTurnOffTile = (CCTurnOffTiles)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCTurnOffTile.initWithSeed(this.m_nSeed, this.m_sGridSize, this.m_fDuration);
			return cCTurnOffTile;
		}

		public bool initWithSeed(int s, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_nSeed = s;
			this.m_pTilesOrder = null;
			return true;
		}

		public void shuffle(int[] pArray, int nLen)
		{
			for (int i = nLen - 1; i >= 0; i--)
			{
				int num = this.random.Next() % (i + 1);
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
				this.random.Next(this.m_nSeed);
			}
			this.m_nTilesCount = this.m_sGridSize.x * this.m_sGridSize.y;
			this.m_pTilesOrder = new int[this.m_nTilesCount];
			for (int i = 0; i < this.m_nTilesCount; i++)
			{
				this.m_pTilesOrder[i] = i;
			}
			this.shuffle(this.m_pTilesOrder, this.m_nTilesCount);
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
			int num = (int)(time * (float)this.m_nTilesCount);
			for (int i = 0; i < this.m_nTilesCount; i++)
			{
				int mPTilesOrder = this.m_pTilesOrder[i];
				ccGridSize _ccGridSize = new ccGridSize(mPTilesOrder / this.m_sGridSize.y, mPTilesOrder % this.m_sGridSize.y);
				if (i >= num)
				{
					this.turnOnTile(_ccGridSize);
				}
				else
				{
					this.turnOffTile(_ccGridSize);
				}
			}
		}
	}
}