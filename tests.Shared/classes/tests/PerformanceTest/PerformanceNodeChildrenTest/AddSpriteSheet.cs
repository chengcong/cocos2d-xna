using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class AddSpriteSheet : AddRemoveSpriteSheet
    {
        public override void update(float dt)
        {
            // reset seed
            //srandom(0);

            // 15 percent
            int totalToAdd = (int)(currentQuantityOfNodes * 0.15f);

            if (totalToAdd > 0)
            {
                List<CCSprite> sprites = new List<CCSprite>();
                //int		 zs      = new int[totalToAdd];
                int[] zs = new int[totalToAdd];

                // Don't include the sprite creation time and random as part of the profiling
                for (int i = 0; i < totalToAdd; i++)
                {
                    CCSprite pSprite = CCSprite.spriteWithTexture(batchNode.Texture, new CCRect(0, 0, 32, 32));
                    sprites.Add(pSprite);
                    zs[i] = (int)(ccMacros.CCRANDOM_MINUS1_1() * 50);

                }

                // add them with random Z (very important!)
                //#if CC_ENABLE_PROFILERS
                //        CCProfilingBeginTimingBlock(_profilingTimer);
                //#endif

                for (int i = 0; i < totalToAdd; i++)
                {
                    batchNode.addChild((CCNode)(sprites[i]), zs[i], PerformanceNodeChildrenTest.kTagBase + i);
                }

                //#if CC_ENABLE_PROFILERS
                //        CCProfilingEndTimingBlock(_profilingTimer);
                //#endif

                // remove them
                for (int i = 0; i < totalToAdd; i++)
                {
                    batchNode.removeChildByTag(PerformanceNodeChildrenTest.kTagBase + i, true);
                }

            }
        }

        public override string title()
        {
            return "C - Add to spritesheet";
        }

        public override string subtitle()
        {
            return "Adds %10 of total sprites with random z. See console";
        }

        public override string profilerName()
        {
            return "add sprites";
        }
    }
}
