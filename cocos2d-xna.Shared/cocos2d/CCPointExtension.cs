using System;

namespace cocos2d
{
	public class CCPointExtension
	{
		public CCPointExtension()
		{
		}

		public static CCPoint ccp(float x, float y)
		{
			return new CCPoint(x, y);
		}

		public static CCPoint ccpAdd(CCPoint v1, CCPoint v2)
		{
			return CCPointExtension.ccp(v1.x + v2.x, v1.y + v2.y);
		}

		public static float ccpAngle(CCPoint a, CCPoint b)
		{
			float single = (float)Math.Acos((double)CCPointExtension.ccpDot(CCPointExtension.ccpNormalize(a), CCPointExtension.ccpNormalize(b)));
			if (Math.Abs(single) < ccMacros.FLT_EPSILON)
			{
				return 0f;
			}
			return single;
		}

		public static float ccpAngleSigned(CCPoint a, CCPoint b)
		{
			CCPoint cCPoint = CCPointExtension.ccpNormalize(a);
			CCPoint cCPoint1 = CCPointExtension.ccpNormalize(b);
			float single = (float)Math.Atan2((double)(cCPoint.x * cCPoint1.y - cCPoint.y * cCPoint1.x), (double)CCPointExtension.ccpDot(cCPoint, cCPoint1));
			if (Math.Abs(single) < ccMacros.FLT_EPSILON)
			{
				return 0f;
			}
			return single;
		}

		public static CCPoint ccpClamp(CCPoint p, CCPoint from, CCPoint to)
		{
			return CCPointExtension.ccp(CCPointExtension.clampf(p.x, from.x, to.x), CCPointExtension.clampf(p.y, from.y, to.y));
		}

		public static CCPoint ccpCompMult(CCPoint a, CCPoint b)
		{
			return CCPointExtension.ccp(a.x * b.x, a.y * b.y);
		}

		public static CCPoint ccpCompOp(CCPoint p, CCPointExtension.ccpCompOpDelegate del)
		{
			return CCPointExtension.ccp(del(p.x), del(p.y));
		}

		public static float ccpCross(CCPoint v1, CCPoint v2)
		{
			return v1.x * v2.y - v1.y * v2.x;
		}

		public static float ccpDistance(CCPoint v1, CCPoint v2)
		{
			return CCPointExtension.ccpLength(CCPointExtension.ccpSub(v1, v2));
		}

		public static float ccpDot(CCPoint v1, CCPoint v2)
		{
			return v1.x * v2.x + v1.y * v2.y;
		}

		public static CCPoint ccpForAngle(float a)
		{
			return CCPointExtension.ccp((float)Math.Cos((double)a), (float)Math.Sin((double)a));
		}

		public static CCPoint ccpFromSize(CCSize s)
		{
			return CCPointExtension.ccp(s.width, s.height);
		}

		public static bool ccpFuzzyEqual(CCPoint a, CCPoint b, float variance)
		{
			if (a.x - variance <= b.x && b.x <= a.x + variance && a.y - variance <= b.y && b.y <= a.y + variance)
			{
				return true;
			}
			return false;
		}

		public static CCPoint ccpIntersectPoint(CCPoint A, CCPoint B, CCPoint C, CCPoint D)
		{
			float single = 0f;
			float single1 = 0f;
			if (!CCPointExtension.ccpLineIntersect(A, B, C, D, ref single, ref single1))
			{
				return new CCPoint();
			}
			CCPoint cCPoint = new CCPoint()
			{
				x = A.x + single * (B.x - A.x),
				y = A.y + single * (B.y - A.y)
			};
			return cCPoint;
		}

		public static float ccpLength(CCPoint v)
		{
			return (float)Math.Sqrt((double)CCPointExtension.ccpLengthSQ(v));
		}

		public static float ccpLengthSQ(CCPoint v)
		{
			return CCPointExtension.ccpDot(v, v);
		}

		public static CCPoint ccpLerp(CCPoint a, CCPoint b, float alpha)
		{
			return CCPointExtension.ccpAdd(CCPointExtension.ccpMult(a, 1f - alpha), CCPointExtension.ccpMult(b, alpha));
		}

