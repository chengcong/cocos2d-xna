namespace cocos2d
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class CCParticleSystem : CCNode, ICCTextureProtocol, ICCBlendProtocol
    {
        private bool m_bIsActive = true;
        private bool m_bIsAutoRemoveOnFinish = false;
        private bool m_bIsBlendAdditive = false;
        private eParticlePositionType m_ePositionType = eParticlePositionType.kCCPositionTypeFree;
        private float m_fAngle = 0f;
        private float m_fAngleVar = 0f;
        private float m_fDuration = 0f;
        public float m_fElapsed = 0f;
        private float m_fEmissionRate = 0f;
        public float m_fEmitCounter = 0f;
        private float m_fEndSize = 0f;
        private float m_fEndSizeVar = 0f;
        private float m_fEndSpin = 0f;
        private float m_fEndSpinVar = 0f;
        private float m_fLife = 0f;
        private float m_fLifeVar = 0f;
        private float m_fStartSize = 0f;
        private float m_fStartSizeVar = 0f;
        private float m_fStartSpin = 0f;
        private float m_fStartSpinVar = 0f;
        private int m_nEmitterMode = 0;
        public CCParticle[] m_pParticles = null;
        private CCTexture2D m_pTexture = null;
        public string m_sPlistFile = "";
        private ccBlendFunc m_tBlendFunc = new ccBlendFunc();
        private ccColor4F m_tEndColor = new ccColor4F();
        private ccColor4F m_tEndColorVar = new ccColor4F();
        private CCPoint m_tPosVar = new CCPoint(0f, 0f);
        private CCPoint m_tSourcePosition = new CCPoint(0f, 0f);
        private ccColor4F m_tStartColor = new ccColor4F();
        private ccColor4F m_tStartColorVar = new ccColor4F();
        private uint m_uParticleCount = 0;
        public uint m_uParticleIdx = 0;
        private uint m_uTotalParticles = 0;
        public sModeA modeA = new sModeA();
        public sModeB modeB;

        public CCParticleSystem()
        {
            this.modeA.gravity = new CCPoint(0f, 0f);
            this.modeA.speed = 0f;
            this.modeA.speedVar = 0f;
            this.modeA.tangentialAccel = 0f;
            this.modeA.tangentialAccelVar = 0f;
            this.modeA.radialAccel = 0f;
            this.modeA.radialAccelVar = 0f;
            this.modeB = new sModeB();
            this.modeB.startRadius = 0f;
            this.modeB.startRadiusVar = 0f;
            this.modeB.endRadius = 0f;
            this.modeB.endRadiusVar = 0f;
            this.modeB.rotatePerSecond = 0f;
            this.modeB.rotatePerSecondVar = 0f;
            this.m_tBlendFunc = new ccBlendFunc();
            this.m_tBlendFunc.src = 0;
            this.m_tBlendFunc.dst = 0x303;
        }

        public bool addParticle()
        {
            if (this.isFull())
            {
                return false;
            }
            CCParticle particle = this.m_pParticles[this.m_uParticleCount];
            this.initParticle(particle);
            this.m_uParticleCount++;
            return true;
        }

        private string ChangeToZeroIfNull(string str)
        {
            if ("" == str)
            {
                str = "0";
            }
            return str;
        }

        ~CCParticleSystem()
        {
        }

        public float getEndRadius()
        {
            return this.modeB.endRadius;
        }

        public float getEndRadiusVar()
        {
            return this.modeB.endRadiusVar;
        }

        public CCPoint getGravity()
        {
            return this.modeA.gravity;
        }

        public float getRadialAccel()
        {
            return this.modeA.radialAccel;
        }

        public float getRadialAccelVar()
        {
            return this.modeA.radialAccelVar;
        }

        public float getRotatePerSecond()
        {
            return this.modeB.rotatePerSecond;
        }

        public float getRotatePerSecondVar()
        {
            return this.modeB.rotatePerSecondVar;
        }

        public float getSpeed()
        {
            return this.modeA.speed;
        }

        public float getSpeedVar()
        {
            return this.modeA.speedVar;
        }

        public float getStartRadius()
        {
            return this.modeB.startRadius;
        }

        public float getStartRadiusVar()
        {
            return this.modeB.startRadiusVar;
        }

        public float getTangentialAccel()
        {
            return this.modeA.tangentialAccel;
        }

        public float getTangentialAccelVar()
        {
            return this.modeA.tangentialAccelVar;
        }

        public void initParticle(CCParticle particle)
        {
            particle.timeToLive = this.m_fLife + (this.m_fLifeVar * ccMacros.CCRANDOM_MINUS1_1());
            particle.timeToLive = (0f > particle.timeToLive) ? 0f : particle.timeToLive;
            particle.pos.x = this.m_tSourcePosition.x + (this.m_tPosVar.x * ccMacros.CCRANDOM_MINUS1_1());
            particle.pos.x *= CCDirector.sharedDirector().ContentScaleFactor;
            particle.pos.y = this.m_tSourcePosition.y + (this.m_tPosVar.y * ccMacros.CCRANDOM_MINUS1_1());
            particle.pos.y *= CCDirector.sharedDirector().ContentScaleFactor;
            ccColor4F colorf = new ccColor4F
            {
                r = CCPointExtension.clampf(this.m_tStartColor.r + (this.m_tStartColorVar.r * ccMacros.CCRANDOM_MINUS1_1()), 0f, 1f),
                g = CCPointExtension.clampf(this.m_tStartColor.g + (this.m_tStartColorVar.g * ccMacros.CCRANDOM_MINUS1_1()), 0f, 1f),
                b = CCPointExtension.clampf(this.m_tStartColor.b + (this.m_tStartColorVar.b * ccMacros.CCRANDOM_MINUS1_1()), 0f, 1f),
                a = CCPointExtension.clampf(this.m_tStartColor.a + (this.m_tStartColorVar.a * ccMacros.CCRANDOM_MINUS1_1()), 0f, 1f)
            };
            ccColor4F colorf2 = new ccColor4F
            {
                r = CCPointExtension.clampf(this.m_tEndColor.r + (this.m_tEndColorVar.r * ccMacros.CCRANDOM_MINUS1_1()), 0f, 1f),
                g = CCPointExtension.clampf(this.m_tEndColor.g + (this.m_tEndColorVar.g * ccMacros.CCRANDOM_MINUS1_1()), 0f, 1f),
                b = CCPointExtension.clampf(this.m_tEndColor.b + (this.m_tEndColorVar.b * ccMacros.CCRANDOM_MINUS1_1()), 0f, 1f),
                a = CCPointExtension.clampf(this.m_tEndColor.a + (this.m_tEndColorVar.a * ccMacros.CCRANDOM_MINUS1_1()), 0f, 1f)
            };
            particle.color = colorf;
            particle.deltaColor.r = (colorf2.r - colorf.r) / particle.timeToLive;
            particle.deltaColor.g = (colorf2.g - colorf.g) / particle.timeToLive;
            particle.deltaColor.b = (colorf2.b - colorf.b) / particle.timeToLive;
            particle.deltaColor.a = (colorf2.a - colorf.a) / particle.timeToLive;
            float num = this.m_fStartSize + (this.m_fStartSizeVar * ccMacros.CCRANDOM_MINUS1_1());
            num = (0f > num) ? 0f : num;
            num *= CCDirector.sharedDirector().ContentScaleFactor;
            particle.size = num;
            if (this.m_fEndSize == -1f)
            {
                particle.deltaSize = 0f;
            }
            else
            {
                float num2 = this.m_fEndSize + (this.m_fEndSizeVar * ccMacros.CCRANDOM_MINUS1_1());
                num2 = (0f > num2) ? 0f : num2;
                num2 *= CCDirector.sharedDirector().ContentScaleFactor;
                particle.deltaSize = (num2 - num) / particle.timeToLive;
            }
            float num3 = this.m_fStartSpin + (this.m_fStartSpinVar * ccMacros.CCRANDOM_MINUS1_1());
            float num4 = this.m_fEndSpin + (this.m_fEndSpinVar * ccMacros.CCRANDOM_MINUS1_1());
            particle.rotation = num3;
            particle.deltaRotation = (num4 - num3) / particle.timeToLive;
            if (this.m_ePositionType == eParticlePositionType.kCCPositionTypeFree)
            {
                CCPoint v = base.convertToWorldSpace(new CCPoint(0f, 0f));
                particle.startPos = CCPointExtension.ccpMult(v, CCDirector.sharedDirector().ContentScaleFactor);
            }
            else if (this.m_ePositionType == eParticlePositionType.kCCPositionTypeRelative)
            {
                particle.startPos = CCPointExtension.ccpMult(base.m_tPosition, CCDirector.sharedDirector().ContentScaleFactor);
            }
            float num5 = ccMacros.CC_DEGREES_TO_RADIANS(this.m_fAngle + (this.m_fAngleVar * ccMacros.CCRANDOM_MINUS1_1()));
            if (this.m_nEmitterMode == 0)
            {
                CCPoint point2 = new CCPoint((float)Math.Cos((double)num5), (float)Math.Sin((double)num5));
                float s = this.modeA.speed + (this.modeA.speedVar * ccMacros.CCRANDOM_MINUS1_1());
                s *= CCDirector.sharedDirector().ContentScaleFactor;
                particle.modeA.dir = CCPointExtension.ccpMult(point2, s);
                particle.modeA.radialAccel = this.modeA.radialAccel + (this.modeA.radialAccelVar * ccMacros.CCRANDOM_MINUS1_1());
                particle.modeA.radialAccel *= CCDirector.sharedDirector().ContentScaleFactor;
                particle.modeA.tangentialAccel = this.modeA.tangentialAccel + (this.modeA.tangentialAccelVar * ccMacros.CCRANDOM_MINUS1_1());
                particle.modeA.tangentialAccel *= CCDirector.sharedDirector().ContentScaleFactor;
            }
            else
            {
                float num7 = this.modeB.startRadius + (this.modeB.startRadiusVar * ccMacros.CCRANDOM_MINUS1_1());
                float num8 = this.modeB.endRadius + (this.modeB.endRadiusVar * ccMacros.CCRANDOM_MINUS1_1());
                num7 *= CCDirector.sharedDirector().ContentScaleFactor;
                num8 *= CCDirector.sharedDirector().ContentScaleFactor;
                particle.modeB.radius = num7;
                if (this.modeB.endRadius == -1f)
                {
                    particle.modeB.deltaRadius = 0f;
                }
                else
                {
                    particle.modeB.deltaRadius = (num8 - num7) / particle.timeToLive;
                }
                particle.modeB.angle = num5;
                particle.modeB.degreesPerSecond = ccMacros.CC_DEGREES_TO_RADIANS(this.modeB.rotatePerSecond + (this.modeB.rotatePerSecondVar * ccMacros.CCRANDOM_MINUS1_1()));
            }
        }

        public bool initWithDictionary(Dictionary<string, object> dictionary)
        {
            bool flag = false;
            int num = ccUtils.ccParseInt(this.ChangeToZeroIfNull(this.valueForKey("maxParticles", dictionary)));
            if (this.initWithTotalParticles((uint)num))
            {
                this.m_fAngle = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("angle", dictionary)));
                this.m_fAngleVar = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("angleVariance", dictionary)));
                this.m_fDuration = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("duration", dictionary)));
                this.m_tBlendFunc.src = ccUtils.ccParseInt(this.ChangeToZeroIfNull(this.valueForKey("blendFuncSource", dictionary)));
                this.m_tBlendFunc.dst = ccUtils.ccParseInt(this.ChangeToZeroIfNull(this.valueForKey("blendFuncDestination", dictionary)));
                this.m_tStartColor.r = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startColorRed", dictionary)));
                this.m_tStartColor.g = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startColorGreen", dictionary)));
                this.m_tStartColor.b = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startColorBlue", dictionary)));
                this.m_tStartColor.a = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startColorAlpha", dictionary)));
                this.m_tStartColorVar.r = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startColorVarianceRed", dictionary)));
                this.m_tStartColorVar.g = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startColorVarianceGreen", dictionary)));
                this.m_tStartColorVar.b = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startColorVarianceBlue", dictionary)));
                this.m_tStartColorVar.a = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startColorVarianceAlpha", dictionary)));
                this.m_tEndColor.r = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishColorRed", dictionary)));
                this.m_tEndColor.g = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishColorGreen", dictionary)));
                this.m_tEndColor.b = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishColorBlue", dictionary)));
                this.m_tEndColor.a = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishColorAlpha", dictionary)));
                this.m_tEndColorVar.r = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishColorVarianceRed", dictionary)));
                this.m_tEndColorVar.g = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishColorVarianceGreen", dictionary)));
                this.m_tEndColorVar.b = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishColorVarianceBlue", dictionary)));
                this.m_tEndColorVar.a = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishColorVarianceAlpha", dictionary)));
                this.m_fStartSize = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startParticleSize", dictionary)));
                this.m_fStartSizeVar = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("startParticleSizeVariance", dictionary)));
                this.m_fEndSize = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishParticleSize", dictionary)));
                this.m_fEndSizeVar = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("finishParticleSizeVariance", dictionary)));
                float x = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("sourcePositionx", dictionary)));
                float y = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("sourcePositiony", dictionary)));
                this.position = new CCPoint(x, y);
                this.m_tPosVar.x = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("sourcePositionVariancex", dictionary)));
                this.m_tPosVar.y = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("sourcePositionVariancey", dictionary)));
                this.m_fStartSpin = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("rotationStart", dictionary)));
                this.m_fStartSpinVar = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("rotationStartVariance", dictionary)));
                this.m_fEndSpin = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("rotationEnd", dictionary)));
                this.m_fEndSpinVar = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("rotationEndVariance", dictionary)));
                this.m_nEmitterMode = ccUtils.ccParseInt(this.ChangeToZeroIfNull(this.valueForKey("emitterType", dictionary)), NumberStyles.AllowDecimalPoint);
                if (this.m_nEmitterMode == 0)
                {
                    this.modeA.gravity.x = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("gravityx", dictionary)));
                    this.modeA.gravity.y = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("gravityy", dictionary)));
                    this.modeA.speed = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("speed", dictionary)));
                    this.modeA.speedVar = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("speedVariance", dictionary)));
                    string str = this.valueForKey("radialAcceleration", dictionary);
                    this.modeA.radialAccel = (str != null) ? ccUtils.ccParseFloat(this.ChangeToZeroIfNull(str)) : 0f;
                    str = this.valueForKey("radialAccelVariance", dictionary);
                    this.modeA.radialAccelVar = (str != null) ? ccUtils.ccParseFloat(this.ChangeToZeroIfNull(str)) : 0f;
                    str = this.valueForKey("tangentialAcceleration", dictionary);
                    this.modeA.tangentialAccel = (str != null) ? ccUtils.ccParseFloat(this.ChangeToZeroIfNull(str)) : 0f;
                    str = this.valueForKey("tangentialAccelVariance", dictionary);
                    this.modeA.tangentialAccelVar = (str != null) ? ccUtils.ccParseFloat(this.ChangeToZeroIfNull(str)) : 0f;
                }
                else
                {
                    if (this.m_nEmitterMode != 1)
                    {
                        return flag;
                    }
                    this.modeB.startRadius = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("maxRadius", dictionary)));
                    this.modeB.startRadiusVar = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("maxRadiusVariance", dictionary)));
                    this.modeB.endRadius = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("minRadius", dictionary)));
                    this.modeB.endRadiusVar = 0f;
                    this.modeB.rotatePerSecond = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("rotatePerSecond", dictionary)));
                    this.modeB.rotatePerSecondVar = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("rotatePerSecondVariance", dictionary)));
                }
                this.m_fLife = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("particleLifespan", dictionary)));
                this.m_fLifeVar = ccUtils.ccParseFloat(this.ChangeToZeroIfNull(this.valueForKey("particleLifespanVariance", dictionary)));
                this.m_fEmissionRate = ((float)this.m_uTotalParticles) / this.m_fLife;
                string pszFilename = this.valueForKey("textureFileName", dictionary);
                string fileimage = CCFileUtils.fullPathFromRelativeFile(pszFilename, this.m_sPlistFile);
                CCTexture2D textured = null;
                if (pszFilename.Length > 0)
                {
                    bool isPopupNotify = CCFileUtils.IsPopupNotify;
                    CCFileUtils.IsPopupNotify = false;
                    textured = CCTextureCache.sharedTextureCache().addImage(fileimage);
                    CCFileUtils.IsPopupNotify = isPopupNotify;
                }
                if (textured == null)
                {
                    throw new NotImplementedException();
                }
                this.m_pTexture = textured;
                if (this.m_pTexture != null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool initWithFile(string plistFile)
        {
            this.m_sPlistFile = CCFileUtils.fullPathFromRelativePath(plistFile);
            Dictionary<string, object> dictionary = CCFileUtils.dictionaryWithContentsOfFile(this.m_sPlistFile);
            return this.initWithDictionary(dictionary);
        }

        public virtual bool initWithTotalParticles(uint numberOfParticles)
        {
            this.m_uTotalParticles = numberOfParticles;
            this.m_pParticles = new CCParticle[this.m_uTotalParticles];
            if (this.m_pParticles == null)
            {
                CCLog.Log("Particle system: not enough memory");
                return false;
            }
            for (int i = 0; i < this.m_uTotalParticles; i++)
            {
                this.m_pParticles[i] = new CCParticle();
            }
            this.m_bIsActive = true;
            this.m_tBlendFunc.src = 1;
            this.m_tBlendFunc.dst = 0x303;
            this.m_ePositionType = eParticlePositionType.kCCPositionTypeFree;
            this.m_nEmitterMode = 0;
            this.m_bIsAutoRemoveOnFinish = false;
            base.scheduleUpdateWithPriority(1);
            return true;
        }

        public bool isFull()
        {
            return (this.m_uParticleCount == this.m_uTotalParticles);
        }

        public static CCParticleSystem particleWithDictionary(Dictionary<string, object> dic)
        {
            CCParticleSystem system = new CCParticleSystem();
            if ((system != null) && system.initWithDictionary(dic))
            {
                return system;
            }
            return system;
        }

        public static CCParticleSystem particleWithFile(string plistFile)
        {
            CCParticleSystem system = new CCParticleSystem();
            if ((system != null) && system.initWithFile(plistFile))
            {
                return system;
            }
            return system;
        }

        public virtual void postStep()
        {
        }

        public void resetSystem()
        {
            this.m_bIsActive = true;
            this.m_fElapsed = 0f;
            this.m_uParticleIdx = 0;
            while (this.m_uParticleIdx < this.m_uParticleCount)
            {
                CCParticle particle = this.m_pParticles[this.m_uParticleIdx];
                particle.timeToLive = 0f;
                this.m_uParticleIdx++;
            }
        }

        public void setEndRadius(float endRadius)
        {
            this.modeB.endRadius = endRadius;
        }

        public void setEndRadiusVar(float endRadiusVar)
        {
            this.modeB.endRadiusVar = endRadiusVar;
        }

        public void setGravity(CCPoint g)
        {
            this.modeA.gravity = g;
        }

        public void setRadialAccel(float t)
        {
            this.modeA.radialAccel = t;
        }

        public void setRadialAccelVar(float t)
        {
            this.modeA.radialAccelVar = t;
        }

        public void setRotatePerSecond(float degrees)
        {
            this.modeB.rotatePerSecond = degrees;
        }

        public void setRotatePerSecondVar(float degrees)
        {
            this.modeB.rotatePerSecondVar = degrees;
        }

        public void setSpeed(float speed)
        {
            this.modeA.speed = speed;
        }

        public void setSpeedVar(float speedVar)
        {
            this.modeA.speedVar = speedVar;
        }

        public void setStartRadius(float startRadius)
        {
            this.modeB.startRadius = startRadius;
        }

        public void setStartRadiusVar(float startRadiusVar)
        {
            this.modeB.startRadiusVar = startRadiusVar;
        }

        public void setTangentialAccel(float t)
        {
            this.modeA.tangentialAccel = t;
        }

        public void setTangentialAccelVar(float t)
        {
            this.modeA.tangentialAccelVar = t;
        }

        public void stopSystem()
        {
            this.m_bIsActive = false;
            this.m_fElapsed = this.m_fDuration;
            this.m_fEmitCounter = 0f;
        }

        public override void update(float dt)
        {
            if (this.m_bIsActive && (this.m_fEmissionRate != 0f))
            {
                float num = 1f / this.m_fEmissionRate;
                this.m_fEmitCounter += dt;
                while ((this.m_uParticleCount < this.m_uTotalParticles) && (this.m_fEmitCounter > num))
                {
                    this.addParticle();
                    this.m_fEmitCounter -= num;
                }
                this.m_fElapsed += dt;
                if ((this.m_fDuration != -1f) && (this.m_fDuration < this.m_fElapsed))
                {
                    this.stopSystem();
                }
            }
            this.m_uParticleIdx = 0;
            CCPoint tPosition = new CCPoint(0f, 0f);
            if (this.m_ePositionType == eParticlePositionType.kCCPositionTypeFree)
            {
                tPosition = base.convertToWorldSpace(new CCPoint(0f, 0f));
                tPosition.x *= CCDirector.sharedDirector().ContentScaleFactor;
                tPosition.y *= CCDirector.sharedDirector().ContentScaleFactor;
            }
            else if (this.m_ePositionType == eParticlePositionType.kCCPositionTypeRelative)
            {
                tPosition = base.m_tPosition;
                tPosition.x *= CCDirector.sharedDirector().ContentScaleFactor;
                tPosition.y *= CCDirector.sharedDirector().ContentScaleFactor;
            }
            while (this.m_uParticleIdx < this.m_uParticleCount)
            {
                CCParticle particle = this.m_pParticles[this.m_uParticleIdx];
                particle.timeToLive -= dt;
                if (particle.timeToLive > 0f)
                {
                    CCPoint pos;
                    if (this.m_nEmitterMode == 0)
                    {
                        CCPoint v = new CCPoint(0f, 0f);
                        if ((particle.pos.x != 0f) || (particle.pos.y != 0f))
                        {
                            v = CCPointExtension.ccpNormalize(particle.pos);
                        }
                        CCPoint point4 = v;
                        v = CCPointExtension.ccpMult(v, particle.modeA.radialAccel);
                        float x = point4.x;
                        point4.x = -point4.y;
                        point4.y = x;
                        point4 = CCPointExtension.ccpMult(point4, particle.modeA.tangentialAccel);
                        CCPoint point2 = CCPointExtension.ccpMult(CCPointExtension.ccpAdd(CCPointExtension.ccpAdd(v, point4), this.modeA.gravity), dt);
                        particle.modeA.dir = CCPointExtension.ccpAdd(particle.modeA.dir, point2);
                        point2 = CCPointExtension.ccpMult(particle.modeA.dir, dt);
                        particle.pos = CCPointExtension.ccpAdd(particle.pos, point2);
                    }
                    else
                    {
                        particle.modeB.angle += particle.modeB.degreesPerSecond * dt;
                        particle.modeB.radius += particle.modeB.deltaRadius * dt;
                        particle.pos.x = -((float)Math.Cos((double)particle.modeB.angle)) * particle.modeB.radius;
                        particle.pos.y = -((float)Math.Sin((double)particle.modeB.angle)) * particle.modeB.radius;
                    }
                    particle.color.r += particle.deltaColor.r * dt;
                    particle.color.g += particle.deltaColor.g * dt;
                    particle.color.b += particle.deltaColor.b * dt;
                    particle.color.a += particle.deltaColor.a * dt;
                    particle.size += particle.deltaSize * dt;
                    particle.size = (0f > particle.size) ? 0f : particle.size;
                    particle.rotation += particle.deltaRotation * dt;
                    if ((this.m_ePositionType == eParticlePositionType.kCCPositionTypeFree) || (this.m_ePositionType == eParticlePositionType.kCCPositionTypeRelative))
                    {
                        CCPoint point6 = CCPointExtension.ccpSub(tPosition, particle.startPos);
                        pos = CCPointExtension.ccpSub(particle.pos, point6);
                    }
                    else
                    {
                        pos = particle.pos;
                    }
                    this.updateQuadWithParticle(particle, pos);
                    this.m_uParticleIdx++;
                }
                else
                {
                    if (this.m_uParticleIdx != (this.m_uParticleCount - 1))
                    {
                        this.m_pParticles[this.m_uParticleIdx].copy(this.m_pParticles[(int)((IntPtr)(this.m_uParticleCount - 1))]);
                    }
                    this.m_uParticleCount--;
                    if ((this.m_uParticleCount == 0) && this.m_bIsAutoRemoveOnFinish)
                    {
                        base.unscheduleUpdate();
                        base.m_pParent.removeChild(this, true);
                        return;
                    }
                }
            }
            this.postStep();
        }

        public virtual void updateQuadWithParticle(CCParticle particle, CCPoint newPosition)
        {
        }

        private string valueForKey(string key, Dictionary<string, object> dict)
        {
            if (dict == null)
            {
                return "";
            }
            object obj2 = new object();
            if (!dict.TryGetValue(key, out obj2))
            {
                return "";
            }
            return (obj2 as string);
        }

        public float Angle
        {
            get
            {
                return this.m_fAngle;
            }
            set
            {
                this.m_fAngle = value;
            }
        }

        public float AngleVar
        {
            get
            {
                return this.m_fAngleVar;
            }
            set
            {
                this.m_fAngleVar = value;
            }
        }

        public ccBlendFunc BlendFunc
        {
            get
            {
                return this.m_tBlendFunc;
            }
            set
            {
                this.m_tBlendFunc = value;
            }
        }

        public float Duration
        {
            get
            {
                return this.m_fDuration;
            }
            set
            {
                this.m_fDuration = value;
            }
        }

        public float EmissionRate
        {
            get
            {
                return this.m_fEmissionRate;
            }
            set
            {
                this.m_fEmissionRate = value;
            }
        }

        public int EmitterMode
        {
            get
            {
                return this.m_nEmitterMode;
            }
            set
            {
                this.m_nEmitterMode = value;
            }
        }

        public ccColor4F EndColor
        {
            get
            {
                return this.m_tEndColor;
            }
            set
            {
                this.m_tEndColor = value;
            }
        }

        public ccColor4F EndColorVar
        {
            get
            {
                return this.m_tEndColorVar;
            }
            set
            {
                this.m_tEndColorVar = value;
            }
        }

        public float EndSize
        {
            get
            {
                return this.m_fEndSize;
            }
            set
            {
                this.m_fEndSize = value;
            }
        }

        public float EndSizeVar
        {
            get
            {
                return this.m_fEndSizeVar;
            }
            set
            {
                this.m_fEndSizeVar = value;
            }
        }

        public float EndSpin
        {
            get
            {
                return this.m_fEndSpin;
            }
            set
            {
                this.m_fEndSpin = value;
            }
        }

        public float EndSpinVar
        {
            get
            {
                return this.m_fEndSpinVar;
            }
            set
            {
                this.m_fEndSpinVar = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.m_bIsActive;
            }
        }

        public bool IsAutoRemoveOnFinish
        {
            get
            {
                return this.m_bIsAutoRemoveOnFinish;
            }
            set
            {
                this.m_bIsAutoRemoveOnFinish = value;
            }
        }

        public bool IsBlendAdditive
        {
            get
            {
                return this.m_bIsBlendAdditive;
            }
            set
            {
                this.m_bIsBlendAdditive = value;
            }
        }

        public float Life
        {
            get
            {
                return this.m_fLife;
            }
            set
            {
                this.m_fLife = value;
            }
        }

        public float LifeVar
        {
            get
            {
                return this.m_fLifeVar;
            }
            set
            {
                this.m_fLifeVar = value;
            }
        }

        public uint ParticleCount
        {
            get
            {
                return this.m_uParticleCount;
            }
        }

        public eParticlePositionType PositionType
        {
            get
            {
                return this.m_ePositionType;
            }
            set
            {
                this.m_ePositionType = value;
            }
        }

        public virtual CCPoint PosVar
        {
            get
            {
                return this.m_tPosVar;
            }
            set
            {
                this.m_tPosVar = value;
            }
        }

        public virtual CCPoint SourcePosition
        {
            get
            {
                return this.m_tSourcePosition;
            }
            set
            {
                this.m_tSourcePosition = value;
            }
        }

        public ccColor4F StartColor
        {
            get
            {
                return this.m_tStartColor;
            }
            set
            {
                this.m_tStartColor = value;
            }
        }

        public ccColor4F StartColorVar
        {
            get
            {
                return this.m_tStartColorVar;
            }
            set
            {
                this.m_tStartColorVar = value;
            }
        }

        public float StartSize
        {
            get
            {
                return this.m_fStartSize;
            }
            set
            {
                this.m_fStartSize = value;
            }
        }

        public float StartSizeVar
        {
            get
            {
                return this.m_fStartSizeVar;
            }
            set
            {
                this.m_fStartSizeVar = value;
            }
        }

        public float StartSpin
        {
            get
            {
                return this.m_fStartSpin;
            }
            set
            {
                this.m_fStartSpin = value;
            }
        }

        public float StartSpinVar
        {
            get
            {
                return this.m_fStartSpinVar;
            }
            set
            {
                this.m_fStartSpinVar = value;
            }
        }

        public virtual CCTexture2D Texture
        {
            get
            {
                return this.m_pTexture;
            }
            set
            {
                this.m_pTexture = value;
            }
        }

        public uint TotalParticles
        {
            get
            {
                return this.m_uTotalParticles;
            }
            set
            {
                this.m_uTotalParticles = value;
            }
        }

        public class sModeA
        {
            public CCPoint gravity;
            public float radialAccel;
            public float radialAccelVar;
            public float speed;
            public float speedVar;
            public float tangentialAccel;
            public float tangentialAccelVar;
        }

        public class sModeB
        {
            public float endRadius;
            public float endRadiusVar;
            public float rotatePerSecond;
            public float rotatePerSecondVar;
            public float startRadius;
            public float startRadiusVar;
        }
    }
}
