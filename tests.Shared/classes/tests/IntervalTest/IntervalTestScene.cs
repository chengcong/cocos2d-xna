using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class IntervalTestScene : TestScene
    {
        public override void runThisTest()
        {
            CCLayer pLayer = new IntervalLayer();
            addChild(pLayer);

            CCDirector.sharedDirector().replaceScene(this);
        }
    }
}
