using Caching.NorthwindDAL;
using System;
using System.Runtime.Caching;

namespace Caching.Core.CacheManagers.Shippers
{
	public class ShippersMemoryExpTimeCache: BaseMemoryCache<Shipper>, IShipperCache
	{
		public ShippersMemoryExpTimeCache() : base()
		{
		}

		protected override string KeyPrefix => "Cache_Shippers";

		protected override CacheItemPolicy GetExpireItemPolicy()
		{
			return new CacheItemPolicy()
			{
				AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddSeconds(10))
			};
		}
	}
}
