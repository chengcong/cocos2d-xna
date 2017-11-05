using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class tHashElement
	{
		public List<CCAction> actions;

		public CCObject target;

		public int actionIndex;

		public CCAction currentAction;

		public bool currentActionSalvaged;

		public bool paused;

		public tHashElement()
		{
		}
	}
}