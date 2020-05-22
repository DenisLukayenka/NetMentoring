using Caching.NorthwindDAL;

namespace Caching.Core.CacheManagers.Regions
{
	public class RegionsCacheManager: BaseCacheManager<Region>
	{
		public RegionsCacheManager(IRegionCache cache) : base(cache)
		{
		}
	}
}
