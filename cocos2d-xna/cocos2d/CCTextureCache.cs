namespace cocos2d
{
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class CCTextureCache : CCObject
    {
        private static CCTextureCache g_sharedTextureCache;
        private object m_pContextLock = new object();
        private object m_pDictLock = new object();
        protected Dictionary<string, CCTexture2D> m_pTextures = new Dictionary<string, CCTexture2D>();

        private CCTextureCache()
        {
        }

        public CCTexture2D addImage(string fileimage)
        {
            CCTexture2D textured;
            lock (this.m_pDictLock)
            {
                string key = fileimage;
                if (this.m_pTextures.TryGetValue(key, out textured))
                {
                    return textured;
                }
                Texture2D texture = CCApplication.sharedApplication().content.Load<Texture2D>(fileimage);
                textured = new CCTexture2D();
                if (textured.initWithTexture(texture))
                {
                    this.m_pTextures.Add(key, textured);
                    return textured;
                }
                return null;
            }
            return textured;
        }

        public CCTexture2D addPVRImage(string filename)
        {
            throw new NotImplementedException();
        }

        public void dumpCachedTextureInfo()
        {
        }

        ~CCTextureCache()
        {
            CCLog.Log("cocos2d: deallocing CCTextureCache.");
            this.m_pTextures.Clear();
        }

        public static void purgeSharedTextureCache()
        {
            g_sharedTextureCache = null;
        }

        public void removeAllTextures()
        {
            this.m_pTextures.Clear();
        }

        public void removeTexture(CCTexture2D texture)
        {
            if (texture != null)
            {
                string key = null;
                foreach (KeyValuePair<string, CCTexture2D> pair in this.m_pTextures)
                {
                    if (pair.Value == texture)
                    {
                        key = pair.Key;
                        break;
                    }
                }
                if (key != null)
                {
                    this.m_pTextures.Remove(key);
                }
            }
        }

        public void removeTextureForKey(string textureKeyName)
        {
            if (textureKeyName != null)
            {
                this.m_pTextures.Remove(textureKeyName);
            }
        }

        public void removeUnusedTextures()
        {
        }

        public static CCTextureCache sharedTextureCache()
        {
            if (g_sharedTextureCache == null)
            {
                g_sharedTextureCache = new CCTextureCache();
            }
            return g_sharedTextureCache;
        }

        public CCTexture2D textureForKey(string key)
        {
            CCTexture2D textured = null;
            try
            {
                this.m_pTextures.TryGetValue(key, out textured);
            }
            catch (ArgumentNullException)
            {
                CCLog.Log("Texture of key {0} is not exist.", new object[] { key });
            }
            return textured;
        }
    }
}
