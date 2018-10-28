using System;

namespace cocos2d
{
	public class ccV3F_C4B_T2F
	{
		public ccVertex3F vertices;

		public ccColor4B colors;

		public ccTex2F texCoords;

		public ccV3F_C4B_T2F()
		{
			this.vertices = new ccVertex3F();
			this.colors = new ccColor4B();
			this.texCoords = new ccTex2F();
		}
	}
}