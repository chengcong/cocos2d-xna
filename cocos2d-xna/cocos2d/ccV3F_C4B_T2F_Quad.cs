using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cocos2d
{
	public class ccV3F_C4B_T2F_Quad
	{
		public ccV3F_C4B_T2F tl;

		public ccV3F_C4B_T2F bl;

		public ccV3F_C4B_T2F tr;

		public ccV3F_C4B_T2F br;

		public ccV3F_C4B_T2F_Quad()
		{
			this.tl = new ccV3F_C4B_T2F();
			this.bl = new ccV3F_C4B_T2F();
			this.tr = new ccV3F_C4B_T2F();
			this.br = new ccV3F_C4B_T2F();
		}

		public short[] getIndexes(ccDirectorProjection projection)
		{
			short[] numArray = new short[] { 0, 1, 2, 2, 1, 3 };
			return numArray;
		}

		public VertexPositionColorTexture[] getVertices(ccDirectorProjection projection)
		{
			VertexPositionColorTexture[] vertexPositionColorTexture = new VertexPositionColorTexture[4];
			if (projection != ccDirectorProjection.kCCDirectorProjection2D)
			{
				vertexPositionColorTexture[0] = new VertexPositionColorTexture(new Vector3(this.tl.vertices.x, this.tl.vertices.y, this.tl.vertices.z), new Color((int)this.tl.colors.r, (int)this.tl.colors.g, (int)this.tl.colors.b, (int)this.tl.colors.a), new Vector2(this.tl.texCoords.u, this.tl.texCoords.v));
				vertexPositionColorTexture[1] = new VertexPositionColorTexture(new Vector3(this.tr.vertices.x, this.tr.vertices.y, this.tr.vertices.z), new Color((int)this.tr.colors.r, (int)this.tr.colors.g, (int)this.tr.colors.b, (int)this.tr.colors.a), new Vector2(this.tr.texCoords.u, this.tr.texCoords.v));
				vertexPositionColorTexture[2] = new VertexPositionColorTexture(new Vector3(this.bl.vertices.x, this.bl.vertices.y, this.bl.vertices.z), new Color((int)this.bl.colors.r, (int)this.bl.colors.g, (int)this.bl.colors.b, (int)this.bl.colors.a), new Vector2(this.bl.texCoords.u, this.bl.texCoords.v));
				vertexPositionColorTexture[3] = new VertexPositionColorTexture(new Vector3(this.br.vertices.x, this.br.vertices.y, this.br.vertices.z), new Color((int)this.br.colors.r, (int)this.br.colors.g, (int)this.br.colors.b, (int)this.br.colors.a), new Vector2(this.br.texCoords.u, this.br.texCoords.v));
			}
			else
			{
				vertexPositionColorTexture[0] = new VertexPositionColorTexture(new Vector3(this.bl.vertices.x, this.bl.vertices.y, this.bl.vertices.z), new Color((int)this.tl.colors.r, (int)this.tl.colors.g, (int)this.tl.colors.b, (int)this.tl.colors.a), new Vector2(this.bl.texCoords.u, this.bl.texCoords.v));
				vertexPositionColorTexture[1] = new VertexPositionColorTexture(new Vector3(this.br.vertices.x, this.br.vertices.y, this.br.vertices.z), new Color((int)this.tr.colors.r, (int)this.tr.colors.g, (int)this.tr.colors.b, (int)this.tr.colors.a), new Vector2(this.br.texCoords.u, this.br.texCoords.v));
				vertexPositionColorTexture[2] = new VertexPositionColorTexture(new Vector3(this.tl.vertices.x, this.tl.vertices.y, this.tl.vertices.z), new Color((int)this.bl.colors.r, (int)this.bl.colors.g, (int)this.bl.colors.b, (int)this.bl.colors.a), new Vector2(this.tl.texCoords.u, this.tl.texCoords.v));
				vertexPositionColorTexture[3] = new VertexPositionColorTexture(new Vector3(this.tr.vertices.x, this.tr.vertices.y, this.tr.vertices.z), new Color((int)this.br.colors.r, (int)this.br.colors.g, (int)this.br.colors.b, (int)this.br.colors.a), new Vector2(this.tr.texCoords.u, this.tr.texCoords.v));
			}
			return vertexPositionColorTexture;
		}
	}
}