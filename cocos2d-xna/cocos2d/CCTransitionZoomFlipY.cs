using System;

namespace cocos2d
{
	public class CCTransitionZoomFlipY : CCTransitionSceneOriented
	{
		public CCTransitionZoomFlipY()
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
			if (this.m_eOrientation != tOrientation.kOrientationLeftOver)
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
			CCFiniteTimeAction[] cCFiniteTimeActionArray = new CCFiniteTimeAction[] { CCDelayTime.actionWithDuration(this.m_fDuration / 2f), null, null };
			CCFiniteTimeAction[] cCFiniteTimeActionArray1 = new CCFiniteTimeAction[] { CCOrbitCamera.actionWithDuration(this.m_fDuration / 2f, 1f, 0f, single1, single, 90f, 0f), CCScaleTo.actionWithDuration(this.m_fDuration / 2f, 1f), CCShow.action() };
			cCFiniteTimeActionArray[1] = CCSpawn.actions(cCFiniteTimeActionArray1);
			cCFiniteTimeActionArray[2] = CCCallFunc.actionWithTarget(this, new SEL_CallFunc(this.finish));
			CCActionInterval cCActionInterval = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray);
			CCFiniteTimeAction[] cCFiniteTimeActionArray2 = new CCFiniteTimeAction[3];
			CCFiniteTimeAction[] cCFiniteTimeActionArray3 = new CCFiniteTimeAction[] { CCOrbitCamera.actionWithDuration(this.m_fDuration / 2f, 1f, 0f, single3, single2, 90f, 0f), CCScaleTo.actionWithDuration(this.m_fDuration / 2f, 0.5f) };
			cCFiniteTimeActionArray2[0] = CCSpawn.actions(cCFiniteTimeActionArray3);
			cCFiniteTimeActionArray2[1] = CCHide.action();
			cCFiniteTimeActionArray2[2] = CCDelayTime.actionWithDuration(this.m_fDuration / 2f);
			CCActionInterval cCActionInterval1 = (CCActionInterval)CCSequence.actions(cCFiniteTimeActionArray2);
			this.m_pInScene.scale = 0.5f;
			this.m_pInScene.runAction(cCActionInterval);
			this.m_pOutScene.runAction(cCActionInterval1);
		}

		public static new CCTransitionZoomFlipY transitionWithDuration(float t, CCScene s, tOrientation o)
		{
			CCTransitionZoomFlipY cCTransitionZoomFlipY = new CCTransitionZoomFlipY();
			cCTransitionZoomFlipY.initWithDuration(t, s, o);
			return cCTransitionZoomFlipY;
		}
	}
}