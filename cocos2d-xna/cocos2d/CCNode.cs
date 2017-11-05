namespace cocos2d
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;

    public class CCNode : CCObject, SelectorProtocol
    {
        private static int kCCNodeTagInvalid = -1;
        protected bool m_bIsInverseDirty = true;
        protected bool m_bIsRelativeAnchorPoint = true;
        protected bool m_bIsRunning;
        protected bool m_bIsTransformDirty = true;
        protected bool m_bIsTransformGLDirty = true;
        protected bool m_bIsVisible = true;
        protected float m_fRotation;
        protected float m_fScaleX = 1f;
        protected float m_fScaleY = 1f;
        protected float m_fSkewX;
        protected float m_fSkewY;
        protected float m_fVertexZ;
        protected int m_nTag = kCCNodeTagInvalid;
        protected int m_nZOrder;
        private CCCamera m_pCamera;
        protected List<CCNode> m_pChildren = new List<CCNode>();
        private CCGridBase m_pGrid;
        protected CCNode m_pParent;
        private float[] m_pTransformGL = new float[0x10];
        protected object m_pUserData;
        protected CCPoint m_tAnchorPoint = new CCPoint();
        protected CCPoint m_tAnchorPointInPixels = new CCPoint();
        protected Matrix m_tCCNodeTransform;
        protected CCSize m_tContentSize = new CCSize();
        protected CCSize m_tContentSizeInPixels = new CCSize();
        protected CCAffineTransform m_tInverse;
        protected CCPoint m_tPosition = new CCPoint();
        protected CCPoint m_tPositionInPixels = new CCPoint();
        protected CCAffineTransform m_tTransform;
        protected Matrix m_tView;

        public virtual void addChild(CCNode child)
        {
            if (child == null)
            {
                throw new ArgumentNullException("child", "Child can not be null.");
            }
            this.addChild(child, child.zOrder, child.tag);
        }

        public virtual void addChild(CCNode child, int zOrder)
        {
            if (child == null)
            {
                throw new ArgumentNullException("child", "Child can not be null.");
            }
            this.addChild(child, zOrder, child.tag);
        }

        public virtual void addChild(CCNode child, int zOrder, int tag)
        {
            if (child == null)
            {
                throw new ArgumentNullException("child", "Child can not be null.");
            }
            if (child.m_pParent == null)
            {
                this.insertChild(child, zOrder);
                child.m_nTag = tag;
                child.parent = this;
                if (this.m_bIsRunning)
                {
                    child.onEnter();
                    child.onEnterTransitionDidFinish();
                }
            }
        }

        public CCRect boundingBox()
        {
            return ccMacros.CC_RECT_PIXELS_TO_POINTS(this.boundingBoxInPixels());
        }

        public CCRect boundingBoxInPixels()
        {
            CCRect rect = new CCRect(0f, 0f, this.m_tContentSizeInPixels.width, this.m_tContentSizeInPixels.height);
            return CCAffineTransform.CCRectApplyAffineTransform(rect, this.nodeToParentTransform());
        }

        public virtual void cleanup()
        {
            this.stopAllActions();
            this.unscheduleAllSelectors();
            foreach (CCNode node in this.m_pChildren)
            {
                if (node != null)
                {
                    node.cleanup();
                }
            }
        }

        public CCPoint convertToNodeSpace(CCPoint worldPoint)
        {
            if (CCDirector.sharedDirector().ContentScaleFactor == 1f)
            {
                return CCAffineTransform.CCPointApplyAffineTransform(worldPoint, this.worldToNodeTransform());
            }
            return CCPointExtension.ccpMult(CCAffineTransform.CCPointApplyAffineTransform(CCPointExtension.ccpMult(worldPoint, CCDirector.sharedDirector().ContentScaleFactor), this.worldToNodeTransform()), 1f / CCDirector.sharedDirector().ContentScaleFactor);
        }

        public CCPoint convertToNodeSpaceAR(CCPoint worldPoint)
        {
            CCPoint tAnchorPointInPixels;
            CCPoint point = this.convertToNodeSpace(worldPoint);
            if (CCDirector.sharedDirector().ContentScaleFactor == 1f)
            {
                tAnchorPointInPixels = this.m_tAnchorPointInPixels;
            }
            else
            {
                tAnchorPointInPixels = CCPointExtension.ccpMult(this.m_tAnchorPointInPixels, 1f / CCDirector.sharedDirector().ContentScaleFactor);
            }
            return CCPointExtension.ccpSub(point, tAnchorPointInPixels);
        }

        public CCPoint convertTouchToNodeSpace(CCTouch touch)
        {
            CCPoint obPoint = touch.locationInView(touch.view());
            obPoint = CCDirector.sharedDirector().convertToGL(obPoint);
            return this.convertToNodeSpace(obPoint);
        }

        public CCPoint convertTouchToNodeSpaceAR(CCTouch touch)
        {
            CCPoint obPoint = touch.locationInView(touch.view());
            obPoint = CCDirector.sharedDirector().convertToGL(obPoint);
            return this.convertToNodeSpaceAR(obPoint);
        }

        private CCPoint convertToWindowSpace(CCPoint nodePoint)
        {
            CCPoint obPoint = this.convertToWorldSpace(nodePoint);
            return CCDirector.sharedDirector().convertToUI(obPoint);
        }

        public CCPoint convertToWorldSpace(CCPoint nodePoint)
        {
            if (CCDirector.sharedDirector().ContentScaleFactor == 1f)
            {
                return CCAffineTransform.CCPointApplyAffineTransform(nodePoint, this.nodeToWorldTransform());
            }
            return CCPointExtension.ccpMult(CCAffineTransform.CCPointApplyAffineTransform(CCPointExtension.ccpMult(nodePoint, CCDirector.sharedDirector().ContentScaleFactor), this.nodeToWorldTransform()), 1f / CCDirector.sharedDirector().ContentScaleFactor);
        }

        public CCPoint convertToWorldSpaceAR(CCPoint nodePoint)
        {
            CCPoint tAnchorPointInPixels;
            if (CCDirector.sharedDirector().ContentScaleFactor == 1f)
            {
                tAnchorPointInPixels = this.m_tAnchorPointInPixels;
            }
            else
            {
                tAnchorPointInPixels = CCPointExtension.ccpMult(this.m_tAnchorPointInPixels, 1f / CCDirector.sharedDirector().ContentScaleFactor);
            }
            CCPoint point2 = CCPointExtension.ccpAdd(nodePoint, tAnchorPointInPixels);
            return this.convertToWorldSpace(point2);
        }

        private void detachChild(CCNode child, bool doCleanup)
        {
            if (this.m_bIsRunning)
            {
                child.onExit();
            }
            if (doCleanup)
            {
                child.cleanup();
            }
            child.parent = null;
            this.m_pChildren.Remove(child);
        }

        public virtual void draw()
        {
        }

        ~CCNode()
        {
            foreach (CCNode node in this.m_pChildren)
            {
                node.parent = null;
            }
        }

        public CCAction getActionByTag(int tag)
        {
            return CCActionManager.sharedManager().getActionByTag((uint)tag, this);
        }

        public CCNode getChildByTag(int tag)
        {
            foreach (CCNode node in this.m_pChildren)
            {
                if ((node != null) && (node.m_nTag == tag))
                {
                    return node;
                }
            }
            return null;
        }

        private void insertChild(CCNode child, int z)
        {
            CCNode node = (this.m_pChildren.Count > 0) ? this.m_pChildren[this.m_pChildren.Count - 1] : null;
            if ((node == null) || (node.zOrder <= z))
            {
                this.m_pChildren.Add(child);
            }
            else
            {
                int index = 0;
                foreach (CCNode node2 in this.m_pChildren)
                {
                    if ((node2 != null) && (node2.m_nZOrder > z))
                    {
                        this.m_pChildren.Insert(index, child);
                        break;
                    }
                    index++;
                }
            }
            child.zOrder = z;
        }

        public static CCNode node()
        {
            return new CCNode();
        }

        public CCAffineTransform nodeToParentTransform()
        {
            if (this.m_bIsTransformDirty)
            {
                this.m_tTransform = CCAffineTransform.CCAffineTransformMakeIdentity();
                if (!this.m_bIsRelativeAnchorPoint && !this.m_tAnchorPointInPixels.IsZero)
                {
                    this.m_tTransform = CCAffineTransform.CCAffineTransformTranslate(this.m_tTransform, this.m_tAnchorPointInPixels.x, this.m_tAnchorPointInPixels.y);
                }
                if (!this.m_tPositionInPixels.IsZero)
                {
                    this.m_tTransform = CCAffineTransform.CCAffineTransformTranslate(this.m_tTransform, this.m_tPositionInPixels.x, this.m_tPositionInPixels.y);
                }
                if (this.m_fRotation != 0f)
                {
                    this.m_tTransform = CCAffineTransform.CCAffineTransformRotate(this.m_tTransform, -ccMacros.CC_DEGREES_TO_RADIANS(this.m_fRotation));
                }
                if ((this.m_fSkewX != 0f) || (this.m_fSkewY != 0f))
                {
                    CCAffineTransform transform = CCAffineTransform.CCAffineTransformMake(1f, (float)Math.Tan((double)ccMacros.CC_DEGREES_TO_RADIANS(this.m_fSkewY)), (float)Math.Tan((double)ccMacros.CC_DEGREES_TO_RADIANS(this.m_fSkewX)), 1f, 0f, 0f);
                    this.m_tTransform = CCAffineTransform.CCAffineTransformConcat(transform, this.m_tTransform);
                }
                if ((this.m_fScaleX != 1f) || (this.m_fScaleY != 1f))
                {
                    this.m_tTransform = CCAffineTransform.CCAffineTransformScale(this.m_tTransform, this.m_fScaleX, this.m_fScaleY);
                }
                if (!this.m_tAnchorPointInPixels.IsZero)
                {
                    this.m_tTransform = CCAffineTransform.CCAffineTransformTranslate(this.m_tTransform, -this.m_tAnchorPointInPixels.x, -this.m_tAnchorPointInPixels.y);
                }
                this.m_bIsTransformDirty = false;
            }
            return this.m_tTransform;
        }

        public CCAffineTransform nodeToWorldTransform()
        {
            CCAffineTransform transform = this.nodeToParentTransform();
            for (CCNode node = this.m_pParent; node != null; node = node.parent)
            {
                CCAffineTransform transform2 = node.nodeToParentTransform();
                transform = CCAffineTransform.CCAffineTransformConcat(transform, transform2);
            }
            return transform;
        }

        public uint numberOfRunningActions()
        {
            return CCActionManager.sharedManager().numberOfRunningActionsInTarget(this);
        }

        public virtual void onEnter()
        {
            foreach (CCNode node in this.m_pChildren)
            {
                if (node != null)
                {
                    node.onEnter();
                }
            }
            this.resumeSchedulerAndActions();
            this.m_bIsRunning = true;
        }

        public virtual void onEnterTransitionDidFinish()
        {
            foreach (CCNode node in this.m_pChildren)
            {
                if (node != null)
                {
                    node.onEnterTransitionDidFinish();
                }
            }
        }

        public virtual void onExit()
        {
            this.pauseSchedulerAndActions();
            this.m_bIsRunning = false;
            foreach (CCNode node in this.m_pChildren)
            {
                if (node != null)
                {
                    node.onExit();
                }
            }
        }

        public CCAffineTransform parentToNodeTransform()
        {
            if (this.m_bIsInverseDirty)
            {
                this.m_tInverse = CCAffineTransform.CCAffineTransformInvert(this.nodeToParentTransform());
                this.m_bIsInverseDirty = false;
            }
            return this.m_tInverse;
        }

        public void pauseSchedulerAndActions()
        {
            CCScheduler.sharedScheduler().pauseTarget(this);
            CCActionManager.sharedManager().pauseTarget(this);
        }

        public virtual void removeAllChildrenWithCleanup(bool cleanup)
        {
            foreach (CCNode node in this.m_pChildren)
            {
                if (node != null)
                {
                    if (this.m_bIsRunning)
                    {
                        node.onExit();
                    }
                    if (cleanup)
                    {
                        node.cleanup();
                    }
                    node.parent = null;
                }
            }
            this.m_pChildren.Clear();
        }

        public virtual void removeChild(CCNode child, bool cleanup)
        {
            if ((this.m_pChildren != null) && this.m_pChildren.Contains(child))
            {
                this.detachChild(child, cleanup);
            }
        }

        public void removeChildByTag(int tag, bool cleanup)
        {
            CCNode child = this.getChildByTag(tag);
            if (child == null)
            {
                CCLog.Log("cocos2d: removeChildByTag: child not found!");
            }
            else
            {
                this.removeChild(child, cleanup);
            }
        }

        public void removeFromParentAndCleanup(bool cleanup)
        {
            this.m_pParent.removeChild(this, cleanup);
        }

        public virtual void reorderChild(CCNode child, int zOrder)
        {
            this.m_pChildren.Remove(child);
            this.insertChild(child, zOrder);
        }

        public void resumeSchedulerAndActions()
        {
            CCScheduler.sharedScheduler().resumeTarget(this);
            CCActionManager.sharedManager().resumeTarget(this);
        }

        public CCAction runAction(CCAction action)
        {
            CCActionManager.sharedManager().addAction(action, this, !this.m_bIsRunning);
            return action;
        }

        public void schedule(SEL_SCHEDULE selector)
        {
            this.schedule(selector, 0f);
        }

        public void schedule(SEL_SCHEDULE selector, float interval)
        {
            CCScheduler.sharedScheduler().scheduleSelector(selector, this, interval, !this.m_bIsRunning);
        }

        public void scheduleUpdate()
        {
            this.scheduleUpdateWithPriority(0);
        }

        public void scheduleUpdateWithPriority(int priority)
        {
            CCScheduler.sharedScheduler().scheduleUpdateForTarget(this, priority, !this.m_bIsRunning);
        }

        [Obsolete("Use scheduleUpdate() instead")]
        public void sheduleUpdate()
        {
            this.scheduleUpdateWithPriority(0);
        }

        public void stopAction(CCAction action)
        {
            CCActionManager.sharedManager().removeAction(action);
        }

        public void stopActionByTag(int tag)
        {
            CCActionManager.sharedManager().removeActionByTag(tag, this);
        }

        public void stopAllActions()
        {
            CCActionManager.sharedManager().removeAllActionsFromTarget(this);
        }

        public void transform()
        {
            CCApplication application = CCApplication.sharedApplication();
            if (this.m_bIsTransformGLDirty)
            {
                TransformUtils.CGAffineToGL(this.nodeToParentTransform(), ref this.m_pTransformGL);
                this.m_bIsTransformGLDirty = false;
            }
            this.m_tCCNodeTransform = TransformUtils.CGAffineToMatrix(this.m_pTransformGL);
            if (this.m_fVertexZ > 0f)
            {
                this.m_tCCNodeTransform *= Matrix.CreateRotationZ(this.m_fVertexZ);
            }
            if ((this.m_pCamera != null) && ((this.m_pGrid == null) || !this.m_pGrid.Active))
            {
                if (this.m_tAnchorPointInPixels.x == 0f)
                {
                    float y = this.m_tAnchorPointInPixels.y;
                }
                Matrix? nullable = this.m_pCamera.locate();
                if (nullable.HasValue)
                {
                    this.m_tCCNodeTransform = ((Matrix.CreateTranslation(-this.m_tAnchorPointInPixels.x, -this.m_tAnchorPointInPixels.y, 0f) * nullable.Value) * Matrix.CreateTranslation(this.m_tAnchorPointInPixels.x, this.m_tAnchorPointInPixels.y, 0f)) * this.m_tCCNodeTransform;
                }
            }
            application.basicEffect.World = this.m_tCCNodeTransform * application.basicEffect.World;
        }

        public void transformAncestors()
        {
            if (this.m_pParent != null)
            {
                this.m_pParent.transformAncestors();
                this.m_pParent.transform();
            }
        }

        public void unschedule(SEL_SCHEDULE selector)
        {
            if (selector != null)
            {
                CCScheduler.sharedScheduler().unscheduleSelector(selector, this);
            }
        }

        public void unscheduleAllSelectors()
        {
            CCScheduler.sharedScheduler().unscheduleAllSelectorsForTarget(this);
        }

        public void unscheduleUpdate()
        {
            CCScheduler.sharedScheduler().unscheduleUpdateForTarget(this);
        }

        [Obsolete("use unscheduleAllSelectors()")]
        public void unsheduleAllSelectors()
        {
            this.unscheduleAllSelectors();
        }

        public virtual void update(float dt)
        {
        }

        public virtual void visit()
        {
            Matrix world = CCApplication.sharedApplication().basicEffect.World;
            if (this.m_bIsVisible)
            {
                CCNode node;
                this.m_tCCNodeTransform = Matrix.Identity;
                Matrix matrix2 = Matrix.Identity * CCApplication.sharedApplication().basicEffect.View;
                if ((this.m_pGrid != null) && this.m_pGrid.Active)
                {
                    this.m_pGrid.beforeDraw();
                    this.transformAncestors();
                }
                this.transform();
                int num = 0;
                if ((this.m_pChildren != null) && (this.m_pChildren.Count > 0))
                {
                    while (num < this.m_pChildren.Count)
                    {
                        node = this.m_pChildren[num];
                        if ((node == null) || (node.m_nZOrder >= 0))
                        {
                            break;
                        }
                        Matrix matrix3 = CCApplication.sharedApplication().basicEffect.World;
                        node.visit();
                        CCApplication.sharedApplication().basicEffect.World = matrix3;
                        num++;
                    }
                }
                this.draw();
                if ((this.m_pChildren != null) && (this.m_pChildren.Count > 0))
                {
                    while (num < this.m_pChildren.Count)
                    {
                        node = this.m_pChildren[num];
                        if (node != null)
                        {
                            Matrix matrix4 = CCApplication.sharedApplication().basicEffect.World;
                            node.visit();
                            CCApplication.sharedApplication().basicEffect.World = matrix4;
                        }
                        num++;
                    }
                }
                if ((this.m_pGrid != null) && this.m_pGrid.Active)
                {
                    this.m_pGrid.afterDraw(this);
                }
                CCApplication.sharedApplication().basicEffect.World = world;
                CCApplication.sharedApplication().basicEffect.View = matrix2;
                CCApplication.sharedApplication().viewMatrix = matrix2;
            }
        }

        public virtual void visitDraw()
        {
            if (this.m_bIsVisible && ((this.m_pGrid != null) && this.m_pGrid.Active))
            {
                this.m_pGrid.blit();
            }
        }

        public CCAffineTransform worldToNodeTransform()
        {
            return CCAffineTransform.CCAffineTransformInvert(this.nodeToWorldTransform());
        }

        public virtual CCPoint anchorPoint
        {
            get
            {
                return this.m_tAnchorPoint;
            }
            set
            {
                if (!CCPoint.CCPointEqualToPoint(value, this.m_tAnchorPoint))
                {
                    this.m_tAnchorPoint = value;
                    this.m_tAnchorPointInPixels = CCPointExtension.ccp(this.m_tContentSizeInPixels.width * this.m_tAnchorPoint.x, this.m_tContentSizeInPixels.height * this.m_tAnchorPoint.y);
                    this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                    this.m_bIsTransformGLDirty = true;
                }
            }
        }

        public CCPoint anchorPointInPixels
        {
            get
            {
                return this.m_tAnchorPointInPixels;
            }
        }

        public CCCamera Camera
        {
            get
            {
                if (this.m_pCamera == null)
                {
                    this.m_pCamera = new CCCamera();
                }
                return this.m_pCamera;
            }
        }

        public List<CCNode> children
        {
            get
            {
                return this.m_pChildren;
            }
        }

        public virtual CCSize contentSize
        {
            get
            {
                return this.m_tContentSize;
            }
            set
            {
                if (!CCSize.CCSizeEqualToSize(value, this.m_tContentSize))
                {
                    this.m_tContentSize = value;
                    if (ccMacros.CC_CONTENT_SCALE_FACTOR() == 1)
                    {
                        this.m_tContentSizeInPixels = this.m_tContentSize;
                    }
                    else
                    {
                        this.m_tContentSizeInPixels = new CCSize(value.width * ccMacros.CC_CONTENT_SCALE_FACTOR(), value.height * ccMacros.CC_CONTENT_SCALE_FACTOR());
                    }
                    this.m_tAnchorPointInPixels = CCPointExtension.ccp(this.m_tContentSizeInPixels.width * this.m_tAnchorPoint.x, this.m_tContentSizeInPixels.height * this.m_tAnchorPoint.y);
                    this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                    this.m_bIsTransformGLDirty = true;
                }
            }
        }

        public CCSize contentSizeInPixels
        {
            get
            {
                return this.m_tContentSizeInPixels;
            }
            set
            {
                if (!CCSize.CCSizeEqualToSize(value, this.m_tContentSizeInPixels))
                {
                    this.m_tContentSizeInPixels = value;
                    if (ccMacros.CC_CONTENT_SCALE_FACTOR() == 1)
                    {
                        this.m_tContentSize = this.m_tContentSizeInPixels;
                    }
                    else
                    {
                        this.m_tContentSize = new CCSize(value.width / ((float)ccMacros.CC_CONTENT_SCALE_FACTOR()), value.height / ((float)ccMacros.CC_CONTENT_SCALE_FACTOR()));
                    }
                    this.m_tAnchorPointInPixels = CCPointExtension.ccp(this.m_tContentSizeInPixels.width * this.m_tAnchorPoint.x, this.m_tContentSizeInPixels.height * this.m_tAnchorPoint.y);
                    this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                    this.m_bIsTransformGLDirty = true;
                }
            }
        }

        public CCGridBase Grid
        {
            get
            {
                return this.m_pGrid;
            }
            set
            {
                this.m_pGrid = value;
            }
        }

        public virtual bool isRelativeAnchorPoint
        {
            get
            {
                return this.m_bIsRelativeAnchorPoint;
            }
            set
            {
                this.m_bIsRelativeAnchorPoint = value;
                this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                this.m_bIsTransformGLDirty = true;
            }
        }

        public bool isRunning
        {
            get
            {
                return this.m_bIsRunning;
            }
        }

        public CCNode parent
        {
            get
            {
                return this.m_pParent;
            }
            set
            {
                this.m_pParent = value;
            }
        }

        public virtual CCPoint position
        {
            get
            {
                return this.m_tPosition;
            }
            set
            {
                this.m_tPosition = value;
                if (ccMacros.CC_CONTENT_SCALE_FACTOR() == 1)
                {
                    this.m_tPositionInPixels = this.m_tPosition;
                }
                else
                {
                    this.m_tPositionInPixels = CCPointExtension.ccpMult(value, (float)ccMacros.CC_CONTENT_SCALE_FACTOR());
                }
                this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                this.m_bIsTransformGLDirty = true;
            }
        }

        public virtual CCPoint positionInPixels
        {
            get
            {
                return this.m_tPositionInPixels;
            }
            set
            {
                this.m_tPositionInPixels = value;
                if (ccMacros.CC_CONTENT_SCALE_FACTOR() == 1)
                {
                    this.m_tPosition = this.m_tPositionInPixels;
                }
                else
                {
                    this.m_tPosition = CCPointExtension.ccpMult(value, (float)(1 / ccMacros.CC_CONTENT_SCALE_FACTOR()));
                }
                this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                this.m_bIsTransformGLDirty = true;
            }
        }

        public virtual float rotation
        {
            get
            {
                return this.m_fRotation;
            }
            set
            {
                this.m_fRotation = value;
                this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                this.m_bIsTransformGLDirty = true;
            }
        }

        public virtual float scale
        {
            get
            {
                return this.m_fScaleX;
            }
            set
            {
                this.m_fScaleX = this.m_fScaleY = value;
                this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                this.m_bIsTransformGLDirty = true;
            }
        }

        public virtual float scaleX
        {
            get
            {
                return this.m_fScaleX;
            }
            set
            {
                this.m_fScaleX = value;
                this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                this.m_bIsTransformGLDirty = true;
            }
        }

        public virtual float scaleY
        {
            get
            {
                return this.m_fScaleY;
            }
            set
            {
                this.m_fScaleY = value;
                this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                this.m_bIsTransformGLDirty = true;
            }
        }

        public virtual float skewX
        {
            get
            {
                return this.m_fSkewX;
            }
            set
            {
                this.m_fSkewX = value;
                this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                this.m_bIsTransformGLDirty = true;
            }
        }

        public virtual float skewY
        {
            get
            {
                return this.m_fSkewY;
            }
            set
            {
                this.m_fSkewY = value;
                this.m_bIsTransformDirty = this.m_bIsInverseDirty = true;
                this.m_bIsTransformGLDirty = true;
            }
        }

        public int tag
        {
            get
            {
                return this.m_nTag;
            }
            set
            {
                this.m_nTag = value;
            }
        }

        public object userData
        {
            get
            {
                return this.m_pUserData;
            }
            set
            {
                this.m_pUserData = value;
            }
        }

        public virtual float vertexZ
        {
            get
            {
                return (this.m_fVertexZ / CCDirector.sharedDirector().ContentScaleFactor);
            }
            set
            {
                this.m_fVertexZ = value * CCDirector.sharedDirector().ContentScaleFactor;
            }
        }

        public virtual bool visible
        {
            get
            {
                return this.m_bIsVisible;
            }
            set
            {
                this.m_bIsVisible = value;
            }
        }

        public int zOrder
        {
            get
            {
                return this.m_nZOrder;
            }
            private set
            {
                this.m_nZOrder = value;
            }
        }
    }
}
