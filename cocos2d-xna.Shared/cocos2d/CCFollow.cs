namespace cocos2d
{
    using System;

    public class CCFollow : CCAction
    {
        protected bool m_bBoundaryFullyCovered;
        protected bool m_bBoundarySet;
        protected float m_fBottomBoundary;
        protected float m_fLeftBoundary;
        protected float m_fRightBoundary;
        protected float m_fTopBoundary;
        protected CCPoint m_obFullScreenSize;
        protected CCPoint m_obHalfScreenSize;
        protected CCNode m_pobFollowedNode;

        public static CCFollow actionWithTarget(CCNode followedNode)
        {
            CCFollow follow = new CCFollow();
            if ((follow != null) && follow.initWithTarget(followedNode))
            {
                return follow;
            }
            return null;
        }

        public static CCFollow actionWithTarget(CCNode followedNode, CCRect rect)
        {
            CCFollow follow = new CCFollow();
            if ((follow != null) && follow.initWithTarget(followedNode, rect))
            {
                return follow;
            }
            return null;
        }

        public override CCObject copyWithZone(CCZone zone)
        {
            CCZone zone2 = zone;
            CCFollow pCopyObject = null;
            if ((zone2 != null) && (zone2.m_pCopyObject != null))
            {
                pCopyObject = (CCFollow)zone2.m_pCopyObject;
            }
            else
            {
                pCopyObject = new CCFollow();
                zone2 = new CCZone(pCopyObject);
            }
            base.copyWithZone(zone2);
            pCopyObject.m_nTag = base.m_nTag;
            return pCopyObject;
        }

        ~CCFollow()
        {
        }

        public bool initWithTarget(CCNode followedNode)
        {
            this.m_pobFollowedNode = followedNode;
            this.m_bBoundarySet = false;
            this.m_bBoundaryFullyCovered = false;
            CCSize size = CCDirector.sharedDirector().getWinSize();
            this.m_obFullScreenSize = new CCPoint(size.width, size.height);
            this.m_obHalfScreenSize = CCPointExtension.ccpMult(this.m_obFullScreenSize, 0.5f);
            return true;
        }

        public bool initWithTarget(CCNode followedNode, CCRect rect)
        {
            this.m_pobFollowedNode = followedNode;
            this.m_bBoundarySet = true;
            this.m_bBoundaryFullyCovered = false;
            CCSize size = CCDirector.sharedDirector().getWinSize();
            this.m_obFullScreenSize = new CCPoint(size.width, size.height);
            this.m_obHalfScreenSize = CCPointExtension.ccpMult(this.m_obFullScreenSize, 0.5f);
            this.m_fLeftBoundary = -((rect.origin.x + rect.size.width) - this.m_obFullScreenSize.x);
            this.m_fRightBoundary = -rect.origin.x;
            this.m_fLeftBoundary = -rect.origin.y;
            this.m_fBottomBoundary = -((rect.origin.y + rect.size.height) - this.m_obFullScreenSize.y);
            if (this.m_fRightBoundary < this.m_fLeftBoundary)
            {
                this.m_fRightBoundary = this.m_fLeftBoundary = (this.m_fLeftBoundary + this.m_fRightBoundary) / 2f;
            }
            if (this.m_fTopBoundary < this.m_fBottomBoundary)
            {
                this.m_fTopBoundary = this.m_fBottomBoundary = (this.m_fTopBoundary + this.m_fBottomBoundary) / 2f;
            }
            if ((this.m_fTopBoundary == this.m_fBottomBoundary) && (this.m_fLeftBoundary == this.m_fRightBoundary))
            {
                this.m_bBoundaryFullyCovered = true;
            }
            return true;
        }

        public override bool isDone()
        {
            return !this.m_pobFollowedNode.isRunning;
        }

        public override void step(float dt)
        {
            if (this.m_bBoundarySet)
            {
                if (!this.m_bBoundaryFullyCovered)
                {
                    CCPoint point = CCPointExtension.ccpSub(this.m_obHalfScreenSize, this.m_pobFollowedNode.position);
                    base.m_pTarget.position = CCPointExtension.ccp(CCPointExtension.clampf(point.x, this.m_fLeftBoundary, this.m_fRightBoundary), CCPointExtension.clampf(point.y, this.m_fBottomBoundary, this.m_fTopBoundary));
                }
            }
            else
            {
                base.m_pTarget.position = CCPointExtension.ccpSub(this.m_obHalfScreenSize, this.m_pobFollowedNode.position);
            }
        }

        public override void stop()
        {
            base.m_pTarget = null;
            base.stop();
        }

        public bool boundarySet
        {
            get
            {
                return this.m_bBoundarySet;
            }
            set
            {
                this.m_bBoundarySet = value;
            }
        }
    }
}
