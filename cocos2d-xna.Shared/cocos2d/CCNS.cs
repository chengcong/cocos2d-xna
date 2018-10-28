using System;
using System.Collections.Generic;

namespace cocos2d
{
	public static class CCNS
	{
		public static CCPoint CCPointFromString(string pszContent)
		{
			CCPoint cCPoint = new CCPoint();
			List<string> strs = new List<string>();
			if (CCNS.splitWithForm(pszContent, strs))
			{
				float single = ccUtils.ccParseFloat(strs[0]);
				float single1 = ccUtils.ccParseFloat(strs[1]);
				cCPoint = new CCPoint(single, single1);
			}
			return cCPoint;
		}

		public static CCRect CCRectFromString(string pszContent)
		{
			CCRect cCRect = new CCRect(0f, 0f, 0f, 0f);
			if (pszContent != null)
			{
				string str = pszContent;
				int num = str.IndexOf('{');
				int num1 = str.IndexOf('}');
				for (int i = 1; i < 3 && num1 != -1; i++)
				{
					num1 = str.IndexOf('}', num1 + 1);
				}
				if (num != -1 && num1 != -1)
				{
					str = str.Substring(num + 1, num1 - num - 1);
					int num2 = str.IndexOf('}');
					if (num2 != -1)
					{
						num2 = str.IndexOf(',', num2);
						if (num2 != -1)
						{
							string str1 = str.Substring(0, num2);
							string str2 = str.Substring(num2 + 1);
							List<string> strs = new List<string>();
							if (CCNS.splitWithForm(str1, strs))
							{
								List<string> strs1 = new List<string>();
								if (CCNS.splitWithForm(str2, strs1))
								{
									float single = ccUtils.ccParseFloat(strs[0]);
									float single1 = ccUtils.ccParseFloat(strs[1]);
									float single2 = ccUtils.ccParseFloat(strs1[0]);
									float single3 = ccUtils.ccParseFloat(strs1[1]);
									cCRect = new CCRect(single, single1, single2, single3);
								}
							}
						}
					}
				}
			}
			return cCRect;
		}

		public static CCSize CCSizeFromString(string pszContent)
		{
			CCSize cCSize = new CCSize();
			List<string> strs = new List<string>();
			if (CCNS.splitWithForm(pszContent, strs))
			{
				float single = ccUtils.ccParseFloat(strs[0]);
				float single1 = ccUtils.ccParseFloat(strs[1]);
				cCSize = new CCSize(single, single1);
			}
			return cCSize;
		}

		public static void split(string src, string token, List<string> vect)
		{
			int num = 0;
			int length = 0;
			while (num != -1)
			{
				num = src.IndexOf(token, length);
				if (num != -1)
				{
					vect.Add(src.Substring(length, num - length));
				}
				else
				{
					vect.Add(src.Substring(length, src.Length - length));
				}
				length = num + token.Length;
			}
		}

		public static bool splitWithForm(string pStr, List<string> strs)
		{
			bool flag = false;
			if (pStr != null)
			{
				string str = pStr;
				if (str.Length != 0)
				{
					int num = str.IndexOf('{');
					int num1 = str.IndexOf('}');
					if (num != -1 && num1 != -1 && num <= num1)
					{
						string str1 = str.Substring(num + 1, num1 - num - 1);
						if (str1.Length != 0)
						{
							int num2 = str1.IndexOf('{');
							int num3 = str1.IndexOf('}');
							if (num2 == -1 && num3 == -1)
							{
								CCNS.split(str1, ",", strs);
								if (strs.Count != 2 || strs[0].Length == 0 || strs[1].Length == 0)
								{
									strs.Clear();
								}
								else
								{
									flag = true;
								}
							}
						}
					}
				}
			}
			return flag;
		}
	}
}