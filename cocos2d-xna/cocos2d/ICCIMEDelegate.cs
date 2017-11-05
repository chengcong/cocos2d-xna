using System;

namespace cocos2d
{
	public interface ICCIMEDelegate
	{
		bool attachWithIME();

		bool canAttachWithIME();

		bool canDetachWithIME();

		void deleteBackward();

		bool detachWithIME();

		void didAttachWithIME();

		void didDetachWithIME();

		string getContentText();

		void insertText(string text, int len);

		void keyboardDidHide(CCIMEKeyboardNotificationInfo info);

		void keyboardDidShow(CCIMEKeyboardNotificationInfo info);

		void keyboardWillHide(CCIMEKeyboardNotificationInfo info);

		void keyboardWillShow(CCIMEKeyboardNotificationInfo info);
	}
}