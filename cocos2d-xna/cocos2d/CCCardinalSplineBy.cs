using System;

namespace cocos2d
{
	public class CCCardinalSplineBy : CCCardinalSplineTo
	{
		protected CCPoint m_startPosition;

		public CCCardinalSplineBy()
		{
		}

		public static new CCCardinalSplineBy actionWithDuration(float duration, CCPointArray points, float tension)
		{
			return CCCardinalSplineBy.create(duration, points, tension);
		}

		public static new CCCardinalSplineBy create(float duration, CCPointArray points, float tension)
		{
			CCCardinalSplineBy cCCardinalSplineBy = new CCCardinalSplineBy();
			cCCardinalSplineBy.initWithDuration(duration, points, tension);
			return cCCardinalSplineBy;
		}

		public virtual new CCActionInterval reverse()
		{
			CCPointArray cCPointArray = this.m_pPoints.copy();
			CCPoint controlPointAtIndex = cCPointArray.getControlPointAtIndex(0);
			for (int i = 1; i < cCPointArray.count(); i++)
			{
				CCPoint cCPoint = cCPointArray.getControlPointAtIndex(i);
				cCPointArray.replaceControlPoint(cCPoint.Sub(controlPointAtIndex), i);
				controlPointAtIndex = cCPoint;
			}
			CCPointArray cCPointArray1 = cCPointArray.reverse();
			controlPointAtIndex = cCPointArray1.getControlPointAtIndex(cCPointArray1.count() - 1);
			cCPointArray1.removeControlPointAtIndex(cCPointArray1.count() - 1);
			controlPointAtIndex = controlPointAtIndex.Neg();
			cCPointArray1.insertControlPoint(controlPointAtIndex, 0);
			for (int j = 1; j < cCPointArray1.count(); j++)
			{
				CCPoint controlPointAtIndex1 = cCPointArray1.getControlPointAtIndex(j);
				CCPoint cCPoint1 = controlPointAtIndex1.Neg().Add(controlPointAtIndex);
				cCPointArray1.replaceControlPoint(cCPoint1, j);
				controlPointAtIndex = cCPoint1;
			}
			return CCCardinalSplineBy.create(this.m_fDuration, cCPointArray1, this.m_fTension);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.m_startPosition = pTarget.position;
		}

		public virtual new void updatePosition(CCPoint newPos)
		{
			this.m_pTarget.position = newPos.Add(this.m_startPosition);
		}
	}
}