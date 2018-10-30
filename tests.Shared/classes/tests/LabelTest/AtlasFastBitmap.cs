using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class AtlasFastBitmap : AtlasDemo
    {
        public AtlasFastBitmap()
        {
            // Upper Label
            for (int i = 0; i < 100; i++)
            {
                //char str[6] = {0};
                string str;
                //sprintf(str, "-%d-", i);
                str = string.Format("-{0}-", i);
                CCLabelBMFont label = CCLabelBMFont.labelWithString(str, "fonts/fnt/bitmapFontTest");
                addChild(label);

                CCSize s = CCDirector.sharedDirector().getWinSize();

                CCPoint p = new CCPoint(ccMacros.CCRANDOM_0_1() * s.width, ccMacros.CCRANDOM_0_1() * s.height);
                label.position = p;
                label.anchorPoint = new CCPoint(0.5f, 0.5f);
            }
        }

        public override string title()
        {
            return "CCLabelBMFont";
        }

        public override string subtitle()
        {
            return "Creating several CCLabelBMFont with the same .fnt file should be fast";
        }
    }
}
