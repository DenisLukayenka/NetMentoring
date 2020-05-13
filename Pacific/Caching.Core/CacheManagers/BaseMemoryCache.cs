using System.Collections.Generic;
using System.Runtime.Caching;

namespace Caching.Core.CacheManagers
{
	public abstract class BaseMemoryCache<T> : IItemCache<T>
	{
		private readonly ObjectCache _cache;

		public BaseMemoryCache()
		{
			this._cache = MemoryCache.Default;
		}

		protected abstract string KeyPrefix { get; }

		protected abstract CacheItemPolicy GetExpireItemPolicy();

		public virtual IEnumerable<T> Get(string key)
		{
			return (IEnumerable<T>)this._cache.Get(this.KeyPrefix + key);
		}

		public virtual void Set(string key, IEnumerable<T> items)
		{
			this._cache.Set(this.KeyPrefix + key, items, this.GetExpireItemPolicy());
		}

		public virtual bool Contains(string key)
		{
			return this._cache.Contains(key);
		}
	}
}
