using System;

namespace cocos2d
{
	public class CCTransitionFlipX : CCTransitionSceneOriented
	{
		public CCTransitionFlipX()
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
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { CCDelayTime.actionWithDuration(this.m_fDuration / 2f), CCShow.action(), CCOrbitCamera.actionWithDuration(this.m_fDuration / 2f, 1f, 0f, single1, single, 0f, 0f), CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish)) };
			CCActionInterval cCActionInterval = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray);
			CCFiniteTimeAction[] cCFiniteTimeActionArray1 = new CCFiniteTimeAction[] { CCOrbitCamera.actionWithDuration(this.m_fDuration / 2f, 1f, 0f, single3, single2, 0f, 0f), CCHide.action(), CCDelayTime.actionWithDuration(this.m_fDuration / 2f) };
			CCActionInterval cCActionInterval1 = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray1);
			this.m_pInScene.runAction(cCActionInterval);
			this.m_pOutScene.runAction(cCActionInterval1);
		}

		public static new CCTransitionFlipX transitionWithDuration(float t, CCScene s, tOrientation o)
		{
			CCTransitionFlipX cCTransitionFlipX = new CCTransitionFlipX();
			cCTransitionFlipX.initWithDuration(t, s, o);
			return cCTransitionFlipX;
		}
	}
}