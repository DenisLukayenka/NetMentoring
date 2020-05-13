using Caching.NorthwindDAL;

namespace Caching.Core.CacheManagers.Shippers
{
	public class ShippersMemorySqlMonitorCache: BaseMemoryMonitorCache<Shipper>, IShipperCache
	{
		public ShippersMemorySqlMonitorCache(string connectionString) : base(connectionString)
		{
		}

		protected override string KeyPrefix => "Cache_Shippers";

		protected override string GetMonitorCommand()
		{
			return "SELECT [ShipperID],[CompanyName] FROM [dbo].[Shippers]";
		}
	}
}
