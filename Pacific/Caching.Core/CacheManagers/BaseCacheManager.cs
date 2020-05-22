using Caching.NorthwindDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Caching.Core.CacheManagers
{
	public class BaseCacheManager<T> where T: class
	{
		private readonly IItemCache<T> _cache;

		public BaseCacheManager(IItemCache<T> cache)
		{
			this._cache = cache;
		}

		public virtual IEnumerable<T> GetItems()
		{
			Console.WriteLine("Get Categories");

			var user = Thread.CurrentPrincipal.Identity.Name;
			var items = this.FetchDataFromCache(user);

			if (items == null)
			{
				Console.WriteLine("From DB");

				items = this.FetchData();
				this.SetItemsToCache(items, user);
			}

			return items;
		}

		protected virtual IEnumerable<T> FetchData()
		{
			IEnumerable<T> items;

			using (var dbContext = new Northwind())
			{
				dbContext.Configuration.LazyLoadingEnabled = false;
				dbContext.Configuration.ProxyCreationEnabled = false;

				items = dbContext.Set<T>().ToList();
			}

			return items;
		}

		protected virtual IEnumerable<T> FetchDataFromCache(string userName)
		{
			return this._cache.Get(userName);
		}

		protected virtual void SetItemsToCache(IEnumerable<T> items, string userName)
		{
			this._cache.Set(userName, items);
		}
	}
}
