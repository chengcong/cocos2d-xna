namespace cocos2d
{
    using System;
    using System.Collections.Generic;

    public class CCAnimate : CCActionInterval
    {
        private bool m_bRestoreOriginalFrame;
        private CCAnimation m_pAnimation;
        private CCSpriteFrame m_pOrigFrame;

        public static CCAnimate actionWithAnimation(CCAnimation pAnimation)
        {
            CCAnimate animate = new CCAnimate();
            animate.initWithAnimation(pAnimation, true);
            return animate;
        }

        public static CCAnimate actionWithAnimation(CCAnimation pAnimation, bool bRestoreOriginalFrame)
        {
            CCAnimate animate = new CCAnimate();
            animate.initWithAnimation(pAnimation, bRestoreOriginalFrame);
            return animate;
        }

        public static CCAnimate actionWithDuration(float duration, CCAnimation pAnimation, bool bRestoreOriginalFrame)
        {
            CCAnimate animate = new CCAnimate();
            animate.initWithDuration(duration, pAnimation, bRestoreOriginalFrame);
            return animate;
        }

        public override CCObject copyWithZone(CCZone pZone)
        {
            CCAnimate pCopyObject = null;
            if ((pZone != null) && (pZone.m_pCopyObject != null))
            {
                pCopyObject = (CCAnimate)pZone.m_pCopyObject;
            }
            else
            {
                pCopyObject = new CCAnimate();
                pZone = new CCZone(pCopyObject);
            }
            base.copyWithZone(pZone);
            pCopyObject.initWithDuration(base.m_fDuration, this.m_pAnimation, this.m_bRestoreOriginalFrame);
            return pCopyObject;
        }

        ~CCAnimate()
        {
        }

        public bool initWithAnimation(CCAnimation pAnimation)
        {
            return this.initWithAnimation(pAnimation, true);
        }

        public bool initWithAnimation(CCAnimation pAnimation, bool bRestoreOriginalFrame)
        {
            if (base.initWithDuration(pAnimation.getFrames().Count * pAnimation.getDelay()))
            {
                this.m_bRestoreOriginalFrame = bRestoreOriginalFrame;
                this.m_pAnimation = pAnimation;
                this.m_pOrigFrame = null;
                return true;
            }
            return false;
        }

        public bool initWithDuration(float duration, CCAnimation pAnimation, bool bRestoreOriginalFrame)
        {
            if (base.initWithDuration(duration))
            {
                this.m_bRestoreOriginalFrame = bRestoreOriginalFrame;
                this.m_pAnimation = pAnimation;
                this.m_pOrigFrame = null;
                return true;
            }
            return false;
        }

        public override CCFiniteTimeAction reverse()
        {
            List<CCSpriteFrame> list = this.m_pAnimation.getFrames();
            List<CCSpriteFrame> frames = new List<CCSpriteFrame>(list.Count);
            if (list.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    CCSpriteFrame frame = list[i];
                    if (frame == null)
                    {
                        break;
                    }
                    frames.Insert((list.Count - 1) - i, (CCSpriteFrame)frame.copy());
                }
            }
            CCAnimation pAnimation = CCAnimation.animationWithFrames(frames, this.m_pAnimation.getDelay());
            return actionWithDuration(base.m_fDuration, pAnimation, this.m_bRestoreOriginalFrame);
        }

        public override void startWithTarget(CCNode pTarget)
        {
            base.startWithTarget(pTarget);
            CCSprite sprite = (CCSprite)pTarget;
            if (this.m_bRestoreOriginalFrame)
            {
                this.m_pOrigFrame = sprite.DisplayFrame;
            }
        }

        public override void stop()
        {
            if (this.m_bRestoreOriginalFrame && (base.m_pTarget != null))
            {
                ((CCSprite)base.m_pTarget).DisplayFrame = this.m_pOrigFrame;
            }
            base.stop();
        }

        public override void update(float time)
        {
            List<CCSpriteFrame> list = this.m_pAnimation.getFrames();
            int count = list.Count;
            int num2 = (int)(time * count);
            if (num2 >= count)
            {
                num2 = count - 1;
            }
            CCSprite pTarget = (CCSprite)base.m_pTarget;
            if (!pTarget.isFrameDisplayed(list[num2]))
            {
                pTarget.DisplayFrame = list[num2];
            }
        }
    }
}
