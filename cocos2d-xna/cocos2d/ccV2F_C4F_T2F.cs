using System;

namespace cocos2d
{
	public class ccV2F_C4F_T2F
	{
		public ccVertex2F vertices;

		public ccColor4F colors;

		public ccTex2F texCoords;

		public ccV2F_C4F_T2F()
		{
			this.vertices = new ccVertex2F();
			this.colors = new ccColor4F();
			this.texCoords = new ccTex2F();
		}
	}
}