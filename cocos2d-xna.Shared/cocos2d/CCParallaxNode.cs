using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCParallaxNode : CCNode
	{
		protected List<CCPointObject> m_pParallaxArray;

		protected CCPoint m_tLastPosition;

		public List<CCPointObject> ParallaxArray
		{
			get
			{
				return this.m_pParallaxArray;
			}
			set
			{
				this.m_pParallaxArray = value;
			}
		}

		public CCParallaxNode()
		{
			this.m_pParallaxArray = new List<CCPointObject>();
			this.m_tLastPosition = new CCPoint(-100f, -100f);
		}

		private CCPoint absolutePosition()
		{
			CCPoint mTPosition = this.m_tPosition;
			CCNode cCNode = this;
			while (cCNode.parent != null)
			{
				cCNode = cCNode.parent;
				mTPosition = new CCPoint(mTPosition.x + this.position.x, mTPosition.y + this.position.y);
			}
			return mTPosition;
		}

		public virtual void addChild(CCNode child, uint zOrder, int tag)
		{
		}

		public virtual void addChild(CCNode child, uint z, CCPoint parallaxRatio, CCPoint positionOffset)
		{
			CCPointObject.pointWithCCPoint(parallaxRatio, positionOffset).Child = child;
			CCPoint mTPosition = this.m_tPosition;
			mTPosition.x = mTPosition.x * parallaxRatio.x + positionOffset.x;
			mTPosition.y = mTPosition.y * parallaxRatio.y + positionOffset.y;
			child.position = mTPosition;
			base.addChild(child, (int)z, child.tag);
		}

		public static new CCParallaxNode node()
		{
			return new CCParallaxNode();
		}

		public virtual new void removeAllChildrenWithCleanup(bool cleanup)
		{
			base.removeAllChildrenWithCleanup(cleanup);
		}

		public virtual new void removeChild(CCNode child, bool cleanup)
		{
			int num = 0;
			while (num < this.m_pParallaxArray.Count && this.m_pParallaxArray[num].Child != child)
			{
				num++;
			}
			base.removeChild(child, cleanup);
		}

		public virtual new void visit()
		{
			CCPoint cCPoint = this.absolutePosition();
			if (!CCPoint.CCPointEqualToPoint(cCPoint, this.m_tLastPosition))
			{
				for (int i = 0; i < this.m_pParallaxArray.Count; i++)
				{
					CCPointObject item = this.m_pParallaxArray[i];
					float ratio = -cCPoint.x + cCPoint.x * item.Ratio.x + item.Offset.x;
					float single = -cCPoint.y + cCPoint.y * item.Ratio.y + item.Offset.y;
					item.Child.position = new CCPoint(ratio, single);
				}
				this.m_tLastPosition = cCPoint;
			}
			base.visit();
		}
	}
}