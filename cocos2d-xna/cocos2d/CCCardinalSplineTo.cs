using System;

namespace cocos2d
{
	public class CCCardinalSplineTo : CCActionInterval
	{
		protected CCPointArray m_pPoints;

		protected float m_fDeltaT;

		protected float m_fTension;

		public CCCardinalSplineTo()
		{
		}

		public static CCCardinalSplineTo actionWithDuration(float duration, CCPointArray points, float tension)
		{
			return CCCardinalSplineTo.create(duration, points, tension);
		}

		public virtual new CCCardinalSplineTo copyWithZone(CCZone pZone)
		{
			CCCardinalSplineTo cCCardinalSplineTo;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCCardinalSplineTo = new CCCardinalSplineTo();
				pZone = new CCZone(cCCardinalSplineTo);
			}
			else
			{
				cCCardinalSplineTo = (CCCardinalSplineTo)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCCardinalSplineTo.initWithDuration(base.duration, this.m_pPoints, this.m_fTension);
			return cCCardinalSplineTo;
		}

		public static CCCardinalSplineTo create(float duration, CCPointArray points, float tension)
		{
			CCCardinalSplineTo cCCardinalSplineTo = new CCCardinalSplineTo();
			cCCardinalSplineTo.initWithDuration(duration, points, tension);
			return cCCardinalSplineTo;
		}

		public virtual CCPointArray getPoints()
		{
			return this.m_pPoints;
		}

		public virtual bool initWithDuration(float duration, CCPointArray points, float tension)
		{
			if (points == null || points.count() == 0)
			{
				return false;
			}
			if (!base.initWithDuration(duration))
			{
				return false;
			}
			this.setPoints(points);
			this.m_fTension = tension;
			return true;
		}

		public virtual new CCActionInterval reverse()
		{
			CCPointArray cCPointArray = this.m_pPoints.reverse();
			return CCCardinalSplineTo.create(this.m_fDuration, cCPointArray, this.m_fTension);
		}

		public virtual void setPoints(CCPointArray points)
		{
			this.m_pPoints = points;
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_fDeltaT = 1f / (float)this.m_pPoints.count();
		}

		public override void update(float time)
		{
			int num;
			float single;
			if (time != 1f)
			{
				num = (int)(time / this.m_fDeltaT);
				single = (time - this.m_fDeltaT * (float)num) / this.m_fDeltaT;
			}
			else
			{
				num = this.m_pPoints.count() - 1;
				single = 1f;
			}
			CCPoint controlPointAtIndex = this.m_pPoints.getControlPointAtIndex(num - 1);
			CCPoint cCPoint = this.m_pPoints.getControlPointAtIndex(num);
			CCPoint controlPointAtIndex1 = this.m_pPoints.getControlPointAtIndex(num + 1);
			CCPoint cCPoint1 = this.m_pPoints.getControlPointAtIndex(num + 2);
			CCPoint cCPoint2 = ccUtils.ccCardinalSplineAt(controlPointAtIndex, cCPoint, controlPointAtIndex1, cCPoint1, this.m_fTension, single);
			this.updatePosition(cCPoint2);
		}

		public virtual void updatePosition(CCPoint newPos)
		{
			this.m_pTarget.position = new CCPoint(newPos);
		}
	}
}