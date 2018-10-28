using cocos2d.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

namespace cocos2d
{
	public class CCSAXParser
	{
		private ICCSAXDelegator m_pDelegator;

		public CCSAXParser()
		{
		}

		public static void endElement(object ctx, string name)
		{
			((CCSAXParser)ctx).m_pDelegator.endElement(ctx, name);
		}

		public bool init(string pszEncoding)
		{
			return true;
		}

		public bool parse(string pszFile)
		{
#if WINDOWS_UWP
#else
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
#endif
			CCContent cCContent = CCApplication.sharedApplication().content.Load<CCContent>(pszFile);
			string content = cCContent.Content;
			if (cCContent == null)
			{
				return false;
			}
			TextReader stringReader = new System.IO.StringReader(content);
			XmlReaderSettings xmlReaderSetting = new XmlReaderSettings()
			{
				DtdProcessing = DtdProcessing.Ignore
			};
			XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSetting);
			int num = 0;
			int num1 = 0;
			int num2 = 0;
			while (xmlReader.Read())
			{
				string name = xmlReader.Name;
				XmlNodeType nodeType = xmlReader.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					string[] value = null;
					if (name == "map")
					{
						num1 = ccUtils.ccParseInt(xmlReader.GetAttribute("width"));
						num2 = ccUtils.ccParseInt(xmlReader.GetAttribute("height"));
					}
					if (xmlReader.HasAttributes)
					{
						value = new string[xmlReader.AttributeCount * 2];
						xmlReader.MoveToFirstAttribute();
						int num3 = 0;
						value[0] = xmlReader.Name;
						value[1] = xmlReader.Value;
						num3 = num3 + 2;
						while (xmlReader.MoveToNextAttribute())
						{
							value[num3] = xmlReader.Name;
							value[num3 + 1] = xmlReader.Value;
							num3 = num3 + 2;
						}
						xmlReader.MoveToElement();
					}
					CCSAXParser.startElement(this, name, value);
					byte[] bytes = null;
					if (name == "data")
					{
						if (value != null)
						{
							string str = "";
							for (int i = 0; i < (int)value.Length; i++)
							{
								if (value[i] == "encoding")
								{
									str = value[i + 1];
								}
							}
							if (str != "base64")
							{
								string str1 = xmlReader.ReadElementContentAsString();
								bytes = Encoding.UTF8.GetBytes(str1);
							}
							else
							{
								int num4 = num1 * num2 * 4 + 1024;
								bytes = new byte[num4];
								xmlReader.ReadElementContentAsBase64(bytes, 0, num4);
							}
						}
						CCSAXParser.textHandler(this, bytes, (int)bytes.Length);
						CCSAXParser.endElement(this, name);
					}
					else if (name == "key" || name == "integer" || name == "real" || name == "string" || name == "true" || name == "false")
					{
						string str2 = xmlReader.ReadElementContentAsString();
						bytes = Encoding.UTF8.GetBytes(str2);
						CCSAXParser.textHandler(this, bytes, (int)bytes.Length);
						CCSAXParser.endElement(this, name);
					}
					else
					{
						IXmlLineInfo xmlLineInfo = (IXmlLineInfo)xmlReader;
						object[] lineNumber = new object[] { "Failed to handle XML tag: ", name, " in ", xmlLineInfo.LineNumber, "@", xmlLineInfo.LinePosition, ":", pszFile };
						CCLog.Log(string.Concat(lineNumber));
					}
				}
				else if (nodeType == XmlNodeType.EndElement)
				{
					CCSAXParser.endElement(this, xmlReader.Name);
					num++;
				}
			}
			return true;
		}

		public void setDelegator(ICCSAXDelegator pDelegator)
		{
			this.m_pDelegator = pDelegator;
		}

		public static void startElement(object ctx, string name, string[] atts)
		{
			((CCSAXParser)ctx).m_pDelegator.startElement(ctx, name, atts);
		}

		public static void textHandler(object ctx, byte[] ch, int len)
		{
			((CCSAXParser)ctx).m_pDelegator.textHandler(ctx, ch, len);
		}
	}
}