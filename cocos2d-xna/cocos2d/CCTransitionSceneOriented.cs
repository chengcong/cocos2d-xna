using System;

namespace cocos2d
{
	public class CCTransitionSceneOriented : CCTransitionScene
	{
		protected tOrientation m_eOrientation;

		public CCTransitionSceneOriented()
		{
		}

		public virtual bool initWithDuration(float t, CCScene scene, tOrientation orientation)
		{
			if (base.initWithDuration(t, scene))
			{
				this.m_eOrientation = orientation;
			}
			return true;
		}

		public static CCTransitionSceneOriented transitionWithDuration(float t, CCScene scene, tOrientation orientation)
		{
			CCTransitionSceneOriented cCTransitionSceneOriented = new CCTransitionSceneOriented();
			cCTransitionSceneOriented.initWithDuration(t, scene, orientation);
			return cCTransitionSceneOriented;
		}
	}
}