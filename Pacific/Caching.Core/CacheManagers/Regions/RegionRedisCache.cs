using Caching.NorthwindDAL;

namespace Caching.Core.CacheManagers.Regions
{
	public class RegionRedisCache: BaseRedisCache<Region>, IRegionCache
	{
		public RegionRedisCache(string hostName) : base(hostName)
		{
		}

		protected override string KeyPrefix => "Cache_Regions";
	}
}
