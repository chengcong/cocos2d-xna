using System;
using System.IO;
using System.Text;

namespace WP7Contrib.Communications.Compression
{
	internal class SharedUtils
	{
		public SharedUtils()
		{
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

		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}

		public static int URShift(int number, int bits)
		{
			return number >> (bits & 31);
		}

		public static long URShift(long number, int bits)
		{
			return number >> (bits & 63);
		}
	}
}