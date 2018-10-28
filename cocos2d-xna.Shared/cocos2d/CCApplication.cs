using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace cocos2d
{
	public abstract class CCApplication : DrawableGameComponent
	{
		private Microsoft.Xna.Framework.Game game;

		internal GraphicsDeviceManager graphics;

		protected Rectangle m_rcViewPort;

		protected IEGLTouchDelegate m_pDelegate;

		protected bool m_bCaptured;

		protected float m_fScreenScaleFactor;

		private readonly LinkedList<CCTouch> m_pTouches;

		private readonly Dictionary<int, LinkedListNode<CCTouch>> m_pTouchMap;

		private TouchCollection m_currentTouchCollection;

		protected static CCApplication sm_pSharedApplication;

		internal Matrix worldMatrix;

		internal Matrix viewMatrix;

		internal Matrix projectionMatrix;

		private bool m_bOrientationReverted;

		private Point m_tSizeInPoints;

		private CCSize _size = new CCSize(800f, 480f);

		public double animationInterval
		{
			set
			{
				this.game.TargetElapsedTime = TimeSpan.FromSeconds(value);
			}
		}

		public BasicEffect basicEffect
		{
			get;
			private set;
		}

		public bool canSetContentScaleFactor
		{
			get
			{
				return true;
			}
		}

		public ContentManager content
		{
			get;
			private set;
		}

		public TouchCollection currentTouchCollection
		{
			get
			{
				return this.m_currentTouchCollection;
			}
		}

		internal SpriteBatch spriteBatch
		{
			get;
			private set;
		}

		public IEGLTouchDelegate TouchDelegate
		{
			set
			{
				this.m_pDelegate = value;
			}
		}

		public CCApplication(Microsoft.Xna.Framework.Game game, GraphicsDeviceManager graphics) : base(game)
		{
			this.game = game;
			this.graphics = graphics;
			this.content = game.Content;
			game.Window.OrientationChanged += new EventHandler<EventArgs>(this.Window_OrientationChanged);
			TouchPanel.EnabledGestures = GestureType.Tap;
			this.m_pTouches = new LinkedList<CCTouch>();
			this.m_pTouchMap = new Dictionary<int, LinkedListNode<CCTouch>>();
			this.m_fScreenScaleFactor = 1f;
			this.m_rcViewPort = new Rectangle(0, 0, 800, 480);
			this._size = new CCSize(800f, 480f);
		}

		public virtual void applicationDidEnterBackground()
		{
		}

		public virtual bool applicationDidFinishLaunching()
		{
			return false;
		}

		public virtual void applicationWillEnterForeground()
		{
		}

		private void centerWindow()
		{
		}

		public void ClearTouches()
		{
			this.m_pTouches.Clear();
			this.m_pTouchMap.Clear();
		}

		public override void Draw(GameTime gameTime)
		{
			this.basicEffect.View = this.viewMatrix;
			this.basicEffect.World = this.worldMatrix;
			this.basicEffect.Projection = this.projectionMatrix;
			CCDirector.sharedDirector().mainLoop(gameTime);
			base.Draw(gameTime);
		}

		public CCSize getSize()
		{
			return new CCSize(this._size.width, this._size.height);
		}

		private CCTouch getTouchBasedOnID(int nID)
		{
			if (this.m_pTouchMap.ContainsKey(nID))
			{
				LinkedListNode<CCTouch> item = this.m_pTouchMap[nID];
				if (item.Value.view() == nID)
				{
					return item.Value;
				}
			}
			return null;
		}

		private void graphics_DeviceCreated(object sender, EventArgs e)
		{
			int width = this.graphics.GraphicsDevice.Viewport.Width;
			Viewport viewport = this.graphics.GraphicsDevice.Viewport;
			this.m_rcViewPort = new Rectangle(0, 0, width, viewport.Height);
			float single = (float)this.graphics.GraphicsDevice.Viewport.Width;
			Viewport viewport1 = this.graphics.GraphicsDevice.Viewport;
			this._size = new CCSize(single, (float)viewport1.Height);
		}

		public override void Initialize()
		{
			CCApplication.sm_pSharedApplication = this;
			CCApplication.PVRFrameEnableControlWindow(false);
			this.initInstance();
			base.Initialize();
		}

		public virtual bool initInstance()
		{
			return true;
		}

		protected override void LoadContent()
		{
			this.spriteBatch = new SpriteBatch(base.GraphicsDevice);
			this.basicEffect = new BasicEffect(base.GraphicsDevice);
			base.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
			base.LoadContent();
			this.applicationDidFinishLaunching();
		}

		private void ProcessTouch()
		{
			if (this.m_pDelegate != null)
			{
				this.m_currentTouchCollection = TouchPanel.GetState();
				List<CCTouch> cCTouches = new List<CCTouch>();
				List<CCTouch> cCTouches1 = new List<CCTouch>();
				List<CCTouch> cCTouches2 = new List<CCTouch>();
				foreach (TouchLocation mCurrentTouchCollection in this.m_currentTouchCollection)
				{
					switch (mCurrentTouchCollection.State)
					{
						case TouchLocationState.Released:
						{
							if (!this.m_pTouchMap.ContainsKey(mCurrentTouchCollection.Id))
							{
								continue;
							}
							cCTouches2.Add(this.m_pTouchMap[mCurrentTouchCollection.Id].Value);
							this.m_pTouches.Remove(this.m_pTouchMap[mCurrentTouchCollection.Id]);
							this.m_pTouchMap.Remove(mCurrentTouchCollection.Id);
							continue;
						}
						case TouchLocationState.Pressed:
						{
							if (!this.m_rcViewPort.Contains((int)mCurrentTouchCollection.Position.X, (int)mCurrentTouchCollection.Position.Y))
							{
								continue;
							}
							this.m_pTouches.AddLast(new CCTouch(mCurrentTouchCollection.Id, mCurrentTouchCollection.Position.X - (float)this.m_rcViewPort.Left / this.m_fScreenScaleFactor, mCurrentTouchCollection.Position.Y - (float)this.m_rcViewPort.Top / this.m_fScreenScaleFactor));
							this.m_pTouchMap[mCurrentTouchCollection.Id] = this.m_pTouches.Last;
							cCTouches.Add(this.m_pTouches.Last.Value);
							continue;
						}
						case TouchLocationState.Moved:
						{
							if (!this.m_pTouchMap.ContainsKey(mCurrentTouchCollection.Id))
							{
								continue;
							}
							cCTouches1.Add(this.m_pTouchMap[mCurrentTouchCollection.Id].Value);
							this.m_pTouchMap[mCurrentTouchCollection.Id].Value.SetTouchInfo(mCurrentTouchCollection.Id, mCurrentTouchCollection.Position.X - (float)this.m_rcViewPort.Left / this.m_fScreenScaleFactor, mCurrentTouchCollection.Position.Y - (float)this.m_rcViewPort.Top / this.m_fScreenScaleFactor);
							continue;
						}
					}
					throw new ArgumentOutOfRangeException();
				}
				if (cCTouches.Count > 0)
				{
					this.m_pDelegate.touchesBegan(cCTouches, null);
				}
				if (cCTouches1.Count > 0)
				{
					this.m_pDelegate.touchesMoved(cCTouches1, null);
				}
				if (cCTouches2.Count > 0)
				{
					this.m_pDelegate.touchesEnded(cCTouches2, null);
				}
			}
		}

		private static void PVRFrameEnableControlWindow(bool bEnable)
		{
		}

		private void resize(int width, int height)
		{
		}

		public void setContentScaleFactor(float contentScaleFactor)
		{
			this.m_fScreenScaleFactor = contentScaleFactor;
			bool mBOrientationReverted = this.m_bOrientationReverted;
			this.centerWindow();
		}

		public Orientation setOrientation(Orientation orientation)
		{
			int num = 480;
			int num1 = 800;
			switch (orientation)
			{
				case Orientation.kOrientationLandscapeLeft:
				{
					this.graphics.PreferredBackBufferWidth = num1;
					this.graphics.PreferredBackBufferHeight = num;
					this._size = new CCSize((float)num1, (float)num);
					this.m_rcViewPort = new Rectangle(0, 0, num1, num);
					this.graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
					this.graphics.ApplyChanges();
					return Orientation.kOrientationLandscapeLeft;
				}
				case Orientation.kOrientationLandscapeRight:
				{
					this.graphics.PreferredBackBufferWidth = num1;
					this.graphics.PreferredBackBufferHeight = num;
					this._size = new CCSize((float)num1, (float)num);
					this.m_rcViewPort = new Rectangle(0, 0, num1, num);
					this.graphics.SupportedOrientations = DisplayOrientation.LandscapeRight;
					this.graphics.ApplyChanges();
					return Orientation.kOrientationLandscapeRight;
				}
			}
			this.graphics.PreferredBackBufferWidth = num;
			this.graphics.PreferredBackBufferHeight = num1;
			this._size = new CCSize((float)num, (float)num1);
			this.m_rcViewPort = new Rectangle(0, 0, num, num1);
			this.graphics.SupportedOrientations = DisplayOrientation.Portrait;
			this.graphics.ApplyChanges();
			return Orientation.kOrientationPortrait;
		}

		public static CCApplication sharedApplication()
		{
			return CCApplication.sm_pSharedApplication;
		}

		public void statusBarFrame(out CCRect rect)
		{
			rect = new CCRect(0f, 0f, 0f, 0f);
		}

		public override void Update(GameTime gameTime)
		{
			this.ProcessTouch();
			if (!CCDirector.sharedDirector().isPaused)
			{
				CCScheduler.sharedScheduler().tick((float)gameTime.ElapsedGameTime.TotalSeconds);
			}
			base.Update(gameTime);
		}

		private void Window_OrientationChanged(object sender, EventArgs e)
		{
		}
	}
}