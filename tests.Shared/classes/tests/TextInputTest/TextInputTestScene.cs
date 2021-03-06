using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;
using tests;

namespace cocos2d
{
    public class TextInputTestScene : TestScene
    {
        int kTextFieldTTFDefaultTest = 0;
        int kTextFieldTTFActionTest;
        int kTextInputTestsCount;

        public static string FONT_NAME = "Thonburi";
        public static int FONT_SIZE = 36;

        public static int testIdx = -1;

        public override void runThisTest()
        {
            CCLayer pLayer = nextTextInputTest();
            addChild(pLayer);

            CCDirector.sharedDirector().replaceScene(this);
        }

        public KeyboardNotificationLayer createTextInputTest(int nIndex)
        {
            switch (nIndex)
            {
                //case kTextFieldTTFDefaultTest:
                //    return new TextFieldTTFDefaultTest();
                //case kTextFieldTTFActionTest:
                //    return new TextFieldTTFActionTest();
                //default: return 0;

                case 0:
                    return new TextFieldTTFDefaultTest();
                case 1:
                    return new TextFieldTTFActionTest();
                default: break;
            }

            return null;
        }

        public CCLayer restartTextInputTest()
        {
            TextInputTest pContainerLayer = new TextInputTest();
            //pContainerLayer->autorelease();

            KeyboardNotificationLayer pTestLayer = createTextInputTest(testIdx);
            //pTestLayer->autorelease();
            pContainerLayer.addKeyboardNotificationLayer(pTestLayer);

            return pContainerLayer;
        }

        public CCLayer nextTextInputTest()
        {
            testIdx++;
            testIdx = testIdx % kTextInputTestsCount;

            return restartTextInputTest();
        }

        public CCLayer backTextInputTest()
        {
            testIdx--;
            int total = kTextInputTestsCount;
            if (testIdx < 0)
                testIdx += total;

            return restartTextInputTest();
        }

        public static CCRect getRect(CCNode pNode)
        {
            CCRect rc = new CCRect();
            rc.origin = pNode.position;
            rc.size = pNode.contentSize;
            rc.origin.x -= rc.size.width / 2;
            rc.origin.y -= rc.size.height / 2;
            return rc;
        }
    }
}
