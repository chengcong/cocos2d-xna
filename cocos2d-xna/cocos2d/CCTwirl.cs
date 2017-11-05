using System;

namespace cocos2d
{
	public class CCTwirl : CCGrid3DAction
	{
		protected CCPoint m_position;

		protected int m_nTwirls;

		protected float m_fAmplitude;

		protected float m_fAmplitudeRate;

		protected CCPoint m_positionInPixels;

		public CCTwirl()
		{
		}

		public static CCTwirl actionWithPosition(CCPoint pos, int t, float amp, ccGridSize gridSize, float duration)
		{
			CCTwirl cCTwirl = new CCTwirl();
			if (cCTwirl.initWithPosition(pos, t, amp, gridSize, duration))
			{
				return cCTwirl;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCTwirl cCTwirl = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCTwirl = new CCTwirl();
				pZone = new CCZone(cCTwirl);
			}
			else
			{
				cCTwirl = (CCTwirl)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCTwirl.initWithPosition(this.m_position, this.m_nTwirls, this.m_fAmplitude, this.m_sGridSize, this.m_fDuration);
			return cCTwirl;
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

		public bool initWithPosition(CCPoint pos, int t, float amp, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_positionInPixels = new CCPoint();
			this.setPosition(pos);
			this.m_nTwirls = t;
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
			CCPoint mPositionInPixels = this.m_positionInPixels;
			for (int i = 0; i < this.m_sGridSize.x + 1; i++)
			{
				for (int j = 0; j < this.m_sGridSize.y + 1; j++)
				{
					ccVertex3F _ccVertex3F = base.originalVertex(new ccGridSize(i, j));
					CCPoint cCPoint = new CCPoint((float)i - (float)this.m_sGridSize.x / 2f, (float)j - (float)this.m_sGridSize.y / 2f);
					float single = (float)Math.Sqrt((double)(cCPoint.x * cCPoint.x + cCPoint.y * cCPoint.y));
					float mFAmplitude = 0.1f * this.m_fAmplitude * this.m_fAmplitudeRate;
					float single1 = single * (float)Math.Cos((double)(1.57079637f + time * 3.14159274f * (float)this.m_nTwirls * 2f)) * mFAmplitude;
					CCPoint cCPoint1 = new CCPoint()
					{
						x = (float)Math.Sin((double)single1) * (_ccVertex3F.y - mPositionInPixels.y) + (float)Math.Cos((double)single1) * (_ccVertex3F.x - mPositionInPixels.x),
						y = (float)Math.Cos((double)single1) * (_ccVertex3F.y - mPositionInPixels.y) - (float)Math.Sin((double)single1) * (_ccVertex3F.x - mPositionInPixels.x)
					};
					_ccVertex3F.x = mPositionInPixels.x + cCPoint1.x;
					_ccVertex3F.y = mPositionInPixels.y + cCPoint1.y;
					base.setVertex(new ccGridSize(i, j), _ccVertex3F);
				}
			}
		}
	}
}