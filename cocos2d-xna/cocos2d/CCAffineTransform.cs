using System;

namespace cocos2d
{
	public class CCAffineTransform
	{
		public float a;

		public float b;

		public float c;

		public float d;

		public float tx;

		public float ty;

		private CCAffineTransform()
		{
		}

		public static CCAffineTransform CCAffineTransformConcat(CCAffineTransform t1, CCAffineTransform t2)
		{
			return CCAffineTransform.CCAffineTransformMake(t1.a * t2.a + t1.b * t2.c, t1.a * t2.b + t1.b * t2.d, t1.c * t2.a + t1.d * t2.c, t1.c * t2.b + t1.d * t2.d, t1.tx * t2.a + t1.ty * t2.c + t2.tx, t1.tx * t2.b + t1.ty * t2.d + t2.ty);
		}

		public static bool CCAffineTransformEqualToTransform(CCAffineTransform t1, CCAffineTransform t2)
		{
			if (t1.a != t2.a || t1.b != t2.b || t1.c != t2.c || t1.d != t2.d || t1.tx != t2.tx)
			{
				return false;
			}
			return t1.ty == t2.ty;
		}

		public static CCAffineTransform CCAffineTransformInvert(CCAffineTransform t)
		{
			float single = 1f / (t.a * t.d - t.b * t.c);
			return CCAffineTransform.CCAffineTransformMake(single * t.d, -single * t.b, -single * t.c, single * t.a, single * (t.c * t.ty - t.d * t.tx), single * (t.b * t.tx - t.a * t.ty));
		}

		public static CCAffineTransform CCAffineTransformMake(float a, float b, float c, float d, float tx, float ty)
		{
			CCAffineTransform cCAffineTransform = new CCAffineTransform()
			{
				a = a,
				b = b,
				c = c,
				d = d,
				tx = tx,
				ty = ty
			};
			return cCAffineTransform;
		}

		public static CCAffineTransform CCAffineTransformMakeIdentity()
		{
			return CCAffineTransform.CCAffineTransformMake(1f, 0f, 0f, 1f, 0f, 0f);
		}

		public static CCAffineTransform CCAffineTransformRotate(CCAffineTransform t, float anAngle)
		{
			float single = (float)Math.Sin((double)anAngle);
			float single1 = (float)Math.Cos((double)anAngle);
			return CCAffineTransform.CCAffineTransformMake(t.a * single1 + t.c * single, t.b * single1 + t.d * single, t.c * single1 - t.a * single, t.d * single1 - t.b * single, t.tx, t.ty);
		}

		public static CCAffineTransform CCAffineTransformScale(CCAffineTransform t, float sx, float sy)
		{
			return CCAffineTransform.CCAffineTransformMake(t.a * sx, t.b * sx, t.c * sy, t.d * sy, t.tx, t.ty);
		}

		public static CCAffineTransform CCAffineTransformTranslate(CCAffineTransform t, float tx, float ty)
		{
			return CCAffineTransform.CCAffineTransformMake(t.a, t.b, t.c, t.d, t.tx + t.a * tx + t.c * ty, t.ty + t.b * tx + t.d * ty);
		}

		public static CCPoint CCPointApplyAffineTransform(CCPoint point, CCAffineTransform t)
		{
			CCPoint cCPoint = new CCPoint()
			{
				x = (float)((double)t.a * (double)point.x + (double)t.c * (double)point.y + (double)t.tx),
				y = (float)((double)t.b * (double)point.x + (double)t.d * (double)point.y + (double)t.ty)
			};
			return cCPoint;
		}

		public static CCRect CCRectApplyAffineTransform(CCRect rect, CCAffineTransform anAffineTransform)
		{
			float single = CCRect.CCRectGetMinY(rect);
			float single1 = CCRect.CCRectGetMinX(rect);
			float single2 = CCRect.CCRectGetMaxX(rect);
			float single3 = CCRect.CCRectGetMaxY(rect);
			CCPoint cCPoint = CCAffineTransform.CCPointApplyAffineTransform(new CCPoint(single1, single), anAffineTransform);
			CCPoint cCPoint1 = CCAffineTransform.CCPointApplyAffineTransform(new CCPoint(single2, single), anAffineTransform);
			CCPoint cCPoint2 = CCAffineTransform.CCPointApplyAffineTransform(new CCPoint(single1, single3), anAffineTransform);
			CCPoint cCPoint3 = CCAffineTransform.CCPointApplyAffineTransform(new CCPoint(single2, single3), anAffineTransform);
			float single4 = Math.Min(Math.Min(cCPoint.x, cCPoint1.x), Math.Min(cCPoint2.x, cCPoint3.x));
			float single5 = Math.Max(Math.Max(cCPoint.x, cCPoint1.x), Math.Max(cCPoint2.x, cCPoint3.x));
			float single6 = Math.Min(Math.Min(cCPoint.y, cCPoint1.y), Math.Min(cCPoint2.y, cCPoint3.y));
			float single7 = Math.Max(Math.Max(cCPoint.y, cCPoint1.y), Math.Max(cCPoint2.y, cCPoint3.y));
			return new CCRect(single4, single6, single5 - single4, single7 - single6);
		}

		public static CCSize CCSizeApplyAffineTransform(CCSize size, CCAffineTransform t)
		{
			CCSize cCSize = new CCSize()
			{
				width = (float)((double)t.a * (double)size.width + (double)t.c * (double)size.height),
				height = (float)((double)t.b * (double)size.width + (double)t.d * (double)size.height)
			};
			return cCSize;
		}
	}
}