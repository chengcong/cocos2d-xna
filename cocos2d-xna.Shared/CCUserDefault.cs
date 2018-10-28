using cocos2d;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Xml;

public class CCUserDefault
{
	private static CCUserDefault m_spUserDefault;

	private static string USERDEFAULT_ROOT_NAME;

	private static string XML_FILE_NAME;

	private IsolatedStorageFile myIsolatedStorage;

	private Dictionary<string, string> values = new Dictionary<string, string>();

	static CCUserDefault()
	{
		CCUserDefault.m_spUserDefault = null;
		CCUserDefault.USERDEFAULT_ROOT_NAME = "userDefaultRoot";
		CCUserDefault.XML_FILE_NAME = "UserDefault.xml";
	}

	private CCUserDefault()
	{
		this.myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
		if (!this.isXMLFileExist())
		{
			this.createXMLFile();
		}
		using (IsolatedStorageFileStream isolatedStorageFileStream = this.myIsolatedStorage.OpenFile(CCUserDefault.XML_FILE_NAME, FileMode.Open, FileAccess.Read))
		{
			this.parseXMLFile(isolatedStorageFileStream);
		}
	}

	private bool createXMLFile()
	{
		bool flag = false;
		using (StreamWriter streamWriter = new StreamWriter(new IsolatedStorageFileStream(CCUserDefault.XML_FILE_NAME, FileMode.Create, FileAccess.Write, this.myIsolatedStorage)))
		{
			streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?><userDefaultRoot></userDefaultRoot>");
#if WINDOWS_UWP
            streamWriter.Dispose();
#else
            streamWriter.Close();
#endif
        }
		return flag;
	}

	public void flush()
	{
		using (StreamWriter streamWriter = new StreamWriter(new IsolatedStorageFileStream(CCUserDefault.XML_FILE_NAME, FileMode.Create, FileAccess.Write, this.myIsolatedStorage)))
		{
			XmlWriterSettings xmlWriterSetting = new XmlWriterSettings()
			{
				Encoding = Encoding.UTF8,
				Indent = true
			};
			using (XmlWriter xmlWriter = XmlWriter.Create(streamWriter, xmlWriterSetting))
			{
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement(CCUserDefault.USERDEFAULT_ROOT_NAME);
				foreach (KeyValuePair<string, string> value in this.values)
				{
					xmlWriter.WriteStartElement(value.Key);
					xmlWriter.WriteString(value.Value);
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndDocument();
			}
		}
	}

	public bool getBoolForKey(string pKey, bool defaultValue)
	{
		string valueForKey = this.getValueForKey(pKey);
		bool flag = defaultValue;
		if (valueForKey != null)
		{
			flag = bool.Parse(valueForKey);
		}
		return flag;
	}

	public double getDoubleForKey(string pKey, double defaultValue)
	{
		string valueForKey = this.getValueForKey(pKey);
		double num = defaultValue;
		if (valueForKey != null)
		{
			num = double.Parse(valueForKey);
		}
		return num;
	}

	public float getFloatForKey(string pKey, float defaultValue)
	{
		return (float)this.getDoubleForKey(pKey, (double)defaultValue);
	}

	public int getIntegerForKey(string pKey, int defaultValue)
	{
		string valueForKey = this.getValueForKey(pKey);
		int num = defaultValue;
		if (valueForKey != null)
		{
			num = ccUtils.ccParseInt(valueForKey);
		}
		return num;
	}

	public string getStringForKey(string pKey, string defaultValue)
	{
		string valueForKey = this.getValueForKey(pKey);
		string str = defaultValue;
		if (valueForKey != null)
		{
			str = valueForKey;
		}
		return str;
	}

	private string getValueForKey(string key)
	{
		string str = null;
		if (!this.values.TryGetValue(key, out str))
		{
			str = null;
		}
		return str;
	}

	private bool isXMLFileExist()
	{
		bool flag = false;
		if (this.myIsolatedStorage.FileExists(CCUserDefault.XML_FILE_NAME))
		{
			flag = true;
		}
		return flag;
	}

	private bool parseXMLFile(IsolatedStorageFileStream xmlFile)
	{
		this.values.Clear();
		string name = "";
		using (XmlReader xmlReader = XmlReader.Create(xmlFile))
		{
			(new XmlWriterSettings()).Indent = false;
			while (xmlReader.Read())
			{
				XmlNodeType nodeType = xmlReader.NodeType;
				switch (nodeType)
				{
					case XmlNodeType.Element:
					{
						name = xmlReader.Name;
						continue;
					}
					case XmlNodeType.Attribute:
					{
						continue;
					}
					case XmlNodeType.Text:
					{
						this.values.Add(name, xmlReader.Value);
						continue;
					}
					default:
					{
						switch (nodeType)
						{
							case XmlNodeType.ProcessingInstruction:
							case XmlNodeType.Comment:
							{
								continue;
							}
							default:
							{
								switch (nodeType)
								{
									default:
									{
										continue;
									}
								}
								break;
							}
						}
						break;
					}
				}
			}
		}
		return true;
	}

	public void purgeSharedUserDefault()
	{
		CCUserDefault.m_spUserDefault = null;
	}

	public void setBoolForKey(string pKey, bool value)
	{
		if (pKey == null)
		{
			return;
		}
		this.setStringForKey(pKey, value.ToString());
	}

	public void setDoubleForKey(string pKey, double value)
	{
		if (pKey == null)
		{
			return;
		}
		this.setValueForKey(pKey, value.ToString());
	}

	public void setFloatForKey(string pKey, float value)
	{
		this.setDoubleForKey(pKey, (double)value);
	}

	public void setIntegerForKey(string pKey, int value)
	{
		if (pKey == null)
		{
			return;
		}
		this.setValueForKey(pKey, value.ToString());
	}

	public void setStringForKey(string pKey, string value)
	{
		if (pKey == null)
		{
			return;
		}
		this.setValueForKey(pKey, value.ToString());
	}

	private void setValueForKey(string key, string value)
	{
		this.values[key] = value;
	}

	public static CCUserDefault sharedUserDefault()
	{
		if (CCUserDefault.m_spUserDefault == null)
		{
			CCUserDefault.m_spUserDefault = new CCUserDefault();
		}
		return CCUserDefault.m_spUserDefault;
	}
}