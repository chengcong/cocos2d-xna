using System;

namespace cocos2d
{
	public class CCAction : CCObject
	{
		protected CCNode m_pTarget;

		protected CCNode m_pOriginalTarget;

		protected int m_nTag;

		public CCNode originalTarget
		{
			get
			{
				return this.m_pOriginalTarget;
			}
			set
			{
				this.m_pOriginalTarget = value;
			}
		}

		public int tag
		{
			get
			{
				return this.m_nTag;
			}
			set
			{
				this.m_nTag = value;
			}
		}

		public CCNode target
		{
			get
			{
				return this.m_pTarget;
			}
			set
			{
				this.m_pTarget = value;
			}
		}

		public CCAction()
		{
			this.m_nTag = -1;
		}

		public static CCAction action()
		{
			return new CCAction();
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCAction mNTag = null;
			mNTag = (cCZone == null || cCZone.m_pCopyObject == null ? new CCAction() : (CCAction)cCZone.m_pCopyObject);
			mNTag.m_nTag = this.m_nTag;
			return mNTag;
		}

		public virtual bool isDone()
		{
			return true;
		}

		public virtual void startWithTarget(CCNode target)
		{
			CCNode cCNode = target;
			CCNode cCNode1 = cCNode;
			this.m_pTarget = cCNode;
			this.m_pOriginalTarget = cCNode1;
		}

		public virtual void step(float dt)
		{
			CCLog.Log("[Action step]. override me");
		}

		public virtual void stop()
		{
			this.m_pTarget = null;
		}

		public virtual void update(float dt)
		{
			CCLog.Log("[Action update]. override me");
		}
	}
}