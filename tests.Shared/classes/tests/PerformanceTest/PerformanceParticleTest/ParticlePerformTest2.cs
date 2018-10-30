using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class ParticlePerformTest2 : ParticleMainScene
    {

        public override string title()
        {
            //char str[20] = {0};
            string str;
            //sprintf(str, "B (%d) size=8", subtestNumber);
            str = string.Format("B {0:D} size=8", subtestNumber);
            string strRet = str;
            return strRet;
        }

        public override void doTest()
        {
            CCSize s = CCDirector.sharedDirector().getWinSize();
            CCParticleSystem particleSystem = (CCParticleSystem)getChildByTag(PerformanceParticleTest.kTagParticleSystem);

            // duration
            particleSystem.Duration = -1;

            // gravity
            particleSystem.setGravity(new CCPoint(0, -90));

            // angle
            particleSystem.Angle = 90;
            particleSystem.AngleVar = 0;

            // radial
            particleSystem.setRadialAccel(0);
            particleSystem.setRadialAccelVar(0);

            // speed of particles
            particleSystem.setSpeed(180);
            particleSystem.setSpeedVar(50);

            // emitter position
            particleSystem.position = new CCPoint(s.width / 2, 100);
            particleSystem.PosVar = new CCPoint(s.width / 2, 0);

            // life of particles
            particleSystem.Life = 2.0f;
            particleSystem.LifeVar = 1;

            // emits per frame
            particleSystem.EmissionRate = particleSystem.TotalParticles / particleSystem.Life;

            // color of particles
            ccColor4F startColor = new ccColor4F { r = 0.5f, g = 0.5f, b = 0.5f, a = 1.0f };
            particleSystem.StartColor = startColor;

            ccColor4F startColorVar = new ccColor4F { r = 0.5f, g = 0.5f, b = 0.5f, a = 1.0f };
            particleSystem.StartColor = startColorVar;

            ccColor4F endColor = new ccColor4F { r = 0.1f, g = 0.1f, b = 0.1f, a = 0.2f };
            particleSystem.EndColor = endColor;

            ccColor4F endColorVar = new ccColor4F { r = 0.1f, g = 0.1f, b = 0.1f, a = 0.2f };
            particleSystem.EndColorVar = endColorVar;

            // size, in pixels
            particleSystem.EndSize = 8.0f;
            particleSystem.StartSize = 8.0f;
            particleSystem.EndSizeVar = 0;
            particleSystem.StartSizeVar = 0;

            // additive
            particleSystem.IsBlendAdditive = false;
        }
    }
}
