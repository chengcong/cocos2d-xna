using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace cocos2d
{
	public abstract class CCDirector : CCObject
	{
		private readonly double kDefaultFPS = 60;

		private static CCDirector s_sharedDirector;

		private static bool s_bFirstRun;

		protected bool m_bPurgeDirecotorInNextLoop;

		protected double m_dAnimationInterval;

		protected double m_dOldAnimationInterval;

		private bool m_bLandscape;

		private bool m_bDisplayFPS;

		private float m_fAccumDt;

		private float m_fFrameRate;

		private bool m_bPaused;

		private uint m_uTotalFrames;

		private uint m_uFrames;

		private float m_fDeltaTime;

		private CCScene m_pRunningScene;

		private CCScene m_pNextScene;

		private bool m_bSendCleanupToScene;

		private List<CCScene> m_pobScenesStack = new List<CCScene>();

		private ccDirectorProjection m_eProjection;

		private CCSize m_obWinSizeInPoints;

		private CCSize m_obWinSizeInPixels;

		private float m_fContentScaleFactor = 1f;

		private string m_pszFPS;

		private CCNode m_pNotificationNode;

		private ccDeviceOrientation m_eDeviceOrientation;

		private bool m_bIsContentScaleSupported;

		private bool m_bRetinaDisplay;

		public virtual double animationInterval
		{
			get
			{
				return this.m_dAnimationInterval;
			}
			set
			{
			}
		}

		public float ContentScaleFactor
		{
			get
			{
				return this.m_fContentScaleFactor;
			}
			set
			{
				if (value != this.m_fContentScaleFactor)
				{
					this.m_fContentScaleFactor = value;
					this.m_obWinSizeInPixels = new CCSize(this.m_obWinSizeInPoints.width * value, this.m_obWinSizeInPoints.height * value);
					this.updateContentScaleFactor();
					this.Projection = this.m_eProjection;
				}
			}
		}

		public ccDeviceOrientation deviceOrientation
		{
			get
			{
				return this.m_eDeviceOrientation;
			}
			set
			{
				ccDeviceOrientation _ccDeviceOrientation = (ccDeviceOrientation)CCApplication.sharedApplication().setOrientation((Orientation)value);
				if (this.m_eDeviceOrientation == _ccDeviceOrientation)
				{
					this.m_obWinSizeInPoints = CCApplication.sharedApplication().getSize();
					this.m_obWinSizeInPixels = new CCSize(this.m_obWinSizeInPoints.width * this.m_fContentScaleFactor, this.m_obWinSizeInPoints.height * this.m_fContentScaleFactor);
					this.Projection = this.m_eProjection;
					return;
				}
				this.m_eDeviceOrientation = _ccDeviceOrientation;
				this.m_obWinSizeInPoints = CCApplication.sharedApplication().getSize();
				this.m_obWinSizeInPixels = new CCSize(this.m_obWinSizeInPoints.width * this.m_fContentScaleFactor, this.m_obWinSizeInPoints.height * this.m_fContentScaleFactor);
				this.Projection = this.m_eProjection;
			}
		}

		public bool DisplayFPS
		{
			get
			{
				return this.m_bDisplayFPS;
			}
			set
			{
				this.m_bDisplayFPS = value;
			}
		}

		public CCSize displaySizeInPixels
		{
			get
			{
				return new CCSize(this.m_obWinSizeInPixels.width, this.m_obWinSizeInPixels.height);
			}
		}

		public bool isPaused
		{
			get
			{
				return this.m_bPaused;
			}
		}

		public ccDirectorProjection Projection
		{
			set
			{
				CCApplication identity = CCApplication.sharedApplication();
				CCSize size = CCApplication.sharedApplication().getSize();
				float single = this.zEye;
				switch (value)
				{
					case ccDirectorProjection.kCCDirectorProjection2D:
					{
						identity.viewMatrix = Matrix.CreateLookAt(new Vector3(size.width / 2f, size.height / 2f, 5f), new Vector3(size.width / 2f, size.height / 2f, 0f), Vector3.Up);
						identity.projectionMatrix = Matrix.CreateOrthographicOffCenter(-size.width / 2f, size.width / 2f, -size.height / 2f, size.height / 2f, -1024f, 1024f);
						identity.worldMatrix = Matrix.Identity;
						this.m_eProjection = value;
						return;
					}
					case ccDirectorProjection.kCCDirectorProjection3D:
					{
						identity.viewMatrix = Matrix.CreateLookAt(new Vector3(size.width / 2f, size.height / 2f, size.height / 1.1566f), new Vector3(size.width / 2f, size.height / 2f, 0f), Vector3.Up);
						identity.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(1.04719758f, size.width / size.height, 0.5f, 1500f);
						identity.worldMatrix = Matrix.Identity;
						this.m_eProjection = value;
						return;
					}
					case ccDirectorProjection.kCCDirectorProjectionCustom:
					{
						this.m_eProjection = value;
						return;
					}
					default:
					{
						this.m_eProjection = value;
						return;
					}
				}
			}
		}

		public CCScene runningScene
		{
			get
			{
				return this.m_pRunningScene;
			}
		}

		public CCSize winSizeInPixels
		{
			get
			{
				CCSize winSize = this.getWinSize();
				CCSize contentScaleFactor = winSize;
				contentScaleFactor.width = contentScaleFactor.width * CCDirector.sharedDirector().ContentScaleFactor;
				CCSize cCSize = winSize;
				cCSize.height = cCSize.height * CCDirector.sharedDirector().ContentScaleFactor;
				return winSize;
			}
		}

		public float zEye
		{
			get
			{
				return this.m_obWinSizeInPixels.height / 1.1566f;
			}
		}

		static CCDirector()
		{
			CCDirector.s_sharedDirector = new CCDisplayLinkDirector();
			CCDirector.s_bFirstRun = true;
		}

		protected CCDirector()
		{
		}

		public void applyOrientation()
		{
			switch (this.m_eDeviceOrientation)
			{
				default:
				{
					return;
				}
			}
		}

		public CCPoint convertToGL(CCPoint obPoint)
		{
			CCSize mObWinSizeInPoints = this.m_obWinSizeInPoints;
			return new CCPoint(obPoint.x, mObWinSizeInPoints.height - obPoint.y);
		}

		public CCPoint convertToUI(CCPoint obPoint)
		{
			CCSize mObWinSizeInPoints = this.m_obWinSizeInPoints;
			return new CCPoint(obPoint.x, mObWinSizeInPoints.height - obPoint.y);
		}

		protected void drawScene(GameTime gameTime)
		{
			if (!this.m_bPaused)
			{
				this.m_fDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
			if (this.m_pNextScene != null)
			{
				this.setNextScene();
			}
			this.applyOrientation();
			if (this.m_pRunningScene != null)
			{
				this.m_pRunningScene.visit();
			}
			if (this.m_pNotificationNode != null)
			{
				this.m_pNotificationNode.visit();
			}
			if (this.m_bDisplayFPS)
			{
				this.showFPS();
			}
			CCDirector mUTotalFrames = this;
			mUTotalFrames.m_uTotalFrames = mUTotalFrames.m_uTotalFrames + 1;
		}

		public bool enableRetinaDisplay(bool enabled)
		{
			if (enabled && this.m_fContentScaleFactor == 2f)
			{
				return true;
			}
			if (!enabled && this.m_fContentScaleFactor == 1f)
			{
				return false;
			}
			if (!CCApplication.sharedApplication().canSetContentScaleFactor)
			{
				return false;
			}
			float single = (float)((enabled ? 2 : 1));
			CCApplication.sharedApplication().setContentScaleFactor(single);
			if (this.m_fContentScaleFactor != 2f)
			{
				this.m_bRetinaDisplay = false;
			}
			else
			{
				this.m_bRetinaDisplay = true;
			}
			return true;
		}

		public void end()
		{
			this.m_bPurgeDirecotorInNextLoop = true;
		}

		public void endToLua()
		{
			this.end();
		}

		public uint getFrames()
		{
			return this.m_uFrames;
		}

		public CCScene getLastScene()
		{
			if (this.m_pobScenesStack.Count <= 1)
			{
				return null;
			}
			return this.m_pobScenesStack[this.m_pobScenesStack.Count - 2];
		}

		public CCSize getWinSize()
		{
			return new CCSize(this.m_obWinSizeInPoints.width, this.m_obWinSizeInPoints.height);
		}

		public virtual bool init()
		{
			double num = 1 / this.kDefaultFPS;
			double num1 = num;
			this.m_dAnimationInterval = num;
			this.m_dOldAnimationInterval = num1;
			this.m_eProjection = ccDirectorProjection.kCCDirectorProjection3D;
			this.m_bDisplayFPS = false;
			int num2 = 0;
			uint num3 = (uint)num2;
			this.m_uFrames = (uint)num2;
			this.m_uTotalFrames = num3;
			this.m_pszFPS = "";
			this.m_bPaused = false;
			this.m_bPaused = false;
			this.m_bPurgeDirecotorInNextLoop = false;
			CCSize cCSize = new CCSize(0f, 0f);
			CCSize cCSize1 = cCSize;
			this.m_obWinSizeInPoints = cCSize;
			this.m_obWinSizeInPixels = cCSize1;
			this.m_eDeviceOrientation = ccDeviceOrientation.kCCDeviceOrientationPortrait;
			this.m_bRetinaDisplay = false;
			this.m_fContentScaleFactor = 1f;
			this.m_bIsContentScaleSupported = false;
			return true;
		}

		public bool isRetinaDisplay()
		{
			return this.m_bRetinaDisplay;
		}

		public bool isSendCleanupToScene()
		{
			return this.m_bSendCleanupToScene;
		}

		public abstract void mainLoop(GameTime gameTime);

		public void pause()
		{
			if (this.m_bPaused)
			{
				return;
			}
			this.m_dOldAnimationInterval = this.m_dAnimationInterval;
			this.animationInterval = 0.25;
			this.m_bPaused = true;
		}

		public void popScene()
		{
			if (this.m_pobScenesStack.Count > 0)
			{
				this.m_pobScenesStack.RemoveAt(this.m_pobScenesStack.Count - 1);
			}
			int count = this.m_pobScenesStack.Count;
			if (count == 0)
			{
				//CCApplication.sharedApplication().Game.Exit();
                Environment.Exit(0);
				this.end();
				return;
			}
			this.m_bSendCleanupToScene = true;
			this.m_pNextScene = this.m_pobScenesStack[count - 1];
		}

		public void purgeCachedData()
		{
		}

		protected void purgeDirector()
		{
			CCScene mPRunningScene = this.m_pRunningScene;
			this.m_pRunningScene = null;
			this.m_pNextScene = null;
			this.m_pobScenesStack.Clear();
			this.stopAnimation();
		}

		public void pushScene(CCScene pScene)
		{
			this.m_bSendCleanupToScene = false;
			this.m_pobScenesStack.Add(pScene);
			this.m_pNextScene = pScene;
		}

		public void replaceScene(CCScene pScene)
		{
			int count = this.m_pobScenesStack.Count;
			this.m_bSendCleanupToScene = true;
			this.m_pobScenesStack[count - 1] = pScene;
			this.m_pNextScene = pScene;
		}

		public void reshapeProjection(CCSize newWindowSize)
		{
			this.m_obWinSizeInPoints = CCApplication.sharedApplication().getSize();
			this.m_obWinSizeInPixels = new CCSize(this.m_obWinSizeInPoints.width * this.m_fContentScaleFactor, this.m_obWinSizeInPoints.height * this.m_fContentScaleFactor);
			this.Projection = this.m_eProjection;
		}

		public void resume()
		{
			if (!this.m_bPaused)
			{
				return;
			}
			this.animationInterval = this.m_dOldAnimationInterval;
			this.m_bPaused = false;
		}

		public void runWithScene(CCScene pScene)
		{
			this.pushScene(pScene);
			this.startAnimation();
		}

		public void setAlphaBlending(bool bOn)
		{
		}

		public void setDepthTest(bool bOn)
		{
		}

		public static bool setDirectorType(ccDirectorType obDirectorType)
		{
			CCDirector.sharedDirector();
			return true;
		}

		public void setGLDefaultValues()
		{
			this.Projection = this.m_eProjection;
		}

		protected void setNextScene()
		{
			if (!(this.m_pNextScene is CCTransitionScene))
			{
				if (this.m_pRunningScene != null)
				{
					this.m_pRunningScene.onExit();
					CCApplication.sharedApplication().ClearTouches();
				}
				if (this.m_bSendCleanupToScene && this.m_pRunningScene != null)
				{
					this.m_pRunningScene.cleanup();
				}
			}
			this.m_pRunningScene = this.m_pNextScene;
			this.m_pNextScene = null;
			if (this.m_pRunningScene != null)
			{
				this.m_pRunningScene.onEnter();
				if (this.m_pRunningScene is CCTransitionScene)
				{
					this.m_pRunningScene.onEnterTransitionDidFinish();
				}
			}
		}

		public void setOpenGLView()
		{
			this.m_obWinSizeInPoints = CCApplication.sharedApplication().getSize();
			this.m_obWinSizeInPixels = new CCSize(this.m_obWinSizeInPoints.width * this.m_fContentScaleFactor, this.m_obWinSizeInPoints.height * this.m_fContentScaleFactor);
			this.setGLDefaultValues();
			if (this.m_fContentScaleFactor != 1f)
			{
				this.updateContentScaleFactor();
			}
			CCTouchDispatcher cCTouchDispatcher = CCTouchDispatcher.sharedDispatcher();
			CCApplication.sharedApplication().TouchDelegate = cCTouchDispatcher;
			cCTouchDispatcher.IsDispatchEvents = true;
		}

		public static CCDirector sharedDirector()
		{
			if (CCDirector.s_bFirstRun)
			{
				CCDirector.s_sharedDirector.init();
				CCDirector.s_bFirstRun = false;
			}
			return CCDirector.s_sharedDirector;
		}

		protected void showFPS()
		{
			CCDirector mUFrames = this;
			mUFrames.m_uFrames = mUFrames.m_uFrames + 1;
			CCDirector mFAccumDt = this;
			mFAccumDt.m_fAccumDt = mFAccumDt.m_fAccumDt + this.m_fDeltaTime;
			if (this.m_fAccumDt > ccMacros.CC_DIRECTOR_FPS_INTERVAL)
			{
				this.m_fFrameRate = (float)((float)this.m_uFrames) / this.m_fAccumDt;
				this.m_uFrames = 0;
				this.m_fAccumDt = 0f;
				this.m_pszFPS = string.Format("{0}", this.m_fFrameRate);
			}
			SpriteFont spriteFont = CCApplication.sharedApplication().content.Load<SpriteFont>("fonts/Arial");
			CCApplication.sharedApplication().spriteBatch.Begin();
			CCApplication.sharedApplication().spriteBatch.DrawString(spriteFont, this.m_pszFPS, new Vector2(0f, CCApplication.sharedApplication().getSize().height - 50f), new Color(0, 255, 255));
			CCApplication.sharedApplication().spriteBatch.End();
		}

		public void showProfilers()
		{
		}

		public abstract void startAnimation();

		public abstract void stopAnimation();

		protected void updateContentScaleFactor()
		{
			if (CCApplication.sharedApplication().canSetContentScaleFactor)
			{
				CCApplication.sharedApplication().setContentScaleFactor(this.m_fContentScaleFactor);
				this.m_bIsContentScaleSupported = true;
			}
		}
	}
}