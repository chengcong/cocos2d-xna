using System;

namespace cocos2d
{
	public class CCCallFuncND : CCCallFuncN
	{
		protected object m_pData;

		protected SEL_CallFuncND m_pCallFuncND;

		public CCCallFuncND()
		{
		}

		public static CCCallFuncND actionWithTarget(SelectorProtocol pSelectorTarget, SEL_CallFuncND selector, object d)
		{
			CCCallFuncND cCCallFuncND = new CCCallFuncND();
			if (cCCallFuncND != null && cCCallFuncND.initWithTarget(pSelectorTarget, selector, d))
			{
				return cCCallFuncND;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCCallFuncND cCCallFuncND = null;
			if (zone == null || zone.m_pCopyObject == null)
			{
				cCCallFuncND = new CCCallFuncND();
				zone = new CCZone(cCCallFuncND);
			}
			else
			{
				cCCallFuncND = (CCCallFuncND)zone.m_pCopyObject;
			}
			base.copyWithZone(zone);
			cCCallFuncND.initWithTarget(this.m_pSelectorTarget, this.m_pCallFuncND, this.m_pData);
			return cCCallFuncND;
		}

		public override void execute()
		{
			if (this.m_pCallFuncND != null)
			{
				this.m_pCallFuncND(this.m_pTarget, this.m_pData);
			}
		}

		public bool initWithTarget(SelectorProtocol pSelectorTarget, SEL_CallFuncND selector, object d)
		{
			if (!base.initWithTarget(pSelectorTarget))
			{
				return false;
			}
			this.m_pData = d;
			this.m_pCallFuncND = selector;
			return true;
		}
	}
}