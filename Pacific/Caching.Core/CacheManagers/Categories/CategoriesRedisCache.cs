using Caching.NorthwindDAL;

namespace Caching.Core.CacheManagers.Categories
{
	public class CategoriesRedisCache : BaseRedisCache<Category>, ICategoryCache
	{
		public CategoriesRedisCache(string hostName) : base(hostName)
		{
		}

		protected override string KeyPrefix => "Cache_Categories";
	}
}
