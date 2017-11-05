using System;

namespace cocos2d
{
	public interface ICCTextFieldDelegate
	{
		bool onDraw(CCTextFieldTTF sender);

		bool onTextFieldAttachWithIME(CCTextFieldTTF sender);

		bool onTextFieldDeleteBackward(CCTextFieldTTF sender, string delText, int nLen);

		bool onTextFieldDetachWithIME(CCTextFieldTTF sender);

		bool onTextFieldInsertText(CCTextFieldTTF sender, string text, int nLen);
	}
}