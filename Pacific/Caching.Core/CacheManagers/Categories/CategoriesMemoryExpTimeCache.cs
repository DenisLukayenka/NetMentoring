using Caching.NorthwindDAL;
using System.Runtime.Caching;
using System;

namespace Caching.Core.CacheManagers.Categories
{
	public class CategoriesMemoryExpTimeCache : BaseMemoryCache<Category>, ICategoryCache
	{
		public CategoriesMemoryExpTimeCache() : base()
		{
		}

		protected override string KeyPrefix => "Cache_Categories";

		protected override CacheItemPolicy GetExpireItemPolicy()
		{
			return new CacheItemPolicy()
			{
				AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddSeconds(10))
			};
		}
	}
}
