using System;

namespace cocos2d
{
	public class CCParticleMeteor : CCParticleSystemQuad
	{
		public CCParticleMeteor()
		{
		}

		~CCParticleMeteor()
		{
		}

		public bool init()
		{
			return this.initWithTotalParticles(150);
		}

		public override bool initWithTotalParticles(uint numberOfParticles)
		{
			if (!base.initWithTotalParticles(numberOfParticles))
			{
				return false;
			}
			base.Duration = -1f;
			base.EmitterMode = 0;
			this.modeA.gravity = new CCPoint(-200f, 200f);
			this.modeA.speed = 15f;
			this.modeA.speedVar = 5f;
			this.modeA.radialAccel = 0f;
			this.modeA.radialAccelVar = 0f;
			this.modeA.tangentialAccel = 0f;
			this.modeA.tangentialAccelVar = 0f;
			base.Angle = 90f;
			base.AngleVar = 360f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			this.PosVar = new CCPoint(0f, 0f);
			base.Life = 2f;
			base.LifeVar = 1f;
			base.StartSize = 60f;
			base.StartSizeVar = 10f;
			base.EndSize = -1f;
			base.EmissionRate = (float)((float)base.TotalParticles) / base.Life;
			base.StartColor.r = 0.2f;
			base.StartColor.g = 0.4f;
			base.StartColor.b = 0.7f;
			base.StartColor.a = 1f;
			base.StartColorVar.r = 0f;
			base.StartColorVar.g = 0f;
			base.StartColorVar.b = 0.2f;
			base.StartColorVar.a = 0.1f;
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

		public static new CCParticleMeteor node()
		{
			CCParticleMeteor cCParticleMeteor = new CCParticleMeteor();
			if (cCParticleMeteor.init())
			{
				return cCParticleMeteor;
			}
			return null;
		}
	}
}