using System;

namespace cocos2d
{
	public class CCTransitionRadialCCW : CCTransitionScene
	{
		private const int kSceneRadial = 2147483647;

		public CCTransitionRadialCCW()
		{
		}

		public override void onEnter()
		{
			base.onEnter();
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			CCRenderTexture cCPoint = CCRenderTexture.renderTextureWithWidthAndHeight((int)winSize.width, (int)winSize.height);
			if (cCPoint == null)
			{
				return;
			}
			cCPoint.Sprite.anchorPoint = new CCPoint(0.5f, 0.5f);
			cCPoint.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			cCPoint.anchorPoint = new CCPoint(0.5f, 0.5f);
			cCPoint.clear(0f, 0f, 0f, 1f);
			cCPoint.begin();
			this.m_pOutScene.visit();
			cCPoint.end();
			base.hideOutShowIn();
			CCProgressTimer cCProgressTimer = CCProgressTimer.progressWithTexture(cCPoint.Sprite.Texture);
			cCProgressTimer.Type = this.radialType();
			cCProgressTimer.Percentage = 100f;
			cCProgressTimer.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			cCProgressTimer.anchorPoint = new CCPoint(0.5f, 0.5f);
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { CCProgressFromTo.actionWithDuration(this.m_fDuration, 100f, 0f), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			cCProgressTimer.runAction(CCSequence.actions(cCFiniteTimeActionArray));
			this.addChild(cCProgressTimer, 2, 2147483647);
		}

		public override void onExit()
		{
			base.removeChildByTag(2147483647, false);
			base.onExit();
		}

		protected virtual CCProgressTimerType radialType()
		{
			return CCProgressTimerType.kCCProgressTimerTypeRadialCCW;
		}

		protected override void sceneOrder()
		{
			this.m_bIsInSceneOnTop = false;
		}

		public static new CCTransitionRadialCCW transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionRadialCCW cCTransitionRadialCCW = new CCTransitionRadialCCW();
			cCTransitionRadialCCW.initWithDuration(t, scene);
			return cCTransitionRadialCCW;
		}
	}
}