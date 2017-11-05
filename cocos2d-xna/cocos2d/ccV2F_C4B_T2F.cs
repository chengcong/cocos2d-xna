using System;

namespace cocos2d
{
	public class ccV2F_C4B_T2F
	{
		public ccVertex2F vertices;

		public ccColor4B colors;

		public ccTex2F texCoords;

		public ccV2F_C4B_T2F()
		{
			this.vertices = new ccVertex2F();
			this.colors = new ccColor4B();
			this.texCoords = new ccTex2F();
		}
	}
}