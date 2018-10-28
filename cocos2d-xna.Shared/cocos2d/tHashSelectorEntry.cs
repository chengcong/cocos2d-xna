using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class tHashSelectorEntry
	{
		public List<CCTimer> timers;

		public SelectorProtocol target;

		public uint timerIndex;

		public CCTimer currentTimer;

		public bool currentTimerSalvaged;

		public bool paused;

		public tHashSelectorEntry()
		{
		}
	}
}