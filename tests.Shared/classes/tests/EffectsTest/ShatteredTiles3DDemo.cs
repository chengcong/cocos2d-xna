using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class ShatteredTiles3DDemo : CCShatteredTiles3D
    {
        public new static CCActionInterval actionWithDuration(float t)
        {
            return CCShatteredTiles3D.actionWithRange(5, true, new ccGridSize(16, 12), t);
        }
    }
}
