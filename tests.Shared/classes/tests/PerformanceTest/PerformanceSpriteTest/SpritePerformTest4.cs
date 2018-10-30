using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class SpritePerformTest4 : SpriteMainScene
    {

        public override void doTest(CCSprite sprite)
        {
            performanceOut100(sprite);
        }

        public override string title()
        {
            //char str[32] = {0};
            string str;
            //sprintf(str, "D (%d) 100%% out", subtestNumber);
            str = string.Format("D {0:D} 100%% out", subtestNumber);
            string strRet = str;
            return strRet;
        }

        private void performanceOut100(CCSprite pSprite)
        {
            pSprite.position = new CCPoint(-1000, -1000);
        }
    }
}
