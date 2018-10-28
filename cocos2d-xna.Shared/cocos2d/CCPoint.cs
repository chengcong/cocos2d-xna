using System;

namespace cocos2d
{
	public class CCPoint
	{
		private float _x;

		private float _y;

		public readonly static CCPoint CCPointZero;

		public bool IsZero
		{
			get
			{
				if (this._x != 0f)
				{
					return false;
				}
				return this._y == 0f;
			}
		}

		public float x
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
			}
		}

		public float y
		{
			get
			{
				return this._y;
			}
			set
			{
				this._y = value;
			}
		}

		public static CCPoint Zero
		{
			get
			{
				return new CCPoint(0f, 0f);
			}
		}

		static CCPoint()
		{
			CCPoint.CCPointZero = new CCPoint(0f, 0f);
		}

		public CCPoint()
		{
			this._x = 0f;
			this._y = 0f;
		}

		public CCPoint(CCPoint copy)
		{
			this._x = copy.x;
			this._y = copy.y;
		}

		public CCPoint(float x, float y)
		{
			this._x = x;
			this._y = y;
		}

		public CCPoint Add(CCPoint pt)
		{
			CCPoint cCPoint = new CCPoint(this.x + pt.x, this.y + pt.y);
			return cCPoint;
		}

		public float Angle(CCPoint b)
		{
			float single = (float)Math.Acos((double)CCPoint.Normalize(this).Dot(CCPoint.Normalize(b)));
			if (Math.Abs(single) < ccMacros.FLT_EPSILON)
			{
				return 0f;
			}
			return single;
		}

		public static float AngleSigned(CCPoint a, CCPoint b)
		{
			CCPoint cCPoint = CCPoint.Normalize(a);
			CCPoint cCPoint1 = CCPoint.Normalize(b);
			float single = (float)Math.Atan2((double)(cCPoint.x * cCPoint1.y - cCPoint.y * cCPoint1.x), (double)cCPoint.Dot(cCPoint1));
			if (Math.Abs(single) < ccMacros.FLT_EPSILON)
			{
				return 0f;
			}
			return single;
		}

		public CCPoint ccp(float x, float y)
		{
			return new CCPoint(x, y);
		}

		public static bool CCPointEqualToPoint(CCPoint point1, CCPoint point2)
		{
			if (point1.x != point2.x)
			{
				return false;
			}
			return point1.y == point2.y;
		}

		public CCPoint Clamp(CCPoint p, CCPoint min_inclusive, CCPoint max_inclusive)
		{
			return new CCPoint(CCPoint.clampf(p.x, min_inclusive.x, max_inclusive.x), CCPoint.clampf(p.y, min_inclusive.y, max_inclusive.y));
		}

		public static float clampf(float value, float min_inclusive, float max_inclusive)
		{
			if (min_inclusive > max_inclusive)
			{
				ccMacros.CC_SWAP<float>(ref min_inclusive, ref max_inclusive);
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

		public static CCPoint CompMult(CCPoint a, CCPoint b)
		{
			return new CCPoint(a.x * b.x, a.y * b.y);
		}

		public float Cross(CCPoint v2)
		{
			return this.x * v2.y - this.y * v2.x;
		}

		public static float Distance(CCPoint v1, CCPoint v2)
		{
			return CCPoint.Length(v1.Sub(v2));
		}

		[Obsolete("Use DistanceSQ instead. This method was a typograhpic error.")]
		public float DistanceCQ(CCPoint v2)
		{
			return this.DistanceSQ(v2);
		}

		public float DistanceSQ(CCPoint v2)
		{
			return this.Sub(v2).LengthSQ();
		}

		public float Dot(CCPoint v2)
		{
			return this.x * v2.x + this.y * v2.y;
		}

		public static CCPoint ForAngle(float a)
		{
			return new CCPoint((float)Math.Cos((double)a), (float)Math.Sin((double)a));
		}

		public static CCPoint FromSize(CCSize s)
		{
			return new CCPoint(s.width, s.height);
		}

		public static bool FuzzyEqual(CCPoint a, CCPoint b, float var)
		{
			if (a.x - var <= b.x && b.x <= a.x + var && a.y - var <= b.y && b.y <= a.y + var)
			{
				return true;
			}
			return false;
		}

		public static CCPoint IntersectPoint(CCPoint A, CCPoint B, CCPoint C, CCPoint D)
		{
			float single;
			float single1;
			if (!CCPoint.LineIntersect(A, B, C, D, out single, out single1))
			{
				return CCPoint.CCPointZero;
			}
			CCPoint cCPoint = new CCPoint()
			{
				x = A.x + single * (B.x - A.x),
				y = A.y + single * (B.y - A.y)
			};
			return cCPoint;
		}

		public static float Length(CCPoint v)
		{
			return (float)Math.Sqrt((double)v.LengthSQ());
		}

		public float LengthSQ()
		{
			return this.Dot(this);
		}

		public static CCPoint Lerp(CCPoint a, CCPoint b, float alpha)
		{
			return a.Mult(1f - alpha).Add(b.Mult(alpha));
		}

		public static bool LineIntersect(CCPoint A, CCPoint B, CCPoint C, CCPoint D, out float S, out float T)
		{
			S = 0f;
			T = 0f;
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

		public CCPoint Midpoint(CCPoint v2)
		{
			return this.Add(v2).Mult(0.5f);
		}

		public CCPoint Mult(float s)
		{
			return this.ccp(this.x * s, this.y * s);
		}

		public CCPoint Neg()
		{
			return this.ccp(-this.x, -this.y);
		}

		public static CCPoint Normalize(CCPoint v)
		{
			return v.Mult(1f / CCPoint.Length(v));
		}

		public CCPoint Perp()
		{
			return this.ccp(-this.y, this.x);
		}

		public CCPoint Project(CCPoint v2)
		{
			return v2.Mult(this.Dot(v2) / v2.Dot(v2));
		}

		public CCPoint Rotate(CCPoint v2)
		{
			return this.ccp(this.x * v2.x - this.y * v2.y, this.x * v2.y + this.y * v2.x);
		}

		public static CCPoint RotateByAngle(CCPoint v, CCPoint pivot, float angle)
		{
			CCPoint cCPoint = v.Sub(pivot);
			float single = (float)Math.Cos((double)angle);
			float single1 = (float)Math.Sin((double)angle);
			float single2 = cCPoint.x;
			cCPoint.x = single2 * single - cCPoint.y * single1 + pivot.x;
			cCPoint.y = single2 * single1 + cCPoint.y * single + pivot.y;
			return cCPoint;
		}

		public CCPoint RPerp()
		{
			return this.ccp(this.y, -this.x);
		}

		public static bool SegmentIntersect(CCPoint A, CCPoint B, CCPoint C, CCPoint D)
		{
			float single;
			float single1;
			if (CCPoint.LineIntersect(A, B, C, D, out single, out single1) && single >= 0f && single <= 1f && single1 >= 0f && single1 <= 1f)
			{
				return true;
			}
			return false;
		}

		public CCPoint Sub(CCPoint v2)
		{
			return this.ccp(this.x - v2.x, this.y - v2.y);
		}

		public static float ToAngle(CCPoint v)
		{
			return (float)Math.Atan2((double)v.y, (double)v.x);
		}

		public CCPoint Unrotate(CCPoint v2)
		{
			return this.ccp(this.x * v2.x + this.y * v2.y, this.y * v2.x - this.x * v2.y);
		}
	}
}