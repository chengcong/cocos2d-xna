namespace cocos2d
{
    using System;

    public class CCFiniteTimeAction : CCAction
    {
        protected float m_fDuration;

        ~CCFiniteTimeAction()
        {
        }

        public virtual CCFiniteTimeAction reverse()
        {
            CCLog.Log("cocos2d: FiniteTimeAction#reverse: Implement me");
            return null;
        }

        public float duration
        {
            get
            {
                return this.m_fDuration;
            }
            set
            {
                this.m_fDuration = value;
            }
        }
    }
}
