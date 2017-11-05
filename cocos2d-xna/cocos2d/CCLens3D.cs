using System;

namespace cocos2d
{
	public class CCLens3D : CCGrid3DAction
	{
		protected CCPoint m_position;

		protected float m_fRadius;

		protected float m_fLensEffect;

		protected CCPoint m_positionInPixels;

		protected bool m_bDirty;

		public CCLens3D()
		{
		}

		public static CCLens3D actionWithPosition(CCPoint pos, float r, ccGridSize gridSize, float duration)
		{
			CCLens3D cCLens3D = new CCLens3D();
			if (cCLens3D.initWithPosition(pos, r, gridSize, duration))
			{
				return cCLens3D;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCLens3D cCLens3D = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCLens3D = new CCLens3D();
				pZone = new CCZone(cCLens3D);
			}
			else
			{
				cCLens3D = (CCLens3D)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCLens3D.initWithPosition(this.m_position, this.m_fRadius, this.m_sGridSize, this.m_fDuration);
			return cCLens3D;
		}

		public float getLensEffect()
		{
			return this.m_fLensEffect;
		}

		public CCPoint getPosition()
		{
			return this.m_position;
		}

		public bool initWithPosition(CCPoint pos, float r, ccGridSize gridSize, float duration)
		{
			if (!base.initWithSize(gridSize, duration))
			{
				return false;
			}
			this.m_position = new CCPoint(-1f, -1f);
			this.m_positionInPixels = new CCPoint();
			this.setPosition(pos);
			this.m_fRadius = r;
			this.m_fLensEffect = 0.7f;
			this.m_bDirty = true;
			return true;
		}

		public void setLensEffect(float fLensEffect)
		{
			this.m_fLensEffect = fLensEffect;
		}

		public void setPosition(CCPoint pos)
		{
			if (!CCPoint.CCPointEqualToPoint(pos, this.m_position))
			{
				this.m_position = pos;
				this.m_positionInPixels.x = pos.x * CCDirector.sharedDirector().ContentScaleFactor;
				this.m_positionInPixels.y = pos.y * CCDirector.sharedDirector().ContentScaleFactor;
				this.m_bDirty = true;
			}
		}

		public override void update(float time)
		{
			if (this.m_bDirty)
			{
				for (int i = 0; i < this.m_sGridSize.x + 1; i++)
				{
					for (int j = 0; j < this.m_sGridSize.y + 1; j++)
					{
						ccVertex3F _ccVertex3F = base.originalVertex(new ccGridSize(i, j));
						CCPoint cCPoint = new CCPoint(this.m_positionInPixels.x - (new CCPoint(_ccVertex3F.x, _ccVertex3F.y)).x, this.m_positionInPixels.y - (new CCPoint(_ccVertex3F.x, _ccVertex3F.y)).y);
						float mFRadius = CCPointExtension.ccpLength(cCPoint);
						if (mFRadius < this.m_fRadius)
						{
							mFRadius = this.m_fRadius - mFRadius;
							float single = mFRadius / this.m_fRadius;
							if (single == 0f)
							{
								single = 0.001f;
							}
							float single1 = (float)Math.Log((double)single) * this.m_fLensEffect;
							float single2 = (float)Math.Exp((double)single1) * this.m_fRadius;
							if (Math.Sqrt((double)(cCPoint.x * cCPoint.x + cCPoint.y * cCPoint.y)) > 0)
							{
								cCPoint = CCPointExtension.ccpNormalize(cCPoint);
								CCPoint cCPoint1 = CCPointExtension.ccpMult(cCPoint, single2);
								ccVertex3F _ccVertex3F1 = _ccVertex3F;
								_ccVertex3F1.z = _ccVertex3F1.z + CCPointExtension.ccpLength(cCPoint1) * this.m_fLensEffect;
							}
						}
						base.setVertex(new ccGridSize(i, j), _ccVertex3F);
					}
				}
				this.m_bDirty = false;
			}
		}
	}
}