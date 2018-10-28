using System;

namespace cocos2d
{
	public enum CCSAXState
	{
		SAX_NONE,
		SAX_KEY,
		SAX_DICT,
		SAX_INT,
		SAX_REAL,
		SAX_STRING,
		SAX_ARRAY
	}
}