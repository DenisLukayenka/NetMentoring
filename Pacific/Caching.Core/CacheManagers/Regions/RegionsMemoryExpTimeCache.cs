using Caching.NorthwindDAL;
using System;
using System.Runtime.Caching;

namespace Caching.Core.CacheManagers.Regions
{
	public class RegionsMemoryExpTimeCache: BaseMemoryCache<Region>, IRegionCache
	{
		public RegionsMemoryExpTimeCache() : base()
		{
		}

		protected override string KeyPrefix => "Cache_Regions";

		protected override CacheItemPolicy GetExpireItemPolicy()
		{
			return new CacheItemPolicy()
			{
				AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddSeconds(10))
			};
		}
	}
}
