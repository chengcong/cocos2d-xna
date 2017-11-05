using System;

namespace cocos2d
{
	public class CCFlipX3D : CCGrid3DAction
	{
		public CCFlipX3D()
		{
		}

		public static new CCFlipX3D actionWithDuration(float duration)
		{
			CCFlipX3D cCFlipX3D = new CCFlipX3D();
			if (cCFlipX3D.initWithSize(new ccGridSize(1, 1), duration))
			{
				return cCFlipX3D;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCFlipX3D cCFlipX3D = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCFlipX3D = new CCFlipX3D();
				pZone = new CCZone(cCFlipX3D);
			}
			else
			{
				cCFlipX3D = (CCFlipX3D)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCFlipX3D.initWithSize(this.m_sGridSize, this.m_fDuration);
			return cCFlipX3D;
		}

		public new bool initWithDuration(float duration)
		{
			return this.initWithSize(new ccGridSize(1, 1), duration);
		}

		public virtual new bool initWithSize(ccGridSize gridSize, float duration)
		{
			if (gridSize.x != 1 || gridSize.y != 1)
			{
				return false;
			}
			return base.initWithSize(gridSize, duration);
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
			float single4 = _ccVertex3F1.x;
			float single5 = _ccVertex3F2.x;
			if (single4 <= single5)
			{
				_ccGridSize2 = new ccGridSize(0, 0);
				_ccGridSize3 = new ccGridSize(0, 1);
				_ccGridSize = new ccGridSize(1, 0);
				_ccGridSize1 = new ccGridSize(1, 1);
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
			_ccVertex3F.x = single - single * single3;
			_ccVertex3F.z = Math.Abs((float)Math.Floor((double)(single * single2 / 4f)));
			ccVertex3F _ccVertex3F3 = base.originalVertex(_ccGridSize);
			_ccVertex3F3.x = _ccVertex3F.x;
			ccVertex3F _ccVertex3F4 = _ccVertex3F3;
			_ccVertex3F4.z = _ccVertex3F4.z + _ccVertex3F.z;
			base.setVertex(_ccGridSize, _ccVertex3F3);
			_ccVertex3F3 = base.originalVertex(_ccGridSize1);
			_ccVertex3F3.x = _ccVertex3F.x;
			ccVertex3F _ccVertex3F5 = _ccVertex3F3;
			_ccVertex3F5.z = _ccVertex3F5.z + _ccVertex3F.z;
			base.setVertex(_ccGridSize1, _ccVertex3F3);
			_ccVertex3F3 = base.originalVertex(_ccGridSize2);
			ccVertex3F _ccVertex3F6 = _ccVertex3F3;
			_ccVertex3F6.x = _ccVertex3F6.x - _ccVertex3F.x;
			ccVertex3F _ccVertex3F7 = _ccVertex3F3;
			_ccVertex3F7.z = _ccVertex3F7.z - _ccVertex3F.z;
			base.setVertex(_ccGridSize2, _ccVertex3F3);
			_ccVertex3F3 = base.originalVertex(_ccGridSize3);
			ccVertex3F _ccVertex3F8 = _ccVertex3F3;
			_ccVertex3F8.x = _ccVertex3F8.x - _ccVertex3F.x;
			ccVertex3F _ccVertex3F9 = _ccVertex3F3;
			_ccVertex3F9.z = _ccVertex3F9.z - _ccVertex3F.z;
			base.setVertex(_ccGridSize3, _ccVertex3F3);
		}
	}
}