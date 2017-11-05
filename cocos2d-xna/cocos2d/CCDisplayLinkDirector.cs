using Microsoft.Xna.Framework;
using System;

namespace cocos2d
{
	public class CCDisplayLinkDirector : CCDirector
	{
		private bool m_bInvalid;

		public override double animationInterval
		{
			get
			{
				return base.animationInterval;
			}
			set
			{
				this.m_dAnimationInterval = value;
				if (!this.m_bInvalid)
				{
					this.stopAnimation();
					this.startAnimation();
				}
			}
		}

		public CCDisplayLinkDirector()
		{
		}

		public override void mainLoop(GameTime gameTime)
		{
			if (this.m_bPurgeDirecotorInNextLoop)
			{
				base.purgeDirector();
				this.m_bPurgeDirecotorInNextLoop = false;
				return;
			}
			if (!this.m_bInvalid)
			{
				base.drawScene(gameTime);
			}
		}

		public override void startAnimation()
		{
			this.m_bInvalid = false;
		}

		public override void stopAnimation()
		{
			this.m_bInvalid = true;
		}
	}
}