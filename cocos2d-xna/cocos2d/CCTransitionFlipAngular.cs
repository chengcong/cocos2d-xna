using System;

namespace cocos2d
{
	public class CCTransitionFlipAngular : CCTransitionSceneOriented
	{
		public CCTransitionFlipAngular()
		{
		}

		public override void onEnter()
		{
			float single;
			float single1;
			float single2;
			float single3;
			base.onEnter();
			this.m_pInScene.visible = false;
			if (this.m_eOrientation != tOrientation.kOrientationRightOver)
			{
				single = -90f;
				single1 = 90f;
				single2 = -90f;
				single3 = 0f;
			}
			else
			{
				single = 90f;
				single1 = 270f;
				single2 = 90f;
				single3 = 0f;
			}
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { CCDelayTime.actionWithDuration(this.m_fDuration / 2f), CCShow.action(), CCOrbitCamera.actionWithDuration(this.m_fDuration / 2f, 1f, 0f, single1, single, -45f, 0f), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			CCActionInterval cCActionInterval = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray);
			CCFiniteTimeAction[] cCFiniteTimeActionArray1 = new CCFiniteTimeAction[] { CCOrbitCamera.actionWithDuration(this.m_fDuration / 2f, 1f, 0f, single3, single2, 45f, 0f), CCHide.action(), CCDelayTime.actionWithDuration(this.m_fDuration / 2f) };
			CCActionInterval cCActionInterval1 = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray1);
			this.m_pInScene.runAction(cCActionInterval);
			this.m_pOutScene.runAction(cCActionInterval1);
		}

		public static new CCTransitionFlipAngular transitionWithDuration(float t, CCScene s, tOrientation o)
		{
			CCTransitionFlipAngular cCTransitionFlipAngular = new CCTransitionFlipAngular();
			cCTransitionFlipAngular.initWithDuration(t, s, o);
			return cCTransitionFlipAngular;
		}
	}
}