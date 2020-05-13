using Caching.NorthwindDAL;

namespace Caching.Core.CacheManagers.Regions
{
	public class RegionsMemorySqlMonitorCache: BaseMemoryMonitorCache<Region>, IRegionCache
	{
		public RegionsMemorySqlMonitorCache(string connectionString) : base(connectionString)
		{
		}

		protected override string KeyPrefix => "Cache_Regions";

		protected override string GetMonitorCommand()
		{
			return "SELECT [RegionID],[RegionDescription] FROM [dbo].[Region]";
		}
	}
}
