using Caching.NorthwindDAL;

namespace Caching.Core.CacheManagers.Shippers
{
	public class ShipperRedisCache: BaseRedisCache<Shipper>, IShipperCache
	{
		public ShipperRedisCache(string hostName) : base(hostName)
		{
		}

		protected override string KeyPrefix => "Cache_Shippers";
	}
}
