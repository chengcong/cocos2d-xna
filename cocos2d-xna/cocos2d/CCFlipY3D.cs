using System;

namespace cocos2d
{
	public class CCFlipY3D : CCFlipX3D
	{
		public CCFlipY3D()
		{
		}

		public static new CCFlipY3D actionWithDuration(float duration)
		{
			CCFlipY3D cCFlipY3D = new CCFlipY3D();
			if (cCFlipY3D != null)
			{
				cCFlipY3D.initWithSize(new ccGridSize(1, 1), duration);
			}
			return cCFlipY3D;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCFlipY3D cCFlipY3D = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCFlipY3D = new CCFlipY3D();
				pZone = new CCZone(cCFlipY3D);
			}
			else
			{
				cCFlipY3D = (CCFlipY3D)pZone.m_pCopyObject;
			}
			this.copyWithZone(pZone);
			cCFlipY3D.initWithSize(this.m_sGridSize, this.m_fDuration);
			return cCFlipY3D;
		}

		public override void update(float time)
		{
			float single;
			ccGridSize _ccGridSize;
			ccGridSize _ccGridSize1;
			ccGridSize _ccGridSize2;
			ccGridSize _ccGridSize3;
			float single1 = 3.14159274f * time;
			float single2 = (float)Math.Sin((double)single1);
			single1 = single1 / 2f;
			float single3 = (float)Math.Cos((double)single1);
			ccVertex3F _ccVertex3F = new ccVertex3F();
			ccVertex3F _ccVertex3F1 = base.originalVertex(new ccGridSize(1, 1));
			ccVertex3F _ccVertex3F2 = base.originalVertex(new ccGridSize(0, 0));
			float single4 = _ccVertex3F1.y;
			float single5 = _ccVertex3F2.y;
			if (single4 <= single5)
			{
				_ccGridSize1 = new ccGridSize(0, 0);
				_ccGridSize = new ccGridSize(0, 1);
				_ccGridSize3 = new ccGridSize(1, 0);
				_ccGridSize2 = new ccGridSize(1, 1);
				single = single5;
			}
			else
			{
				_ccGridSize = new ccGridSize(0, 0);
				_ccGridSize1 = new ccGridSize(0, 1);
				_ccGridSize2 = new ccGridSize(1, 0);
				_ccGridSize3 = new ccGridSize(1, 1);
				single = single4;
			}
			_ccVertex3F.y = single - single * single3;
			_ccVertex3F.z = Math.Abs((float)Math.Floor((double)(single * single2 / 4f)));
			ccVertex3F _ccVertex3F3 = base.originalVertex(_ccGridSize);
			_ccVertex3F3.y = _ccVertex3F.y;
			ccVertex3F _ccVertex3F4 = _ccVertex3F3;
			_ccVertex3F4.z = _ccVertex3F4.z + _ccVertex3F.z;
			base.setVertex(_ccGridSize, _ccVertex3F3);
			_ccVertex3F3 = base.originalVertex(_ccGridSize1);
			ccVertex3F _ccVertex3F5 = _ccVertex3F3;
			_ccVertex3F5.y = _ccVertex3F5.y - _ccVertex3F.y;
			ccVertex3F _ccVertex3F6 = _ccVertex3F3;
			_ccVertex3F6.z = _ccVertex3F6.z - _ccVertex3F.z;
			base.setVertex(_ccGridSize1, _ccVertex3F3);
			_ccVertex3F3 = base.originalVertex(_ccGridSize2);
			_ccVertex3F3.y = _ccVertex3F.y;
			ccVertex3F _ccVertex3F7 = _ccVertex3F3;
			_ccVertex3F7.z = _ccVertex3F7.z + _ccVertex3F.z;
			base.setVertex(_ccGridSize2, _ccVertex3F3);
			_ccVertex3F3 = base.originalVertex(_ccGridSize3);
			ccVertex3F _ccVertex3F8 = _ccVertex3F3;
			_ccVertex3F8.y = _ccVertex3F8.y - _ccVertex3F.y;
			ccVertex3F _ccVertex3F9 = _ccVertex3F3;
			_ccVertex3F9.z = _ccVertex3F9.z - _ccVertex3F.z;
			base.setVertex(_ccGridSize3, _ccVertex3F3);
		}
	}
}