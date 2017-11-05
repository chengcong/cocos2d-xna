using System;

namespace cocos2d
{
	public class CCParticle
	{
		public CCPoint pos;

		public CCPoint startPos;

		public ccColor4F color;

		public ccColor4F deltaColor;

		public float size;

		public float deltaSize;

		public float rotation;

		public float deltaRotation;

		public float timeToLive;

		public CCParticle.sModeA modeA;

		public CCParticle.sModeB modeB;

		public CCParticle()
		{
			this.pos = new CCPoint();
			this.startPos = new CCPoint();
			this.color = new ccColor4F();
			this.deltaColor = new ccColor4F();
			this.modeA = new CCParticle.sModeA();
			this.modeB = new CCParticle.sModeB();
		}

		public void copy(CCParticle particleCopied)
		{
			this.pos.x = particleCopied.pos.x;
			this.pos.y = particleCopied.pos.y;
			this.startPos.x = particleCopied.startPos.x;
			this.startPos.y = particleCopied.startPos.y;
			this.color.r = particleCopied.color.r;
			this.color.g = particleCopied.color.g;
			this.color.b = particleCopied.color.b;
			this.color.a = particleCopied.color.a;
			this.deltaColor.r = particleCopied.deltaColor.r;
			this.deltaColor.g = particleCopied.deltaColor.g;
			this.deltaColor.b = particleCopied.deltaColor.b;
			this.deltaColor.a = particleCopied.deltaColor.a;
			this.size = particleCopied.size;
			this.deltaSize = particleCopied.deltaSize;
			this.rotation = particleCopied.rotation;
			this.deltaRotation = particleCopied.deltaRotation;
			this.timeToLive = particleCopied.timeToLive;
			this.modeA.dir.x = particleCopied.modeA.dir.x;
			this.modeA.dir.y = particleCopied.modeA.dir.y;
			this.modeA.radialAccel = particleCopied.modeA.radialAccel;
			this.modeA.tangentialAccel = particleCopied.modeA.tangentialAccel;
			this.modeB.angle = particleCopied.modeB.angle;
			this.modeB.degreesPerSecond = particleCopied.modeB.degreesPerSecond;
			this.modeB.radius = particleCopied.modeB.radius;
			this.modeB.deltaRadius = particleCopied.modeB.deltaRadius;
		}

		public class sModeA
		{
			public CCPoint dir;

			public float radialAccel;

			public float tangentialAccel;

			public sModeA()
			{
				this.dir = new CCPoint();
			}
		}

		public class sModeB
		{
			public float angle;

			public float degreesPerSecond;

			public float radius;

			public float deltaRadius;

			public sModeB()
			{
			}
		}
	}
}