using System;

namespace cocos2d
{
	public class CCCallFunc : CCActionInstant
	{
		protected SelectorProtocol m_pSelectorTarget;

		protected string m_scriptFuncName;

		private SEL_CallFunc m_pCallFunc;

		public CCCallFunc()
		{
			this.m_pSelectorTarget = null;
			this.m_scriptFuncName = "";
			this.m_pCallFunc = null;
		}

		public static CCCallFunc actionWithTarget(SelectorProtocol pSelectorTarget, SEL_CallFunc selector)
		{
			CCCallFunc cCCallFunc = new CCCallFunc();
			if (cCCallFunc == null || !cCCallFunc.initWithTarget(pSelectorTarget))
			{
				return null;
			}
			cCCallFunc.m_pCallFunc = selector;
			return cCCallFunc;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCCallFunc cCCallFunc = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCCallFunc = new CCCallFunc();
				pZone = new CCZone(cCCallFunc);
			}
			else
			{
				cCCallFunc = (CCCallFunc)pZone.m_pCopyObject;
			}
			base.copyWithZone(pZone);
			cCCallFunc.initWithTarget(this.m_pSelectorTarget);
			cCCallFunc.m_pCallFunc = this.m_pCallFunc;
			cCCallFunc.m_scriptFuncName = this.m_scriptFuncName;
			return cCCallFunc;
		}

		public virtual void execute()
		{
			if (this.m_pCallFunc != null)
			{
				this.m_pCallFunc();
			}
		}

		~CCCallFunc()
		{
		}

		public SelectorProtocol getTargetCallback()
		{
			return this.m_pSelectorTarget;
		}

		public virtual bool initWithTarget(SelectorProtocol pSelectorTarget)
		{
			this.m_pSelectorTarget = pSelectorTarget;
			return true;
		}

		public void setTargetCallback(SelectorProtocol pSel)
		{
			if (pSel != this.m_pSelectorTarget)
			{
				this.m_pSelectorTarget = pSel;
			}
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			this.execute();
		}
	}
}