using System;

namespace cocos2d
{
	public interface ICCTextureProtocol : ICCBlendProtocol
	{
		CCTexture2D Texture
		{
			get;
			set;
		}
	}
}