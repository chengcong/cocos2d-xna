using System;

namespace cocos2d
{
	public class CCParticleRain : CCParticleSystemQuad
	{
		public CCParticleRain()
		{
		}

		~CCParticleRain()
		{
		}

		public bool init()
		{
			return this.initWithTotalParticles(1000);
		}

		public override bool initWithTotalParticles(uint numberOfParticles)
		{
			if (!base.initWithTotalParticles(numberOfParticles))
			{
				return false;
			}
			base.Duration = -1f;
			base.EmitterMode = 0;
			this.modeA.gravity = new CCPoint(10f, -10f);
			this.modeA.radialAccel = 0f;
			this.modeA.radialAccelVar = 1f;
			this.modeA.tangentialAccel = 0f;
			this.modeA.tangentialAccelVar = 1f;
			this.modeA.speed = 130f;
			this.modeA.speedVar = 30f;
			base.Angle = -90f;
			base.AngleVar = 5f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, winSize.height);
			this.PosVar = new CCPoint(winSize.width / 2f, 0f);
			base.Life = 4.5f;
			base.LifeVar = 0f;
			base.StartSize = 4f;
			base.StartSizeVar = 2f;
			base.EndSize = -1f;
			base.EmissionRate = 20f;
			base.StartColor.r = 0.7f;
			base.StartColor.g = 0.8f;
			base.StartColor.b = 1f;
			base.StartColor.a = 1f;
			base.StartColorVar.r = 0f;
			base.StartColorVar.g = 0f;
			base.StartColorVar.b = 0f;
			base.StartColorVar.a = 0f;
			base.EndColor.r = 0.7f;
			base.EndColor.g = 0.8f;
			base.EndColor.b = 1f;
			base.EndColor.a = 0.5f;
			base.EndColorVar.r = 0f;
			base.EndColorVar.g = 0f;
			base.EndColorVar.b = 0f;
			base.EndColorVar.a = 0f;
			base.IsBlendAdditive = false;
			return true;
		}

		public static new CCParticleRain node()
		{
			CCParticleRain cCParticleRain = new CCParticleRain();
			if (cCParticleRain.init())
			{
				return cCParticleRain;
			}
			return null;
		}
	}
}