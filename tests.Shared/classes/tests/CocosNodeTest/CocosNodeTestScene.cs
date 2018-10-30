using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class CocosNodeTestScene : TestScene
    {
        static int sceneIdx = -1;

        static int MAX_LAYER = 12;

        public static CCLayer createCocosNodeLayer(int nIndex)
        {
            switch (nIndex)
            {
                case 0: return new CameraCenterTest();
                case 1: return new Test2();
                case 2: return new Test4();
                case 3: return new Test5();
                case 4: return new Test6();
                case 5: return new StressTest1();
                case 6: return new StressTest2();
                case 7: return new NodeToWorld();
                case 8: return new SchedulerTest1();
                case 9: return new CameraOrbitTest();
                case 10: return new CameraZoomTest();
                case 11: return new ConvertToNode();
            }

            return null;
        }

        public static CCLayer nextCocosNodeAction()
        {
            sceneIdx++;
            sceneIdx = sceneIdx % MAX_LAYER;

            CCLayer pLayer = createCocosNodeLayer(sceneIdx);

            return pLayer;
        }

        public static CCLayer backCocosNodeAction()
        {
            sceneIdx--;
            int total = MAX_LAYER;
            if (sceneIdx < 0)
                sceneIdx += total;

            CCLayer pLayer = createCocosNodeLayer(sceneIdx);

            return pLayer;
        }

        public static CCLayer restartCocosNodeAction()
        {
            CCLayer pLayer = createCocosNodeLayer(sceneIdx);

            return pLayer;
        }

        public override void runThisTest()
        {
            CCLayer pLayer = nextCocosNodeAction();
            addChild(pLayer);

            CCDirector.sharedDirector().replaceScene(this);
        }
    }
}
