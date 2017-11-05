using System;

namespace cocos2d
{
	public class CCParticleSmoke : CCParticleSystemQuad
	{
		public CCParticleSmoke()
		{
		}

		~CCParticleSmoke()
		{
		}

		public bool init()
		{
			return this.initWithTotalParticles(200);
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
			this.modeA.speed = 25f;
			this.modeA.speedVar = 10f;
			base.Angle = 90f;
			base.AngleVar = 5f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, 0f);
			this.PosVar = new CCPoint(20f, 0f);
			base.Life = 4f;
			base.LifeVar = 1f;
			base.StartSize = 60f;
			base.StartSizeVar = 10f;
			base.EndSize = -1f;
			base.EmissionRate = (float)((float)base.TotalParticles) / base.Life;
			base.StartColor.r = 0.8f;
			base.StartColor.g = 0.8f;
			base.StartColor.b = 0.8f;
			base.StartColor.a = 1f;
			base.StartColorVar.r = 0.02f;
			base.StartColorVar.g = 0.02f;
			base.StartColorVar.b = 0.02f;
			base.StartColorVar.a = 0f;
			base.EndColor.r = 0f;
			base.EndColor.g = 0f;
			base.EndColor.b = 0f;
			base.EndColor.a = 1f;
			base.EndColorVar.r = 0f;
			base.EndColorVar.g = 0f;
			base.EndColorVar.b = 0f;
			base.EndColorVar.a = 0f;
			base.IsBlendAdditive = false;
			return true;
		}

		public static new CCParticleSmoke node()
		{
			CCParticleSmoke cCParticleSmoke = new CCParticleSmoke();
			if (cCParticleSmoke.init())
			{
				return cCParticleSmoke;
			}
			return null;
		}
	}
}