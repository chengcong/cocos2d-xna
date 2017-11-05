using System;

namespace cocos2d
{
	public class tListEntry
	{
		public SelectorProtocol target;

		public int priority;

		public bool paused;

		public bool markedForDeletion;

		public tListEntry()
		{
		}
	}
}