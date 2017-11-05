using System;

namespace cocos2d
{
	public interface ICCRGBAProtocol
	{
		ccColor3B Color
		{
			get;
			set;
		}

		bool IsOpacityModifyRGB
		{
			get;
			set;
		}

		byte Opacity
		{
			get;
			set;
		}
	}
}