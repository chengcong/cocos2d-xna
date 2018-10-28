using System;

namespace cocos2d
{
	public class CCScene : CCNode
	{
		protected ccSceneFlag m_eSceneType;

		public ccSceneFlag SceneType
		{
			get
			{
				return this.m_eSceneType;
			}
		}

		public CCScene()
		{
			this.isRelativeAnchorPoint = false;
			this.anchorPoint = CCPointExtension.ccp(0.5f, 0.5f);
			this.m_eSceneType = ccSceneFlag.ccNormalScene;
		}

		public bool init()
		{
			bool flag = false;
			CCDirector cCDirector = CCDirector.sharedDirector();
			if (cCDirector != null)
			{
				this.contentSize = cCDirector.getWinSize();
				flag = true;
			}
			return flag;
		}

		public static new CCScene node()
		{
			CCScene cCScene = new CCScene();
			if (cCScene.init())
			{
				return cCScene;
			}
			return null;
		}
	}
}