using System;

namespace cocos2d
{
	public class CCActionCamera : CCActionInterval
	{
		protected float m_fCenterXOrig;

		protected float m_fCenterYOrig;

		protected float m_fCenterZOrig;

		protected float m_fEyeXOrig;

		protected float m_fEyeYOrig;

		protected float m_fEyeZOrig;

		protected float m_fUpXOrig;

		protected float m_fUpYOrig;

		protected float m_fUpZOrig;

		public CCActionCamera()
		{
			this.m_fCenterXOrig = 0f;
			this.m_fCenterYOrig = 0f;
			this.m_fCenterZOrig = 0f;
			this.m_fEyeXOrig = 0f;
			this.m_fEyeYOrig = 0f;
			this.m_fEyeZOrig = 0f;
			this.m_fUpXOrig = 0f;
			this.m_fUpYOrig = 0f;
			this.m_fUpZOrig = 0f;
		}

		public override CCFiniteTimeAction reverse()
		{
			return CCReverseTime.actionWithAction(this);
		}

		public override void startWithTarget(CCNode pTarget)
		{
			base.startWithTarget(pTarget);
			CCCamera camera = pTarget.Camera;
			camera.getCenterXYZ(out this.m_fCenterXOrig, out this.m_fCenterYOrig, out this.m_fCenterZOrig);
			camera.getEyeXYZ(out this.m_fEyeXOrig, out this.m_fEyeYOrig, out this.m_fEyeZOrig);
			camera.getUpXYZ(out this.m_fUpXOrig, out this.m_fUpYOrig, out this.m_fUpZOrig);
		}
	}
}