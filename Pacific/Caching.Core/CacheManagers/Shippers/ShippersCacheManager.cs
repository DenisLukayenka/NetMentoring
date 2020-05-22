using Caching.NorthwindDAL;

namespace Caching.Core.CacheManagers.Shippers
{
	public class ShippersCacheManager: BaseCacheManager<Shipper>
	{
		public ShippersCacheManager(IShipperCache cache) : base(cache)
		{
		}
	}
}
