namespace cocos2d
{
    using System;

    public class CCTouchHandler
    {
        protected int m_nEnabledSelectors;
        protected int m_nPriority;
        protected ICCTouchDelegate m_pDelegate;

        public static CCTouchHandler handlerWithDelegate(ICCTouchDelegate pDelegate, int nPriority)
        {
            CCTouchHandler handler = new CCTouchHandler();
            if (handler.initWithDelegate(pDelegate, nPriority))
            {
                return null;
            }
            return null;
        }

        public virtual bool initWithDelegate(ICCTouchDelegate pDelegate, int nPriority)
        {
            this.m_pDelegate = pDelegate;
            this.m_nPriority = nPriority;
            this.m_nEnabledSelectors = 0;
            return true;
        }

        public ICCTouchDelegate Delegate
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

        public int getEnabledSelectors
        {
            get
            {
                return this.m_nEnabledSelectors;
            }
            set
            {
                this.m_nEnabledSelectors = value;
            }
        }

        public int Priority
        {
            get
            {
                return this.m_nPriority;
            }
            set
            {
                this.m_nPriority = value;
            }
        }
    }
}
