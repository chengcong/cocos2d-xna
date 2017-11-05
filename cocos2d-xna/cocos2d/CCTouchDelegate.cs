using System;
using System.Collections.Generic;
using System.Linq;

namespace cocos2d
{
	public class CCTouchDelegate : ICCTouchDelegate
	{
		protected Dictionary<int, string> m_pEventTypeFuncMap;

		public CCTouchDelegate()
		{
		}

		public virtual void destroy()
		{
		}

		public void excuteScriptTouchesHandler(int eventType, List<CCTouch> pTouches)
		{
			if (this.m_pEventTypeFuncMap != null && CCScriptEngineManager.sharedScriptEngineManager().ScriptEngine != null)
			{
				CCScriptEngineManager.sharedScriptEngineManager().ScriptEngine.executeTouchesEvent(this.m_pEventTypeFuncMap[eventType].ToString(), pTouches);
			}
		}

		public void excuteScriptTouchHandler(int eventType, CCTouch pTouch)
		{
			if (this.m_pEventTypeFuncMap != null && CCScriptEngineManager.sharedScriptEngineManager().ScriptEngine != null)
			{
				CCScriptEngineManager.sharedScriptEngineManager().ScriptEngine.executeTouchEvent(this.m_pEventTypeFuncMap[eventType].ToString(), pTouch);
			}
		}

		public bool isScriptHandlerExist(int eventType)
		{
			if (this.m_pEventTypeFuncMap == null)
			{
				return false;
			}
			return this.m_pEventTypeFuncMap[eventType].Count<char>() != 0;
		}

		public virtual void keep()
		{
		}

		public void registerScriptTouchHandler(int eventType, string pszScriptFunctionName)
		{
			if (this.m_pEventTypeFuncMap == null)
			{
				this.m_pEventTypeFuncMap = new Dictionary<int, string>();
			}
			this.m_pEventTypeFuncMap[eventType] = pszScriptFunctionName;
		}
	}
}