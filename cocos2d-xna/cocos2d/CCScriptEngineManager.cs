using System;
using System.Runtime.CompilerServices;

namespace cocos2d
{
	public class CCScriptEngineManager
	{
		private CCScriptEngineProtocol m_pScriptEngine;

		public CCScriptEngineProtocol ScriptEngine
		{
			get;
			set;
		}

		private CCScriptEngineManager()
		{
		}

		public void removeScriptEngine()
		{
			throw new NotImplementedException();
		}

		public static CCScriptEngineManager sharedScriptEngineManager()
		{
			throw new NotImplementedException();
		}
	}
}