using System;

namespace cocos2d
{
	public class CCWaves3D : CCGrid3DAction
	{
		protected int m_nWaves;

		protected float m_fAmplitude;

		protected float m_fAmplitudeRate;

		public CCWaves3D()
		{
		}

		public static CCWaves3D actionWithWaves(int wav, float amp, ccGridSize gridSize, float duration)
		{
			CCWaves3D cCWaves3D = new CCWaves3D();
			if (cCWaves3D.initWithWaves(wav, amp, gridSize, duration))
			{
				return cCWaves3D;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCWaves3D cCWaves3D = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCWaves3D = new CCWaves3D();
				pZone = new CCZone(cCWaves3D);
			}
			else
			{
				cCWaves3D = (CCWaves3D)pZone.m_pCopyObject;
			}
			this.copyWithZone(pZone);
			cCWaves3D.initWithWaves(this.m_nWaves, this.m_fAmplitude, this.m_sGridSize, this.m_fDuration);
			return cCWaves3D;
		}

		public float getAmplitude()
		{
			return this.m_fAmplitude;
		}

		public new float getAmplitudeRate()
		{
			return this.m_fAmplitudeRate;
		}

		public bool initWithWaves(int wav, float amp, ccGridSize gridSize, float duration)
		{
			if (!this.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_nWaves = wav;
			this.m_fAmplitude = amp;
			this.m_fAmplitudeRate = 1f;
			return true;
		}

		public void setAmplitude(float fAmplitude)
		{
			this.m_fAmplitude = fAmplitude;
		}

		public new void setAmplitudeRate(float fAmplitudeRate)
		{
			this.m_fAmplitudeRate = fAmplitudeRate;
		}

		public override void update(float time)
		{
			for (int i = 0; i < this.m_sGridSize.x + 1; i++)
			{
				for (int j = 0; j < this.m_sGridSize.y + 1; j++)
				{
					ccVertex3F _ccVertex3F = base.originalVertex(new ccGridSize(i, j));
					ccVertex3F _ccVertex3F1 = _ccVertex3F;
					_ccVertex3F1.z = _ccVertex3F1.z + (float)Math.Sin((double)(3.14159274f * time * (float)this.m_nWaves * 2f + (_ccVertex3F.y + _ccVertex3F.x) * 0.01f)) * this.m_fAmplitude * this.m_fAmplitudeRate;
					base.setVertex(new ccGridSize(i, j), _ccVertex3F);
				}
			}
		}
	}
}