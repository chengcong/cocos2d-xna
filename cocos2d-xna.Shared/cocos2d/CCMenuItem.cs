using System;

namespace cocos2d
{
	public class CCMenuItem : CCNode
	{
		public const int kCurrentItem = 32767;

		public const uint kZoomActionTag = 3233828866;

		protected static uint _fontSize;

		protected static string _fontName;

		protected static bool _fontNameRelease;

		protected bool m_bIsSelected;

		protected bool m_bIsEnabled;

		protected SelectorProtocol m_pListener;

		protected SEL_MenuHandler m_pfnSelector;

		protected string m_functionName;

		public virtual bool Enabled
		{
			get
			{
				return this.m_bIsEnabled;
			}
			set
			{
				this.m_bIsEnabled = value;
			}
		}

		public virtual bool Selected
		{
			get
			{
				return this.m_bIsSelected;
			}
		}

		static CCMenuItem()
		{
			CCMenuItem._fontSize = 32;
			CCMenuItem._fontName = "Arial";
			CCMenuItem._fontNameRelease = false;
		}

		public CCMenuItem()
		{
			this.m_bIsSelected = false;
			this.m_bIsEnabled = false;
			this.m_pListener = null;
			this.m_pfnSelector = null;
		}

		public virtual void activate()
		{
			if (this.m_bIsEnabled)
			{
				SelectorProtocol mPListener = this.m_pListener;
				if (this.m_pfnSelector != null)
				{
					this.m_pfnSelector(this);
				}
			}
		}

		public bool initWithTarget(SelectorProtocol rec, SEL_MenuHandler selector)
		{
			this.anchorPoint = new CCPoint(0.5f, 0.5f);
			this.m_pListener = rec;
			this.m_pfnSelector = selector;
			this.m_bIsEnabled = true;
			this.m_bIsSelected = false;
			return true;
		}

		public static CCMenuItem itemWithTarget(SelectorProtocol rec, SEL_MenuHandler selector)
		{
			CCMenuItem cCMenuItem = new CCMenuItem();
			cCMenuItem.initWithTarget(rec, selector);
			return cCMenuItem;
		}

		public CCRect rect()
		{
			return new CCRect(this.m_tPosition.x - this.m_tContentSize.width * this.m_tAnchorPoint.x, this.m_tPosition.y - this.m_tContentSize.height * this.m_tAnchorPoint.y, this.m_tContentSize.width, this.m_tContentSize.height);
		}

		public virtual void registerScriptHandler(string pszFunctionName)
		{
			throw new NotImplementedException();
		}

		public virtual void selected()
		{
			this.m_bIsSelected = true;
		}

		public virtual void setTarget(SelectorProtocol rec, SEL_MenuHandler selector)
		{
			this.m_pListener = rec;
			this.m_pfnSelector = selector;
		}

		public virtual void unselected()
		{
			this.m_bIsSelected = false;
		}
	}
}