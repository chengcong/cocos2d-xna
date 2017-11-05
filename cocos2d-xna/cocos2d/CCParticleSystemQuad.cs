namespace cocos2d
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class CCParticleSystemQuad : CCParticleSystem
    {
        private uint[] m_pIndices = null;
        private ccV2F_C4B_T2F_Quad[] m_pQuads = null;
        private uint m_uQuadsID;

        public override void draw()
        {
            base.draw();
            BlendState blendState = CCApplication.sharedApplication().GraphicsDevice.BlendState;
            BlendState alphaBlend = BlendState.AlphaBlend;
            if (base.IsBlendAdditive)
            {
                alphaBlend = BlendState.Additive;
            }
            CCApplication.sharedApplication().spriteBatch.Begin(SpriteSortMode.Deferred, alphaBlend);
            for (int i = 0; i < base.ParticleCount; i++)
            {
                CCParticle particle = base.m_pParticles[i];
                CCPoint obPoint = CCPointExtension.ccpAdd(CCAffineTransform.CCPointApplyAffineTransform(new CCPoint(), base.nodeToWorldTransform()), new CCPoint(this.m_pQuads[i].bl.vertices.x, this.m_pQuads[i].bl.vertices.y));
                obPoint = CCDirector.sharedDirector().convertToUI(obPoint);
                Vector2 position = new Vector2(obPoint.x, obPoint.y);
                Color color = new Color(particle.color.r, particle.color.g, particle.color.b, particle.color.a);
                float scale = 1f;
                if (this.Texture.getTexture2D().Width > this.Texture.getTexture2D().Height)
                {
                    scale = particle.size / ((float)this.Texture.getTexture2D().Height);
                }
                else
                {
                    scale = particle.size / ((float)this.Texture.getTexture2D().Width);
                }
                float rotation = particle.rotation;
                Vector2 origin = new Vector2((float)(this.Texture.getTexture2D().Width / 2), (float)(this.Texture.getTexture2D().Height / 2));
                Rectangle? sourceRectangle = null;
                CCApplication.sharedApplication().spriteBatch.Draw(this.Texture.getTexture2D(), position, sourceRectangle, color, rotation, origin, scale, SpriteEffects.None, 0f);
            }
            CCApplication.sharedApplication().spriteBatch.End();
            CCApplication.sharedApplication().GraphicsDevice.BlendState = blendState;
        }

        ~CCParticleSystemQuad()
        {
        }

        public void initIndices()
        {
            for (uint i = 0; i < base.TotalParticles; i++)
            {
                uint index = i * 6;
                uint num3 = i * 4;
                this.m_pIndices[index] = num3;
                this.m_pIndices[(int)((IntPtr)(index + 1))] = num3 + 1;
                this.m_pIndices[(int)((IntPtr)(index + 2))] = num3 + 2;
                this.m_pIndices[(int)((IntPtr)(index + 5))] = num3 + 1;
                this.m_pIndices[(int)((IntPtr)(index + 4))] = num3 + 2;
                this.m_pIndices[(int)((IntPtr)(index + 3))] = num3 + 3;
            }
        }

        public void initTexCoordsWithRect(CCRect pointRect)
        {
            CCRect rect = new CCRect(pointRect.origin.x * CCDirector.sharedDirector().ContentScaleFactor, pointRect.origin.y * CCDirector.sharedDirector().ContentScaleFactor, pointRect.size.width * CCDirector.sharedDirector().ContentScaleFactor, pointRect.size.height * CCDirector.sharedDirector().ContentScaleFactor);
            float width = pointRect.size.width;
            float height = pointRect.size.height;
            if (this.Texture != null)
            {
                width = this.Texture.PixelsWide;
                height = this.Texture.PixelsHigh;
            }
            float num3 = rect.origin.x / width;
            float y = rect.origin.y / height;
            float num5 = num3 + (rect.size.width / width);
            float x = y + (rect.size.height / height);
            ccMacros.CC_SWAP<float>(ref x, ref y);
            for (uint i = 0; i < base.TotalParticles; i++)
            {
                this.m_pQuads[i] = new ccV2F_C4B_T2F_Quad();
                this.m_pQuads[i].bl.texCoords.u = num3;
                this.m_pQuads[i].bl.texCoords.v = y;
                this.m_pQuads[i].br.texCoords.u = num5;
                this.m_pQuads[i].br.texCoords.v = y;
                this.m_pQuads[i].tl.texCoords.u = num3;
                this.m_pQuads[i].tl.texCoords.v = x;
                this.m_pQuads[i].tr.texCoords.u = num5;
                this.m_pQuads[i].tr.texCoords.v = x;
            }
        }

        public override bool initWithTotalParticles(uint numberOfParticles)
        {
            if (!base.initWithTotalParticles(numberOfParticles))
            {
                return false;
            }
            this.m_pQuads = new ccV2F_C4B_T2F_Quad[base.TotalParticles];
            this.m_pIndices = new uint[base.TotalParticles * 6];
            if ((this.m_pQuads == null) || (this.m_pIndices == null))
            {
                CCLog.Log("cocos2d: Particle system: not enough memory");
                return false;
            }
            if (this.Texture != null)
            {
                this.initTexCoordsWithRect(new CCRect(0f, 0f, (float)this.Texture.PixelsWide, (float)this.Texture.PixelsHigh));
            }
            else
            {
                this.initTexCoordsWithRect(new CCRect(0f, 0f, 1f, 1f));
            }
            this.initIndices();
            return true;
        }

        public static CCParticleSystemQuad particleWithDictionary(Dictionary<string, object> dic)
        {
            CCParticleSystemQuad quad = new CCParticleSystemQuad();
            if ((quad != null) && quad.initWithDictionary(dic))
            {
                return quad;
            }
            return quad;
        }

        public static CCParticleSystemQuad particleWithFile(string plistFile)
        {
            CCParticleSystemQuad quad = new CCParticleSystemQuad();
            if ((quad != null) && quad.initWithFile(plistFile))
            {
                return quad;
            }
            return quad;
        }

        public override void postStep()
        {
        }

        public void setDisplayFrame(CCSpriteFrame spriteFrame)
        {
            if ((this.Texture == null) || (spriteFrame.Texture.Name != this.Texture.Name))
            {
                this.Texture = spriteFrame.Texture;
            }
        }

        public void setTextureWithRect(CCTexture2D texture, CCRect rect)
        {
            if ((this.Texture == null) || (texture.Name != this.Texture.Name))
            {
                base.Texture = texture;
            }
            this.initTexCoordsWithRect(rect);
        }

        public override void updateQuadWithParticle(CCParticle particle, CCPoint newPosition)
        {
            ccV2F_C4B_T2F_Quad quad = this.m_pQuads[base.m_uParticleIdx];
            ccColor4B colorb = new ccColor4B((byte)(particle.color.r * 255f), (byte)(particle.color.g * 255f), (byte)(particle.color.b * 255f), (byte)(particle.color.a * 255f));
            quad.bl.colors = colorb;
            quad.br.colors = colorb;
            quad.tl.colors = colorb;
            quad.tr.colors = colorb;
            float num = particle.size / 2f;
            if (particle.rotation != 0f)
            {
                float num2 = -num;
                float num3 = -num;
                float num4 = num;
                float num5 = num;
                float x = newPosition.x;
                float y = newPosition.y;
                float num8 = -ccMacros.CC_DEGREES_TO_RADIANS(particle.rotation);
                float num9 = (float)Math.Cos((double)num8);
                float num10 = (float)Math.Sin((double)num8);
                float num11 = ((num2 * num9) - (num3 * num10)) + x;
                float num12 = ((num2 * num10) + (num3 * num9)) + y;
                float num13 = ((num4 * num9) - (num3 * num10)) + x;
                float num14 = ((num4 * num10) + (num3 * num9)) + y;
                float num15 = ((num4 * num9) - (num5 * num10)) + x;
                float num16 = ((num4 * num10) + (num5 * num9)) + y;
                float num17 = ((num2 * num9) - (num5 * num10)) + x;
                float num18 = ((num2 * num10) + (num5 * num9)) + y;
                quad.bl.vertices.x = num11;
                quad.bl.vertices.y = num12;
                quad.br.vertices.x = num13;
                quad.br.vertices.y = num14;
                quad.tl.vertices.x = num17;
                quad.tl.vertices.y = num18;
                quad.tr.vertices.x = num15;
                quad.tr.vertices.y = num16;
            }
            else
            {
                quad.bl.vertices.x = newPosition.x - num;
                quad.bl.vertices.y = newPosition.y - num;
                quad.br.vertices.x = newPosition.x + num;
                quad.br.vertices.y = newPosition.y - num;
                quad.tl.vertices.x = newPosition.x - num;
                quad.tl.vertices.y = newPosition.y + num;
                quad.tr.vertices.x = newPosition.x + num;
                quad.tr.vertices.y = newPosition.y + num;
            }
        }

        public override CCTexture2D Texture
        {
            get
            {
                return base.Texture;
            }
            set
            {
                CCSize size = value.getContentSize();
                this.setTextureWithRect(value, new CCRect(0f, 0f, size.width, size.height));
            }
        }
    }
}
