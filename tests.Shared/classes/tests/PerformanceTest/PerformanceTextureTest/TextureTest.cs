using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;
using System.Diagnostics;

namespace tests
{
    public class TextureTest : TextureMenuLayer
    {
        public TextureTest(bool bControlMenuVisible, int nMaxCases, int nCurCase)
            : base(bControlMenuVisible, nMaxCases, nCurCase)
        {
        }

        public override void performTests()
        {
            //     CCTexture2D *texture;
            //     struct timeval now;
            //     CCTextureCache *cache = CCTextureCache::sharedTextureCache();

            Debug.WriteLine("\n\n--------\n\n");

            Debug.WriteLine("--- PNG 128x128 ---\n");
            performTestsPNG("Images/test_image");

            //     CCLog("--- PVR 128x128 ---\n");
            //     CCLog("RGBA 8888");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/test_image_rgba8888.pvr");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
            // 
            //     CCLog("BGRA 8888");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/test_image_bgra8888.pvr");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
            // 
            //     CCLog("RGBA 4444");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/test_image_rgba4444.pvr");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
            // 
            //     CCLog("RGB 565");
            //     gettimeofday(&now, NULL);
            //     texture = cache->addImage("Images/test_image_rgb565.pvr");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);


            Debug.WriteLine("\n\n--- PNG 512x512 ---\n");
            performTestsPNG("Images/texture512x512");

            //     CCLog("--- PVR 512x512 ---\n");
            //     CCLog("RGBA 4444");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/texture512x512_rgba4444.pvr");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);

            //
            // ---- 1024X1024
            // RGBA4444
            // Empty image
            //

            Debug.WriteLine("\n\nEMPTY IMAGE\n\n");
            Debug.WriteLine("--- PNG 1024x1024 ---\n");
            performTestsPNG("Images/texture1024x1024");

            //     CCLog("--- PVR 1024x1024 ---\n");
            //     CCLog("RGBA 4444");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/texture1024x1024_rgba4444.pvr");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
            // 
            //     CCLog("--- PVR.GZ 1024x1024 ---\n");
            //     CCLog("RGBA 4444");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/texture1024x1024_rgba4444.pvr.gz");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
            // 
            //     CCLog("--- PVR.CCZ 1024x1024 ---\n");
            //     CCLog("RGBA 4444");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/texture1024x1024_rgba4444.pvr.ccz");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);

            //
            // ---- 1024X1024
            // RGBA4444
            // SpriteSheet images
            //

            Debug.WriteLine("\n\nSPRITESHEET IMAGE\n\n");
            Debug.WriteLine("--- PNG 1024x1024 ---\n");
            performTestsPNG("Images/PlanetCute-1024x1024");

            //     CCLog("--- PVR 1024x1024 ---\n");
            //     CCLog("RGBA 4444");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/PlanetCute-1024x1024-rgba4444.pvr");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
            // 
            //     CCLog("--- PVR.GZ 1024x1024 ---\n");
            //     CCLog("RGBA 4444");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/PlanetCute-1024x1024-rgba4444.pvr.gz");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
            // 
            //     CCLog("--- PVR.CCZ 1024x1024 ---\n");
            //     CCLog("RGBA 4444");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/PlanetCute-1024x1024-rgba4444.pvr.ccz");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);


            //
            // ---- 1024X1024
            // RGBA8888
            // Landscape Image
            //

            Debug.WriteLine("\n\nLANDSCAPE IMAGE\n\n");

            Debug.WriteLine("--- PNG 1024x1024 ---\n");
            performTestsPNG("Images/landscape-1024x1024");

            //     CCLog("--- PVR 1024x1024 ---\n");
            //     CCLog("RGBA 8888");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/landscape-1024x1024-rgba8888.pvr");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
            // 
            //     CCLog("--- PVR.GZ 1024x1024 ---\n");
            //     CCLog("RGBA 8888");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/landscape-1024x1024-rgba8888.pvr.gz");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
            // 
            //     CCLog("--- PVR.CCZ 1024x1024 ---\n");
            //     CCLog("RGBA 8888");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/landscape-1024x1024-rgba8888.pvr.ccz");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);


            //
            // 2048x2048
            // RGBA444
            //

            // most platform don't support texture with width/height is 2048
            //     CCLog("\n\n--- PNG 2048x2048 ---\n");
            //     performTestsPNG("Images/texture2048x2048.png");

            //     CCLog("--- PVR 2048x2048 ---\n");
            //     CCLog("RGBA 4444");
            //     gettimeofday(&now, NULL);	
            //     texture = cache->addImage("Images/texture2048x2048_rgba4444.pvr");
            //     if( texture )
            //         CCLog("  ms:%f\n", calculateDeltaTime(&now) );
            //     else
            //         CCLog("ERROR\n");
            //     cache->removeTexture(texture);
        }

        public override string title()
        {
            return "Texture Performance Test";
        }

        public override string subtitle()
        {
            return "See console for results";
        }

        public void performTestsPNG(string filename)
        {
            //struct timeval now;
            DateTime now;
            CCTexture2D texture;
            CCTextureCache cache = CCTextureCache.sharedTextureCache();

            Debug.WriteLine("RGBA 8888");
            CCTexture2D.setDefaultAlphaPixelFormat(CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGBA8888);
            //gettimeofday(now);
            texture = cache.addImage(filename);
            //if (texture != null)
            //    Debug.WriteLine("  ms:%f\n", calculateDeltaTime(now));
            //else
            //    Debug.WriteLine(" ERROR\n");
            cache.removeTexture(texture);

            Debug.WriteLine("RGBA 4444");
            CCTexture2D.setDefaultAlphaPixelFormat(CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGBA4444);

            //gettimeofday(now);
            texture = cache.addImage(filename);
            //if (texture != null)
            //    Debug.WriteLine("  ms:%f\n", calculateDeltaTime(now));
            //else
            //    Debug.WriteLine(" ERROR\n");
            cache.removeTexture(texture);

            Debug.WriteLine("RGBA 5551");
            CCTexture2D.setDefaultAlphaPixelFormat(CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGB5A1);
            //gettimeofday(now);
            texture = cache.addImage(filename);
            //if (texture != null)
            //    Debug.WriteLine("  ms:%f\n", calculateDeltaTime(now));
            //else
            //    Debug.WriteLine(" ERROR\n");
            cache.removeTexture(texture);

            Debug.WriteLine("RGB 565");
            CCTexture2D.setDefaultAlphaPixelFormat(CCTexture2DPixelFormat.kCCTexture2DPixelFormat_RGB565);
            //gettimeofday(now);
            texture = cache.addImage(filename);
            //if (texture != null)
            //    Debug.WriteLine("  ms:%f\n", calculateDeltaTime(now));
            //else
            //    Debug.WriteLine(" ERROR\n");
            cache.removeTexture(texture);
        }

        public static CCScene scene()
        {
            CCScene pScene = CCScene.node();
            TextureTest layer = new TextureTest(false, PerformanceTextureTest.TEST_COUNT, PerformanceTextureTest.s_nTexCurCase);
            pScene.addChild(layer);

            return pScene;
        }
    }
}
