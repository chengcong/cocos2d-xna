using System;

namespace cocos2d
{
	public class CCParticleSnow : CCParticleSystemQuad
	{
		public CCParticleSnow()
		{
		}

		~CCParticleSnow()
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
			base.Duration = -1f;
			base.EmitterMode = 0;
			this.modeA.gravity = new CCPoint(0f, -1f);
			this.modeA.speed = 5f;
			this.modeA.speedVar = 1f;
			this.modeA.radialAccel = 0f;
			this.modeA.radialAccelVar = 1f;
			this.modeA.tangentialAccel = 0f;
			this.modeA.tangentialAccelVar = 1f;
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			this.position = new CCPoint(winSize.width / 2f, winSize.height + 10f);
			this.PosVar = new CCPoint(winSize.width / 2f, 0f);
			base.Angle = -90f;
			base.AngleVar = 5f;
			base.Life = 45f;
			base.LifeVar = 15f;
			base.StartSize = 10f;
			base.StartSizeVar = 5f;
			base.EndSize = -1f;
			base.EmissionRate = 10f;
			base.StartColor.r = 1f;
			base.StartColor.g = 1f;
			base.StartColor.b = 1f;
			base.StartColor.a = 1f;
			base.StartColorVar.r = 0f;
			base.StartColorVar.g = 0f;
			base.StartColorVar.b = 0f;
			base.StartColorVar.a = 0f;
			base.EndColor.r = 1f;
			base.EndColor.g = 1f;
			base.EndColor.b = 1f;
			base.EndColor.a = 0f;
			base.EndColorVar.r = 0f;
			base.EndColorVar.g = 0f;
			base.EndColorVar.b = 0f;
			base.EndColorVar.a = 0f;
			base.IsBlendAdditive = false;
			return true;
		}

		public static new CCParticleSnow node()
		{
			CCParticleSnow cCParticleSnow = new CCParticleSnow();
			if (cCParticleSnow.init())
			{
				return cCParticleSnow;
			}
			return null;
		}
	}
}