using System;

namespace cocos2d
{
	public class CCRipple3D : CCGrid3DAction
	{
		protected CCPoint m_position;

		protected float m_fRadius;

		protected int m_nWaves;

		protected float m_fAmplitude;

		protected float m_fAmplitudeRate;

		protected CCPoint m_positionInPixels;

		public CCRipple3D()
		{
		}

		public static CCRipple3D actionWithPosition(CCPoint pos, float r, int wav, float amp, ccGridSize gridSize, float duration)
		{
			CCRipple3D cCRipple3D = new CCRipple3D();
			if (cCRipple3D.initWithPosition(pos, r, wav, amp, gridSize, duration))
			{
				return cCRipple3D;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCRipple3D cCRipple3D = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCRipple3D = new CCRipple3D();
				pZone = new CCZone(cCRipple3D);
			}
			else
			{
				cCRipple3D = (CCRipple3D)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCRipple3D.initWithPosition(this.m_position, this.m_fRadius, this.m_nWaves, this.m_fAmplitude, this.m_sGridSize, this.m_fDuration);
			return cCRipple3D;
		}

		public float getAmplitude()
		{
			return this.m_fAmplitude;
		}

		public new float getAmplitudeRate()
		{
			return this.m_fAmplitudeRate;
		}

		public CCPoint getPosition()
		{
			return this.m_position;
		}

		public bool initWithPosition(CCPoint pos, float r, int wav, float amp, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_positionInPixels = new CCPoint();
			this.setPosition(pos);
			this.m_fRadius = r;
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

		public void setPosition(CCPoint position)
		{
			this.m_position = position;
			this.m_positionInPixels.x = position.x * CCDirector.sharedDirector().ContentScaleFactor;
			this.m_positionInPixels.y = position.y * CCDirector.sharedDirector().ContentScaleFactor;
		}

		public override void update(float time)
		{
			for (int i = 0; i < this.m_sGridSize.x + 1; i++)
			{
				for (int j = 0; j < this.m_sGridSize.y + 1; j++)
				{
					ccVertex3F _ccVertex3F = base.originalVertex(new ccGridSize(i, j));
					CCPoint cCPoint = CCPointExtension.ccpSub(this.m_positionInPixels, new CCPoint(_ccVertex3F.x, _ccVertex3F.y));
					float mFRadius = (float)Math.Sqrt((double)(cCPoint.x * cCPoint.x + cCPoint.y * cCPoint.y));
					if (mFRadius < this.m_fRadius)
					{
						mFRadius = this.m_fRadius - mFRadius;
						float single = (float)Math.Pow((double)(mFRadius / this.m_fRadius), 2);
						ccVertex3F _ccVertex3F1 = _ccVertex3F;
						_ccVertex3F1.z = _ccVertex3F1.z + (float)Math.Sin((double)(time * 3.14159274f * (float)this.m_nWaves * 2f + mFRadius * 0.1f)) * this.m_fAmplitude * this.m_fAmplitudeRate * single;
					}
					base.setVertex(new ccGridSize(i, j), _ccVertex3F);
				}
			}
		}
	}
}