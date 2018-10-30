using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class PageTransitionBackward : CCTransitionPageTurn
    {
        public new static CCTransitionScene transitionWithDuration(float t, CCScene s)
        {
            CCDirector.sharedDirector().setDepthTest(true);
            return CCTransitionPageTurn.transitionWithDuration(t, s, true);
        }
    }
}
