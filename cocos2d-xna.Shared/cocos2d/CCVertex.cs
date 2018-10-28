using System;

namespace cocos2d
{
	public abstract class CCVertex
	{
		protected CCVertex()
		{
		}

		public static bool LineIntersect(float Ax, float Ay, float Bx, float By, float Cx, float Cy, float Dx, float Dy, out float T)
		{
			T = 0f;
			if (Ax == Bx && Ay == By || Cx == Dx && Cy == Dy)
			{
				return false;
			}
			Bx = Bx - Ax;
			By = By - Ay;
			Cx = Cx - Ax;
			Cy = Cy - Ay;
			Dx = Dx - Ax;
			Dy = Dy - Ay;
			float single = (float)Math.Sqrt((double)(Bx * Bx + By * By));
			float bx = Bx / single;
			float by = By / single;
			float cx = Cx * bx + Cy * by;
			Cy = Cy * bx - Cx * by;
			Cx = cx;
			cx = Dx * bx + Dy * by;
			Dy = Dy * bx - Dx * by;
			Dx = cx;
			if (Cy == Dy)
			{
				return false;
			}
			T = (Dx + (Cx - Dx) * Dy / (Dy - Cy)) / single;
			return true;
		}

		public static void LineToPolygon(CCPoint[] points, float stroke, ccVertex2F[] vertices, int offset, int nuPoints)
		{
			int num;
			CCPoint cCPoint;
			nuPoints = nuPoints + offset;
			if (nuPoints <= 1)
			{
				return;
			}
			stroke = stroke * 0.5f;
			int num1 = nuPoints - 1;
			for (int i = offset; i < nuPoints; i++)
			{
				num = i * 2;
				CCPoint cCPoint1 = points[i];
				if (i == 0)
				{
					cCPoint = CCPointExtension.ccpPerp(CCPointExtension.ccpNormalize(CCPointExtension.ccpSub(cCPoint1, points[i + 1])));
				}
				else if (i != num1)
				{
					CCPoint cCPoint2 = points[i + 1];
					CCPoint cCPoint3 = points[i - 1];
					CCPoint cCPoint4 = CCPointExtension.ccpNormalize(CCPointExtension.ccpSub(cCPoint2, cCPoint1));
					CCPoint cCPoint5 = CCPointExtension.ccpNormalize(CCPointExtension.ccpSub(cCPoint3, cCPoint1));
					float single = (float)Math.Acos((double)CCPointExtension.ccpDot(cCPoint4, cCPoint5));
					if (single >= ccMacros.CC_DEGREES_TO_RADIANS(70f))
					{
						cCPoint = (single >= ccMacros.CC_DEGREES_TO_RADIANS(170f) ? CCPointExtension.ccpPerp(CCPointExtension.ccpNormalize(CCPointExtension.ccpSub(cCPoint2, cCPoint3))) : CCPointExtension.ccpNormalize(CCPointExtension.ccpMidpoint(cCPoint4, cCPoint5)));
					}
					else
					{
						cCPoint = CCPointExtension.ccpPerp(CCPointExtension.ccpNormalize(CCPointExtension.ccpMidpoint(cCPoint4, cCPoint5)));
					}
				}
				else
				{
					cCPoint = CCPointExtension.ccpPerp(CCPointExtension.ccpNormalize(CCPointExtension.ccpSub(points[i - 1], cCPoint1)));
				}
				cCPoint = CCPointExtension.ccpMult(cCPoint, stroke);
				vertices[num] = new ccVertex2F(cCPoint1.x + cCPoint.x, cCPoint1.y + cCPoint.y);
				vertices[num + 1] = new ccVertex2F(cCPoint1.x - cCPoint.x, cCPoint1.y - cCPoint.y);
			}
			offset = (offset == 0 ? 0 : offset - 1);
			for (int j = offset; j < num1; j++)
			{
				num = j * 2;
				int num2 = num + 2;
				ccVertex2F _ccVertex2F = vertices[num];
				ccVertex2F _ccVertex2F1 = vertices[num + 1];
				ccVertex2F _ccVertex2F2 = vertices[num2];
				ccVertex2F _ccVertex2F3 = vertices[num2 + 1];
				float single1 = 0f;
				bool flag = !CCVertex.LineIntersect(_ccVertex2F.x, _ccVertex2F.y, _ccVertex2F3.x, _ccVertex2F3.y, _ccVertex2F1.x, _ccVertex2F1.y, _ccVertex2F2.x, _ccVertex2F2.y, out single1);
				if (!flag && (single1 < 0f || single1 > 1f))
				{
					flag = true;
				}
				if (flag)
				{
					vertices[num2] = _ccVertex2F3;
					vertices[num2 + 1] = _ccVertex2F2;
				}
			}
		}
	}
}