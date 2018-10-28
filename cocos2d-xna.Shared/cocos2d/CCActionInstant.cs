using System;

namespace cocos2d
{
	public class CCActionInstant : CCFiniteTimeAction
	{
		public CCActionInstant()
		{
		}

		public override CCObject copyWithZone(CCZone zone)
		{
			CCZone cCZone = zone;
			CCActionInstant cCActionInstant = null;
			if (cCZone == null || cCZone.m_pCopyObject == null)
			{
				cCActionInstant = new CCActionInstant();
				cCZone = new CCZone(cCActionInstant);
			}
			else
			{
				cCActionInstant = (CCActionInstant)cCZone.m_pCopyObject;
			}
			base.copyWithZone(cCZone);
			return cCActionInstant;
		}

		~CCActionInstant()
		{
		}

		public override bool isDone()
		{
			return true;
		}

		public override CCFiniteTimeAction reverse()
		{
			return (CCFiniteTimeAction)this.copy();
		}

		public override void step(float dt)
		{
			this.update(1f);
		}

		public override void update(float dt)
		{
		}
	}
}