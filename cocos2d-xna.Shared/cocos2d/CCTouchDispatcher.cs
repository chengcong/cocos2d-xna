using System;
using System.Collections.Generic;
using System.Linq;

namespace cocos2d
{
	public class CCTouchDispatcher : IEGLTouchDelegate
	{
		private static CCTouchDispatcher pSharedDispatcher;

		protected List<CCTouchHandler> m_pTargetedHandlers;

		protected List<CCTouchHandler> m_pStandardHandlers;

		private bool m_bLocked;

		private bool m_bToAdd;

		private bool m_bToRemove;

		private List<CCTouchHandler> m_pHandlersToAdd;

		private List<object> m_pHandlersToRemove;

		private bool m_bToQuit;

		private bool m_bDispatchEvents;

		public bool IsDispatchEvents
		{
			get
			{
				return this.m_bDispatchEvents;
			}
			set
			{
				this.m_bDispatchEvents = value;
			}
		}

		private CCTouchDispatcher()
		{
		}

		public void addStandardDelegate(ICCStandardTouchDelegate pDelegate, int nPriority)
		{
			CCTouchHandler cCTouchHandler = CCStandardTouchHandler.handlerWithDelegate(pDelegate, nPriority);
			if (!this.m_bLocked)
			{
				this.forceAddHandler(cCTouchHandler, this.m_pStandardHandlers);
				return;
			}
			this.m_pHandlersToAdd.Add(cCTouchHandler);
			this.m_bToAdd = true;
		}

		public void addTargetedDelegate(ICCTargetedTouchDelegate pDelegate, int nPriority, bool bSwallowsTouches)
		{
			CCTouchHandler cCTouchHandler = CCTargetedTouchHandler.handlerWithDelegate(pDelegate, nPriority, bSwallowsTouches);
			if (!this.m_bLocked)
			{
				this.forceAddHandler(cCTouchHandler, this.m_pTargetedHandlers);
				return;
			}
			this.m_pHandlersToAdd.Add(cCTouchHandler);
			this.m_bToAdd = true;
		}

		public CCTouchHandler findHandler(ICCTouchDelegate pDelegate)
		{
			CCTouchHandler cCTouchHandler;
			foreach (CCTouchHandler mPTargetedHandler in this.m_pTargetedHandlers)
			{
				if (mPTargetedHandler.Delegate != pDelegate)
				{
					continue;
				}
				cCTouchHandler = mPTargetedHandler;
				return cCTouchHandler;
			}
			List<CCTouchHandler>.Enumerator enumerator = this.m_pStandardHandlers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CCTouchHandler current = enumerator.Current;
					if (current.Delegate != pDelegate)
					{
						continue;
					}
					cCTouchHandler = current;
					return cCTouchHandler;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return cCTouchHandler;
		}

		protected void forceAddHandler(CCTouchHandler pHandler, List<CCTouchHandler> pArray)
		{
			int num = 0;
			for (int i = 0; i < pArray.Count; i++)
			{
				CCTouchHandler item = pArray[i];
				if (item != null)
				{
					if (item.Priority < pHandler.Priority)
					{
						num++;
					}
					if (item.Delegate == pHandler.Delegate)
					{
						return;
					}
				}
			}
			pArray.Insert(num, pHandler);
		}

		protected void forceRemoveAllDelegates()
		{
			this.m_pStandardHandlers.Clear();
			this.m_pTargetedHandlers.Clear();
		}

		protected void forceRemoveDelegate(ICCTouchDelegate pDelegate)
		{
			foreach (CCTouchHandler mPStandardHandler in this.m_pStandardHandlers)
			{
				if (mPStandardHandler == null || mPStandardHandler.Delegate != pDelegate)
				{
					continue;
				}
				this.m_pStandardHandlers.Remove(mPStandardHandler);
				break;
			}
			foreach (CCTouchHandler mPTargetedHandler in this.m_pTargetedHandlers)
			{
				if (mPTargetedHandler == null || mPTargetedHandler.Delegate != pDelegate)
				{
					continue;
				}
				this.m_pTargetedHandlers.Remove(mPTargetedHandler);
				break;
			}
		}

