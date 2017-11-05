namespace cocos2d
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CCSpriteBatchNode : CCNode, ICCTextureProtocol, ICCBlendProtocol
    {
        private const int defaultCapacity = 0x1d;
        protected ccBlendFunc m_blendFunc = new ccBlendFunc();
        protected List<CCSprite> m_pobDescendants;
        protected CCTextureAtlas m_pobTextureAtlas;

        public override void addChild(CCNode child, int zOrder, int tag)
        {
            CCSprite pobSprite = child as CCSprite;
            if (pobSprite != null)
            {
                base.addChild(child, zOrder, tag);
                int uIndex = this.atlasIndexForChild(pobSprite, zOrder);
                this.insertChild(pobSprite, uIndex);
            }
        }

        protected void addQuadFromSprite(CCSprite sprite, int index)
        {
            while ((index >= this.m_pobTextureAtlas.Capacity) || (this.m_pobTextureAtlas.Capacity == this.m_pobTextureAtlas.TotalQuads))
            {
                this.increaseAtlasCapacity();
            }
            sprite.useBatchNode(this);
            sprite.atlasIndex = index;
            ccV3F_C4B_T2F_Quad quad = sprite.quad;
            this.m_pobTextureAtlas.insertQuad(quad, index);
            sprite.dirty = true;
            sprite.updateTransform();
        }

        protected CCSpriteBatchNode addSpriteWithoutQuad(CCSprite child, int z, int aTag)
        {
            child.atlasIndex = z;
            int index = 0;
            if ((this.m_pobDescendants != null) && (this.m_pobDescendants.Count > 0))
            {
                CCObject obj2 = null;
                for (int i = 0; i < this.m_pobDescendants.Count; i++)
                {
                    obj2 = this.m_pobDescendants[index];
                    CCSprite sprite = obj2 as CCSprite;
                    if ((sprite != null) && (sprite.atlasIndex >= z))
                    {
                        index++;
                    }
                }
            }
            this.m_pobDescendants.Insert(index, child);
            base.addChild(child, z, aTag);
            return this;
        }

        public int atlasIndexForChild(CCSprite pobSprite, int nZ)
        {
            List<CCNode> children = pobSprite.parent.children;
            uint index = (uint)children.IndexOf(pobSprite);
            bool flag = pobSprite.parent is CCSpriteBatchNode;
            CCSprite pSprite = null;
            if ((index > 0) && (index < uint.MaxValue))
            {
                pSprite = children[((int)index) - 1] as CCSprite;
            }
            if (flag)
            {
                if (index == 0)
                {
                    return 0;
                }
                return (this.highestAtlasIndexInChild(pSprite) + 1);
            }
            if (index == 0)
            {
                CCSprite sprite2 = pobSprite.parent as CCSprite;
                if (sprite2 == null)
                {
                    return 0;
                }
                if (nZ < 0)
                {
                    return sprite2.atlasIndex;
                }
                return (sprite2.atlasIndex + 1);
            }
            if (((pSprite.zOrder < 0) && (nZ < 0)) || ((pSprite.zOrder >= 0) && (nZ >= 0)))
            {
                return (this.highestAtlasIndexInChild(pSprite) + 1);
            }
            CCSprite parent = pobSprite.parent as CCSprite;
            return (parent.atlasIndex + 1);
        }

        public static CCSpriteBatchNode batchNodeWithFile(string fileImage)
        {
            CCSpriteBatchNode node = new CCSpriteBatchNode();
            node.initWithFile(fileImage, 0x1d);
            return node;
        }

        public static CCSpriteBatchNode batchNodeWithFile(string fileImage, int capacity)
        {
            CCSpriteBatchNode node = new CCSpriteBatchNode();
            node.initWithFile(fileImage, capacity);
            return node;
        }

        public static CCSpriteBatchNode batchNodeWithTexture(CCTexture2D tex)
        {
            CCSpriteBatchNode node = new CCSpriteBatchNode();
            node.initWithTexture(tex, 0x1d);
            return node;
        }

        public static CCSpriteBatchNode batchNodeWithTexture(CCTexture2D tex, int capacity)
        {
            CCSpriteBatchNode node = new CCSpriteBatchNode();
            node.initWithTexture(tex, capacity);
            return node;
        }

        public override void draw()
        {
            base.draw();
            if (this.m_pobTextureAtlas.TotalQuads != 0)
            {
                if ((this.m_pobDescendants != null) && (this.m_pobDescendants.Count > 0))
                {
                    foreach (CCSprite sprite in this.m_pobDescendants)
                    {
                        sprite.updateTransform();
                    }
                }
                this.m_pobTextureAtlas.drawQuads();
            }
        }

        public int highestAtlasIndexInChild(CCSprite pSprite)
        {
            List<CCNode> children = pSprite.children;
            if ((children == null) && (children.Count != 0))
            {
                return this.highestAtlasIndexInChild(children.Last<CCNode>() as CCSprite);
            }
            return pSprite.atlasIndex;
        }

        public void increaseAtlasCapacity()
        {
            int newCapacity = ((this.m_pobTextureAtlas.Capacity + 1) * 4) / 3;
            CCLog.Log(string.Format("cocos2d: CCSpriteBatchNode: resizing TextureAtlas capacity from [{0}] to [{1}].", (long)this.m_pobTextureAtlas.Capacity, (long)this.m_pobTextureAtlas.Capacity));
            if (!this.m_pobTextureAtlas.resizeCapacity(newCapacity))
            {
                CCLog.Log("cocos2d: WARNING: Not enough memory to resize the atlas");
            }
        }

        public bool initWithFile(string fileImage, int capacity)
        {
            CCTexture2D tex = CCTextureCache.sharedTextureCache().addImage(fileImage);
            return this.initWithTexture(tex, capacity);
        }

        public bool initWithTexture(CCTexture2D tex, int capacity)
        {
            this.m_blendFunc.src = 1;
            this.m_blendFunc.dst = 0x303;
            this.m_pobTextureAtlas = new CCTextureAtlas();
            this.m_pobTextureAtlas.initWithTexture(tex, capacity);
            this.updateBlendFunc();
            this.contentSize = tex.getContentSize();
            base.m_pChildren = new List<CCNode>();
            this.m_pobDescendants = new List<CCSprite>();
            return true;
        }

        public void insertChild(CCSprite pobSprite, int uIndex)
        {
            pobSprite.useBatchNode(this);
            pobSprite.atlasIndex = uIndex;
            pobSprite.dirty = true;
            if (this.m_pobTextureAtlas.TotalQuads == this.m_pobTextureAtlas.Capacity)
            {
                this.increaseAtlasCapacity();
            }
            ccV3F_C4B_T2F_Quad quad = pobSprite.quad;
            this.m_pobTextureAtlas.insertQuad(quad, uIndex);
            this.m_pobDescendants.Insert(uIndex, pobSprite);
            uint num = 0;
            if ((this.m_pobDescendants != null) && (this.m_pobDescendants.Count > 0))
            {
                for (int i = 0; i < this.m_pobDescendants.Count; i++)
                {
                    CCObject obj2 = this.m_pobDescendants[i];
                    CCSprite sprite = obj2 as CCSprite;
                    if (sprite != null)
                    {
                        if (num > uIndex)
                        {
                            sprite.atlasIndex++;
                        }
                        num++;
                    }
                }
            }
            List<CCNode> children = pobSprite.children;
            if ((children != null) && (children.Count > 0))
            {
                for (int j = 0; j < children.Count; j++)
                {
                    CCObject obj3 = children[j];
                    CCSprite sprite2 = obj3 as CCSprite;
                    if (sprite2 != null)
                    {
                        uIndex = this.atlasIndexForChild(sprite2, sprite2.zOrder);
                        this.insertChild(sprite2, uIndex);
                    }
                }
            }
        }

        public int lowestAtlasIndexInChild(CCSprite pSprite)
        {
            List<CCNode> children = pSprite.children;
            if ((children == null) && (children.Count != 0))
            {
                return this.lowestAtlasIndexInChild(children[0] as CCSprite);
            }
            return pSprite.atlasIndex;
        }

        public int rebuildIndexInOrder(CCSprite pobParent, int uIndex)
        {
            List<CCNode> children = pobParent.children;
            if ((children != null) && (children.Count > 0))
            {
                CCObject obj2 = null;
                for (int i = 0; i < children.Count; i++)
                {
                    obj2 = children[i];
                    CCSprite sprite = obj2 as CCSprite;
                    if ((sprite != null) && (sprite.zOrder < 0))
                    {
                        uIndex = this.rebuildIndexInOrder(sprite, uIndex);
                    }
                }
            }
            if (!pobParent.Equals(this))
            {
                pobParent.atlasIndex = uIndex;
                uIndex++;
            }
            if ((children != null) && (children.Count > 0))
            {
                CCObject obj3 = null;
                for (int j = 0; j < children.Count; j++)
                {
                    obj3 = children[j];
                    CCSprite sprite2 = obj3 as CCSprite;
                    if ((sprite2 != null) && (sprite2.zOrder >= 0))
                    {
                        uIndex = this.rebuildIndexInOrder(sprite2, uIndex);
                    }
                }
            }
            return uIndex;
        }

        public override void removeAllChildrenWithCleanup(bool cleanup)
        {
            if ((base.m_pChildren != null) && (base.m_pChildren.Count > 0))
            {
                CCObject obj2 = null;
                for (int i = 0; i < base.m_pChildren.Count; i++)
                {
                    obj2 = base.m_pChildren[i];
                    CCSprite pobSprite = obj2 as CCSprite;
                    if (pobSprite != null)
                    {
                        this.removeSpriteFromAtlas(pobSprite);
                    }
                }
            }
            base.removeAllChildrenWithCleanup(cleanup);
            this.m_pobDescendants.Clear();
            this.m_pobTextureAtlas.removeAllQuads();
        }

        public override void removeChild(CCNode child, bool cleanup)
        {
            CCSprite pobSprite = child as CCSprite;
            if (pobSprite != null)
            {
                this.removeSpriteFromAtlas(pobSprite);
                base.removeChild(pobSprite, cleanup);
            }
        }

        public void removeChildAtIndex(int index, bool doCleanup)
        {
            if ((index >= 0) && (index < base.m_pChildren.Count))
            {
                this.removeChild((CCSprite)base.m_pChildren[index], doCleanup);
            }
        }

        public void removeSpriteFromAtlas(CCSprite pobSprite)
        {
            this.m_pobTextureAtlas.removeQuadAtIndex(pobSprite.atlasIndex);
            pobSprite.useSelfRender();
            uint index = (uint)this.m_pobDescendants.IndexOf(pobSprite);
            if (index != uint.MaxValue)
            {
                this.m_pobDescendants.RemoveAt((int)index);
                int count = this.m_pobDescendants.Count;
                while (index < count)
                {
                    CCSprite sprite = this.m_pobDescendants[(int)index];
                    sprite.atlasIndex--;
                    index++;
                }
            }
            List<CCNode> children = pobSprite.children;
            if ((children != null) && (children.Count > 0))
            {
                CCObject obj2 = null;
                for (int i = 0; i < children.Count; i++)
                {
                    obj2 = children[i];
                    CCSprite sprite2 = obj2 as CCSprite;
                    if (sprite2 != null)
                    {
                        this.removeSpriteFromAtlas(sprite2);
                    }
                }
            }
        }

        public override void reorderChild(CCNode child, int zOrder)
        {
            if (zOrder != child.zOrder)
            {
                this.removeChild((CCSprite)child, false);
                this.addChild(child, zOrder);
            }
        }

        private void updateBlendFunc()
        {
            if (!this.m_pobTextureAtlas.Texture.HasPremultipliedAlpha)
            {
                this.m_blendFunc.src = 770;
                this.m_blendFunc.dst = 0x303;
            }
        }

        public override void visit()
        {
            if (base.m_bIsVisible)
            {
                base.transform();
                this.draw();
                CCApplication.sharedApplication().basicEffect.World *= Matrix.Invert(base.m_tCCNodeTransform);
                base.m_tCCNodeTransform = Matrix.Identity;
            }
        }

        public ccBlendFunc BlendFunc
        {
            get
            {
                return this.m_blendFunc;
            }
            set
            {
                this.m_blendFunc = value;
            }
        }

        public List<CCSprite> Descendants
        {
            get
            {
                return this.m_pobDescendants;
            }
        }

        public virtual CCTexture2D Texture
        {
            get
            {
                return this.m_pobTextureAtlas.Texture;
            }
            set
            {
                this.m_pobTextureAtlas.Texture = value;
                this.updateBlendFunc();
            }
        }

        public CCTextureAtlas TextureAtlas
        {
            get
            {
                return this.m_pobTextureAtlas;
            }
            set
            {
                if (value != this.m_pobTextureAtlas)
                {
                    this.m_pobTextureAtlas = value;
                }
            }
        }
    }
}
