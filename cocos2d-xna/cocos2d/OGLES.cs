using Microsoft.Xna.Framework.Graphics;
using System;

namespace cocos2d
{
	public class OGLES
	{
		public const int GL_NEVER = 512;

		public const int GL_ZERO = 0;

		public const int GL_ONE = 1;

		public const int GL_SRC_COLOR = 768;

		public const int GL_ONE_MINUS_SRC_COLOR = 769;

		public const int GL_SRC_ALPHA = 770;

		public const int GL_ONE_MINUS_SRC_ALPHA = 771;

		public const int GL_DST_ALPHA = 772;

		public const int GL_ONE_MINUS_DST_ALPHA = 773;

		public const int GL_DST_COLOR = 774;

		public const int GL_ONE_MINUS_DST_COLOR = 775;

		public const int GL_SRC_ALPHA_SATURATE = 776;

		public readonly static int GL_LESS;

		public readonly static int GL_EQUAL;

		public readonly static int GL_LEQUAL;

		public readonly static int GL_GREATER;

		public readonly static int GL_NOTEQUAL;

		public readonly static int GL_GEQUAL;

		public readonly static int GL_ALWAYS;

		static OGLES()
		{
			OGLES.GL_LESS = 513;
			OGLES.GL_EQUAL = 514;
			OGLES.GL_LEQUAL = 515;
			OGLES.GL_GREATER = 516;
			OGLES.GL_NOTEQUAL = 517;
			OGLES.GL_GEQUAL = 518;
			OGLES.GL_ALWAYS = 519;
		}

		public OGLES()
		{
		}

		public static Blend GetXNABlend(int glBlend)
		{
			int num = glBlend;
			switch (num)
			{
				case 0:
				{
					return Blend.Zero;
				}
				case 1:
				{
					return Blend.One;
				}
				default:
				{
					switch (num)
					{
						case 768:
						{
							return Blend.SourceColor;
						}
						case 769:
						{
							return Blend.InverseSourceColor;
						}
						case 770:
						{
							return Blend.SourceAlpha;
						}
						case 771:
						{
							return Blend.InverseSourceAlpha;
						}
						case 772:
						{
							return Blend.DestinationAlpha;
						}
						case 773:
						{
							return Blend.InverseDestinationAlpha;
						}
						case 774:
						{
							return Blend.DestinationColor;
						}
						case 775:
						{
							return Blend.InverseDestinationColor;
						}
						case 776:
						{
							return Blend.SourceAlphaSaturation;
						}
					}
					break;
				}
			}
			return Blend.One;
		}
	}
}