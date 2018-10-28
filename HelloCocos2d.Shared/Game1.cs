using cocos2d;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace HelloCocos2d
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            
            IsMouseVisible = true;
#if __ANDROID__||__IOS__
            this.graphics.IsFullScreen = true;
#else
            this.graphics.IsFullScreen = false;
#endif
#if WINDOWS_UWP
            TouchPanel.EnableMouseTouchPoint = true;
#endif
            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
#if !MONOGAME
            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
#endif
            CCApplication application = new AppDelegate(this, graphics);
            this.Components.Add(application);
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
#if __IOS__
                Environment.Exit(0);
#else
                this.Exit();
#endif

            // TODO: Add your update logic here


            base.Update(gameTime);
        }

    }
}
