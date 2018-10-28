using System;

namespace cocos2d
{
	public class CCSplitCols : CCTiledGrid3DAction
	{
		protected int m_nCols;

		protected CCSize m_winSize;

		public CCSplitCols()
		{
		}

		public static CCSplitCols actionWithCols(int nCols, float duration)
		{
			CCSplitCols cCSplitCol = new CCSplitCols();
			if (cCSplitCol.initWithCols(nCols, duration))
			{
				return cCSplitCol;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCSplitCols cCSplitCol = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCSplitCol = new CCSplitCols();
				pZone = new CCZone(cCSplitCol);
			}
			else
			{
				cCSplitCol = (CCSplitCols)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCSplitCol.initWithCols(this.m_nCols, this.m_fDuration);
			return cCSplitCol;
		}

		public bool initWithCols(int nCols, float duration)
		{
			this.m_nCols = nCols;
			return base.initWithSize(new ccGridSize(nCols, 1), duration);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_winSize = CCDirector.sharedDirector().winSizeInPixels;
		}

		public override void update(float time)
		{
			for (int i = 0; i < this.m_sGridSize.x; i++)
			{
				ccQuad3 _ccQuad3 = this.originalTile(i, 0);
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
				mWinSize.y = mWinSize.y + single * this.m_winSize.height * time;
				ccVertex3F _ccVertex3F = _ccQuad3.br;
				_ccVertex3F.y = _ccVertex3F.y + single * this.m_winSize.height * time;
				ccVertex3F mWinSize1 = _ccQuad3.tl;
				mWinSize1.y = mWinSize1.y + single * this.m_winSize.height * time;
				ccVertex3F _ccVertex3F1 = _ccQuad3.tr;
				_ccVertex3F1.y = _ccVertex3F1.y + single * this.m_winSize.height * time;
				this.setTile(new ccGridSize(i, 0), _ccQuad3);
			}
		}
	}
}