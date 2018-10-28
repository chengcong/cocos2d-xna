using System;

namespace cocos2d
{
	public class CCParticleSpiral : CCParticleSystemQuad
	{
		public CCParticleSpiral()
		{
		}

		~CCParticleSpiral()
		{
		}

		public bool init()
		{
			return this.initWithTotalParticles(500);
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
			this.modeA.speed = 150f;
			this.modeA.speedVar = 0f;
			this.modeA.radialAccel = -380f;
			this.modeA.radialAccelVar = 0f;
			this.modeA.tangentialAccel = 45f;
			this.modeA.tangentialAccelVar = 0f;
			base.Angle = 90f;
			base.AngleVar = 0f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			this.PosVar = new CCPoint(0f, 0f);
			base.Life = 12f;
			base.LifeVar = 0f;
			base.StartSize = 20f;
			base.StartSizeVar = 0f;
			base.EndSize = -1f;
			base.EmissionRate = (float)((float)base.TotalParticles) / base.Life;
			base.StartColor.r = 0.5f;
			base.StartColor.g = 0.5f;
			base.StartColor.b = 0.5f;
			base.StartColor.a = 1f;
			base.StartColorVar.r = 0.5f;
			base.StartColorVar.g = 0.5f;
			base.StartColorVar.b = 0.5f;
			base.StartColorVar.a = 0f;
			base.EndColor.r = 0.5f;
			base.EndColor.g = 0.5f;
			base.EndColor.b = 0.5f;
			base.EndColor.a = 1f;
			base.EndColorVar.r = 0.5f;
			base.EndColorVar.g = 0.5f;
			base.EndColorVar.b = 0.5f;
			base.EndColorVar.a = 0f;
			base.IsBlendAdditive = false;
			return true;
		}

		public static new CCParticleSpiral node()
		{
			CCParticleSpiral cCParticleSpiral = new CCParticleSpiral();
			if (cCParticleSpiral.init())
			{
				return cCParticleSpiral;
			}
			return null;
		}
	}
}