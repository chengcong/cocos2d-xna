namespace cocos2d
{
    using System;

    public class CCSpeed : CCAction
    {
        protected float m_fSpeed;
        protected CCActionInterval m_pInnerAction;

        public static CCSpeed actionWithAction(CCActionInterval action, float fRate)
        {
            CCSpeed speed = new CCSpeed();
            if ((speed != null) && speed.initWithAction(action, fRate))
            {
                return speed;
            }
            return null;
        }

        public override CCObject copyWithZone(CCZone zone)
        {
            CCZone zone2 = null;
            CCSpeed pCopyObject = null;
            if ((zone2 != null) && (zone2.m_pCopyObject != null))
            {
                pCopyObject = (CCSpeed)zone2.m_pCopyObject;
            }
            else
            {
                pCopyObject = new CCSpeed();
                zone2 = new CCZone(pCopyObject);
            }
            base.copyWithZone(zone);
            pCopyObject.initWithAction((CCActionInterval)this.m_pInnerAction.copy(), this.m_fSpeed);
            return pCopyObject;
        }

        public bool initWithAction(CCActionInterval action, float fRate)
        {
            this.m_pInnerAction = action;
            this.m_fSpeed = fRate;
            return true;
        }

        public override bool isDone()
        {
            return this.m_pInnerAction.isDone();
        }

        public virtual CCActionInterval reverse()
        {
            return (CCActionInterval)this.m_pInnerAction.reverse();
        }

        public override void startWithTarget(CCNode target)
        {
            base.startWithTarget(target);
            this.m_pInnerAction.startWithTarget(target);
        }

        public override void step(float dt)
        {
            this.m_pInnerAction.step(dt * this.m_fSpeed);
        }

        public override void stop()
        {
            this.m_pInnerAction.stop();
            base.stop();
        }

        public float speed
        {
            get
            {
                return this.m_fSpeed;
            }
            set
            {
                this.m_fSpeed = value;
            }
        }
    }
}
