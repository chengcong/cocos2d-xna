namespace cocos2d
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.IO;

    public class CCTexture2D : CCObject
    {
        public static CCTexture2DPixelFormat g_defaultAlphaPixelFormat = CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGBA8888;
        private static Color g_MyBlack = new Color(0, 0, 0, 0);
        private bool m_bHasPremultipliedAlpha = false;
        private bool m_bPVRHaveAlphaPremultiplied = true;
        private CCTexture2DPixelFormat m_ePixelFormat;
        private float m_fMaxS = 0f;
        private float m_fMaxT = 0f;
        private CCSize m_tContentSize = new CCSize();
        private uint m_uName;
        private int m_uPixelsHigh = 0;
        private int m_uPixelsWide = 0;
        private static uint NameIndex = 1;
        private Texture2D texture2D;

        public CCTexture2D()
        {
            this.Name = NameIndex++;
        }

        public uint bitsPerPixelForFormat()
        {
            throw new NotImplementedException();
        }

        public static CCTexture2DPixelFormat defaultAlphaPixelFormat()
        {
            throw new NotImplementedException();
        }

        public string description()
        {
            return string.Concat(new object[] { "<CCTexture2D | Dimensions = ", this.m_uPixelsWide, " x ", this.m_uPixelsHigh, " | Coordinates = (", this.m_fMaxS, ", ", this.m_fMaxT, ")>" });
        }

        public void generateMipmap()
        {
        }

        public CCSize getContentSize()
        {
            return new CCSize { width = this.m_tContentSize.width / ((float)ccMacros.CC_CONTENT_SCALE_FACTOR()), height = this.m_tContentSize.height / ((float)ccMacros.CC_CONTENT_SCALE_FACTOR()) };
        }

        public Texture2D getTexture2D()
        {
            return this.texture2D;
        }

        public bool initPremultipliedATextureWithImage(Texture2D texture, int POTWide, int POTHigh)
        {
            this.initTextureWithImage(texture, POTWide, POTHigh);
            this.m_bHasPremultipliedAlpha = true;
            return true;
        }

        public bool initTextureWithImage(Texture2D texture, int POTWide, int POTHigh)
        {
            this.texture2D = texture;
            this.m_tContentSize.width = this.texture2D.Width;
            this.m_tContentSize.height = this.texture2D.Height;
            this.m_uPixelsWide = POTWide;
            this.m_uPixelsHigh = POTHigh;
            this.m_fMaxS = this.m_tContentSize.width / ((float)POTWide);
            this.m_fMaxT = this.m_tContentSize.height / ((float)POTHigh);
            return true;
        }

        public bool initWithData(object data, CCTexture2DPixelFormat pixelFormat, uint pixelsWide, uint pixelsHigh, CCSize contentSize)
        {
            SurfaceFormat color = SurfaceFormat.Color;
            switch (pixelFormat)
            {
                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_Automatic:
                    color = SurfaceFormat.Color;
                    break;

                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGBA8888:
                    color = SurfaceFormat.Color;
                    break;

                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGB888:
                    color = SurfaceFormat.Color;
                    break;

                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGB565:
                    color = SurfaceFormat.Bgr565;
                    break;

                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_A8:
                    color = SurfaceFormat.Alpha8;
                    break;

                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_I8:
                    color = SurfaceFormat.Single;
                    break;

                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGBA4444:
                    color = SurfaceFormat.Bgra4444;
                    break;

                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGB5A1:
                    color = SurfaceFormat.Bgra5551;
                    break;

                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_PVRTC4:
                    color = SurfaceFormat.Vector4;
                    break;

                case CCTexture2DPixelFormat.kCCTexture2DPixelFormat_PVRTC2:
                    color = SurfaceFormat.Vector2;
                    break;
            }
            Texture2D textured = new Texture2D(CCApplication.sharedApplication().GraphicsDevice, (int)pixelsWide, (int)pixelsHigh, false, color);
            if (data is byte[])
            {
                textured.SetData<byte>((byte[])data);
            }
            this.m_tContentSize = new CCSize(contentSize);
            this.m_uPixelsWide = (int)pixelsWide;
            this.m_uPixelsHigh = (int)pixelsHigh;
            this.m_ePixelFormat = pixelFormat;
            this.m_fMaxS = contentSize.width / ((float)pixelsWide);
            this.m_fMaxT = contentSize.height / ((float)pixelsHigh);
            this.m_bHasPremultipliedAlpha = false;
            this.texture2D = textured;
            return true;
        }

        public bool initWithFile(string file)
        {
            throw new NotImplementedException();
        }

        public bool initWithPVRFile(string file)
        {
            throw new NotImplementedException();
        }

        public bool initWithString(string text, string fontName, float fontSize)
        {
            return this.initWithString(text, new CCSize(0f, 0f), CCTextAlignment.CCTextAlignmentCenter, fontName, fontSize);
        }

        public bool initWithString(string text, CCSize dimensions, CCTextAlignment alignment, string fontName, float fontSize)
        {
            return this.initWithString(text, dimensions, alignment, fontName, fontSize, Color.YellowGreen, g_MyBlack);
        }

        public bool initWithString(string text, string fontName, float fontSize, Color fgColor, Color bgColor)
        {
            return this.initWithString(text, new CCSize(0f, 0f), CCTextAlignment.CCTextAlignmentCenter, fontName, fontSize, fgColor, bgColor);
        }

        public bool initWithString(string text, CCSize dimensions, CCTextAlignment alignment, string fontName, float fontSize, Color fgColor, Color bgColor)
        {
            if ((dimensions.width >= 0f) && (dimensions.height >= 0f))
            {
                Vector2 vector2;
                if (string.IsNullOrEmpty(text))
                {
                    return false;
                }
                SpriteFont spriteFont = null;
                try
                {
                    spriteFont = CCApplication.sharedApplication().content.Load<SpriteFont>("fonts/" + fontName);
                }
                catch (Exception)
                {
                    if (fontName.EndsWith(".spritefont", StringComparison.OrdinalIgnoreCase))
                    {
                        fontName = fontName.Substring(0, fontName.Length - 11);
                        spriteFont = CCApplication.sharedApplication().content.Load<SpriteFont>("fonts/" + fontName);
                    }
                }
                if (CCSize.CCSizeEqualToSize(dimensions, new CCSize()))
                {
                    Vector2 vector = spriteFont.MeasureString(text);
                    dimensions.width = vector.X;
                    dimensions.height = vector.Y;
                }
                if (CCTextAlignment.CCTextAlignmentRight == alignment)
                {
                    vector2 = new Vector2(-(dimensions.width - spriteFont.MeasureString(text).X), 0f);
                }
                else if (CCTextAlignment.CCTextAlignmentCenter == alignment)
                {
                    vector2 = new Vector2(-(dimensions.width - spriteFont.MeasureString(text).X) / 2f, 0f);
                }
                else
                {
                    vector2 = new Vector2(0f, 0f);
                }
                float scale = 1f;
                try
                {
                    CCApplication application = CCApplication.sharedApplication();
                    RenderTarget2D renderTarget = new RenderTarget2D(application.graphics.GraphicsDevice, (int)dimensions.width, (int)dimensions.height);
                    application.graphics.GraphicsDevice.SetRenderTarget(renderTarget);
                    application.graphics.GraphicsDevice.Clear(bgColor);
                    application.spriteBatch.Begin();
                    application.spriteBatch.DrawString(spriteFont, text, new Vector2(0f, 0f), fgColor, 0f, vector2, scale, SpriteEffects.None, 0f);
                    application.spriteBatch.End();
                    application.graphics.GraphicsDevice.SetRenderTarget(null);
                    Color[] data = new Color[renderTarget.Width * renderTarget.Height];
                    renderTarget.GetData<Color>(data);
                    this.texture2D = new Texture2D(application.GraphicsDevice, renderTarget.Width, renderTarget.Height);
                    this.texture2D.SetData<Color>(data);
                    return this.initWithTexture(this.texture2D);
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        public bool initWithTexture(Texture2D texture)
        {
            if (texture != null)
            {
                long num = ccUtils.ccNextPOT((long)texture.Width);
                long num2 = ccUtils.ccNextPOT((long)texture.Height);
                int maxTextureSize = CCConfiguration.sharedConfiguration().MaxTextureSize;
                if ((num2 <= maxTextureSize) && (num <= maxTextureSize))
                {
                    return this.initPremultipliedATextureWithImage(texture, texture.Width, texture.Height);
                }
                CCLog.Log(string.Format("cocos2d: WARNING: Image ({0} x {1}) is bigger than the supported {2} x {3}", new object[] { num, num2, maxTextureSize, maxTextureSize }));
            }
            return false;
        }

        public bool initWithTexture(Texture2D texture, CCSize contentSize)
        {
            bool flag = this.initWithTexture(texture);
            this.ContentSizeInPixels = contentSize;
            return flag;
        }

        public void releaseData(object data)
        {
        }

        public void SaveAsJpeg(Stream stream, int width, int height)
        {
            Texture2D textured1 = this.texture2D;
        }

        public void SaveAsPng(Stream stream, int width, int height)
        {
            Texture2D textured1 = this.texture2D;
        }

        public void setAliasTexParameters()
        {
            ccTexParams texParams = new ccTexParams();
            this.setTexParameters(texParams);
        }

        public void setAntiAliasTexParameters()
        {
        }

        public static void setDefaultAlphaPixelFormat(CCTexture2DPixelFormat format)
        {
            throw new NotImplementedException();
        }

        public void setPVRImagesHavePremultipliedAlpha(bool haveAlphaPremultiplied)
        {
            this.m_bPVRHaveAlphaPremultiplied = haveAlphaPremultiplied;
        }

        public void setTexParameters(ccTexParams texParams)
        {
        }

        public CCSize ContentSizeInPixels
        {
            get
            {
                return this.m_tContentSize;
            }
            set
            {
                this.m_tContentSize = value;
            }
        }

        public bool HasPremultipliedAlpha
        {
            get
            {
                return this.m_bHasPremultipliedAlpha;
            }
            set
            {
                this.m_bHasPremultipliedAlpha = value;
            }
        }

        public float MaxS
        {
            get
            {
                return this.m_fMaxS;
            }
            set
            {
                this.m_fMaxS = value;
            }
        }

        public float MaxT
        {
            get
            {
                return this.m_fMaxT;
            }
            set
            {
                this.m_fMaxT = value;
            }
        }

        public uint Name
        {
            get
            {
                return this.m_uName;
            }
            set
            {
                this.m_uName = value;
            }
        }

        public CCTexture2DPixelFormat PixelFormat
        {
            get
            {
                return this.m_ePixelFormat;
            }
            set
            {
                this.m_ePixelFormat = value;
            }
        }

        public int PixelsHigh
        {
            get
            {
                return this.m_uPixelsHigh;
            }
            set
            {
                this.m_uPixelsHigh = value;
            }
        }

        public int PixelsWide
        {
            get
            {
                return this.m_uPixelsWide;
            }
            set
            {
                this.m_uPixelsWide = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return this.texture2D;
            }
            set
            {
                this.texture2D = value;
            }
        }
    }
}
