using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCMenuItemToggle : CCMenuItem, ICCRGBAProtocol
	{
		private byte m_cOpacity;

		private ccColor3B m_tColor;

		private int m_uSelectedIndex;

		public List<CCMenuItem> m_pSubItems;

		public ccColor3B Color
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
				foreach (CCMenuItem mPSubItem in this.m_pSubItems)
				{
					mPSubItem.Enabled = value;
				}
			}
		}

		public bool IsOpacityModifyRGB
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public byte Opacity
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int SelectedIndex
		{
			get
			{
				return this.m_uSelectedIndex;
			}
			set
			{
				if (value != this.m_uSelectedIndex)
				{
					this.m_uSelectedIndex = value;
				}
				base.removeChildByTag(32767, false);
				CCMenuItem item = this.m_pSubItems[this.m_uSelectedIndex];
				this.addChild(item, 0, 32767);
				CCSize cCSize = new CCSize(item.contentSize.width, item.contentSize.height);
				this.contentSize = cCSize;
				item.position = new CCPoint(cCSize.width / 2f, cCSize.height / 2f);
			}
		}

		public List<CCMenuItem> SubItems
		{
			get
			{
				return this.m_pSubItems;
			}
			set
			{
				this.m_pSubItems = value;
			}
		}

		public CCMenuItemToggle()
		{
		}

		public override void activate()
		{
			if (this.m_bIsEnabled)
			{
				int mUSelectedIndex = (this.m_uSelectedIndex + 1) % this.m_pSubItems.Count;
				this.SelectedIndex = mUSelectedIndex;
			}
			base.activate();
		}

		public void addSubItem(CCMenuItem item)
		{
			this.m_pSubItems.Add(item);
		}

		public bool initWithItem(CCMenuItem item)
		{
			base.initWithTarget(null, null);
			this.m_pSubItems = new List<CCMenuItem>()
			{
				item
			};
			this.SelectedIndex = 0;
			return true;
		}

		public bool initWithTarget(SelectorProtocol target, SEL_MenuHandler selector, CCMenuItem[] items)
		{
			base.initWithTarget(target, selector);
			this.m_pSubItems = new List<CCMenuItem>();
			CCMenuItem[] cCMenuItemArray = items;
			for (int i = 0; i < (int)cCMenuItemArray.Length; i++)
			{
				CCMenuItem cCMenuItem = cCMenuItemArray[i];
				this.m_pSubItems.Add(cCMenuItem);
			}
			this.SelectedIndex = 0;
			return true;
		}

		public static CCMenuItemToggle itemWithItem(CCMenuItem item)
		{
			CCMenuItemToggle cCMenuItemToggle = new CCMenuItemToggle();
			cCMenuItemToggle.initWithItem(item);
			return cCMenuItemToggle;
		}

		public static CCMenuItemToggle itemWithTarget(SelectorProtocol target, SEL_MenuHandler selector, params CCMenuItem[] items)
		{
			CCMenuItemToggle cCMenuItemToggle = new CCMenuItemToggle();
			cCMenuItemToggle.initWithTarget(target, selector, items);
			return cCMenuItemToggle;
		}

		public override void selected()
		{
			base.selected();
			this.m_pSubItems[this.m_uSelectedIndex].selected();
		}

		public CCMenuItem selectedItem()
		{
			return this.m_pSubItems[this.m_uSelectedIndex];
		}

		public override void unselected()
		{
			base.unselected();
			this.m_pSubItems[this.m_uSelectedIndex].unselected();
		}
	}
}