		public static bool ccpLineIntersect(CCPoint A, CCPoint B, CCPoint C, CCPoint D, ref float S, ref float T)
		{
			if (A.x == B.x && A.y == B.y || C.x == D.x && C.y == D.y)
			{
				return false;
			}
			float b = B.x - A.x;
			float single = B.y - A.y;
			float d = D.x - C.x;
			float d1 = D.y - C.y;
			float a = A.x - C.x;
			float a1 = A.y - C.y;
			float single1 = d1 * b - d * single;
			S = d * a1 - d1 * a;
			T = b * a1 - single * a;
			if (single1 == 0f)
			{
				if (S != 0f && T != 0f)
				{
					return false;
				}
				return true;
			}
			S = S / single1;
			T = T / single1;
			return true;
		}

		public static CCPoint ccpMidpoint(CCPoint v1, CCPoint v2)
		{
			return CCPointExtension.ccpMult(CCPointExtension.ccpAdd(v1, v2), 0.5f);
		}

		public static CCPoint ccpMult(CCPoint v, float s)
		{
			return CCPointExtension.ccp(v.x * s, v.y * s);
		}

		public static CCPoint ccpNeg(CCPoint v)
		{
			return CCPointExtension.ccp(-v.x, -v.y);
		}

		public static CCPoint ccpNormalize(CCPoint v)
		{
			return CCPointExtension.ccpMult(v, 1f / CCPointExtension.ccpLength(v));
		}

		public static CCPoint ccpPerp(CCPoint v)
		{
			return CCPointExtension.ccp(-v.y, v.x);
		}

		public static CCPoint ccpProject(CCPoint v1, CCPoint v2)
		{
			return CCPointExtension.ccpMult(v2, CCPointExtension.ccpDot(v1, v2) / CCPointExtension.ccpDot(v2, v2));
		}

		public static CCPoint ccpRotate(CCPoint v1, CCPoint v2)
		{
			return CCPointExtension.ccp(v1.x * v2.x - v1.y * v2.y, v1.x * v2.y + v1.y * v2.x);
		}

		public static CCPoint ccpRotateByAngle(CCPoint v, CCPoint pivot, float angle)
		{
			CCPoint cCPoint = CCPointExtension.ccpSub(v, pivot);
			float single = (float)Math.Cos((double)angle);
			float single1 = (float)Math.Sin((double)angle);
			float single2 = cCPoint.x;
			cCPoint = CCPointExtension.ccp(single2 * single - cCPoint.y * single1 + pivot.x, single2 * single1 + cCPoint.y * single + pivot.y);
			return cCPoint;
		}

		public static CCPoint ccpRPerp(CCPoint v)
		{
			return CCPointExtension.ccp(v.y, -v.x);
		}

		public static bool ccpSegmentIntersect(CCPoint A, CCPoint B, CCPoint C, CCPoint D)
		{
			float single = 0f;
			float single1 = 0f;
			if (CCPointExtension.ccpLineIntersect(A, B, C, D, ref single, ref single1) && single >= 0f && single <= 1f && single1 >= 0f && single1 <= 1f)
			{
				return true;
			}
			return false;
		}

		public static CCPoint ccpSub(CCPoint v1, CCPoint v2)
		{
			return CCPointExtension.ccp(v1.x - v2.x, v1.y - v2.y);
		}

		public static float ccpToAngle(CCPoint v)
		{
			return (float)Math.Atan2((double)v.y, (double)v.x);
		}

		public static CCPoint ccpUnrotate(CCPoint v1, CCPoint v2)
		{
			return CCPointExtension.ccp(v1.x * v2.x + v1.y * v2.y, v1.y * v2.x - v1.x * v2.y);
		}

		public static float clampf(float value, float min_inclusive, float max_inclusive)
		{
			if (min_inclusive > max_inclusive)
			{
				min_inclusive = max_inclusive;
				max_inclusive = min_inclusive;
			}
			if (value < min_inclusive)
			{
				return min_inclusive;
			}
			if (value >= max_inclusive)
			{
				return max_inclusive;
			}
			return value;
		}

		public delegate float ccpCompOpDelegate(float a);
	}
}