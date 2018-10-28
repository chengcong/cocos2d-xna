using System;

namespace cocos2d
{
	public class CCCallFuncN : CCCallFunc
	{
		private SEL_CallFuncN m_pCallFuncN;

		public CCCallFuncN()
		{
			this.m_pCallFuncN = null;
		}

		public static CCCallFuncN actionWithTarget(SelectorProtocol pSelectorTarget, SEL_CallFuncN selector)
		{
			CCCallFuncN cCCallFuncN = new CCCallFuncN();
			if (cCCallFuncN != null && cCCallFuncN.initWithTarget(pSelectorTarget, selector))
			{
				return cCCallFuncN;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCCallFuncN cCCallFuncN = null;
			if (zone == null || zone.m_pCopyObject == null)
			{
				cCCallFuncN = new CCCallFuncN();
				zone = new CCZone(cCCallFuncN);
			}
			else
			{
				cCCallFuncN = (CCCallFuncN)zone.m_pCopyObject;
			}
			base.copyWithZone(zone);
			cCCallFuncN.initWithTarget(this.m_pSelectorTarget, this.m_pCallFuncN);
			return cCCallFuncN;
		}

		public override void execute()
		{
			if (this.m_pCallFuncN != null)
			{
				this.m_pCallFuncN(this.m_pTarget);
			}
		}

		~CCCallFuncN()
		{
		}

		public bool initWithTarget(SelectorProtocol pSelectorTarget, SEL_CallFuncN selector)
		{
			if (!base.initWithTarget(pSelectorTarget))
			{
				return false;
			}
			this.m_pCallFuncN = selector;
			return true;
		}
	}
}