using System;

namespace cocos2d
{
	public interface ICCSAXDelegator
	{
		void endElement(object ctx, string name);

		void startElement(object ctx, string name, string[] atts);

		void textHandler(object ctx, byte[] ch, int len);
	}
}