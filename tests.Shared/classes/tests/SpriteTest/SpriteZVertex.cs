using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class SpriteZVertex : SpriteTestDemo
    {
        int m_dir;
        float m_time;
        public override void onEnter()
        {
            base.onEnter();

            // TIP: don't forget to enable Alpha test
            //glEnable(GL_ALPHA_TEST);
            //glAlphaFunc(GL_GREATER, 0.0f);
            CCDirector.sharedDirector().Projection = (ccDirectorProjection.kCCDirectorProjection3D);
        }
        public override void onExit()
        {
            //glDisable(GL_ALPHA_TEST);
            CCDirector.sharedDirector().Projection = (ccDirectorProjection.kCCDirectorProjection2D);
            base.onExit();
        }
        public SpriteZVertex()
        {
            //
            // This test tests z-order
            // If you are going to use it is better to use a 3D projection
            //
            // WARNING:
            // The developer is resposible for ordering it's sprites according to it's Z if the sprite has
            // transparent parts.
            //

            m_dir = 1;
            m_time = 0;

            CCSize s = CCDirector.sharedDirector().getWinSize();
            float step = s.width / 12;

            CCNode node = CCNode.node();
            // camera uses the center of the image as the pivoting point
            node.contentSize = (new CCSize(s.width, s.height));
            node.anchorPoint = (new CCPoint(0.5f, 0.5f));
            node.position = (new CCPoint(s.width / 2, s.height / 2));

            addChild(node, 0);

            for (int i = 0; i < 5; i++)
            {
                CCSprite sprite = CCSprite.spriteWithFile("Images/grossini_dance_atlas", new CCRect(85 * 0, 121 * 1, 85, 121));
                sprite.position = (new CCPoint((i + 1) * step, s.height / 2));
                sprite.vertexZ = (10 + i * 40);
                node.addChild(sprite, 0);
            }

            for (int i = 5; i < 11; i++)
            {
                CCSprite sprite = CCSprite.spriteWithFile("Images/grossini_dance_atlas", new CCRect(85 * 1, 121 * 0, 85, 121));
                sprite.position = (new CCPoint((i + 1) * step, s.height / 2));
                sprite.vertexZ = 10 + (10 - i) * 40;
                node.addChild(sprite, 0);
            }

            node.runAction(CCOrbitCamera.actionWithDuration(10, 1, 0, 0, 360, 0, 0));
        }

        public override string title()
        {
            return "Sprite: openGL Z vertex";
        }
    }
}
