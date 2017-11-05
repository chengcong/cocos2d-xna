using System;

namespace cocos2d
{
	public class CCPointObject
	{
		private CCPoint m_tRatio;

		private CCPoint m_tOffset;

		private CCNode m_pChild;

		public CCNode Child
		{
			get
			{
				return this.m_pChild;
			}
			set
			{
				this.m_pChild = value;
			}
		}

		public CCPoint Offset
		{
			get
			{
				return this.m_tOffset;
			}
			set
			{
				this.m_tOffset = value;
			}
		}

		public CCPoint Ratio
		{
			get
			{
				return this.m_tRatio;
			}
			set
			{
				this.m_tRatio = value;
			}
		}

		public CCPointObject()
		{
		}

		public bool initWithCCPoint(CCPoint ratio, CCPoint offset)
		{
			this.m_tRatio = ratio;
			this.m_tOffset = offset;
			this.m_pChild = null;
			return true;
		}

		public static CCPointObject pointWithCCPoint(CCPoint ratio, CCPoint offset)
		{
			CCPointObject cCPointObject = new CCPointObject();
			cCPointObject.initWithCCPoint(ratio, offset);
			return cCPointObject;
		}
	}
}