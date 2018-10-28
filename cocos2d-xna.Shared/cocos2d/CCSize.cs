using System;

namespace cocos2d
{
	public class CCSize
	{
		public float width;

		public float height;

		public CCSize(CCSize copy)
		{
			this.width = copy.width;
			this.height = copy.height;
		}

		public CCSize()
		{
			this.width = 0f;
			this.height = 0f;
		}

		public CCSize(float width, float height)
		{
			this.width = width;
			this.height = height;
		}

		public static bool CCSizeEqualToSize(CCSize size1, CCSize size2)
		{
			if (size1 == null || size2 == null)
			{
				return false;
			}
			if (size1.width != size2.width)
			{
				return false;
			}
			return size1.height == size2.height;
		}
	}
}