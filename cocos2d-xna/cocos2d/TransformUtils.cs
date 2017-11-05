using Microsoft.Xna.Framework;
using System;

namespace cocos2d
{
	public class TransformUtils
	{
		public TransformUtils()
		{
		}

		public static void CGAffineToGL(CCAffineTransform t, ref float[] m)
		{
			float single = 0f;
			float single1 = single;
			m[14] = single;
			float single2 = single1;
			float single3 = single2;
			m[11] = single2;
			float single4 = single3;
			float single5 = single4;
			m[9] = single4;
			float single6 = single5;
			float single7 = single6;
			m[8] = single6;
			float single8 = single7;
			float single9 = single8;
			m[7] = single8;
			float single10 = single9;
			float single11 = single10;
			m[6] = single10;
			float single12 = single11;
			float single13 = single12;
			m[3] = single12;
			m[2] = single13;
			float single14 = 1f;
			float single15 = single14;
			m[15] = single14;
			m[10] = single15;
			m[0] = t.a;
			m[4] = t.c;
			m[12] = t.tx;
			m[1] = t.b;
			m[5] = t.d;
			m[13] = t.ty;
		}

		public static Matrix CGAffineToMatrix(float[] m)
		{
			Matrix matrix = new Matrix()
			{
				M11 = m[0],
				M21 = m[4],
				M31 = m[8],
				M41 = m[12],
				M12 = m[1],
				M22 = m[5],
				M32 = m[9],
				M42 = m[13],
				M13 = m[2],
				M23 = m[6],
				M33 = m[10],
				M43 = m[14],
				M14 = m[3],
				M24 = m[7],
				M34 = m[11],
				M44 = m[15]
			};
			return matrix;
		}

		public static Matrix CGAffineToMatrix(CCAffineTransform t)
		{
			float[] singleArray = new float[16];
			TransformUtils.CGAffineToGL(t, ref singleArray);
			return TransformUtils.CGAffineToMatrix(singleArray);
		}

		public static void GLToCGAffine(float[] m, CCAffineTransform t)
		{
			t.a = m[0];
			t.c = m[4];
			t.tx = m[12];
			t.b = m[1];
			t.d = m[5];
			t.ty = m[13];
		}
	}
}