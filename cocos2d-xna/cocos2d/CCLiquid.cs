using System;

namespace cocos2d
{
	public class CCLiquid : CCGrid3DAction
	{
		protected int m_nWaves;

		protected float m_fAmplitude;

		protected float m_fAmplitudeRate;

		public CCLiquid()
		{
		}

		public static CCLiquid actionWithWaves(int wav, float amp, ccGridSize gridSize, float duration)
		{
			CCLiquid cCLiquid = new CCLiquid();
			if (cCLiquid != null)
			{
				cCLiquid.initWithWaves(wav, amp, gridSize, duration);
			}
			return cCLiquid;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCLiquid cCLiquid = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCLiquid = new CCLiquid();
				pZone = new CCZone(cCLiquid);
			}
			else
			{
				cCLiquid = (CCLiquid)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCLiquid.initWithWaves(this.m_nWaves, this.m_fAmplitude, this.m_sGridSize, this.m_fDuration);
			return cCLiquid;
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
			float single = time * 3.14159274f * (float)this.m_nWaves * 2f;
			float mFAmplitude = this.m_fAmplitude * this.m_fAmplitudeRate;
			for (int i = 1; i < this.m_sGridSize.x; i++)
			{
				for (int j = 1; j < this.m_sGridSize.y; j++)
				{
					ccVertex3F _ccVertex3F = base.originalVertex(i, j);
					_ccVertex3F.x = _ccVertex3F.x + (float)Math.Sin((double)(single + _ccVertex3F.x * 0.01f)) * mFAmplitude;
					_ccVertex3F.y = _ccVertex3F.y + (float)Math.Sin((double)(single + _ccVertex3F.y * 0.01f)) * mFAmplitude;
					base.setVertex(i, j, _ccVertex3F);
				}
			}
		}
	}
}