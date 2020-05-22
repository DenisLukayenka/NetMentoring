using System;
using System.Runtime.Caching;

namespace Caching.Core.CacheManagers.Fibonacci
{
	public class FibonacciCacheManager
	{
		private static readonly ObjectCache cache;

		static FibonacciCacheManager()
		{
			cache = MemoryCache.Default;
		}

		public int FindFibonacciRec(int index)
		{
			if(index < 0)
			{
				throw new ArgumentOutOfRangeException("Fibonacci sequence can't have less than zero index.");
			}
			if(index == 0)
			{
				return 0;
			}
			if(index == 1)
			{
				return 1;
			}

			if (cache.Contains(index.ToString()))
			{
				return (int)cache.Get(index.ToString());
			}

			int result = this.FindFibonacciRec(index - 2) + this.FindFibonacciRec(index - 1);
			cache.Add(index.ToString(), result, new DateTimeOffset(DateTime.UtcNow.AddMinutes(1)));

			return result;
		}
	}
}
