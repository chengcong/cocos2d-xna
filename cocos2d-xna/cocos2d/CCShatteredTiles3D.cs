using System;

namespace cocos2d
{
	public class CCShatteredTiles3D : CCTiledGrid3DAction
	{
		protected int m_nRandrange;

		protected bool m_bOnce;

		protected bool m_bShatterZ;

		public CCShatteredTiles3D()
		{
		}

		public static CCShatteredTiles3D actionWithRange(int nRange, bool bShatterZ, ccGridSize gridSize, float duration)
		{
			CCShatteredTiles3D cCShatteredTiles3D = new CCShatteredTiles3D();
			if (cCShatteredTiles3D.initWithRange(nRange, bShatterZ, gridSize, duration))
			{
				return cCShatteredTiles3D;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCShatteredTiles3D cCShatteredTiles3D = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCShatteredTiles3D = new CCShatteredTiles3D();
				pZone = new CCZone(cCShatteredTiles3D);
			}
			else
			{
				cCShatteredTiles3D = (CCShatteredTiles3D)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCShatteredTiles3D.initWithRange(this.m_nRandrange, this.m_bShatterZ, this.m_sGridSize, this.m_fDuration);
			return cCShatteredTiles3D;
		}

		public bool initWithRange(int nRange, bool bShatterZ, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_bOnce = false;
			this.m_nRandrange = nRange;
			this.m_bShatterZ = bShatterZ;
			return true;
		}

		public override void update(float time)
		{
			if (!this.m_bOnce)
			{
				for (int i = 0; i < this.m_sGridSize.x; i++)
				{
					for (int j = 0; j < this.m_sGridSize.y; j++)
					{
						ccQuad3 _ccQuad3 = this.originalTile(i, j);
						if (_ccQuad3 == null)
						{
							return;
						}
						Random random = new Random();
						ccVertex3F _ccVertex3F = _ccQuad3.bl;
						_ccVertex3F.x = _ccVertex3F.x + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
						ccVertex3F _ccVertex3F1 = _ccQuad3.br;
						_ccVertex3F1.x = _ccVertex3F1.x + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
						ccVertex3F _ccVertex3F2 = _ccQuad3.tl;
						_ccVertex3F2.x = _ccVertex3F2.x + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
						ccVertex3F _ccVertex3F3 = _ccQuad3.tr;
						_ccVertex3F3.x = _ccVertex3F3.x + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
						ccVertex3F _ccVertex3F4 = _ccQuad3.bl;
						_ccVertex3F4.y = _ccVertex3F4.y + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
						ccVertex3F _ccVertex3F5 = _ccQuad3.br;
						_ccVertex3F5.y = _ccVertex3F5.y + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
						ccVertex3F _ccVertex3F6 = _ccQuad3.tl;
						_ccVertex3F6.y = _ccVertex3F6.y + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
						ccVertex3F _ccVertex3F7 = _ccQuad3.tr;
						_ccVertex3F7.y = _ccVertex3F7.y + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
						if (this.m_bShatterZ)
						{
							ccVertex3F _ccVertex3F8 = _ccQuad3.bl;
							_ccVertex3F8.z = _ccVertex3F8.z + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
							ccVertex3F _ccVertex3F9 = _ccQuad3.br;
							_ccVertex3F9.z = _ccVertex3F9.z + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
							ccVertex3F _ccVertex3F10 = _ccQuad3.tl;
							_ccVertex3F10.z = _ccVertex3F10.z + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
							ccVertex3F _ccVertex3F11 = _ccQuad3.tr;
							_ccVertex3F11.z = _ccVertex3F11.z + (float)(random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
						}
						this.setTile(i, j, _ccQuad3);
					}
				}
				this.m_bOnce = true;
			}
		}
	}
}