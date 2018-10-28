using System;

namespace cocos2d
{
	public class CCWaves : CCGrid3DAction
	{
		protected int m_nWaves;

		protected float m_fAmplitude;

		protected float m_fAmplitudeRate;

		protected bool m_bVertical;

		protected bool m_bHorizontal;

		public CCWaves()
		{
		}

		public static CCWaves actionWithWaves(int wav, float amp, bool h, bool v, ccGridSize gridSize, float duration)
		{
			CCWaves cCWafe = new CCWaves();
			if (cCWafe.initWithWaves(wav, amp, h, v, gridSize, duration))
			{
				return cCWafe;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCWaves cCWafe = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCWafe = new CCWaves();
				pZone = new CCZone(cCWafe);
			}
			else
			{
				cCWafe = (CCWaves)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCWafe.initWithWaves(this.m_nWaves, this.m_fAmplitude, this.m_bHorizontal, this.m_bVertical, this.m_sGridSize, this.m_fDuration);
			return cCWafe;
		}

		public float getAmplitude()
		{
			return this.m_fAmplitude;
		}

		public new float getAmplitudeRate()
		{
			return this.m_fAmplitudeRate;
		}

		public bool initWithWaves(int wav, float amp, bool h, bool v, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_nWaves = wav;
			this.m_fAmplitude = amp;
			this.m_fAmplitudeRate = 1f;
			this.m_bHorizontal = h;
			this.m_bVertical = v;
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
					if (this.m_bVertical)
					{
						_ccVertex3F.x = _ccVertex3F.x + (float)Math.Sin((double)(time * 3.14159274f * (float)this.m_nWaves * 2f + _ccVertex3F.y * 0.01f)) * this.m_fAmplitude * this.m_fAmplitudeRate;
					}
					if (this.m_bHorizontal)
					{
						_ccVertex3F.y = _ccVertex3F.y + (float)Math.Sin((double)(time * 3.14159274f * (float)this.m_nWaves * 2f + _ccVertex3F.x * 0.01f)) * this.m_fAmplitude * this.m_fAmplitudeRate;
					}
					base.setVertex(new ccGridSize(i, j), _ccVertex3F);
				}
			}
		}
	}
}