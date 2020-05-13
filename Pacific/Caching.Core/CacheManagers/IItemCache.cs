using System.Collections.Generic;

namespace Caching.Core.CacheManagers
{
	public interface IItemCache<T>
	{
		IEnumerable<T> Get(string key);

		void Set(string key, IEnumerable<T> items);

		bool Contains(string key);
	}
}
