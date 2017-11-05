using System;
using System.IO;
using System.Text;

namespace ComponentAce.Compression.Libs.zlib
{
	public class SupportClass
	{
		public SupportClass()
		{
		}

		public static long Identity(long literal)
		{
			return literal;
		}

		public static ulong Identity(ulong literal)
		{
			return literal;
		}

		public static float Identity(float literal)
		{
			return literal;
		}

		public static double Identity(double literal)
		{
			return literal;
		}

		public static int ReadInput(Stream sourceStream, byte[] target, int start, int count)
		{
			if ((int)target.Length == 0)
			{
				return 0;
			}
			byte[] numArray = new byte[(int)target.Length];
			int num = sourceStream.Read(numArray, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = numArray[i];
			}
			return num;
		}

		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if ((int)target.Length == 0)
			{
				return 0;
			}
			char[] chrArray = new char[(int)target.Length];
			int num = sourceTextReader.Read(chrArray, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)chrArray[i];
			}
			return num;
		}

		public static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		public static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}

		public static int URShift(int number, int bits)
		{
			if (number >= 0)
			{
				return number >> (bits & 31);
			}
			return (number >> (bits & 31)) + (2 << (~bits & 31));
		}

		public static int URShift(int number, long bits)
		{
			return SupportClass.URShift(number, (int)bits);
		}

		public static long URShift(long number, int bits)
		{
			if (number >= (long)0)
			{
				return number >> (bits & 63);
			}
			return (number >> (bits & 63)) + ((long)2 << (~bits & 63));
		}

		public static long URShift(long number, long bits)
		{
			return SupportClass.URShift(number, (int)bits);
		}
	}
}