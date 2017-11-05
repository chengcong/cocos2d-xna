using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCMenu : CCLayer, ICCRGBAProtocol, ICCTouchDelegate
	{
		public const float kDefaultPadding = 5f;

		public const int kCCMenuTouchPriority = -128;

		protected tCCMenuState m_eState;

		protected CCMenuItem m_pSelectedItem;

		protected ccColor3B m_tColor;

		protected byte m_cOpacity;

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
				return this.m_cOpacity;
			}
			set
			{
				this.m_cOpacity = value;
				if (this.m_pChildren != null && this.m_pChildren.Count > 0)
				{
					foreach (CCNode mPChild in this.m_pChildren)
					{
						if (mPChild == null)
						{
							continue;
						}
						ICCRGBAProtocol mCOpacity = mPChild as ICCRGBAProtocol;
						if (mCOpacity == null)
						{
							continue;
						}
						mCOpacity.Opacity = this.m_cOpacity;
					}
				}
			}
		}

		public CCMenu()
		{
			this.m_cOpacity = 0;
			this.m_pSelectedItem = null;
		}

		public void alignItemsHorizontally()
		{
			this.alignItemsHorizontallyWithPadding(5f);
		}

		public void alignItemsHorizontallyWithPadding(float padding)
		{
			float single = -padding;
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				foreach (CCNode mPChild in this.m_pChildren)
				{
					if (mPChild == null)
					{
						continue;
					}
					single = single + (mPChild.contentSize.width * mPChild.scaleX + padding);
				}
			}
			float single1 = -single / 2f;
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				foreach (CCNode cCPoint in this.m_pChildren)
				{
					if (cCPoint == null)
					{
						continue;
					}
					cCPoint.position = new CCPoint(single1 + cCPoint.contentSize.width * cCPoint.scaleX / 2f, 0f);
					single1 = single1 + (cCPoint.contentSize.width * cCPoint.scaleX + padding);
				}
			}
		}

		public void alignItemsInColumns(params int[] columns)
		{
			int num;
			int[] numArray = columns;
			int num1 = -5;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				foreach (CCNode mPChild in this.m_pChildren)
				{
					if (mPChild == null)
					{
						continue;
					}
					num = numArray[num2];
					float single = mPChild.contentSize.height;
					num3 = (int)(((float)num3 >= single ? (float)num3 : single));
					num4++;
					if (num4 < num)
					{
						continue;
					}
					num1 = num1 + num3 + 5;
					num4 = 0;
					num3 = 0;
					num2++;
				}
			}
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			num2 = 0;
			num3 = 0;
			num = 0;
			float single1 = 0f;
			float single2 = 0f;
			float single3 = (float)(num1 / 2);
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				foreach (CCNode cCPoint in this.m_pChildren)
				{
					if (cCPoint == null)
					{
						continue;
					}
					if (num == 0)
					{
						num = numArray[num2];
						if (num == 0)
						{
							throw new ArgumentException("Can not have a zero column size for a row.");
						}
						single1 = (winSize.width - 10f) / (float)num;
						single2 = single1 / 2f;
					}
					float single4 = cCPoint.contentSize.height * cCPoint.scaleY;
					num3 = (int)(((float)num3 >= single4 ? (float)num3 : single4));
					cCPoint.position = new CCPoint(5f + single2 - (winSize.width - 10f) / 2f, single3 - cCPoint.contentSize.height * cCPoint.scaleY / 2f);
					single2 = single2 + single1;
					num4++;
					if (num4 < num)
					{
						continue;
					}
					single3 = single3 - (float)(num3 + 5);
					num4 = 0;
					num = 0;
					num3 = 0;
					num2++;
				}
			}
		}

		public void alignItemsInRows(params int[] rows)
		{
			int num;
			int[] numArray = rows;
			List<int> nums = new List<int>();
			List<int> nums1 = new List<int>();
			int num1 = -10;
			int num2 = -5;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				foreach (CCNode mPChild in this.m_pChildren)
				{
					if (mPChild == null)
					{
						continue;
					}
					num = numArray[num3];
					float single = mPChild.contentSize.width * mPChild.scaleX;
					num4 = (int)(((float)num4 >= single ? (float)num4 : single));
					num2 = num2 + (int)(mPChild.contentSize.height * mPChild.scaleY + 5f);
					num5++;
					if (num5 < num)
					{
						continue;
					}
					nums.Add(num4);
					nums1.Add(num2);
					num1 = num1 + num4 + 10;
					num5 = 0;
					num4 = 0;
					num2 = -5;
					num3++;
				}
			}
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			num3 = 0;
			num4 = 0;
			num = 0;
			float single1 = (float)(-num1 / 2);
			float item = 0f;
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				foreach (CCNode cCPoint in this.m_pChildren)
				{
					if (cCPoint == null)
					{
						continue;
					}
					if (num == 0)
					{
						num = numArray[num3];
						item = (float)nums1[num3];
					}
					float single2 = cCPoint.contentSize.width * cCPoint.scaleX;
					num4 = (int)(((float)num4 >= single2 ? (float)num4 : single2));
					cCPoint.position = new CCPoint(single1 + (float)(nums[num3] / 2), item - winSize.height / 2f);
					item = item - (cCPoint.contentSize.height * cCPoint.scaleY + 10f);
					num5++;
					if (num5 < num)
					{
						continue;
					}
					single1 = single1 + (float)(num4 + 5);
					num5 = 0;
					num = 0;
					num4 = 0;
					num3++;
				}
			}
		}

		public void alignItemsVertically()
		{
			this.alignItemsVerticallyWithPadding(5f);
		}

		public void alignItemsVerticallyWithPadding(float padding)
		{
			float single = -padding;
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				foreach (CCNode mPChild in this.m_pChildren)
				{
					if (mPChild == null)
					{
						continue;
					}
					single = single + (mPChild.contentSize.height * mPChild.scaleY + padding);
				}
			}
			float single1 = single / 2f;
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				foreach (CCNode cCPoint in this.m_pChildren)
				{
					if (cCPoint == null)
					{
						continue;
					}
					cCPoint.position = new CCPoint(0f, single1 - cCPoint.contentSize.height * cCPoint.scaleY / 2f);
					single1 = single1 - (cCPoint.contentSize.height * cCPoint.scaleY + padding);
				}
			}
		}

		public override bool ccTouchBegan(CCTouch touch, CCEvent ccevent)
		{
			if (this.m_eState != tCCMenuState.kCCMenuStateWaiting || !this.m_bIsVisible)
			{
				return false;
			}
			for (CCNode i = this.m_pParent; i != null; i = i.parent)
			{
				if (!i.visible)
				{
					return false;
				}
			}
			this.m_pSelectedItem = this.itemForTouch(touch);
			if (this.m_pSelectedItem == null)
			{
				return false;
			}
			this.m_eState = tCCMenuState.kCCMenuStateTrackingTouch;
			this.m_pSelectedItem.selected();
			return true;
		}

		public override void ccTouchCancelled(CCTouch touch, CCEvent ccevent)
		{
			if (this.m_pSelectedItem != null)
			{
				this.m_pSelectedItem.unselected();
			}
			this.m_eState = tCCMenuState.kCCMenuStateWaiting;
		}

		public override void ccTouchEnded(CCTouch touch, CCEvent ccevent)
		{
			if (this.m_pSelectedItem != null)
			{
				this.m_pSelectedItem.unselected();
				this.m_pSelectedItem.activate();
			}
			this.m_eState = tCCMenuState.kCCMenuStateWaiting;
		}

		public override void ccTouchMoved(CCTouch touch, CCEvent ccevent)
		{
			CCMenuItem cCMenuItem = this.itemForTouch(touch);
			if (cCMenuItem != this.m_pSelectedItem)
			{
				if (this.m_pSelectedItem != null)
				{
					this.m_pSelectedItem.unselected();
				}
				this.m_pSelectedItem = cCMenuItem;
				if (this.m_pSelectedItem != null)
				{
					this.m_pSelectedItem.selected();
				}
			}
		}

		public virtual ICCRGBAProtocol convertToRGBAProtocol()
		{
			return this;
		}

		public virtual void destroy()
		{
		}

		public new bool init()
		{
			return this.initWithItems(null);
		}

		protected bool initWithItems(params CCMenuItem[] item)
		{
			CCRect cCRect;
			if (!base.init())
			{
				return false;
			}
			this.m_bIsTouchEnabled = true;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.m_bIsRelativeAnchorPoint = false;
			this.anchorPoint = new CCPoint(0.5f, 0.5f);
			this.contentSize = winSize;
			CCApplication.sharedApplication().statusBarFrame(out cCRect);
			ccDeviceOrientation _ccDeviceOrientation = CCDirector.sharedDirector().deviceOrientation;
			if (_ccDeviceOrientation == ccDeviceOrientation.kCCDeviceOrientationLandscapeLeft || _ccDeviceOrientation == ccDeviceOrientation.kCCDeviceOrientationLandscapeRight)
			{
				CCSize cCSize = winSize;
				cCSize.height = cCSize.height - cCRect.size.width;
			}
			else
			{
				CCSize cCSize1 = winSize;
				cCSize1.height = cCSize1.height - cCRect.size.height;
			}
			this.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			if (item != null)
			{
				CCMenuItem[] cCMenuItemArray = item;
				for (int i = 0; i < (int)cCMenuItemArray.Length; i++)
				{
					this.addChild(cCMenuItemArray[i]);
				}
			}
			this.m_pSelectedItem = null;
			this.m_eState = tCCMenuState.kCCMenuStateWaiting;
			return true;
		}

		protected CCMenuItem itemForTouch(CCTouch touch)
		{
			CCMenuItem cCMenuItem;
			CCPoint gL = touch.locationInView(touch.view());
			gL = CCDirector.sharedDirector().convertToGL(gL);
			if (this.m_pChildren != null && this.m_pChildren.Count > 0)
			{
				List<CCNode>.Enumerator enumerator = this.m_pChildren.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						CCNode current = enumerator.Current;
						if (current == null || !current.visible || !((CCMenuItem)current).Enabled)
						{
							continue;
						}
						CCPoint nodeSpace = current.convertToNodeSpace(gL);
						CCRect zero = ((CCMenuItem)current).rect();
						zero.origin = CCPoint.Zero;
						if (!CCRect.CCRectContainsPoint(zero, nodeSpace))
						{
							continue;
						}
						cCMenuItem = (CCMenuItem)current;
						return cCMenuItem;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return cCMenuItem;
			}
			return null;
		}

		public virtual void keep()
		{
		}

		public static CCMenu menuWithItem(CCMenuItem item)
		{
			return CCMenu.menuWithItems(new CCMenuItem[] { item });
		}

		public static CCMenu menuWithItems(params CCMenuItem[] item)
		{
			CCMenu cCMenu = new CCMenu();
			if (cCMenu != null && cCMenu.initWithItems(item))
			{
				return cCMenu;
			}
			return null;
		}

		public static new CCMenu node()
		{
			CCMenu cCMenu = new CCMenu();
			if (cCMenu != null && cCMenu.init())
			{
				return cCMenu;
			}
			return null;
		}

		public override void onExit()
		{
			if (this.m_eState == tCCMenuState.kCCMenuStateTrackingTouch)
			{
				this.m_pSelectedItem.unselected();
				this.m_eState = tCCMenuState.kCCMenuStateWaiting;
				this.m_pSelectedItem = null;
			}
			base.onExit();
		}

		public override void registerWithTouchDispatcher()
		{
			CCTouchDispatcher.sharedDispatcher().addTargetedDelegate(this, -128, true);
		}
	}
}