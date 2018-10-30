using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace tests
{
    public class SchedulerUnscheduleAll : SchedulerTestLayer
    {
        public override void onEnter()
        {
            base.onEnter();
            schedule(tick1, 0.5f);
            schedule(tick2, 1.0f);
            schedule(tick3, 1.5f);
            schedule(tick4, 1.5f);
            schedule(unscheduleAll, 4);
        }

        public override string title()
        {
            return "Unschedule All selectors";
        }

        public override string subtitle()
        {
            return "All scheduled selectors will be unscheduled in 4 seconds. See console";
        }

        public void tick1(float dt)
        {
            Debug.WriteLine("tick1");
        }

        public void tick2(float dt)
        {
            Debug.WriteLine("tick2");
        }

        public void tick3(float dt)
        {
            Debug.WriteLine("tick3");
        }

        public void tick4(float dt)
        {
            Debug.WriteLine("tick4");
        }

        public void unscheduleAll(float dt)
        {
            unsheduleAllSelectors();
        }
    }
}
