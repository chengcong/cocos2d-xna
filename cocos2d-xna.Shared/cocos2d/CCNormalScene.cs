namespace cocos2d
{
    using System;

    public class CCNormalScene : CCScene
    {
        ~CCNormalScene()
        {
        }

        public static CCNormalScene node()
        {
            CCNormalScene scene = new CCNormalScene();
            if (scene.init())
            {
                return scene;
            }
            return null;
        }
    }
}
