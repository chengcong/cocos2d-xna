using System;
using System.Runtime.CompilerServices;

namespace cocos2d
{
	public class CCMenuItemSprite : CCMenuItem, ICCRGBAProtocol
	{
		protected CCNode m_pNormalImage;

		protected CCNode m_pSelectedImage;

		protected CCNode m_pDisabledImage;

		public ccColor3B Color
		{
			get;
			set;
		}

		public CCNode DisabledImage
		{
			get
			{
				return this.m_pDisabledImage;
			}
			set
			{
				if (value != null)
				{
					this.addChild(value);
					value.anchorPoint = new CCPoint(0f, 0f);
					value.visible = false;
				}
				if (this.m_pDisabledImage != null)
				{
					this.removeChild(this.m_pDisabledImage, true);
				}
				this.m_pDisabledImage = value;
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
				if (this.m_pSelectedImage != null)
				{
					this.m_pSelectedImage.visible = false;
				}
				if (!value)
				{
					if (this.m_pDisabledImage != null)
					{
						this.m_pDisabledImage.visible = true;
						this.m_pNormalImage.visible = false;
						return;
					}
					this.m_pNormalImage.visible = true;
				}
				else
				{
					this.m_pNormalImage.visible = true;
					if (this.m_pDisabledImage != null)
					{
						this.m_pDisabledImage.visible = false;
						return;
					}
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

		public CCNode NormalImage
		{
			get
			{
				return this.m_pNormalImage;
			}
			set
			{
				if (value != null)
				{
					this.addChild(value);
					value.anchorPoint = new CCPoint(0f, 0f);
					value.visible = true;
				}
				if (this.m_pNormalImage != null)
				{
					this.removeChild(this.m_pNormalImage, true);
				}
				this.m_pNormalImage = value;
			}
		}

		public byte Opacity
		{
			get
			{
				return (this.m_pNormalImage as ICCRGBAProtocol).Opacity;
			}
			set
			{
				(this.m_pNormalImage as ICCRGBAProtocol).Opacity = value;
				if (this.m_pSelectedImage != null)
				{
					(this.m_pSelectedImage as ICCRGBAProtocol).Opacity = value;
				}
				if (this.m_pDisabledImage != null)
				{
					(this.m_pDisabledImage as ICCRGBAProtocol).Opacity = value;
				}
			}
		}

		public CCNode SelectedImage
		{
			get
			{
				return this.m_pSelectedImage;
			}
			set
			{
				if (value != null)
				{
					this.addChild(value);
					value.anchorPoint = new CCPoint(0f, 0f);
					value.visible = false;
				}
				if (this.m_pSelectedImage != null)
				{
					this.removeChild(this.m_pSelectedImage, true);
				}
				this.m_pSelectedImage = value;
			}
		}

		public CCMenuItemSprite()
		{
			this.m_pNormalImage = null;
			this.m_pSelectedImage = null;
			this.m_pDisabledImage = null;
		}

		public virtual ICCRGBAProtocol convertToRGBAProtocol()
		{
			return this;
		}

		public bool initFromNormalSprite(CCNode normalSprite, CCNode selectedSprite, CCNode disabledSprite, SelectorProtocol target, SEL_MenuHandler selector)
		{
			if (normalSprite == null)
			{
				throw new ArgumentNullException("normalSprite");
			}
			base.initWithTarget(target, selector);
			this.NormalImage = normalSprite;
			this.SelectedImage = selectedSprite;
			this.DisabledImage = disabledSprite;
			this.contentSize = this.m_pNormalImage.contentSize;
			return true;
		}

		public static CCMenuItemSprite itemFromNormalSprite(CCNode normalSprite, CCNode selectedSprite)
		{
			return CCMenuItemSprite.itemFromNormalSprite(normalSprite, selectedSprite, null, null, null);
		}

		public static CCMenuItemSprite itemFromNormalSprite(CCNode normalSprite, CCNode selectedSprite, SelectorProtocol target, SEL_MenuHandler selector)
		{
			return CCMenuItemSprite.itemFromNormalSprite(normalSprite, selectedSprite, null, target, selector);
		}

		public static CCMenuItemSprite itemFromNormalSprite(CCNode normalSprite, CCNode selectedSprite, CCNode disabledSprite, SelectorProtocol target, SEL_MenuHandler selector)
		{
			CCMenuItemSprite cCMenuItemSprite = new CCMenuItemSprite();
			cCMenuItemSprite.initFromNormalSprite(normalSprite, selectedSprite, disabledSprite, target, selector);
			return cCMenuItemSprite;
		}

		public override void selected()
		{
			base.selected();
			if (this.m_pDisabledImage != null)
			{
				this.m_pDisabledImage.visible = false;
			}
			if (this.m_pSelectedImage == null)
			{
				this.m_pNormalImage.visible = true;
				return;
			}
			this.m_pNormalImage.visible = false;
			this.m_pSelectedImage.visible = true;
		}

		public override void unselected()
		{
			base.unselected();
			this.m_pNormalImage.visible = true;
			if (this.m_pSelectedImage != null)
			{
				this.m_pSelectedImage.visible = false;
			}
			if (this.m_pDisabledImage != null)
			{
				this.m_pDisabledImage.visible = false;
			}
		}
	}
}