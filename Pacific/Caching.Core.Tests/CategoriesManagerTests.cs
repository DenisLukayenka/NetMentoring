using System;
using System.Linq;
using System.Threading;
using Caching.Core.CacheManagers.Categories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caching.Core.Tests
{
	[TestClass]
	public class CategoriesManagerTests
	{
		private CategoriesCacheManager categoryManager;
		private string connectionString = "data source=localhost;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

		[TestMethod]
		public void MemoryExpirationTimeCache()
		{
			this.categoryManager = new CategoriesCacheManager(new CategoriesMemoryExpTimeCache());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetItems().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void MemoryChangeMonitorCache()
		{
			this.categoryManager = new CategoriesCacheManager(new CategoriesMemorySqlMonitorCache(connectionString));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetItems().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCache()
		{
			this.categoryManager = new CategoriesCacheManager(new CategoriesRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(this.categoryManager.GetItems().Count());
				Thread.Sleep(100);
			}
		}
	}
}
