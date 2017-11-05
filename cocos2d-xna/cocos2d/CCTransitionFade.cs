using Microsoft.Xna.Framework;
using System;

namespace cocos2d
{
	public class CCTransitionFade : CCTransitionScene
	{
		private const int kSceneFade = 2147483647;

		protected ccColor4B m_tColor;

		public CCTransitionFade()
		{
		}

		public virtual bool initWithDuration(float duration, CCScene scene, ccColor3B color)
		{
			if (base.initWithDuration(duration, scene))
			{
				this.m_tColor = new ccColor4B()
				{
					r = color.r,
					g = color.g,
					b = color.b,
					a = 0
				};
			}
			return true;
		}

		public override bool initWithDuration(float t, CCScene scene)
		{
			this.initWithDuration(t, scene, new ccColor3B(Color.Black));
			return true;
		}

		public override void onEnter()
		{
			base.onEnter();
			CCLayerColor cCLayerColor = CCLayerColor.layerWithColor(this.m_tColor);
			this.m_pInScene.visible = false;
			this.addChild(cCLayerColor, 2, 2147483647);
			CCNode childByTag = base.getChildByTag(2147483647);
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { CCFadeIn.actionWithDuration(this.m_fDuration / 2f), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.hideOutShowIn)), CCFadeOut.actionWithDuration(this.m_fDuration / 2f), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			childByTag.runAction((CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray));
		}

		public override void onExit()
		{
			base.onExit();
			base.removeChildByTag(2147483647, false);
		}

		public static CCTransitionFade transitionWithDuration(float duration, CCScene scene, ccColor3B color)
		{
			CCTransitionFade cCTransitionFade = new CCTransitionFade();
			cCTransitionFade.initWithDuration(duration, scene, color);
			return cCTransitionFade;
		}

		public static new CCTransitionScene transitionWithDuration(float t, CCScene scene)
		{
			return CCTransitionFade.transitionWithDuration(t, scene, new ccColor3B());
		}
	}
}