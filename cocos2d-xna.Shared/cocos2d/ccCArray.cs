using System;

namespace cocos2d
{
	public class ccCArray
	{
		public int num;

		public int max;

		public int[] arr;

		public ccCArray()
		{
		}

		public static void ccCArrayDoubleCapacity(ccCArray arr)
		{
			ccCArray _ccCArray = arr;
			_ccCArray.max = _ccCArray.max * 2;
			int[] numArray = new int[arr.max];
			Array.Copy(arr.arr, numArray, (int)arr.arr.Length);
			arr.arr = numArray;
		}

		public static void ccCArrayInsertValueAtIndex(ccCArray arr, int value, int index)
		{
			int num = arr.num - index;
			if (arr.num + 1 == arr.max)
			{
				ccCArray.ccCArrayDoubleCapacity(arr);
			}
			if (num > 0)
			{
				for (int i = (int)arr.arr.Length - 1; i > index; i--)
				{
					arr.arr[i] = arr.arr[i - 1];
				}
			}
			ccCArray _ccCArray = arr;
			_ccCArray.num = _ccCArray.num + 1;
			arr.arr[index] = value;
		}

		public static ccCArray ccCArrayNew(int capacity)
		{
			if (capacity == 0)
			{
				capacity = 1;
			}
			ccCArray _ccCArray = new ccCArray()
			{
				num = 0,
				arr = new int[capacity],
				max = capacity
			};
			return _ccCArray;
		}

		public static void ccCArrayRemoveValueAtIndex(ccCArray arr, int index)
		{
			ccCArray _ccCArray = arr;
			int num = _ccCArray.num - 1;
			int num1 = num;
			_ccCArray.num = num;
			int num2 = num1;
			while (index < num2)
			{
				arr.arr[index] = arr.arr[index + 1];
				index++;
			}
		}
	}
}