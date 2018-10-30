using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using cocos2d;

namespace tests
{
    public class SchedulerPauseResume : SchedulerTestLayer
    {
        public override void onEnter()
        {
            base.onEnter();

            schedule(tick1, 0.5f);
            schedule(tick2, 0.5f);
            schedule(pause, 0.5f);
        }

        public override string title()
        {
            return "Pause / Resume";
        }

        public virtual string subtitle()
        {
            return "Scheduler should be paused after 3 seconds. See console";
        }

        public void tick1(float dt)
        {
            Debug.WriteLine("tick1");
        }

        public void tick2(float dt)
        {
            Debug.WriteLine("tick2");
        }

        public void pause(float dt)
        {
            CCScheduler.sharedScheduler().pauseTarget(this);
        }
    }
}
