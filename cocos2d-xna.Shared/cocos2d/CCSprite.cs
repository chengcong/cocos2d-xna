namespace cocos2d
{
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class CCSprite : CCNode, ICCTextureProtocol, ICCBlendProtocol, ICCRGBAProtocol
    {
        private bool m_bDirty;
        protected bool m_bFlipX;
        protected bool m_bFlipY;
        protected bool m_bHasChildren;
        protected bool m_bOpacityModifyRGB;
        private bool m_bRectRotated;
        protected bool m_bRecursiveDirty;
        private bool m_bUseBatchNode;
        private bool m_bUsesBatchNode;
        private ccHonorParentTransform m_eHonorParentTransform;
        private byte m_nOpacity;
        private CCPoint m_obOffsetPositionInPixels = new CCPoint();
        protected CCRect m_obRect;
        protected CCRect m_obRectInPixels = new CCRect();
        private CCRect m_obTextureRect;
        protected CCPoint m_obUnflippedOffsetPositionFromCenter = new CCPoint();
        protected CCSpriteBatchNode m_pobBatchNode;
        protected CCTexture2D m_pobTexture;
        protected CCTextureAtlas m_pobTextureAtlas;
        private ccBlendFunc m_sBlendFunc = new ccBlendFunc();
        private ccColor3B m_sColor = new ccColor3B();
        protected ccColor3B m_sColorUnmodified;
        private ccV3F_C4B_T2F_Quad m_sQuad = new ccV3F_C4B_T2F_Quad();
        private int m_uAtlasIndex;

        public override void addChild(CCNode child)
        {
            base.addChild(child);
        }

        public override void addChild(CCNode child, int zOrder)
        {
            base.addChild(child, zOrder);
        }

        public override void addChild(CCNode child, int zOrder, int tag)
        {
            base.addChild(child, zOrder, tag);
            if (this.m_bUseBatchNode)
            {
                int uIndex = this.m_pobBatchNode.atlasIndexForChild((CCSprite)child, zOrder);
                this.m_pobBatchNode.insertChild((CCSprite)child, uIndex);
            }
            this.m_bHasChildren = true;
        }

        public override void draw()
        {
            base.draw();
            CCApplication application = CCApplication.sharedApplication();
            CCDirector.sharedDirector().getWinSize();
            bool flag = (this.m_sBlendFunc.src != ccMacros.CC_BLEND_SRC) || (this.m_sBlendFunc.dst != ccMacros.CC_BLEND_DST);
            BlendState blendState = application.GraphicsDevice.BlendState;
            if (flag)
            {
                BlendState state = new BlendState
                {
                    ColorSourceBlend = OGLES.GetXNABlend(this.m_sBlendFunc.src),
                    AlphaSourceBlend = OGLES.GetXNABlend(this.m_sBlendFunc.src),
                    ColorDestinationBlend = OGLES.GetXNABlend(this.m_sBlendFunc.dst),
                    AlphaDestinationBlend = OGLES.GetXNABlend(this.m_sBlendFunc.dst)
                };
                application.GraphicsDevice.BlendState = state;
            }
            if (this.Texture != null)
            {
                application.basicEffect.Texture = this.Texture.getTexture2D();
                application.basicEffect.TextureEnabled = true;
                application.basicEffect.Alpha = ((float)this.Opacity) / 255f;
                application.basicEffect.VertexColorEnabled = true;
            }
            VertexPositionColorTexture[] vertexData = this.m_sQuad.getVertices(ccDirectorProjection.kCCDirectorProjection3D);
            short[] indexData = this.m_sQuad.getIndexes(ccDirectorProjection.kCCDirectorProjection3D);
            foreach (EffectPass pass in application.basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                application.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, vertexData, 0, 4, indexData, 0, 2);
            }
            application.basicEffect.VertexColorEnabled = false;
            if (flag)
            {
                BlendState state2 = new BlendState
                {
                    ColorSourceBlend = OGLES.GetXNABlend(ccMacros.CC_BLEND_SRC),
                    AlphaSourceBlend = OGLES.GetXNABlend(ccMacros.CC_BLEND_SRC),
                    ColorDestinationBlend = OGLES.GetXNABlend(ccMacros.CC_BLEND_DST),
                    AlphaDestinationBlend = OGLES.GetXNABlend(ccMacros.CC_BLEND_DST)
                };
                application.GraphicsDevice.BlendState = state2;
            }
        }

        protected void getTransformValues(transformValues_ tv)
        {
            tv.pos = base.m_tPositionInPixels;
            tv.scale.x = base.m_fScaleX;
            tv.scale.y = base.m_fScaleY;
            tv.rotation = base.m_fRotation;
            tv.skew.x = base.m_fSkewX;
            tv.skew.y = base.m_fSkewY;
            tv.ap = base.m_tAnchorPointInPixels;
            tv.visible = base.m_bIsVisible;
        }

        public virtual bool init()
        {
            this.m_bDirty = this.m_bRecursiveDirty = false;
            this.useSelfRender();
            this.m_bOpacityModifyRGB = true;
            this.m_nOpacity = 0xff;
            this.m_sColor = new ccColor3B(0xff, 0xff, 0xff);
            this.m_sColorUnmodified = new ccColor3B(0xff, 0xff, 0xff);
            this.m_sBlendFunc = new ccBlendFunc();
            this.m_sBlendFunc.src = ccMacros.CC_BLEND_SRC;
            this.m_sBlendFunc.dst = ccMacros.CC_BLEND_DST;
            this.Texture = null;
            this.m_sQuad = new ccV3F_C4B_T2F_Quad();
            this.m_bFlipX = this.m_bFlipY = false;
            this.anchorPoint = CCPointExtension.ccp(0.5f, 0.5f);
            this.m_obOffsetPositionInPixels = new CCPoint();
            this.m_eHonorParentTransform = ccHonorParentTransform.CC_HONOR_PARENT_TRANSFORM_ALL;
            this.m_bHasChildren = false;
            ccColor4B colorb = new ccColor4B(0xff, 0xff, 0xff, 0xff);
            this.m_sQuad.bl.colors = colorb;
            this.m_sQuad.br.colors = colorb;
            this.m_sQuad.tl.colors = colorb;
            this.m_sQuad.tr.colors = colorb;
            this.setTextureRectInPixels(new CCRect(), false, new CCSize());
            return true;
        }

        public bool initWithBatchNode(CCSpriteBatchNode batchNode, CCRect rect)
        {
            if (this.initWithTexture(batchNode.Texture, rect))
            {
                this.useBatchNode(batchNode);
                return true;
            }
            return false;
        }

        public bool initWithBatchNodeRectInPixels(CCSpriteBatchNode batchNode, CCRect rect)
        {
            if (this.initWithTexture(batchNode.Texture))
            {
                this.setTextureRectInPixels(rect, false, rect.size);
                this.useBatchNode(batchNode);
                return true;
            }
            return false;
        }

        public bool initWithFile(string fileName)
        {
            CCTexture2D texture = CCTextureCache.sharedTextureCache().addImage(fileName);
            if (texture != null)
            {
                CCRect rect = new CCRect
                {
                    size = texture.getContentSize()
                };
                return this.initWithTexture(texture, rect);
            }
            return false;
        }

        public bool initWithFile(string fileName, CCRect rect)
        {
            CCTexture2D texture = CCTextureCache.sharedTextureCache().addImage(fileName);
            return ((texture != null) && this.initWithTexture(texture, rect));
        }

        public bool initWithSpriteFrame(CCSpriteFrame pSpriteFrame)
        {
            if (pSpriteFrame == null)
            {
                throw new ArgumentNullException("pSpriteFrame", "SpriteFrame cannot be null");
            }
            bool flag = this.initWithTexture(pSpriteFrame.Texture, pSpriteFrame.Rect);
            this.DisplayFrame = pSpriteFrame;
            return flag;
        }

        public bool initWithSpriteFrameName(string spriteFrameName)
        {
            if (spriteFrameName == null)
            {
                throw new ArgumentNullException("spriteFrameName", "spriteFrameName cannot be null");
            }
            CCSpriteFrame pSpriteFrame = CCSpriteFrameCache.sharedSpriteFrameCache().spriteFrameByName(spriteFrameName);
            return this.initWithSpriteFrame(pSpriteFrame);
        }

        public bool initWithTexture(CCTexture2D texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException("texture", "Texture cannot be null");
            }
            CCRect rect = new CCRect
            {
                size = texture.getContentSize()
            };
            return this.initWithTexture(texture, rect);
        }

        public bool initWithTexture(CCTexture2D texture, CCRect rect)
        {
            if (texture == null)
            {
                throw new ArgumentNullException("texture", "Texture cannot be null");
            }
            this.init();
            this.Texture = texture;
            this.setTextureRect(rect);
            return true;
        }

        public bool isFrameDisplayed(CCSpriteFrame pFrame)
        {
            return (CCRect.CCRectEqualToRect(pFrame.Rect, this.m_obRect) && (pFrame.Texture.Name == this.m_pobTexture.Name));
        }

        public override void removeAllChildrenWithCleanup(bool cleanup)
        {
            if (this.m_bUseBatchNode)
            {
                foreach (CCNode node in base.m_pChildren)
                {
                    this.m_pobBatchNode.removeSpriteFromAtlas((CCSprite)node);
                }
            }
            base.removeAllChildrenWithCleanup(cleanup);
            this.m_bHasChildren = false;
        }

        public override void removeChild(CCNode child, bool cleanup)
        {
            if (this.m_bUseBatchNode)
            {
                this.m_pobBatchNode.removeSpriteFromAtlas((CCSprite)child);
            }
            base.removeChild(child, cleanup);
        }

        public override void reorderChild(CCNode child, int zOrder)
        {
            if (zOrder != child.zOrder)
            {
                if (this.m_bUseBatchNode)
                {
                    this.removeChild(child, false);
                    this.addChild(child, zOrder);
                }
                else
                {
                    base.reorderChild(child, zOrder);
                }
            }
        }

        private void SET_DIRTY_RECURSIVELY()
        {
            if (this.m_bUseBatchNode)
            {
                this.m_bDirty = this.m_bRecursiveDirty = true;
                if (this.m_bHasChildren)
                {
                    this.setDirtyRecursively(true);
                }
            }
        }

        public virtual void setDirtyRecursively(bool bValue)
        {
            this.m_bDirty = this.m_bRecursiveDirty = bValue;
            if (this.m_bHasChildren)
            {
                foreach (CCNode node in base.m_pChildren)
                {
                    ((CCSprite)node).setDirtyRecursively(true);
                }
            }
        }

        public void setDisplayFrameWithAnimationName(string animationName, int frameIndex)
        {
            CCSpriteFrame frame = CCAnimationCache.sharedAnimationCache().animationByName(animationName).getFrames()[frameIndex];
            this.DisplayFrame = frame;
        }

        public void setTextureRect(CCRect rect)
        {
            CCRect rect2 = ccMacros.CC_RECT_POINTS_TO_PIXELS(rect);
            this.m_obTextureRect = rect;
            this.setTextureRectInPixels(rect2, false, rect2.size);
        }

        public void setTextureRectInPixels(CCRect rect, bool rotated, CCSize size)
        {
            this.m_obRectInPixels = rect;
            this.m_obRect = ccMacros.CC_RECT_PIXELS_TO_POINTS(rect);
            this.m_bRectRotated = rotated;
            base.contentSizeInPixels = size;
            this.updateTextureCoords(this.m_obRectInPixels);
            CCPoint point = new CCPoint(this.m_obUnflippedOffsetPositionFromCenter.x, this.m_obUnflippedOffsetPositionFromCenter.y);
            if (this.m_bFlipX)
            {
                point.x = -point.x;
            }
            if (this.m_bFlipY)
            {
                point.y = -point.y;
            }
            this.m_obOffsetPositionInPixels.x = point.x + ((base.m_tContentSizeInPixels.width - this.m_obRectInPixels.size.width) / 2f);
            this.m_obOffsetPositionInPixels.y = point.y + ((base.m_tContentSizeInPixels.height - this.m_obRectInPixels.size.height) / 2f);
            if (this.m_bUseBatchNode)
            {
                this.m_bDirty = true;
            }
            else
            {
                float x = 0f + this.m_obOffsetPositionInPixels.x;
                float y = 0f + this.m_obOffsetPositionInPixels.y;
                float num3 = x + this.m_obRectInPixels.size.width;
                float num4 = y + this.m_obRectInPixels.size.height;
                this.m_sQuad.bl.vertices = ccTypes.vertex3(x, y, 0f);
                this.m_sQuad.br.vertices = ccTypes.vertex3(num3, y, 0f);
                this.m_sQuad.tl.vertices = ccTypes.vertex3(x, num4, 0f);
                this.m_sQuad.tr.vertices = ccTypes.vertex3(num3, num4, 0f);
            }
        }

        public static CCSprite spriteWithBatchNode(CCSpriteBatchNode batchNode, CCRect rect)
        {
            CCSprite sprite = new CCSprite();
            if (sprite.initWithBatchNode(batchNode, rect))
            {
                return sprite;
            }
            return null;
        }

        public static CCSprite spriteWithFile(string fileName)
        {
            CCSprite sprite = new CCSprite();
            if (sprite.initWithFile(fileName))
            {
                return sprite;
            }
            return null;
        }

        public static CCSprite spriteWithFile(string fileName, CCRect rect)
        {
            CCSprite sprite = new CCSprite();
            if (sprite.initWithFile(fileName, rect))
            {
                return sprite;
            }
            return null;
        }

        public static CCSprite spriteWithSpriteFrame(CCSpriteFrame pSpriteFrame)
        {
            CCSprite sprite = new CCSprite();
            if ((sprite != null) && sprite.initWithSpriteFrame(pSpriteFrame))
            {
                return sprite;
            }
            return null;
        }

        public static CCSprite spriteWithSpriteFrameName(string pszSpriteFrameName)
        {
            CCSpriteFrame pSpriteFrame = CCSpriteFrameCache.sharedSpriteFrameCache().spriteFrameByName(pszSpriteFrameName);
            string.Format("Invalid spriteFrameName: {0}", pszSpriteFrameName);
            return spriteWithSpriteFrame(pSpriteFrame);
        }

        public static CCSprite spriteWithTexture(CCTexture2D texture)
        {
            CCSprite sprite = new CCSprite();
            if ((sprite != null) && sprite.initWithTexture(texture))
            {
                return sprite;
            }
            sprite = null;
            return null;
        }

        public static CCSprite spriteWithTexture(CCTexture2D texture, CCRect rect)
        {
            CCSprite sprite = new CCSprite();
            if ((sprite != null) && sprite.initWithTexture(texture, rect))
            {
                return sprite;
            }
            sprite = null;
            return null;
        }

        public static CCSprite spriteWithTexture(CCTexture2D texture, CCRect rect, CCPoint offset)
        {
            return null;
        }

        protected void updateBlendFunc()
        {
            if ((this.m_pobTexture == null) || !this.m_pobTexture.HasPremultipliedAlpha)
            {
                this.m_sBlendFunc.src = 770;
                this.m_sBlendFunc.dst = 0x303;
                this.IsOpacityModifyRGB = false;
            }
            else
            {
                this.m_sBlendFunc.src = ccMacros.CC_BLEND_SRC;
                this.m_sBlendFunc.dst = ccMacros.CC_BLEND_DST;
                this.IsOpacityModifyRGB = true;
            }
        }

        private void updateColor()
        {
            this.m_sQuad.bl.colors = new ccColor4B(this.m_sColor.r, this.m_sColor.g, this.m_sColor.b, this.m_nOpacity);
            this.m_sQuad.br.colors = new ccColor4B(this.m_sColor.r, this.m_sColor.g, this.m_sColor.b, this.m_nOpacity);
            this.m_sQuad.tl.colors = new ccColor4B(this.m_sColor.r, this.m_sColor.g, this.m_sColor.b, this.m_nOpacity);
            this.m_sQuad.tr.colors = new ccColor4B(this.m_sColor.r, this.m_sColor.g, this.m_sColor.b, this.m_nOpacity);
            if (this.m_bUseBatchNode)
            {
                if (this.m_uAtlasIndex != ccMacros.CCSpriteIndexNotInitialized)
                {
                    this.m_pobTextureAtlas.updateQuad(this.m_sQuad, this.m_uAtlasIndex);
                }
                else
                {
                    this.m_bDirty = true;
                }
            }
        }

        protected void updateTextureCoords(CCRect rect)
        {
            CCTexture2D textured = this.m_bUsesBatchNode ? this.m_pobTextureAtlas.Texture : this.m_pobTexture;
            if (textured != null)
            {
                float num3;
                float num4;
                float num5;
                float num6;
                float pixelsWide = textured.PixelsWide;
                float pixelsHigh = textured.PixelsHigh;
                if (this.m_bRectRotated)
                {
                    num3 = rect.origin.x / pixelsWide;
                    num4 = num3 + (rect.size.height / pixelsWide);
                    num5 = rect.origin.y / pixelsHigh;
                    num6 = num5 + (rect.size.width / pixelsHigh);
                    if (this.m_bFlipX)
                    {
                        ccMacros.CC_SWAP<float>(ref num5, ref num6);
                    }
                    if (this.m_bFlipY)
                    {
                        ccMacros.CC_SWAP<float>(ref num3, ref num4);
                    }
                    this.m_sQuad.bl.texCoords.u = num3;
                    this.m_sQuad.bl.texCoords.v = num5;
                    this.m_sQuad.br.texCoords.u = num3;
                    this.m_sQuad.br.texCoords.v = num6;
                    this.m_sQuad.tl.texCoords.u = num4;
                    this.m_sQuad.tl.texCoords.v = num5;
                    this.m_sQuad.tr.texCoords.u = num4;
                    this.m_sQuad.tr.texCoords.v = num6;
                }
                else
                {
                    num3 = rect.origin.x / pixelsWide;
                    num4 = num3 + (rect.size.width / pixelsWide);
                    num5 = rect.origin.y / pixelsHigh;
                    num6 = num5 + (rect.size.height / pixelsHigh);
                    if (this.m_bFlipX)
                    {
                        ccMacros.CC_SWAP<float>(ref num3, ref num4);
                    }
                    if (this.m_bFlipY)
                    {
                        ccMacros.CC_SWAP<float>(ref num5, ref num6);
                    }
                    this.m_sQuad.bl.texCoords.u = num3;
                    this.m_sQuad.bl.texCoords.v = num6;
                    this.m_sQuad.br.texCoords.u = num4;
                    this.m_sQuad.br.texCoords.v = num6;
                    this.m_sQuad.tl.texCoords.u = num3;
                    this.m_sQuad.tl.texCoords.v = num5;
                    this.m_sQuad.tr.texCoords.u = num4;
                    this.m_sQuad.tr.texCoords.v = num5;
                }
            }
        }

        public void updateTransform()
        {
            if (this.m_bDirty)
            {
                if (!base.m_bIsVisible)
                {
                    this.m_sQuad.br.vertices = this.m_sQuad.tl.vertices = this.m_sQuad.tr.vertices = this.m_sQuad.bl.vertices = new ccVertex3F(0f, 0f, 0f);
                    this.m_pobTextureAtlas.updateQuad(this.m_sQuad, this.m_uAtlasIndex);
                }
                else
                {
                    CCAffineTransform transform;
                    if ((base.m_pParent == null) || (base.m_pParent == this.m_pobBatchNode))
                    {
                        float num = -ccMacros.CC_DEGREES_TO_RADIANS(base.m_fRotation);
                        float num2 = (float)Math.Cos((double)num);
                        float num3 = (float)Math.Sin((double)num);
                        transform = CCAffineTransform.CCAffineTransformMake(num2 * base.m_fScaleX, num3 * base.m_fScaleX, -num3 * base.m_fScaleY, num2 * base.m_fScaleY, base.m_tPositionInPixels.x, base.m_tPositionInPixels.y);
                        if ((base.m_fSkewX > 0f) || (base.m_fSkewY > 0f))
                        {
                            transform = CCAffineTransform.CCAffineTransformConcat(CCAffineTransform.CCAffineTransformMake(1f, (float)Math.Tan((double)ccMacros.CC_DEGREES_TO_RADIANS(base.m_fSkewY)), (float)Math.Tan((double)ccMacros.CC_DEGREES_TO_RADIANS(base.m_fSkewX)), 1f, 0f, 0f), transform);
                        }
                        transform = CCAffineTransform.CCAffineTransformTranslate(transform, -base.m_tAnchorPointInPixels.x, -base.m_tAnchorPointInPixels.y);
                    }
                    else
                    {
                        transform = CCAffineTransform.CCAffineTransformMakeIdentity();
                        ccHonorParentTransform honorParentTransform = ccHonorParentTransform.CC_HONOR_PARENT_TRANSFORM_ALL;
                        for (CCNode node = this; ((node != null) && (node is CCSprite)) && (node != this.m_pobBatchNode); node = node.parent)
                        {
                            transformValues_ tv = new transformValues_();
                            ((CCSprite)node).getTransformValues(tv);
                            if (!tv.visible)
                            {
                                this.m_sQuad.br.vertices = this.m_sQuad.tl.vertices = this.m_sQuad.tr.vertices = this.m_sQuad.bl.vertices = new ccVertex3F(0f, 0f, 0f);
                                this.m_pobTextureAtlas.updateQuad(this.m_sQuad, this.m_uAtlasIndex);
                                this.m_bDirty = this.m_bRecursiveDirty = false;
                                return;
                            }
                            CCAffineTransform t = CCAffineTransform.CCAffineTransformMakeIdentity();
                            if (honorParentTransform != ((ccHonorParentTransform)0))
                            {
                                t = CCAffineTransform.CCAffineTransformTranslate(t, tv.pos.x, tv.pos.y);
                            }
                            if (honorParentTransform != ((ccHonorParentTransform)0))
                            {
                                t = CCAffineTransform.CCAffineTransformRotate(t, -ccMacros.CC_DEGREES_TO_RADIANS(tv.rotation));
                            }
                            if (honorParentTransform != ((ccHonorParentTransform)0))
                            {
                                t = CCAffineTransform.CCAffineTransformConcat(CCAffineTransform.CCAffineTransformMake(1f, (float)Math.Tan((double)ccMacros.CC_DEGREES_TO_RADIANS(tv.skew.y)), (float)Math.Tan((double)ccMacros.CC_DEGREES_TO_RADIANS(tv.skew.x)), 1f, 0f, 0f), t);
                            }
                            if (honorParentTransform != ((ccHonorParentTransform)0))
                            {
                                t = CCAffineTransform.CCAffineTransformScale(t, tv.scale.x, tv.scale.y);
                            }
                            t = CCAffineTransform.CCAffineTransformTranslate(t, -tv.ap.x, -tv.ap.y);
                            transform = CCAffineTransform.CCAffineTransformConcat(transform, t);
                            honorParentTransform = ((CCSprite)node).honorParentTransform;
                        }
                    }
                    CCSize size = this.m_obRectInPixels.size;
                    float x = this.m_obOffsetPositionInPixels.x;
                    float y = this.m_obOffsetPositionInPixels.y;
                    float num6 = x + size.width;
                    float num7 = y + size.height;
                    float tx = transform.tx;
                    float ty = transform.ty;
                    float a = transform.a;
                    float b = transform.b;
                    float d = transform.d;
                    float num13 = -transform.c;
                    float inx = ((x * a) - (y * num13)) + tx;
                    float iny = ((x * b) + (y * d)) + ty;
                    float num16 = ((num6 * a) - (y * num13)) + tx;
                    float num17 = ((num6 * b) + (y * d)) + ty;
                    float num18 = ((num6 * a) - (num7 * num13)) + tx;
                    float num19 = ((num6 * b) + (num7 * d)) + ty;
                    float num20 = ((x * a) - (num7 * num13)) + tx;
                    float num21 = ((x * b) + (num7 * d)) + ty;
                    this.m_sQuad.bl.vertices = new ccVertex3F(inx, iny, base.m_fVertexZ);
                    this.m_sQuad.br.vertices = new ccVertex3F(num16, num17, base.m_fVertexZ);
                    this.m_sQuad.tl.vertices = new ccVertex3F(num20, num21, base.m_fVertexZ);
                    this.m_sQuad.tr.vertices = new ccVertex3F(num18, num19, base.m_fVertexZ);
                    this.m_pobTextureAtlas.updateQuad(this.m_sQuad, this.m_uAtlasIndex);
                    this.m_bDirty = this.m_bRecursiveDirty = false;
                }
            }
        }

        public void useBatchNode(CCSpriteBatchNode batchNode)
        {
            this.m_bUseBatchNode = true;
            this.m_pobTextureAtlas = batchNode.TextureAtlas;
            this.m_pobBatchNode = batchNode;
        }

        public void useSelfRender()
        {
            this.m_uAtlasIndex = ccMacros.CCSpriteIndexNotInitialized;
            this.m_bUseBatchNode = false;
            this.m_pobTextureAtlas = null;
            this.m_pobBatchNode = null;
            this.m_bDirty = this.m_bRecursiveDirty = false;
            float x = 0f + this.m_obOffsetPositionInPixels.x;
            float y = 0f + this.m_obOffsetPositionInPixels.y;
            float num3 = x + this.m_obRectInPixels.size.width;
            float num4 = y + this.m_obRectInPixels.size.height;
            this.m_sQuad.bl.vertices = ccTypes.vertex3(x, y, 0f);
            this.m_sQuad.br.vertices = ccTypes.vertex3(num3, y, 0f);
            this.m_sQuad.tl.vertices = ccTypes.vertex3(x, num4, 0f);
            this.m_sQuad.tr.vertices = ccTypes.vertex3(num3, num4, 0f);
        }

        public override CCPoint anchorPoint
        {
            get
            {
                return base.anchorPoint;
            }
            set
            {
                base.anchorPoint = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public int atlasIndex
        {
            get
            {
                return this.m_uAtlasIndex;
            }
            set
            {
                this.m_uAtlasIndex = value;
            }
        }

        public ccBlendFunc BlendFunc
        {
            get
            {
                return this.m_sBlendFunc;
            }
            set
            {
                this.m_sBlendFunc = value;
            }
        }

        public ccColor3B Color
        {
            get
            {
                if (this.m_bOpacityModifyRGB)
                {
                    return this.m_sColorUnmodified;
                }
                return this.m_sColor;
            }
            set
            {
                this.m_sColor = new ccColor3B(value.r, value.g, value.b);
                this.m_sColorUnmodified = new ccColor3B(value.r, value.g, value.b);
                if (this.m_bOpacityModifyRGB)
                {
                    this.m_sColor.r = (byte)((value.r * this.m_nOpacity) / 0xff);
                    this.m_sColor.g = (byte)((value.g * this.m_nOpacity) / 0xff);
                    this.m_sColor.b = (byte)((value.b * this.m_nOpacity) / 0xff);
                }
                this.updateColor();
            }
        }

        public bool dirty
        {
            get
            {
                return this.m_bDirty;
            }
            set
            {
                this.m_bDirty = value;
            }
        }

        public virtual CCSpriteFrame DisplayFrame
        {
            get
            {
                return CCSpriteFrame.frameWithTexture(this.m_pobTexture, this.m_obRectInPixels, this.m_bRectRotated, this.m_obUnflippedOffsetPositionFromCenter, base.m_tContentSizeInPixels);
            }
            set
            {
                this.m_obUnflippedOffsetPositionFromCenter = value.OffsetInPixels;
                CCTexture2D texture = value.Texture;
                if (texture != this.m_pobTexture)
                {
                    this.Texture = texture;
                }
                this.m_bRectRotated = value.IsRotated;
                this.setTextureRectInPixels(value.RectInPixels, value.IsRotated, value.OriginalSizeInPixels);
            }
        }

        public ccHonorParentTransform honorParentTransform
        {
            get
            {
                return this.m_eHonorParentTransform;
            }
            set
            {
                this.m_eHonorParentTransform = value;
            }
        }

        public bool IsFlipX
        {
            get
            {
                return this.m_bFlipX;
            }
            set
            {
                if (this.m_bFlipX != value)
                {
                    this.m_bFlipX = value;
                    this.setTextureRectInPixels(this.m_obRectInPixels, this.m_bRectRotated, base.m_tContentSizeInPixels);
                }
            }
        }

        public bool IsFlipY
        {
            get
            {
                return this.m_bFlipY;
            }
            set
            {
                if (this.m_bFlipY != value)
                {
                    this.m_bFlipY = value;
                    this.setTextureRectInPixels(this.m_obRectInPixels, this.m_bRectRotated, base.m_tContentSizeInPixels);
                }
            }
        }

        public virtual bool IsOpacityModifyRGB
        {
            get
            {
                return this.m_bOpacityModifyRGB;
            }
            set
            {
                ccColor3B sColor = this.m_sColor;
                this.m_bOpacityModifyRGB = value;
                this.m_sColor = sColor;
            }
        }

        public override bool isRelativeAnchorPoint
        {
            get
            {
                return base.isRelativeAnchorPoint;
            }
            set
            {
                base.isRelativeAnchorPoint = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public bool IsUseBatchNode
        {
            get
            {
                return this.m_bUseBatchNode;
            }
            set
            {
                this.m_bUseBatchNode = value;
            }
        }

        public CCPoint offsetPositionInPixels
        {
            get
            {
                return this.m_obOffsetPositionInPixels;
            }
        }

        public byte Opacity
        {
            get
            {
                return this.m_nOpacity;
            }
            set
            {
                this.m_nOpacity = value;
                if (this.m_bOpacityModifyRGB)
                {
                    this.Color = this.m_sColorUnmodified;
                }
                this.updateColor();
            }
        }

        public override CCPoint position
        {
            get
            {
                return base.position;
            }
            set
            {
                base.position = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public override CCPoint positionInPixels
        {
            get
            {
                return base.positionInPixels;
            }
            set
            {
                base.positionInPixels = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public ccV3F_C4B_T2F_Quad quad
        {
            get
            {
                return this.m_sQuad;
            }
        }

        public bool rectRotated
        {
            get
            {
                return this.m_bRectRotated;
            }
        }

        public override float rotation
        {
            get
            {
                return base.rotation;
            }
            set
            {
                base.rotation = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public override float scale
        {
            get
            {
                return base.scale;
            }
            set
            {
                base.scale = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public override float scaleX
        {
            get
            {
                return base.scaleX;
            }
            set
            {
                base.scaleX = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public override float scaleY
        {
            get
            {
                return base.scaleY;
            }
            set
            {
                base.scaleY = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public override float skewX
        {
            get
            {
                return base.skewX;
            }
            set
            {
                base.skewX = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public override float skewY
        {
            get
            {
                return base.skewY;
            }
            set
            {
                base.skewY = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public virtual CCTexture2D Texture
        {
            get
            {
                return this.m_pobTexture;
            }
            set
            {
                this.m_pobTexture = value;
                this.updateBlendFunc();
            }
        }

        public CCRect textureRect
        {
            get
            {
                return this.m_obTextureRect;
            }
        }

        public override float vertexZ
        {
            get
            {
                return base.vertexZ;
            }
            set
            {
                base.vertexZ = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }

        public override bool visible
        {
            get
            {
                return base.visible;
            }
            set
            {
                base.visible = value;
                this.SET_DIRTY_RECURSIVELY();
            }
        }
    }
}
