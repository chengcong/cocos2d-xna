using System;

namespace cocos2d
{
	public class CCParticleSun : CCParticleSystemQuad
	{
		public CCParticleSun()
		{
		}

		~CCParticleSun()
		{
		}

		public bool init()
		{
			return this.initWithTotalParticles(350);
		}

		public override bool initWithTotalParticles(uint numberOfParticles)
		{
			if (!base.initWithTotalParticles(numberOfParticles))
			{
				return false;
			}
			base.IsBlendAdditive = true;
			base.Duration = -1f;
			base.EmitterMode = 0;
			this.modeA.gravity = new CCPoint(0f, 0f);
			this.modeA.radialAccel = 0f;
			this.modeA.radialAccelVar = 0f;
			this.modeA.speed = 20f;
			this.modeA.speedVar = 5f;
			base.Angle = 90f;
			base.AngleVar = 360f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			this.PosVar = new CCPoint(0f, 0f);
			base.Life = 1f;
			base.LifeVar = 0.5f;
			base.StartSize = 30f;
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
			return true;
		}

		public static new CCParticleSun node()
		{
			CCParticleSun cCParticleSun = new CCParticleSun();
			if (cCParticleSun.init())
			{
				return cCParticleSun;
			}
			return null;
		}
	}
}