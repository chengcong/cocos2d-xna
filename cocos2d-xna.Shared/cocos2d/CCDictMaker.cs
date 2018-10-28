using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cocos2d
{
	public class CCDictMaker : ICCSAXDelegator
	{
		public Dictionary<string, object> m_pRootDict;

		public Dictionary<string, object> m_pCurDict;

		public Stack<Dictionary<string, object>> m_tDictStack = new Stack<Dictionary<string, object>>();

		public string m_sCurKey;

		public CCSAXState m_tState;

		public List<object> m_pArray;

		private Stack<List<object>> m_tArrayStack = new Stack<List<object>>();

		private Stack<CCSAXState> m_tStateStack = new Stack<CCSAXState>();

		public CCDictMaker()
		{
			this.m_tState = CCSAXState.SAX_NONE;
		}

		public Dictionary<string, object> dictionaryWithContentsOfFile(string pFileName)
		{
			CCSAXParser cCSAXParser = new CCSAXParser();
			if (!cCSAXParser.init("UTF-8"))
			{
				return null;
			}
			cCSAXParser.setDelegator(this);
			cCSAXParser.parse(pFileName);
			return this.m_pRootDict;
		}

		public void endElement(object ctx, string name)
		{
			CCSAXState cCSAXState = (this.m_tStateStack.Count > 0 ? CCSAXState.SAX_DICT : this.m_tStateStack.FirstOrDefault<CCSAXState>());
			string str = name;
			if (str == "dict")
			{
				this.m_tStateStack.Pop();
				this.m_tDictStack.Pop();
				if (this.m_tDictStack.Count > 0)
				{
					this.m_pCurDict = this.m_tDictStack.FirstOrDefault<Dictionary<string, object>>();
				}
			}
			else if (str == "array")
			{
				this.m_tStateStack.Pop();
				this.m_tArrayStack.Pop();
				if (this.m_tArrayStack.Count > 0)
				{
					this.m_pArray = this.m_tArrayStack.FirstOrDefault<List<object>>();
				}
			}
			else if (str == "true")
			{
				string str1 = "1";
				if (CCSAXState.SAX_ARRAY == cCSAXState)
				{
					this.m_pArray.Add(str1);
				}
				else if (CCSAXState.SAX_DICT == cCSAXState)
				{
					this.m_pCurDict.Add(this.m_sCurKey, str1);
				}
			}
			else if (str == "false")
			{
				string str2 = "0";
				if (CCSAXState.SAX_ARRAY == cCSAXState)
				{
					this.m_pArray.Add(str2);
				}
				else if (CCSAXState.SAX_DICT == cCSAXState)
				{
					this.m_pCurDict.Add(this.m_sCurKey, str2);
				}
			}
			this.m_tState = CCSAXState.SAX_NONE;
		}

		public void startElement(object ctx, string name, string[] atts)
		{
			string str = name;
			if (str == "dict")
			{
				this.m_pCurDict = new Dictionary<string, object>();
				if (this.m_pRootDict == null)
				{
					this.m_pRootDict = this.m_pCurDict;
				}
				this.m_tState = CCSAXState.SAX_DICT;
				CCSAXState cCSAXState = CCSAXState.SAX_NONE;
				if (this.m_tStateStack.Count != 0)
				{
					cCSAXState = this.m_tStateStack.FirstOrDefault<CCSAXState>();
				}
				if (CCSAXState.SAX_ARRAY == cCSAXState)
				{
					this.m_pArray.Add(this.m_pCurDict);
				}
				else if (CCSAXState.SAX_DICT == cCSAXState)
				{
					Dictionary<string, object> strs = this.m_tDictStack.FirstOrDefault<Dictionary<string, object>>();
					strs.Add(this.m_sCurKey, this.m_pCurDict);
				}
				this.m_tStateStack.Push(this.m_tState);
				this.m_tDictStack.Push(this.m_pCurDict);
				return;
			}
			if (str == "key")
			{
				this.m_tState = CCSAXState.SAX_KEY;
				return;
			}
			if (str == "integer")
			{
				this.m_tState = CCSAXState.SAX_INT;
				return;
			}
			if (str == "real")
			{
				this.m_tState = CCSAXState.SAX_REAL;
				return;
			}
			if (str == "string")
			{
				this.m_tState = CCSAXState.SAX_STRING;
				return;
			}
			if (str != "array")
			{
				this.m_tState = CCSAXState.SAX_NONE;
				return;
			}
			this.m_tState = CCSAXState.SAX_ARRAY;
			this.m_pArray = new List<object>();
			CCSAXState cCSAXState1 = (this.m_tStateStack.Count == 0 ? CCSAXState.SAX_DICT : this.m_tStateStack.FirstOrDefault<CCSAXState>());
			if (cCSAXState1 == CCSAXState.SAX_DICT)
			{
				this.m_pCurDict.Add(this.m_sCurKey, this.m_pArray);
			}
			else if (cCSAXState1 == CCSAXState.SAX_ARRAY)
			{
				this.m_tArrayStack.FirstOrDefault<List<object>>().Add(this.m_pArray);
			}
			this.m_tStateStack.Push(this.m_tState);
			this.m_tArrayStack.Push(this.m_pArray);
		}

		public void textHandler(object ctx, byte[] s, int len)
		{
			if (this.m_tState == CCSAXState.SAX_NONE)
			{
				return;
			}
			CCSAXState cCSAXState = (this.m_tStateStack.Count == 0 ? CCSAXState.SAX_DICT : this.m_tStateStack.FirstOrDefault<CCSAXState>());
			string empty = string.Empty;
			empty = Encoding.UTF8.GetString(s, 0, len);
			switch (this.m_tState)
			{
				case CCSAXState.SAX_KEY:
				{
					this.m_sCurKey = empty;
					return;
				}
				case CCSAXState.SAX_DICT:
				{
					return;
				}
				case CCSAXState.SAX_INT:
				case CCSAXState.SAX_REAL:
				case CCSAXState.SAX_STRING:
				{
					if (CCSAXState.SAX_ARRAY == cCSAXState)
					{
						this.m_pArray.Add(empty);
						return;
					}
					if (CCSAXState.SAX_DICT != cCSAXState)
					{
						return;
					}
					this.m_pCurDict.Add(this.m_sCurKey, empty);
					return;
				}
				default:
				{
					return;
				}
			}
		}
	}
}