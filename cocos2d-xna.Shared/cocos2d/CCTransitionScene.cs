using System;

namespace cocos2d
{
	public class CCTransitionScene : CCScene
	{
		protected CCScene m_pInScene;

		protected CCScene m_pOutScene;

		protected float m_fDuration;

		protected bool m_bIsInSceneOnTop;

		protected bool m_bIsSendCleanupToScene;

		public CCTransitionScene()
		{
		}

		public override void cleanup()
		{
			base.cleanup();
			if (this.m_bIsSendCleanupToScene)
			{
				this.m_pOutScene.cleanup();
			}
		}

		public override void draw()
		{
			base.draw();
			if (this.m_bIsInSceneOnTop)
			{
				this.m_pInScene.visit();
				this.m_pOutScene.visit();
				this.m_pInScene.visitDraw();
				return;
			}
			this.m_pOutScene.visit();
			this.m_pInScene.visit();
			this.m_pOutScene.visitDraw();
		}

		public void finish()
		{
			this.m_pInScene.visible = true;
			this.m_pInScene.position = new CCPoint(0f, 0f);
			this.m_pInScene.scale = 1f;
			this.m_pInScene.rotation = 0f;
			this.m_pInScene.Camera.restore();
			this.m_pOutScene.visible = false;
			this.m_pOutScene.position = new CCPoint(0f, 0f);
			this.m_pOutScene.scale = 1f;
			this.m_pOutScene.rotation = 0f;
			this.m_pOutScene.Camera.restore();
			base.schedule(new SEL_SCHEDULE(this.setNewScene), 0f);
		}

		public void hideOutShowIn()
		{
			this.m_pInScene.visible = true;
			this.m_pOutScene.visible = false;
		}

		public virtual bool initWithDuration(float t, CCScene scene)
		{
			if (scene == null)
			{
				throw new ArgumentNullException("scene", "Target scene must not be null");
			}
			if (!base.init())
			{
				return false;
			}
			this.m_fDuration = t;
			this.m_pInScene = scene;
			this.m_pOutScene = CCDirector.sharedDirector().runningScene;
			this.m_eSceneType = ccSceneFlag.ccTransitionScene;
			if (this.m_pInScene == this.m_pOutScene)
			{
				throw new ArgumentException("scene", "Target and source scenes must be different");
			}
			CCTouchDispatcher.sharedDispatcher().IsDispatchEvents = false;
			this.sceneOrder();
			return true;
		}

		public override void onEnter()
		{
			base.onEnter();
			this.m_pInScene.onEnter();
		}

		public override void onExit()
		{
			base.onExit();
			this.m_pOutScene.onExit();
			this.m_pInScene.onEnterTransitionDidFinish();
		}

		protected virtual void sceneOrder()
		{
			this.m_bIsInSceneOnTop = true;
		}

		private void setNewScene(float dt)
		{
			base.unschedule(new SEL_SCHEDULE(this.setNewScene));
			CCDirector cCDirector = CCDirector.sharedDirector();
			this.m_bIsSendCleanupToScene = cCDirector.isSendCleanupToScene();
			cCDirector.replaceScene(this.m_pInScene);
			CCTouchDispatcher.sharedDispatcher().IsDispatchEvents = true;
			this.m_pOutScene.visible = true;
		}

		public static CCTransitionScene transitionWithDuration(float t, CCScene scene)
		{
			CCTransitionScene cCTransitionScene = new CCTransitionScene();
			if (cCTransitionScene.initWithDuration(t, scene))
			{
				return cCTransitionScene;
			}
			return null;
		}
	}
}