using Caching.NorthwindDAL;

namespace Caching.Core.CacheManagers.Categories
{
	public class CategoriesMemorySqlMonitorCache : BaseMemoryMonitorCache<Category>, ICategoryCache
	{
		public CategoriesMemorySqlMonitorCache(string connectionString) : base(connectionString)
		{
		}

		protected override string KeyPrefix => "Cache_Categories";

		protected override string GetMonitorCommand()
		{
			return "SELECT [CategoryID],[CategoryName] FROM [dbo].[Categories]";
		}
	}
}
