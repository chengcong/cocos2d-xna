using System;

namespace cocos2d
{
	public class CCBezierBy : CCActionInterval
	{
		protected ccBezierConfig m_sConfig;

		protected CCPoint m_startPosition;

		public CCBezierBy()
		{
		}

		public static CCBezierBy actionWithDuration(float t, ccBezierConfig c)
		{
			CCBezierBy cCBezierBy = new CCBezierBy();
			cCBezierBy.initWithDuration(t, c);
			return cCBezierBy;
		}

		protected float bezierat(float a, float b, float c, float d, float t)
		{
			return (float)(Math.Pow((double)(1f - t), 3) * (double)a + (double)(3f * t) * Math.Pow((double)(1f - t), 2) * (double)b + 3 * Math.Pow((double)t, 2) * (double)(1f - t) * (double)c + Math.Pow((double)t, 3) * (double)d);
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCBezierBy cCBezierBy = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCBezierBy = new CCBezierBy();
				cCZone = new CCZone(cCBezierBy);
			}
			else
			{
				cCBezierBy = cCZone.m_pCopyObject as CCBezierBy;
				if (cCBezierBy == null)
				{
					return null;
				}
			}
			base.copyWithZone(cCZone);
			cCBezierBy.initWithDuration(this.m_fDuration, this.m_sConfig);
			return cCBezierBy;
		}

		public bool initWithDuration(float t, ccBezierConfig c)
		{
			if (!base.initWithDuration(t))
			{
				return false;
			}
			this.m_sConfig = c;
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			ccBezierConfig _ccBezierConfig = new ccBezierConfig();
			_ccBezierConfig.endPosition = CCPointExtension.ccpNeg(this.m_sConfig.endPosition);
			_ccBezierConfig.controlPoint_1 = CCPointExtension.ccpAdd(this.m_sConfig.controlPoint_2, CCPointExtension.ccpNeg(this.m_sConfig.endPosition));
			_ccBezierConfig.controlPoint_2 = CCPointExtension.ccpAdd(this.m_sConfig.controlPoint_1, CCPointExtension.ccpNeg(this.m_sConfig.endPosition));
			return CCBezierBy.actionWithDuration(this.m_fDuration, _ccBezierConfig);
		}

		public override void startWithTarget(CCNode target)
		{
			base.startWithTarget(target);
			this.m_startPosition = target.position;
		}

		public override void update(float dt)
		{
			if (this.m_pTarget != null)
			{
				float single = 0f;
				float controlPoint1 = this.m_sConfig.controlPoint_1.x;
				float controlPoint2 = this.m_sConfig.controlPoint_2.x;
				float mSConfig = this.m_sConfig.endPosition.x;
				float single1 = 0f;
				float controlPoint11 = this.m_sConfig.controlPoint_1.y;
				float controlPoint21 = this.m_sConfig.controlPoint_2.y;
				float mSConfig1 = this.m_sConfig.endPosition.y;
				float single2 = this.bezierat(single, controlPoint1, controlPoint2, mSConfig, dt);
				float single3 = this.bezierat(single1, controlPoint11, controlPoint21, mSConfig1, dt);
				this.m_pTarget.position = CCPointExtension.ccpAdd(this.m_startPosition, CCPointExtension.ccp(single2, single3));
			}
		}
	}
}