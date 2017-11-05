using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCScriptEngineProtocol
	{
		public CCScriptEngineProtocol()
		{
		}

		public virtual bool addSearchPath(string pszPath)
		{
			return false;
		}

		public virtual bool executeCallFunc(string pszFuncName)
		{
			return false;
		}

		public virtual bool executeCallFunc0(string pszFuncName, CCObject pObject)
		{
			return false;
		}

		public virtual bool executeCallFuncN(string pszFuncName, CCNode pNode)
		{
			return false;
		}

		public virtual bool executeCallFuncND(string pszFuncName, CCNode pNode, object pData)
		{
			return false;
		}

		public virtual int executeFuction(string pszFuncName)
		{
			return 0;
		}

		public virtual bool executeSchedule(string pszFuncName, float t)
		{
			return false;
		}

		public virtual bool executeScriptFile(string pszFileName)
		{
			return false;
		}

		public virtual bool executeString(string pszCodes)
		{
			return false;
		}

		public virtual bool executeTouchesEvent(string pszFuncName, List<CCTouch> pTouches)
		{
			return false;
		}

		public virtual bool executeTouchEvent(string pszFuncName, CCTouch pTouch)
		{
			return false;
		}
	}
}