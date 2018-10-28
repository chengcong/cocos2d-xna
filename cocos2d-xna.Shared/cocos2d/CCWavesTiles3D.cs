using System;

namespace cocos2d
{
	public class CCWavesTiles3D : CCTiledGrid3DAction
	{
		protected int m_nWaves;

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

		public CCWavesTiles3D()
		{
		}

		public static CCWavesTiles3D actionWithWaves(int wav, float amp, ccGridSize gridSize, float duration)
		{
			CCWavesTiles3D cCWavesTiles3D = new CCWavesTiles3D();
			if (cCWavesTiles3D.initWithWaves(wav, amp, gridSize, duration))
			{
				return cCWavesTiles3D;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCWavesTiles3D cCWavesTiles3D = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCWavesTiles3D = new CCWavesTiles3D();
				pZone = new CCZone(cCWavesTiles3D);
			}
			else
			{
				cCWavesTiles3D = (CCWavesTiles3D)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCWavesTiles3D.initWithWaves(this.m_nWaves, this.m_fAmplitude, this.m_sGridSize, this.m_fDuration);
			return cCWavesTiles3D;
		}

		public bool initWithWaves(int wav, float amp, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_nWaves = wav;
			this.m_fAmplitude = amp;
			this.m_fAmplitudeRate = 1f;
			return true;
		}

		public override void update(float time)
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
					_ccQuad3.bl.z = (float)Math.Sin((double)(time * 3.14159274f * (float)this.m_nWaves * 2f + (_ccQuad3.bl.y + _ccQuad3.bl.x) * 0.01f)) * this.m_fAmplitude * this.m_fAmplitudeRate;
					_ccQuad3.br.z = _ccQuad3.bl.z;
					_ccQuad3.tl.z = _ccQuad3.bl.z;
					_ccQuad3.tr.z = _ccQuad3.bl.z;
					this.setTile(i, j, _ccQuad3);
				}
			}
		}
	}
}