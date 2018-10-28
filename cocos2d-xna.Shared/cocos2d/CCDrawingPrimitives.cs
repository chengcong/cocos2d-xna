using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace cocos2d
{
	public class CCDrawingPrimitives
	{
		public CCDrawingPrimitives()
		{
		}

		public static void ccDrawCircle(CCPoint center, float radius, float angle, int segments, bool drawLineToCenter, ccColor4B color)
		{
			int num = 1;
			if (drawLineToCenter)
			{
				num++;
			}
			CCApplication cCApplication = CCApplication.sharedApplication();
			float contentScaleFactor = CCDirector.sharedDirector().ContentScaleFactor;
			float single = 6.28318548f / (float)segments;
			VertexPositionColor[] vector3 = new VertexPositionColor[2 * (segments + 2)];
			for (int i = 0; i <= segments; i++)
			{
				float single1 = (float)i * single;
				float single2 = radius * (float)Math.Cos((double)(single1 + angle)) + center.x;
				float single3 = radius * (float)Math.Sin((double)(single1 + angle)) + center.y;
				vector3[i] = new VertexPositionColor();
				vector3[i].Position = new Vector3(single2 * contentScaleFactor, single3 * contentScaleFactor, 0f);
				vector3[i].Color = new Color((int)color.r, (int)color.g, (int)color.b, (int)color.a);
			}
			cCApplication.basicEffect.TextureEnabled = false;
			cCApplication.basicEffect.VertexColorEnabled = true;
			foreach (EffectPass pass in cCApplication.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				cCApplication.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vector3, 0, segments);
			}
		}

		public static void ccDrawCubicBezier(CCPoint origin, CCPoint control1, CCPoint control2, CCPoint destination, int segments, ccColor4F color)
		{
			VertexPositionColor[] vector3 = new VertexPositionColor[segments + 1];
			float contentScaleFactor = CCDirector.sharedDirector().ContentScaleFactor;
			CCApplication cCApplication = CCApplication.sharedApplication();
			float single = 0f;
			for (int i = 0; i < segments; i++)
			{
				float single1 = (float)Math.Pow((double)(1f - single), 3) * origin.x + 3f * (float)Math.Pow((double)(1f - single), 2) * single * control1.x + 3f * (1f - single) * single * single * control2.x + single * single * single * destination.x;
				float single2 = (float)Math.Pow((double)(1f - single), 3) * origin.y + 3f * (float)Math.Pow((double)(1f - single), 2) * single * control1.y + 3f * (1f - single) * single * single * control2.y + single * single * single * destination.y;
				vector3[i] = new VertexPositionColor();
				vector3[i].Position = new Vector3(single1 * contentScaleFactor, single2 * contentScaleFactor, 0f);
				vector3[i].Color = new Color(color.r, color.g, color.b, color.a);
				single = single + 1f / (float)segments;
			}
			VertexPositionColor vertexPositionColor = new VertexPositionColor()
			{
				Color = new Color(color.r, color.g, color.b, color.a),
				Position = new Vector3(destination.x * contentScaleFactor, destination.y * contentScaleFactor, 0f)
			};
			vector3[segments] = vertexPositionColor;
			cCApplication.basicEffect.TextureEnabled = false;
			cCApplication.basicEffect.VertexColorEnabled = true;
			foreach (EffectPass pass in cCApplication.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				cCApplication.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vector3, 0, segments);
			}
		}

		public static void ccDrawLine(CCPoint origin, CCPoint destination, ccColor4F color)
		{
			float contentScaleFactor = CCDirector.sharedDirector().ContentScaleFactor;
			VertexPositionColor[] vertexPositionColor = new VertexPositionColor[] { new VertexPositionColor(new Vector3(origin.x * contentScaleFactor, origin.y * contentScaleFactor, 0f), new Color(color.r, color.g, color.b, color.a)), new VertexPositionColor(new Vector3(destination.x * contentScaleFactor, destination.y * contentScaleFactor, 0f), new Color(color.r, color.g, color.b, color.a)) };
			CCApplication cCApplication = CCApplication.sharedApplication();
			cCApplication.basicEffect.TextureEnabled = false;
			cCApplication.basicEffect.VertexColorEnabled = true;
			foreach (EffectPass pass in cCApplication.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				cCApplication.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertexPositionColor, 0, 1);
			}
		}

		public static void ccDrawPoint(CCPoint point)
		{
		}

		public static void ccDrawPoints(CCPoint points, int numberOfPoints)
		{
			throw new NotImplementedException();
		}

		public static void ccDrawPoly(CCPoint[] vertices, int numOfVertices, bool closePolygon, ccColor4F color)
		{
			CCDrawingPrimitives.ccDrawPoly(vertices, numOfVertices, closePolygon, false, color);
		}

		public static void ccDrawPoly(CCPoint[] vertices, int numOfVertices, bool closePolygon, bool fill, ccColor4F color)
		{
			VertexPositionColor[] vector3 = new VertexPositionColor[numOfVertices + 1];
			for (int i = 0; i < numOfVertices; i++)
			{
				vector3[i] = new VertexPositionColor();
				vector3[i].Position = new Vector3(vertices[i].x, vertices[i].y, 0f);
				vector3[i].Color = new Color(color.r, color.g, color.b, color.a);
			}
			CCApplication rasterizerState = CCApplication.sharedApplication();
			rasterizerState.GraphicsDevice.RasterizerState = new RasterizerState()
			{
				CullMode = CullMode.None
			};
			rasterizerState.basicEffect.TextureEnabled = false;
			rasterizerState.basicEffect.VertexColorEnabled = true;
			short[] numArray = new short[(numOfVertices - 2) * 3];
			if (!fill)
			{
				if (closePolygon)
				{
					vector3[numOfVertices] = vector3[0];
					numOfVertices++;
				}
				foreach (EffectPass pass in rasterizerState.basicEffect.CurrentTechnique.Passes)
				{
					pass.Apply();
					rasterizerState.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vector3, 0, numOfVertices - 1);
				}
			}
			else
			{
				for (int j = 0; j < numOfVertices - 2; j++)
				{
					numArray[j * 3] = 0;
					numArray[j * 3 + 1] = (short)(j + 2);
					numArray[j * 3 + 2] = (short)(j + 1);
				}
				foreach (EffectPass effectPass in rasterizerState.basicEffect.CurrentTechnique.Passes)
				{
					effectPass.Apply();
					rasterizerState.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vector3, 0, numOfVertices, numArray, 0, numOfVertices - 2);
				}
			}
		}

		public static void ccDrawQuadBezier(CCPoint origin, CCPoint control, CCPoint destination, int segments, ccColor4F color)
		{
			VertexPositionColor[] vector3 = new VertexPositionColor[segments + 1];
			float contentScaleFactor = CCDirector.sharedDirector().ContentScaleFactor;
			CCApplication cCApplication = CCApplication.sharedApplication();
			float single = 0f;
			for (int i = 0; i < segments; i++)
			{
				float single1 = (float)Math.Pow((double)(1f - single), 2) * origin.x + 2f * (1f - single) * single * control.x + single * single * destination.x;
				float single2 = (float)Math.Pow((double)(1f - single), 2) * origin.y + 2f * (1f - single) * single * control.y + single * single * destination.y;
				vector3[i] = new VertexPositionColor();
				vector3[i].Position = new Vector3(single1 * contentScaleFactor, single2 * contentScaleFactor, 0f);
				vector3[i].Color = new Color(color.r, color.g, color.b, color.a);
				single = single + 1f / (float)segments;
			}
			VertexPositionColor vertexPositionColor = new VertexPositionColor()
			{
				Position = new Vector3(destination.x * contentScaleFactor, destination.y * contentScaleFactor, 0f),
				Color = new Color(color.r, color.g, color.b, color.a)
			};
			vector3[segments] = vertexPositionColor;
			cCApplication.basicEffect.TextureEnabled = false;
			cCApplication.basicEffect.VertexColorEnabled = true;
			foreach (EffectPass pass in cCApplication.basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				cCApplication.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vector3, 0, segments);
			}
		}
	}
}