		public bool init()
		{
			this.m_bDispatchEvents = true;
			this.m_pTargetedHandlers = new List<CCTouchHandler>();
			this.m_pStandardHandlers = new List<CCTouchHandler>();
			this.m_pHandlersToAdd = new List<CCTouchHandler>();
			this.m_pHandlersToRemove = new List<object>();
			this.m_bToRemove = false;
			this.m_bToAdd = false;
			this.m_bToQuit = false;
			this.m_bLocked = false;
			return true;
		}

		private int less(CCTouchHandler p1, CCTouchHandler p2)
		{
			return p1.Priority - p2.Priority;
		}

		protected void rearrangeHandlers(List<CCTouchHandler> pArray)
		{
			pArray.Sort(new Comparison<CCTouchHandler>(this.less));
		}

		public void removeAllDelegates()
		{
			if (!this.m_bLocked)
			{
				this.forceRemoveAllDelegates();
				return;
			}
			this.m_bToQuit = true;
		}

		public void removeDelegate(ICCTouchDelegate pDelegate)
		{
			if (pDelegate == null)
			{
				return;
			}
			if (!this.m_bLocked)
			{
				this.forceRemoveDelegate(pDelegate);
				return;
			}
			this.m_pHandlersToRemove.Add(pDelegate);
			this.m_bToRemove = true;
		}

		public void setPriority(int nPriority, ICCTouchDelegate pDelegate)
		{
			this.findHandler(pDelegate).Priority = nPriority;
			this.rearrangeHandlers(this.m_pTargetedHandlers);
			this.rearrangeHandlers(this.m_pStandardHandlers);
		}

		public static CCTouchDispatcher sharedDispatcher()
		{
			if (CCTouchDispatcher.pSharedDispatcher == null)
			{
				CCTouchDispatcher.pSharedDispatcher = new CCTouchDispatcher();
				CCTouchDispatcher.pSharedDispatcher.init();
			}
			return CCTouchDispatcher.pSharedDispatcher;
		}

