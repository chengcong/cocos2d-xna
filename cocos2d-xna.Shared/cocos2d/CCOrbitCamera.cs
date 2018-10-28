using System;

namespace cocos2d
{
	public class CCOrbitCamera : CCActionCamera
	{
		protected float m_fRadius;

		protected float m_fDeltaRadius;

		protected float m_fAngleZ;

		protected float m_fDeltaAngleZ;

		protected float m_fAngleX;

		protected float m_fDeltaAngleX;

		protected float m_fRadZ;

		protected float m_fRadDeltaZ;

		protected float m_fRadX;

		protected float m_fRadDeltaX;

		public CCOrbitCamera()
		{
			this.m_fRadius = 0f;
			this.m_fDeltaRadius = 0f;
			this.m_fAngleZ = 0f;
			this.m_fDeltaAngleZ = 0f;
			this.m_fAngleX = 0f;
			this.m_fDeltaAngleX = 0f;
			this.m_fRadZ = 0f;
			this.m_fRadDeltaZ = 0f;
			this.m_fRadX = 0f;
			this.m_fRadDeltaX = 0f;
		}

		public static CCOrbitCamera actionWithDuration(float t, float radius, float deltaRadius, float angleZ, float deltaAngleZ, float angleX, float deltaAngleX)
		{
			CCOrbitCamera cCOrbitCamera = new CCOrbitCamera();
			if (cCOrbitCamera.initWithDuration(t, radius, deltaRadius, angleZ, deltaAngleZ, angleX, deltaAngleX))
			{
				return cCOrbitCamera;
			}
			return null;
		}

		public override CCObject copyWithZone(CCZone pZone)
		{
			CCOrbitCamera cCOrbitCamera = null;
			if (pZone == null || pZone.m_pCopyObject == null)
			{
				cCOrbitCamera = new CCOrbitCamera();
				pZone = new CCZone(cCOrbitCamera);
			}
			else
			{
				cCOrbitCamera = (CCOrbitCamera)pZone.m_pCopyObject;
			}
			this.copyWithZone(pZone);
			cCOrbitCamera.initWithDuration(this.m_fDuration, this.m_fRadius, this.m_fDeltaRadius, this.m_fAngleZ, this.m_fDeltaAngleZ, this.m_fAngleX, this.m_fDeltaAngleX);
			return cCOrbitCamera;
		}

		public bool initWithDuration(float t, float radius, float deltaRadius, float angleZ, float deltaAngleZ, float angleX, float deltaAngleX)
		{
			if (!base.initWithDuration(t))
			{
				return false;
			}
			this.m_fRadius = radius;
			this.m_fDeltaRadius = deltaRadius;
			this.m_fAngleZ = angleZ;
			this.m_fDeltaAngleZ = deltaAngleZ;
			this.m_fAngleX = angleX;
			this.m_fDeltaAngleX = deltaAngleX;
			this.m_fRadDeltaZ = ccMacros.CC_DEGREES_TO_RADIANS(deltaAngleZ);
			this.m_fRadDeltaX = ccMacros.CC_DEGREES_TO_RADIANS(deltaAngleX);
			return true;
		}

		public void sphericalRadius(out float newRadius, out float zenith, out float azimuth)
		{
			float single;
			float single1;
			float single2;
			float single3;
			float single4;
			float single5;
			CCCamera camera = this.m_pTarget.Camera;
			camera.getEyeXYZ(out single, out single1, out single2);
			camera.getCenterXYZ(out single3, out single4, out single5);
			float single6 = single - single3;
			float single7 = single1 - single4;
			float single8 = single2 - single5;
			float fLTEPSILON = (float)Math.Sqrt((double)((float)Math.Pow((double)single6, 2) + (float)Math.Pow((double)single7, 2) + (float)Math.Pow((double)single8, 2)));
			float fLTEPSILON1 = (float)Math.Sqrt((double)((float)Math.Pow((double)single6, 2) + (float)Math.Pow((double)single7, 2)));
			if (fLTEPSILON1 == 0f)
			{
				fLTEPSILON1 = ccMacros.FLT_EPSILON;
			}
			if (fLTEPSILON == 0f)
			{
				fLTEPSILON = ccMacros.FLT_EPSILON;
			}
			zenith = (float)Math.Acos((double)(single8 / fLTEPSILON));
			if (single6 >= 0f)
			{
				azimuth = (float)Math.Sin((double)(single7 / fLTEPSILON1));
			}
			else
			{
				azimuth = 3.14159274f - (float)Math.Sin((double)(single7 / fLTEPSILON1));
			}
			newRadius = fLTEPSILON / CCCamera.getZEye();
		}

		public override void startWithTarget(CCNode pTarget)
		{
			float single;
			float single1;
			float single2;
			base.startWithTargetUsedByCCOrbitCamera(pTarget);
			this.sphericalRadius(out single, out single1, out single2);
			if (float.IsNaN(this.m_fRadius))
			{
				this.m_fRadius = single;
			}
			if (float.IsNaN(this.m_fAngleZ))
			{
				this.m_fAngleZ = ccMacros.CC_RADIANS_TO_DEGREES(single1);
			}
			if (float.IsNaN(this.m_fAngleX))
			{
				this.m_fAngleX = ccMacros.CC_RADIANS_TO_DEGREES(single2);
			}
			this.m_fRadZ = ccMacros.CC_DEGREES_TO_RADIANS(this.m_fAngleZ);
			this.m_fRadX = ccMacros.CC_DEGREES_TO_RADIANS(this.m_fAngleX);
		}

		public override void update(float dt)
		{
			float mFRadius = (this.m_fRadius + this.m_fDeltaRadius * dt) * CCCamera.getZEye();
			float mFRadZ = this.m_fRadZ + this.m_fRadDeltaZ * dt;
			float mFRadX = this.m_fRadX + this.m_fRadDeltaX * dt;
			float single = (float)Math.Sin((double)mFRadZ) * (float)Math.Cos((double)mFRadX) * mFRadius + this.m_fCenterXOrig;
			float single1 = (float)Math.Sin((double)mFRadZ) * (float)Math.Sin((double)mFRadX) * mFRadius + this.m_fCenterYOrig;
			float single2 = (float)Math.Cos((double)mFRadZ) * mFRadius + this.m_fCenterZOrig;
			this.m_pTarget.Camera.setEyeXYZ(single, single1, single2);
		}
	}
}