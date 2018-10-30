using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class PerformanceTestScene : TestScene
    {
        public override void runThisTest()
        {
            CCLayer pLayer = new PerformanceMainLayer();
            addChild(pLayer);

            CCDirector.sharedDirector().replaceScene(this);
        }

        public static int MAX_COUNT = 5;
        public static int LINE_SPACE = 40;
        public static int kItemTagBasic = 1000;
        public static string[] testsName = new string[5] {
            "PerformanceNodeChildrenTest",
            "PerformanceParticleTest",
            "PerformanceSpriteTest",
            "PerformanceTextureTest",
            "PerformanceTouchesTest"};
    }
}
