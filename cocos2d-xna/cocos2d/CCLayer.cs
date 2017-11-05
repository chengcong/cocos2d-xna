using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCLayer : CCNode, ICCTargetedTouchDelegate, ICCStandardTouchDelegate, ICCTouchDelegate
	{
		protected bool m_bIsTouchEnabled;

		private bool m_bIsAccelerometerEnabled;

		private bool m_bIsKeypadEnabled;

		public bool isAccelerometerEnabled
		{
			get
			{
				return this.m_bIsAccelerometerEnabled;
			}
			set
			{
				if (value != this.m_bIsAccelerometerEnabled)
				{
					this.m_bIsAccelerometerEnabled = value;
					if (base.isRunning)
					{
						if (!value)
						{
							throw new NotImplementedException();
						}
						throw new NotImplementedException();
					}
				}
			}
		}

		public bool isKeypadEnabled
		{
			get
			{
				return this.m_bIsKeypadEnabled;
			}
			set
			{
				if (value != this.m_bIsKeypadEnabled)
				{
					this.m_bIsKeypadEnabled = value;
					if (base.isRunning)
					{
						if (!value)
						{
							throw new NotImplementedException();
						}
						throw new NotImplementedException();
					}
				}
			}
		}

		public bool isTouchEnabled
		{
			get
			{
				return this.m_bIsTouchEnabled;
			}
			set
			{
				if (this.m_bIsTouchEnabled != value)
				{
					this.m_bIsTouchEnabled = value;
					if (base.isRunning)
					{
						if (value)
						{
							this.registerWithTouchDispatcher();
							return;
						}
						CCTouchDispatcher.sharedDispatcher().removeDelegate(this);
					}
				}
			}
		}

		public CCLayer()
		{
			this.anchorPoint = CCPointExtension.ccp(0.5f, 0.5f);
			this.isRelativeAnchorPoint = false;
		}

		public virtual bool ccTouchBegan(CCTouch touch, CCEvent event_)
		{
			return true;
		}

		public virtual void ccTouchCancelled(CCTouch touch, CCEvent event_)
		{
		}

		public virtual void ccTouchEnded(CCTouch touch, CCEvent event_)
		{
		}

		public virtual void ccTouchesBegan(List<CCTouch> touches, CCEvent event_)
		{
		}

		public virtual void ccTouchesCancelled(List<CCTouch> touches, CCEvent event_)
		{
		}

		public virtual void ccTouchesEnded(List<CCTouch> touches, CCEvent event_)
		{
		}

		public virtual void ccTouchesMoved(List<CCTouch> touches, CCEvent event_)
		{
		}

		public virtual void ccTouchMoved(CCTouch touch, CCEvent event_)
		{
		}

		public virtual bool init()
		{
			bool flag = false;
			CCDirector cCDirector = CCDirector.sharedDirector();
			if (cCDirector != null)
			{
				this.contentSize = cCDirector.getWinSize();
				this.m_bIsTouchEnabled = false;
				this.m_bIsAccelerometerEnabled = false;
				flag = true;
			}
			return flag;
		}

		public static new CCLayer node()
		{
			CCLayer cCLayer = new CCLayer();
			if (cCLayer.init())
			{
				return cCLayer;
			}
			return null;
		}

		public override void onEnter()
		{
			if (this.m_bIsTouchEnabled)
			{
				this.registerWithTouchDispatcher();
			}
			base.onEnter();
			if (this.m_bIsAccelerometerEnabled)
			{
				throw new NotImplementedException();
			}
			bool mBIsKeypadEnabled = this.m_bIsKeypadEnabled;
		}

		public override void onEnterTransitionDidFinish()
		{
			if (this.m_bIsAccelerometerEnabled)
			{
				throw new NotImplementedException();
			}
			base.onEnterTransitionDidFinish();
		}

		public override void onExit()
		{
			if (this.m_bIsTouchEnabled)
			{
				CCTouchDispatcher.sharedDispatcher().removeDelegate(this);
			}
			if (this.m_bIsAccelerometerEnabled)
			{
				throw new NotImplementedException();
			}
			bool mBIsKeypadEnabled = this.m_bIsKeypadEnabled;
			base.onExit();
		}

		public virtual void registerWithTouchDispatcher()
		{
			CCTouchDispatcher.sharedDispatcher().addStandardDelegate(this, 0);
		}
	}
}