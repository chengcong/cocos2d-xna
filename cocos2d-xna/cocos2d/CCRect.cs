using System;

namespace cocos2d
{
	public class CCRect
	{
		public CCPoint origin;

		public CCSize size;

		public CCRect() : this(0f, 0f, 0f, 0f)
		{
		}

		public CCRect(CCRect copy)
		{
			this.origin = new CCPoint(copy.origin.x, copy.origin.y);
			this.size = new CCSize(copy.size.width, copy.size.height);
		}

		public CCRect(float x, float y, float width, float height)
		{
			this.origin = new CCPoint();
			this.size = new CCSize();
			this.origin.x = x;
			this.origin.y = y;
			this.size.width = width;
			this.size.height = height;
		}

		public static bool CCRectContainsPoint(CCRect rect, CCPoint point)
		{
			bool flag = false;
			if (rect != null)
			{
				if (float.IsNaN(point.x))
				{
					point.x = 0f;
				}
				if (float.IsNaN(point.y))
				{
					point.y = 0f;
				}
				CCRect.CCRectGetMinX(rect);
				CCRect.CCRectGetMaxX(rect);
				CCRect.CCRectGetMinY(rect);
				CCRect.CCRectGetMaxY(rect);
				if (point.x >= CCRect.CCRectGetMinX(rect) && point.x <= CCRect.CCRectGetMaxX(rect) && point.y >= CCRect.CCRectGetMinY(rect) && point.y <= CCRect.CCRectGetMaxY(rect))
				{
					flag = true;
				}
			}
			return flag;
		}

		public static bool CCRectEqualToRect(CCRect rect1, CCRect rect2)
		{
			if (rect1 == null || rect2 == null)
			{
				return false;
			}
			if (!CCPoint.CCPointEqualToPoint(rect1.origin, rect2.origin))
			{
				return false;
			}
			return CCSize.CCSizeEqualToSize(rect1.size, rect2.size);
		}

		public static float CCRectGetMaxX(CCRect rect)
		{
			return rect.origin.x + rect.size.width;
		}

		public static float CCRectGetMaxY(CCRect rect)
		{
			return rect.origin.y + rect.size.height;
		}

		public static float CCRectGetMidX(CCRect rect)
		{
			return rect.origin.x + rect.size.width / 2f;
		}

		public static float CCRectGetMidY(CCRect rect)
		{
			return rect.origin.y + rect.size.height / 2f;
		}

		public static float CCRectGetMinX(CCRect rect)
		{
			return rect.origin.x;
		}

		public static float CCRectGetMinY(CCRect rect)
		{
			return rect.origin.y;
		}

		public static bool CCRectIntersetsRect(CCRect rectA, CCRect rectB)
		{
			bool flag = false;
			if (rectA != null && rectB != null)
			{
				flag = (CCRect.CCRectGetMaxX(rectA) < CCRect.CCRectGetMinX(rectB) || CCRect.CCRectGetMaxX(rectB) < CCRect.CCRectGetMinX(rectA) || CCRect.CCRectGetMaxY(rectA) < CCRect.CCRectGetMinY(rectB) ? false : CCRect.CCRectGetMaxY(rectB) >= CCRect.CCRectGetMinY(rectA));
			}
			return flag;
		}

		public static CCRect CCRectUnion(CCRect rectA, CCRect rectB)
		{
			float single = rectA.origin.x;
			float single1 = rectB.origin.x;
			float single2 = single + rectA.size.width;
			float single3 = single1 + rectB.size.width;
			if (single1 < single)
			{
				single = single1;
			}
			if (single3 > single2)
			{
				single2 = single3;
			}
			float single4 = rectA.origin.y;
			float single5 = rectB.origin.y;
			float single6 = single4 + rectA.size.height;
			float single7 = single5 + rectB.size.height;
			if (single5 < single4)
			{
				single4 = single5;
			}
			if (single7 > single6)
			{
				single6 = single7;
			}
			CCRect cCRect = new CCRect(single, single4, single2 - single, single6 - single4);
			return cCRect;
		}
	}
}