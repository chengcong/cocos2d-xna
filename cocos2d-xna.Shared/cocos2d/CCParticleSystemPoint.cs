namespace cocos2d
{
    using System;

    public class CCParticleSystemPoint : CCParticleSystem
    {
        public int CC_MAX_PARTICLE_SIZE = 0x40;
        private ccPointSprite[] m_pVertices = null;
        private uint m_uVerticesID;

        public override void draw()
        {
            throw new NotImplementedException();
        }

        ~CCParticleSystemPoint()
        {
        }

        public override bool initWithTotalParticles(uint numberOfParticles)
        {
            if (!base.initWithTotalParticles(numberOfParticles))
            {
                return false;
            }
            this.m_pVertices = new ccPointSprite[base.TotalParticles];
            if (this.m_pVertices == null)
            {
                CCLog.Log("cocos2d: Particle system: not enough memory");
                return false;
            }
            for (int i = 0; i < base.TotalParticles; i++)
            {
                this.m_pVertices[i] = new ccPointSprite();
            }
            return true;
        }

        public static CCParticleSystemPoint particleWithFile(string plistFile)
        {
            CCParticleSystemPoint point = new CCParticleSystemPoint();
            if ((point != null) && point.initWithFile(plistFile))
            {
                return point;
            }
            return point;
        }

        public override void postStep()
        {
        }

        public override void updateQuadWithParticle(CCParticle particle, CCPoint newPosition)
        {
            this.m_pVertices[base.m_uParticleIdx].pos = ccTypes.vertex2(newPosition.x, newPosition.y);
            this.m_pVertices[base.m_uParticleIdx].size = particle.size;
            ccColor4B colorb = new ccColor4B((byte)(particle.color.r * 255f), (byte)(particle.color.g * 255f), (byte)(particle.color.b * 255f), (byte)(particle.color.a * 255f));
            this.m_pVertices[base.m_uParticleIdx].color = colorb;
        }
    }
}
