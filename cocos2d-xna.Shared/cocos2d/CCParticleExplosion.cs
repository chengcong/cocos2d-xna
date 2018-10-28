using System;

namespace cocos2d
{
	public class CCParticleExplosion : CCParticleSystemQuad
	{
		public CCParticleExplosion()
		{
		}

		~CCParticleExplosion()
		{
		}

		public bool init()
		{
			return this.initWithTotalParticles(700);
		}

		public override bool initWithTotalParticles(uint numberOfParticles)
		{
			if (!base.initWithTotalParticles(numberOfParticles))
			{
				return false;
			}
			base.Duration = 0.1f;
			base.EmitterMode = 0;
			this.modeA.gravity = new CCPoint(0f, 0f);
			this.modeA.speed = 70f;
			this.modeA.speedVar = 40f;
			this.modeA.radialAccel = 0f;
			this.modeA.radialAccelVar = 0f;
			this.modeA.tangentialAccel = 0f;
			this.modeA.tangentialAccelVar = 0f;
			base.Angle = 90f;
			base.AngleVar = 360f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			this.PosVar = new CCPoint(0f, 0f);
			base.Life = 5f;
			base.LifeVar = 2f;
			base.StartSize = 15f;
			base.StartSizeVar = 10f;
			base.EndSize = -1f;
			base.EmissionRate = (float)((float)base.TotalParticles) / base.Duration;
			base.StartColor.r = 0.7f;
			base.StartColor.g = 0.1f;
			base.StartColor.b = 0.2f;
			base.StartColor.a = 1f;
			base.StartColorVar.r = 0.5f;
			base.StartColorVar.g = 0.5f;
			base.StartColorVar.b = 0.5f;
			base.StartColorVar.a = 0f;
			base.EndColor.r = 0.5f;
			base.EndColor.g = 0.5f;
			base.EndColor.b = 0.5f;
			base.EndColor.a = 0f;
			base.EndColorVar.r = 0.5f;
			base.EndColorVar.g = 0.5f;
			base.EndColorVar.b = 0.5f;
			base.EndColorVar.a = 0f;
			base.IsBlendAdditive = false;
			return true;
		}

		public static new CCParticleExplosion node()
		{
			CCParticleExplosion cCParticleExplosion = new CCParticleExplosion();
			if (cCParticleExplosion.init())
			{
				return cCParticleExplosion;
			}
			return null;
		}
	}
}