using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class ParallaxTestScene : TestScene
    {
        public override void runThisTest()
        {
            CCLayer pLayer = nextParallaxAction();

            addChild(pLayer);
            CCDirector.sharedDirector().replaceScene(this);
        }

        static int sceneIdx = -1;
        static int MAX_LAYER = 2;
        public static CCLayer nextParallaxAction()
        {
            sceneIdx++;
            sceneIdx = sceneIdx % MAX_LAYER;

            CCLayer pLayer = createParallaxTestLayer(sceneIdx);

            return pLayer;
        }

        public static CCLayer createParallaxTestLayer(int nIndex)
        {
            switch (nIndex)
            {
                case 0: return new Parallax1();
                case 1: return new Parallax2();
            }

            return null;
        }

        public static CCLayer backParallaxAction()
        {
            sceneIdx--;
            int total = MAX_LAYER;
            if (sceneIdx < 0)
                sceneIdx += total;

            CCLayer pLayer = createParallaxTestLayer(sceneIdx);

            return pLayer;
        }

        public static CCLayer restartParallaxAction()
        {
            CCLayer pLayer = createParallaxTestLayer(sceneIdx);

            return pLayer;
        } 
    }
}
