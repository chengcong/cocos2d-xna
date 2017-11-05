using System;

namespace cocos2d
{
	public class CCPageTurn3D : CCGrid3DAction
	{
		public CCPageTurn3D()
		{
		}

		public static new CCPageTurn3D actionWithSize(ccGridSize gridSize, float time)
		{
			CCPageTurn3D cCPageTurn3D = new CCPageTurn3D();
			if (cCPageTurn3D.initWithSize(gridSize, time))
			{
				return cCPageTurn3D;
			}
			return null;
		}

		public override void update(float time)
		{
			float single = Math.Max(0f, time - 0.25f);
			float single1 = -100f - single * single * 500f;
			float single2 = -1.57079637f * (float)Math.Sqrt((double)time);
			float single3 = 1.57079637f + single2;
			float single4 = (float)Math.Sin((double)single3);
			float single5 = (float)Math.Cos((double)single3);
			for (int i = 0; i <= this.m_sGridSize.x; i++)
			{
				for (int j = 0; j <= this.m_sGridSize.y; j++)
				{
					ccVertex3F _ccVertex3F = base.originalVertex(new ccGridSize(i, j));
					float single6 = (float)Math.Sqrt((double)(_ccVertex3F.x * _ccVertex3F.x + (_ccVertex3F.y - single1) * (_ccVertex3F.y - single1)));
					float single7 = single6 * single4;
					float single8 = (float)Math.Asin((double)(_ccVertex3F.x / single6));
					float single9 = single8 / single4;
					float single10 = (float)Math.Cos((double)single9);
					if ((double)single9 > 3.14159265358979)
					{
						_ccVertex3F.x = 0f;
					}
					else
					{
						_ccVertex3F.x = single7 * (float)Math.Sin((double)single9);
					}
					_ccVertex3F.y = single6 + single1 - single7 * (1f - single10) * single4;
					_ccVertex3F.z = single7 * (1f - single10) * single5 / 7f;
					if (_ccVertex3F.z < 0.5f)
					{
						_ccVertex3F.z = 0.5f;
					}
					base.setVertex(new ccGridSize(i, j), _ccVertex3F);
				}
			}
		}
	}
}