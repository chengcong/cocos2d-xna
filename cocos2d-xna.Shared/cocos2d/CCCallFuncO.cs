using System;

namespace cocos2d
{
	public class CCCallFuncO : CCCallFunc
	{
		private CCObject m_pObject;

		private SEL_CallFuncO m_pCallFuncO;

		public CCCallFuncO()
		{
			this.m_pObject = null;
			this.m_pCallFuncO = null;
		}

		public static CCCallFuncO actionWithTarget(SelectorProtocol pSelectorTarget, SEL_CallFuncO selector, CCObject pObject)
		{
			CCCallFuncO cCCallFuncO = new CCCallFuncO();
			if (cCCallFuncO != null && cCCallFuncO.initWithTarget(pSelectorTarget, selector, pObject))
			{
				return cCCallFuncO;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCCallFuncO cCCallFuncO = null;
			if (zone == null || zone.m_pCopyObject == null)
			{
				cCCallFuncO = new CCCallFuncO();
				zone = new CCZone(cCCallFuncO);
			}
			else
			{
				cCCallFuncO = (CCCallFuncO)zone.m_pCopyObject;
			}
			base.copyWithZone(zone);
			cCCallFuncO.initWithTarget(this.m_pSelectorTarget, this.m_pCallFuncO, this.m_pObject);
			return cCCallFuncO;
		}

		public override void execute()
		{
			if (this.m_pCallFuncO != null)
			{
				this.m_pCallFuncO(this.m_pObject);
			}
		}

		~CCCallFuncO()
		{
		}

		public CCObject getObject()
		{
			return this.m_pObject;
		}

		public bool initWithTarget(SelectorProtocol pSelectorTarget, SEL_CallFuncO selector, CCObject pObject)
		{
			if (!base.initWithTarget(pSelectorTarget))
			{
				return false;
			}
			this.m_pObject = pObject;
			this.m_pCallFuncO = selector;
			return true;
		}

		public void setObject(CCObject pObj)
		{
			if (pObj != this.m_pObject)
			{
				this.m_pObject = pObj;
			}
		}
	}
}