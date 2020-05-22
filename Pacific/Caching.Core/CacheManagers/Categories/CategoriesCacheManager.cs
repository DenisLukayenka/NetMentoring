using Caching.NorthwindDAL;

namespace Caching.Core.CacheManagers.Categories
{
	public class CategoriesCacheManager : BaseCacheManager<Category>
	{
		public CategoriesCacheManager(ICategoryCache cache) : base(cache)
		{
		}
	}
}
