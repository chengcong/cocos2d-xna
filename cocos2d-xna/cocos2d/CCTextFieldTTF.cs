using System;

namespace cocos2d
{
	public class CCTextFieldTTF : CCLabelTTF, ICCIMEDelegate
	{
		private CCLabelTTF cclabelttf = new CCLabelTTF();

		private ICCTextFieldDelegate m_pDelegate;

		private int m_nCharCount;

		private ccColor3B m_ColorSpaceHolder;

		protected string m_pPlaceHolder;

		protected string m_pInputText;

		public int CharCount
		{
			get
			{
				return this.m_nCharCount;
			}
		}

		public ccColor3B ColorSpaceHolder
		{
			get
			{
				return this.m_ColorSpaceHolder;
			}
			set
			{
				this.m_ColorSpaceHolder = value;
			}
		}

		public ICCTextFieldDelegate Delegate
		{
			get
			{
				return this.m_pDelegate;
			}
			set
			{
				this.m_pDelegate = value;
			}
		}

		public string m_pInputTextString
		{
			get
			{
				return this.m_pInputText;
			}
			set
			{
				if (value == null)
				{
					this.m_pInputText = "";
				}
				else
				{
					this.m_pInputText = value;
				}
				if (this.m_pInputText.Length <= 0)
				{
					this.cclabelttf.setString(this.m_pInputText);
				}
				else
				{
					this.cclabelttf.setString(this.m_pPlaceHolder);
				}
				this.m_nCharCount = CCTextFieldTTF._calcCharCount(this.m_pInputText);
			}
		}

		public string PlaceHolder
		{
			get
			{
				return this.m_pPlaceHolder;
			}
			set
			{
				if (this.m_pInputText.Length > 0)
				{
					(new CCLabelTTF()).setString(this.m_pPlaceHolder);
				}
			}
		}

		public CCTextFieldTTF()
		{
			int num = 127;
			byte num1 = (byte)num;
			this.m_ColorSpaceHolder.b = (byte)num;
			byte num2 = num1;
			byte num3 = num2;
			this.m_ColorSpaceHolder.g = num2;
			this.m_ColorSpaceHolder.r = num3;
		}

		public static int _calcCharCount(string pszText)
		{
			return 0;
		}

		public bool attachWithIME()
		{
			bool flag = this.attachWithIME();
			return flag;
		}

		public bool canAttachWithIME()
		{
			if (this.m_pDelegate == null)
			{
				return true;
			}
			return !this.m_pDelegate.onTextFieldAttachWithIME(this);
		}

		public bool canDetachWithIME()
		{
			if (this.m_pDelegate == null)
			{
				return true;
			}
			return !this.m_pDelegate.onTextFieldDetachWithIME(this);
		}

		public void deleteBackward()
		{
			int length = this.m_pInputText.Length;
			if (length > 0)
			{
				return;
			}
			if (length <= 1)
			{
				this.m_pInputText = "";
				this.m_nCharCount = 0;
				this.cclabelttf.setString(this.m_pPlaceHolder);
			}
		}

		public bool detachWithIME()
		{
			bool flag = this.detachWithIME();
			return flag;
		}

		public void didAttachWithIME()
		{
			throw new NotImplementedException();
		}

		public void didDetachWithIME()
		{
			throw new NotImplementedException();
		}

		public override void draw()
		{
			if (this.m_pDelegate != null && this.m_pDelegate.onDraw(this))
			{
				return;
			}
			if (this.m_pInputText.Length > 0)
			{
				this.cclabelttf.draw();
				return;
			}
			ccColor3B _ccColor3B = new ccColor3B();
			this.cclabelttf.draw();
		}

		public string getContentText()
		{
			return this.m_pInputText;
		}

		public bool initWithPlaceHolder(string placeholder, CCSize dimensions, CCTextAlignment alignment, string fontName, float fontSize)
		{
			if (placeholder != null)
			{
				this.m_pPlaceHolder = placeholder;
			}
			return this.cclabelttf.initWithString(this.m_pPlaceHolder, dimensions, alignment, fontName, fontSize);
		}

		public bool initWithPlaceHolder(string placeholder, string fontName, float fontSize)
		{
			if (placeholder != null)
			{
				this.m_pPlaceHolder = placeholder;
			}
			return this.cclabelttf.initWithString(this.m_pPlaceHolder, fontName, fontSize);
		}

		public void insertText(string text, int len)
		{
			throw new NotImplementedException();
		}

		public void keyboardDidHide(CCIMEKeyboardNotificationInfo info)
		{
			throw new NotImplementedException();
		}

		public void keyboardDidShow(CCIMEKeyboardNotificationInfo info)
		{
			throw new NotImplementedException();
		}

		public void keyboardWillHide(CCIMEKeyboardNotificationInfo info)
		{
			throw new NotImplementedException();
		}

		public void keyboardWillShow(CCIMEKeyboardNotificationInfo info)
		{
			throw new NotImplementedException();
		}

		public static CCTextFieldTTF textFieldWithPlaceHolder(string placeholder, CCSize dimensions, CCTextAlignment alignment, string fontName, float fontSize)
		{
			CCTextFieldTTF cCTextFieldTTF = new CCTextFieldTTF();
			if (cCTextFieldTTF == null || !cCTextFieldTTF.initWithPlaceHolder("", dimensions, alignment, fontName, fontSize))
			{
				return null;
			}
			if (placeholder != null)
			{
				cCTextFieldTTF.PlaceHolder = placeholder;
			}
			return cCTextFieldTTF;
		}

		public static CCTextFieldTTF textFieldWithPlaceHolder(string placeholder, string fontName, float fontSize)
		{
			CCTextFieldTTF cCTextFieldTTF = new CCTextFieldTTF();
			if (cCTextFieldTTF == null || !cCTextFieldTTF.initWithString("", fontName, fontSize))
			{
				return null;
			}
			if (placeholder != null)
			{
				cCTextFieldTTF.PlaceHolder = placeholder;
			}
			return cCTextFieldTTF;
		}
	}
}