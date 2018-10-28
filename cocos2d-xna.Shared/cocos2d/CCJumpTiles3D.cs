using System;

namespace cocos2d
{
	public class CCJumpTiles3D : CCTiledGrid3DAction
	{
		protected int m_nJumps;

		protected float m_fAmplitude;

		protected float m_fAmplitudeRate;

		public float Amplitude
		{
			get
			{
				return this.m_fAmplitude;
			}
			set
			{
				this.m_fAmplitude = value;
			}
		}

		public float AmplitudeRate
		{
			get
			{
				return this.m_fAmplitudeRate;
			}
			set
			{
				this.m_fAmplitudeRate = value;
			}
		}

		public CCJumpTiles3D()
		{
		}

		public static CCJumpTiles3D actionWithJumps(int j, float amp, ccGridSize gridSize, float duration)
		{
			CCJumpTiles3D cCJumpTiles3D = new CCJumpTiles3D();
			if (cCJumpTiles3D.initWithJumps(j, amp, gridSize, duration))
			{
				return cCJumpTiles3D;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCJumpTiles3D cCJumpTiles3D = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCJumpTiles3D = new CCJumpTiles3D();
				pZone = new CCZone(cCJumpTiles3D);
			}
			else
			{
				cCJumpTiles3D = (CCJumpTiles3D)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCJumpTiles3D.initWithJumps(this.m_nJumps, this.m_fAmplitude, this.m_sGridSize, this.m_fDuration);
			return cCJumpTiles3D;
		}

		public bool initWithJumps(int j, float amp, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_nJumps = j;
			this.m_fAmplitude = amp;
			this.m_fAmplitudeRate = 1f;
			return true;
		}

		public override void update(float time)
		{
			float single = (float)Math.Sin((double)(3.14159274f * time * (float)this.m_nJumps * 2f)) * this.m_fAmplitude * this.m_fAmplitudeRate;
			float single1 = (float)(Math.Sin((double)(3.14159274f * (time * (float)this.m_nJumps * 2f + 1f))) * (double)this.m_fAmplitude * (double)this.m_fAmplitudeRate);
			ccGridSize _ccGridSize = new ccGridSize();
			for (int i = 0; i < this.m_sGridSize.x; i++)
			{
				for (int j = 0; j < this.m_sGridSize.y; j++)
				{
					_ccGridSize.@set(i, j);
					ccQuad3 _ccQuad3 = this.originalTile(i, j);
					if (_ccQuad3 == null)
					{
						return;
					}
					if ((i + j) % 2 != 0)
					{
						ccVertex3F _ccVertex3F = _ccQuad3.bl;
						_ccVertex3F.z = _ccVertex3F.z + single1;
						ccVertex3F _ccVertex3F1 = _ccQuad3.br;
						_ccVertex3F1.z = _ccVertex3F1.z + single1;
						ccVertex3F _ccVertex3F2 = _ccQuad3.tl;
						_ccVertex3F2.z = _ccVertex3F2.z + single1;
						ccVertex3F _ccVertex3F3 = _ccQuad3.tr;
						_ccVertex3F3.z = _ccVertex3F3.z + single1;
					}
					else
					{
						ccVertex3F _ccVertex3F4 = _ccQuad3.bl;
						_ccVertex3F4.z = _ccVertex3F4.z + single;
						ccVertex3F _ccVertex3F5 = _ccQuad3.br;
						_ccVertex3F5.z = _ccVertex3F5.z + single;
						ccVertex3F _ccVertex3F6 = _ccQuad3.tl;
						_ccVertex3F6.z = _ccVertex3F6.z + single;
						ccVertex3F _ccVertex3F7 = _ccQuad3.tr;
						_ccVertex3F7.z = _ccVertex3F7.z + single;
					}
					this.setTile(i, j, _ccQuad3);
				}
			}
		}
	}
}