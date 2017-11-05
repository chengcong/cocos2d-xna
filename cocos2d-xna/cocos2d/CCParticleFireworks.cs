using System;

namespace cocos2d
{
	public class CCParticleFireworks : CCParticleSystemQuad
	{
		public CCParticleFireworks()
		{
		}

		~CCParticleFireworks()
		{
		}

		public bool init()
		{
			return this.initWithTotalParticles(1500);
		}

		public override bool initWithTotalParticles(uint numberOfParticles)
		{
			if (!base.initWithTotalParticles(numberOfParticles))
			{
				return false;
			}
			base.Duration = -1f;
			base.EmitterMode = 0;
			this.modeA.gravity = new CCPoint(0f, -90f);
			this.modeA.radialAccel = 0f;
			this.modeA.radialAccelVar = 0f;
			this.modeA.speed = 180f;
			this.modeA.speedVar = 50f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			base.Angle = 90f;
			base.AngleVar = 20f;
			base.Life = 3.5f;
			base.LifeVar = 1f;
			base.EmissionRate = (float)((float)base.TotalParticles) / base.Life;
			base.StartColor.r = 0.5f;
			base.StartColor.g = 0.5f;
			base.StartColor.b = 0.5f;
			base.StartColor.a = 1f;
			base.StartColorVar.r = 0.5f;
			base.StartColorVar.g = 0.5f;
			base.StartColorVar.b = 0.5f;
			base.StartColorVar.a = 0.1f;
			base.EndColor.r = 0.1f;
			base.EndColor.g = 0.1f;
			base.EndColor.b = 0.1f;
			base.EndColor.a = 0.2f;
			base.EndColorVar.r = 0.1f;
			base.EndColorVar.g = 0.1f;
			base.EndColorVar.b = 0.1f;
			base.EndColorVar.a = 0.2f;
			base.StartSize = 8f;
			base.StartSizeVar = 2f;
			base.EndSize = -1f;
			base.IsBlendAdditive = false;
			return true;
		}

		public static new CCParticleFireworks node()
		{
			CCParticleFireworks cCParticleFirework = new CCParticleFireworks();
			if (cCParticleFirework.init())
			{
				return cCParticleFirework;
			}
			return null;
		}
	}
}