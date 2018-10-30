using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class Shaky3DDemo : CCShaky3D
    {
        public static CCActionInterval actionWithDuration(float t)
        {
            return CCShaky3D.actionWithRange(5, true, new ccGridSize(15, 10), t);
        }
    }
}
