using System;

namespace cocos2d
{
	public class CCParticleFlower : CCParticleSystemQuad
	{
		public CCParticleFlower()
		{
		}

		~CCParticleFlower()
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
			this.modeA.speed = 80f;
			this.modeA.speedVar = 10f;
			this.modeA.radialAccel = -60f;
			this.modeA.radialAccelVar = 0f;
			this.modeA.tangentialAccel = 15f;
			this.modeA.tangentialAccelVar = 0f;
			base.Angle = 90f;
			base.AngleVar = 360f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			this.PosVar = new CCPoint(0f, 0f);
			base.Life = 4f;
			base.LifeVar = 1f;
			base.StartSize = 30f;
			base.StartSizeVar = 10f;
			base.EndSize = -1f;
			base.EmissionRate = (float)((float)base.TotalParticles) / base.Life;
			base.StartColor.r = 0.5f;
			base.StartColor.g = 0.5f;
			base.StartColor.b = 0.5f;
			base.StartColor.a = 1f;
			base.StartColorVar.r = 0.5f;
			base.StartColorVar.g = 0.5f;
			base.StartColorVar.b = 0.5f;
			base.StartColorVar.a = 0.5f;
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

		public static new CCParticleFlower node()
		{
			CCParticleFlower cCParticleFlower = new CCParticleFlower();
			if (cCParticleFlower.init())
			{
				return cCParticleFlower;
			}
			return null;
		}
	}
}