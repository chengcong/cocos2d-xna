using System;

namespace cocos2d
{
	public class CCSplitRows : CCTiledGrid3DAction
	{
		protected int m_nRows;

		protected CCSize m_winSize;

		public CCSplitRows()
		{
		}

		public static CCSplitRows actionWithRows(int nRows, float duration)
		{
			CCSplitRows cCSplitRow = new CCSplitRows();
			if (cCSplitRow.initWithRows(nRows, duration))
			{
				return cCSplitRow;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCSplitRows cCSplitRow = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCSplitRow = new CCSplitRows();
				pZone = new CCZone(cCSplitRow);
			}
			else
			{
				cCSplitRow = (CCSplitRows)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCSplitRow.initWithRows(this.m_nRows, this.m_fDuration);
			return cCSplitRow;
		}

		public bool initWithRows(int nRows, float duration)
		{
			this.m_nRows = nRows;
			return base.initWithSize(new ccGridSize(1, nRows), duration);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_winSize = CCDirector.sharedDirector().winSizeInPixels;
		}

		public override void update(float time)
		{
			for (int i = 0; i < this.m_sGridSize.y; i++)
			{
				ccQuad3 _ccQuad3 = this.originalTile(0, i);
				if (_ccQuad3 == null)
				{
					return;
				}
				float single = 1f;
				if (i % 2 == 0)
				{
					single = -1f;
				}
				ccVertex3F mWinSize = _ccQuad3.bl;
				mWinSize.x = mWinSize.x + single * this.m_winSize.width * time;
				ccVertex3F _ccVertex3F = _ccQuad3.br;
				_ccVertex3F.x = _ccVertex3F.x + single * this.m_winSize.width * time;
				ccVertex3F mWinSize1 = _ccQuad3.tl;
				mWinSize1.x = mWinSize1.x + single * this.m_winSize.width * time;
				ccVertex3F _ccVertex3F1 = _ccQuad3.tr;
				_ccVertex3F1.x = _ccVertex3F1.x + single * this.m_winSize.width * time;
				this.setTile(new ccGridSize(0, i), _ccQuad3);
			}
		}
	}
}