		public void touches(List<CCTouch> pTouches, CCEvent pEvent, int uIndex)
		{
			List<CCTouch> cCTouches;
			this.m_bLocked = true;
			int count = this.m_pTargetedHandlers.Count;
			int num = this.m_pStandardHandlers.Count;
			bool flag = (count <= 0 ? false : num > 0);
			cCTouches = (!flag ? pTouches : pTouches.ToArray().ToList<CCTouch>());
			CCTouchType cCTouchType = (CCTouchType)uIndex;
			if (count > 0)
			{
				foreach (CCTouch pTouch in pTouches)
				{
					foreach (CCTargetedTouchHandler mPTargetedHandler in this.m_pTargetedHandlers)
					{
						ICCTargetedTouchDelegate @delegate = (ICCTargetedTouchDelegate)mPTargetedHandler.Delegate;
						bool flag1 = false;
						if (cCTouchType == CCTouchType.CCTOUCHBEGAN)
						{
							flag1 = @delegate.ccTouchBegan(pTouch, pEvent);
							if (flag1)
							{
								mPTargetedHandler.ClaimedTouches.Add(pTouch);
							}
						}
						else if (mPTargetedHandler.ClaimedTouches.Contains(pTouch))
						{
							flag1 = true;
							switch (cCTouchType)
							{
								case CCTouchType.CCTOUCHMOVED:
								{
									@delegate.ccTouchMoved(pTouch, pEvent);
									break;
								}
								case CCTouchType.CCTOUCHENDED:
								{
									@delegate.ccTouchEnded(pTouch, pEvent);
									mPTargetedHandler.ClaimedTouches.Remove(pTouch);
									break;
								}
								case CCTouchType.CCTOUCHCANCELLED:
								{
									@delegate.ccTouchCancelled(pTouch, pEvent);
									mPTargetedHandler.ClaimedTouches.Remove(pTouch);
									break;
								}
							}
						}
						if (!flag1 || !mPTargetedHandler.IsSwallowsTouches)
						{
							continue;
						}
						if (!flag)
						{
							break;
						}
						cCTouches.Remove(pTouch);
						break;
					}
				}
			}
			if (num > 0 && cCTouches.Count > 0)
			{
				foreach (CCStandardTouchHandler mPStandardHandler in this.m_pStandardHandlers)
				{
					ICCStandardTouchDelegate cCStandardTouchDelegate = (ICCStandardTouchDelegate)mPStandardHandler.Delegate;
					switch (cCTouchType)
					{
						case CCTouchType.CCTOUCHBEGAN:
						{
							cCStandardTouchDelegate.ccTouchesBegan(cCTouches, pEvent);
							continue;
						}
						case CCTouchType.CCTOUCHMOVED:
						{
							cCStandardTouchDelegate.ccTouchesMoved(cCTouches, pEvent);
							continue;
						}
						case CCTouchType.CCTOUCHENDED:
						{
							cCStandardTouchDelegate.ccTouchesEnded(cCTouches, pEvent);
							continue;
						}
						case CCTouchType.CCTOUCHCANCELLED:
						{
							cCStandardTouchDelegate.ccTouchesCancelled(cCTouches, pEvent);
							continue;
						}
						default:
						{
							continue;
						}
					}
				}
			}
			if (flag)
			{
				cCTouches = null;
			}
			this.m_bLocked = false;
			if (this.m_bToRemove)
			{
				this.m_bToRemove = false;
				for (int i = 0; i < this.m_pHandlersToRemove.Count; i++)
				{
					this.forceRemoveDelegate((ICCTouchDelegate)this.m_pHandlersToRemove[i]);
				}
				this.m_pHandlersToRemove.Clear();
			}
			if (this.m_bToAdd)
			{
				this.m_bToAdd = false;
				foreach (CCTouchHandler mPHandlersToAdd in this.m_pHandlersToAdd)
				{
					if (mPHandlersToAdd is CCTargetedTouchHandler && mPHandlersToAdd.Delegate is ICCTargetedTouchDelegate)
					{
						this.forceAddHandler(mPHandlersToAdd, this.m_pTargetedHandlers);
					}
					else if (!(mPHandlersToAdd is CCStandardTouchHandler) || !(mPHandlersToAdd.Delegate is ICCStandardTouchDelegate))
					{
						CCLog.Log("ERROR: inconsistent touch handler and delegate found in m_pHandlersToAdd of CCTouchDispatcher");
					}
					else
					{
						this.forceAddHandler(mPHandlersToAdd, this.m_pStandardHandlers);
					}
				}
				this.m_pHandlersToAdd.Clear();
			}
			if (this.m_bToQuit)
			{
				this.m_bToQuit = false;
				this.forceRemoveAllDelegates();
			}
		}

		public virtual void touchesBegan(List<CCTouch> touches, CCEvent pEvent)
		{
			if (this.m_bDispatchEvents)
			{
				this.touches(touches, pEvent, 0);
			}
		}

		public virtual void touchesCancelled(List<CCTouch> touches, CCEvent pEvent)
		{
			if (this.m_bDispatchEvents)
			{
				this.touches(touches, pEvent, 3);
			}
		}

		public virtual void touchesEnded(List<CCTouch> touches, CCEvent pEvent)
		{
			if (this.m_bDispatchEvents)
			{
				this.touches(touches, pEvent, 2);
			}
		}

		public virtual void touchesMoved(List<CCTouch> touches, CCEvent pEvent)
		{
			if (this.m_bDispatchEvents)
			{
				this.touches(touches, pEvent, 1);
			}
		}
	}
}