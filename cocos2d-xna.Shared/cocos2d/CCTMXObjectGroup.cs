using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCTMXObjectGroup : CCObject
	{
		protected CCPoint m_tPositionOffset;

		protected Dictionary<string, string> m_pProperties;

		protected List<Dictionary<string, string>> m_pObjects;

		protected string m_sGroupName;

		public string GroupName
		{
			get
			{
				return this.m_sGroupName;
			}
			set
			{
				this.m_sGroupName = value;
			}
		}

		public List<Dictionary<string, string>> Objects
		{
			get
			{
				return this.m_pObjects;
			}
			set
			{
				this.m_pObjects = value;
			}
		}

		public CCPoint PositionOffset
		{
			get
			{
				return this.m_tPositionOffset;
			}
			set
			{
				this.m_tPositionOffset = value;
			}
		}

		public Dictionary<string, string> Properties
		{
			get
			{
				return this.m_pProperties;
			}
			set
			{
				this.m_pProperties = value;
			}
		}

		public CCTMXObjectGroup()
		{
			this.m_pObjects = new List<Dictionary<string, string>>();
			this.m_pProperties = new Dictionary<string, string>();
		}

		private Dictionary<string, string> objectNamed(string objectName)
		{
			if (this.m_pObjects != null && this.m_pObjects.Count > 0)
			{
				for (int i = 0; i < this.m_pObjects.Count; i++)
				{
					string item = this.m_pObjects[i]["name"];
					if (item != null && item == objectName)
					{
						return this.m_pObjects[i];
					}
				}
			}
			return null;
		}

		private string propertyNamed(string propertyName)
		{
			return this.m_pProperties[propertyName];
		}
	}
}