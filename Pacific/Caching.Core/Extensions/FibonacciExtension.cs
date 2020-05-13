using System;

namespace Caching.Core.Extensions
{
	public static class FibonacciExtension
	{
		public static int FindFibonacci(this int depth)
		{
			if(depth < 0)
			{
				throw new ArgumentOutOfRangeException("Fibonacci number can't be less than 0!");
			}

			return FindFibonacciRec(depth);
		}

		private static int FindFibonacciRec(int number)
		{
			if(number == 0)
			{
				return 0;
			}
			if(number == 1)
			{
				return 1;
			}

			return FindFibonacciRec(number - 2) + FindFibonacciRec(number - 1);
		}
	}
}
