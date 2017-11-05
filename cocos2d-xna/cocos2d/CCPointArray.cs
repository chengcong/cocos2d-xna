using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCPointArray
	{
		private List<CCPoint> m_pControlPoints;

		public CCPointArray()
		{
		}

		public CCPointArray(List<CCPoint> copy)
		{
			this.m_pControlPoints = copy;
		}

		public void addControlPoint(CCPoint controlPoint)
		{
			this.m_pControlPoints.Add(controlPoint);
		}

		public static CCPointArray arrayWithCapacity(int capacity)
		{
			return new CCPointArray(new List<CCPoint>(capacity));
		}

		public CCPointArray copy()
		{
			return new CCPointArray(this.m_pControlPoints);
		}

		public int count()
		{
			return this.m_pControlPoints.Count;
		}

		public static CCPointArray create(int capacity)
		{
			return new CCPointArray(new List<CCPoint>(capacity));
		}

		public static CCPointArray create(List<CCPoint> pts)
		{
			return new CCPointArray(pts);
		}

		public CCPoint getControlPointAtIndex(int index)
		{
			return this.m_pControlPoints[index];
		}

		public List<CCPoint> getControlPoints()
		{
			return this.m_pControlPoints;
		}

		public bool initWithCapacity(int capacity)
		{
			this.m_pControlPoints = new List<CCPoint>(capacity);
			return true;
		}

		public void insertControlPoint(CCPoint controlPoint, int index)
		{
			this.m_pControlPoints[index] = controlPoint;
		}

		public void removeControlPointAtIndex(int index)
		{
			this.m_pControlPoints.RemoveAt(index);
		}

		public void replaceControlPoint(CCPoint controlPoint, int index)
		{
			CCPoint cCPoint = new CCPoint(controlPoint);
			this.m_pControlPoints[index] = cCPoint;
		}

		public CCPointArray reverse()
		{
			List<CCPoint> cCPoints = new List<CCPoint>(this.m_pControlPoints);
			cCPoints.Reverse();
			return new CCPointArray(cCPoints);
		}

		public void reverseInline()
		{
			int count = this.m_pControlPoints.Count;
			for (int i = 0; i < count / 2; i++)
			{
				CCPoint item = this.m_pControlPoints[i];
				this.m_pControlPoints[i] = this.m_pControlPoints[count - i - 1];
				this.m_pControlPoints[count - i - 1] = item;
			}
		}

		public void setControlPoints(List<CCPoint> controlPoints)
		{
			this.m_pControlPoints = controlPoints;
		}
	}
}