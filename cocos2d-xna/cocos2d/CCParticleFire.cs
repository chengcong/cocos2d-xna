using System;

namespace cocos2d
{
	public class CCParticleFire : CCParticleSystemQuad
	{
		public CCParticleFire()
		{
		}

		~CCParticleFire()
		{
		}

		public bool init()
		{
			return this.initWithTotalParticles(250);
		}

		public override bool initWithTotalParticles(uint numberOfParticles)
		{
			if (!base.initWithTotalParticles(numberOfParticles))
			{
				return false;
			}
			base.Duration = -1f;
			base.EmitterMode = 0;
			this.modeA.gravity = new CCPoint(0f, 0f);
			this.modeA.radialAccel = 0f;
			this.modeA.radialAccelVar = 0f;
			this.modeA.speed = 60f;
			this.modeA.speedVar = 20f;
			base.Angle = 90f;
			base.AngleVar = 10f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, 60f);
			this.PosVar = new CCPoint(40f, 20f);
			base.Life = 3f;
			base.LifeVar = 0.25f;
			base.StartSize = 54f;
			base.StartSizeVar = 10f;
			base.EndSize = -1f;
			base.EmissionRate = (float)((float)base.TotalParticles) / base.Life;
			base.StartColor.r = 0.76f;
			base.StartColor.g = 0.25f;
			base.StartColor.b = 0.12f;
			base.StartColor.a = 1f;
			base.StartColorVar.r = 0f;
			base.StartColorVar.g = 0f;
			base.StartColorVar.b = 0f;
			base.StartColorVar.a = 0f;
			base.EndColor.r = 0f;
			base.EndColor.g = 0f;
			base.EndColor.b = 0f;
			base.EndColor.a = 1f;
			base.EndColorVar.r = 0f;
			base.EndColorVar.g = 0f;
			base.EndColorVar.b = 0f;
			base.EndColorVar.a = 0f;
			base.IsBlendAdditive = true;
			return true;
		}

		public static new CCParticleFire node()
		{
			CCParticleFire cCParticleFire = new CCParticleFire();
			if (cCParticleFire.init())
			{
				return cCParticleFire;
			}
			return null;
		}
	}
}