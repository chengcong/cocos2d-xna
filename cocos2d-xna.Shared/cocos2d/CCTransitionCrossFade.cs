using System;

namespace cocos2d
{
	public class CCTransitionCrossFade : CCTransitionScene
	{
		private const int kSceneFade = 2147483647;

		public CCTransitionCrossFade()
		{
		}

		public override void draw()
		{
		}

		public override void onEnter()
		{
			base.onEnter();
			ccColor4B _ccColor4B = new ccColor4B(0, 0, 0, 0);
			CCSize winSize = CCDirector.sharedDirector().getWinSize();
			CCLayer cCLayer = new CCLayer();
			CCRenderTexture cCPoint = CCRenderTexture.renderTextureWithWidthAndHeight((int)winSize.width, (int)winSize.height);
			if (cCPoint == null)
			{
				return;
			}
			cCPoint.Sprite.anchorPoint = new CCPoint(0.5f, 0.5f);
			cCPoint.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			cCPoint.anchorPoint = new CCPoint(0.5f, 0.5f);
			cCPoint.begin();
			this.m_pInScene.visit();
			cCPoint.end();
			CCRenderTexture cCRenderTexture = CCRenderTexture.renderTextureWithWidthAndHeight((int)winSize.width, (int)winSize.height);
			cCRenderTexture.Sprite.anchorPoint = new CCPoint(0.5f, 0.5f);
			cCRenderTexture.position = new CCPoint(winSize.width / 2f, winSize.height / 2f);
			cCRenderTexture.anchorPoint = new CCPoint(0.5f, 0.5f);
			cCRenderTexture.begin();
			this.m_pOutScene.visit();
			cCRenderTexture.end();
			ccBlendFunc _ccBlendFunc = new ccBlendFunc(1, 1);
			ccBlendFunc _ccBlendFunc1 = new ccBlendFunc(770, 771);
			cCPoint.Sprite.BlendFunc = _ccBlendFunc;
			cCRenderTexture.Sprite.BlendFunc = _ccBlendFunc1;
			cCLayer.addChild(cCPoint);
			cCLayer.addChild(cCRenderTexture);
			cCPoint.Sprite.Opacity = 255;
			cCRenderTexture.Sprite.Opacity = 255;
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { CCFadeTo.actionWithDuration(this.m_fDuration, 0), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.hideOutShowIn)), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			CCAction cCAction = CCSequence.actions(cCFiniteTimeActionArray);
			cCRenderTexture.Sprite.runAction(cCAction);
			this.addChild(cCLayer, 2, 2147483647);
		}

		public override void onExit()
		{
			base.removeChildByTag(2147483647, false);
			base.onExit();
		}

		public static new CCTransitionCrossFade transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionCrossFade cCTransitionCrossFade = new CCTransitionCrossFade();
			if (cCTransitionCrossFade.initWithDuration(t, scene))
			{
				return cCTransitionCrossFade;
			}
			return null;
		}
	}
}