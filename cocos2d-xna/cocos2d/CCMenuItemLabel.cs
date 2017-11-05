using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace cocos2d
{
	public class CCMenuItemLabel : CCMenuItem, ICCRGBAProtocol
	{
		private const uint kCurrentItem = 3233828865;

		private const uint kZoomActionTag = 3233828866;

		protected ccColor3B m_tColorBackup;

		protected float m_fOriginalScale;

		protected CCNode m_pLabel;

		public ccColor3B Color
		{
			get
			{
				return (this.m_pLabel as ICCRGBAProtocol).Color;
			}
			set
			{
				(this.m_pLabel as ICCRGBAProtocol).Color = value;
			}
		}

		public ccColor3B DisabledColor
		{
			get;
			set;
		}

		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				if (this.m_bIsEnabled != value)
				{
					if (value)
					{
						(this.m_pLabel as ICCRGBAProtocol).Color = this.m_tColorBackup;
					}
					else
					{
						this.m_tColorBackup = (this.m_pLabel as ICCRGBAProtocol).Color;
						(this.m_pLabel as ICCRGBAProtocol).Color = this.DisabledColor;
					}
				}
				base.Enabled = value;
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

		public CCNode Label
		{
			get
			{
				return this.m_pLabel;
			}
			set
			{
				if (value != null)
				{
					this.addChild(value);
					value.anchorPoint = new CCPoint(0f, 0f);
					this.contentSize = value.contentSize;
				}
				if (this.m_pLabel != null)
				{
					this.removeChild(this.m_pLabel, true);
				}
				this.m_pLabel = value;
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

		public CCMenuItemLabel()
		{
		}

		public override void activate()
		{
			if (this.Enabled)
			{
				base.stopAllActions();
				this.scale = this.m_fOriginalScale;
				base.activate();
			}
		}

		public virtual ICCRGBAProtocol convertToRGBAProtocol()
		{
			return this;
		}

		protected bool initWithLabel(CCNode label, SelectorProtocol target, SEL_MenuHandler selector)
		{
			base.initWithTarget(target, selector);
			this.m_fOriginalScale = 1f;
			this.m_tColorBackup = new ccColor3B(Microsoft.Xna.Framework.Color.White);
			this.DisabledColor = new ccColor3B(126, 126, 126);
			this.Label = label;
			return true;
		}

		public static CCMenuItemLabel itemWithLabel(CCNode label, SelectorProtocol target, SEL_MenuHandler selector)
		{
			CCMenuItemLabel cCMenuItemLabel = new CCMenuItemLabel();
			cCMenuItemLabel.initWithLabel(label, target, selector);
			return cCMenuItemLabel;
		}

		public static CCMenuItemLabel itemWithLabel(CCNode label)
		{
			CCMenuItemLabel cCMenuItemLabel = new CCMenuItemLabel();
			cCMenuItemLabel.initWithLabel(label, null, null);
			return cCMenuItemLabel;
		}

		public override void selected()
		{
			if (this.Enabled)
			{
				base.selected();
				CCAction actionByTag = base.getActionByTag(-1061138430);
				if (actionByTag == null)
				{
					this.m_fOriginalScale = this.scale;
				}
				else
				{
					base.stopAction(actionByTag);
				}
				CCAction cCAction = CCScaleTo.actionWithDuration(0.1f, this.m_fOriginalScale * 1.2f);
				cCAction.tag = -1061138430;
				base.runAction(cCAction);
			}
		}

		public void setString(string label)
		{
			((ICCLabelProtocol)this.m_pLabel).setString(label);
			this.contentSize = this.m_pLabel.contentSize;
		}

		public override void unselected()
		{
			if (this.m_bIsEnabled)
			{
				base.unselected();
				base.stopActionByTag(-1061138430);
				CCAction cCAction = CCScaleTo.actionWithDuration(0.1f, this.m_fOriginalScale);
				cCAction.tag = -1061138430;
				base.runAction(cCAction);
			}
		}
	}
}