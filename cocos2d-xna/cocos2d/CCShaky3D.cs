using System;

namespace cocos2d
{
	public class CCShaky3D : CCGrid3DAction
	{
		private Random random = new Random();

		protected int m_nRandrange;

		protected bool m_bShakeZ;

		public CCShaky3D()
		{
		}

		public static CCShaky3D actionWithRange(int range, bool shakeZ, ccGridSize gridSize, float duration)
		{
			CCShaky3D cCShaky3D = new CCShaky3D();
			if (cCShaky3D.initWithRange(range, shakeZ, gridSize, duration))
			{
				return cCShaky3D;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCShaky3D cCShaky3D = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCShaky3D = new CCShaky3D();
				pZone = new CCZone(cCShaky3D);
			}
			else
			{
				cCShaky3D = (CCShaky3D)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCShaky3D.initWithRange(this.m_nRandrange, this.m_bShakeZ, this.m_sGridSize, this.m_fDuration);
			return cCShaky3D;
		}

		public bool initWithRange(int range, bool shakeZ, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_nRandrange = range;
			this.m_bShakeZ = shakeZ;
			return true;
		}

		public override void update(float time)
		{
			for (int i = 0; i < this.m_sGridSize.x + 1; i++)
			{
				for (int j = 0; j < this.m_sGridSize.y + 1; j++)
				{
					ccVertex3F _ccVertex3F = base.originalVertex(new ccGridSize(i, j));
					ccVertex3F _ccVertex3F1 = _ccVertex3F;
					_ccVertex3F1.x = _ccVertex3F1.x + (float)(this.random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
					ccVertex3F _ccVertex3F2 = _ccVertex3F;
					_ccVertex3F2.y = _ccVertex3F2.y + (float)(this.random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
					if (this.m_bShakeZ)
					{
						ccVertex3F _ccVertex3F3 = _ccVertex3F;
						_ccVertex3F3.z = _ccVertex3F3.z + (float)(this.random.Next() % (this.m_nRandrange * 2) - this.m_nRandrange);
					}
					base.setVertex(new ccGridSize(i, j), _ccVertex3F);
				}
			}
		}
	}
}