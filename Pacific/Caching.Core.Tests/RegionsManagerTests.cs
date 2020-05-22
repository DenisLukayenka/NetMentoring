using Caching.Core.CacheManagers.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace Caching.Core.Tests
{
	[TestClass]
	public class RegionsManagerTests
	{
		private RegionsCacheManager regionManager;
		private string connectionString = "data source=localhost;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

		[TestMethod]
		public void MemoryExpirationTimeCache()
		{
			this.regionManager = new RegionsCacheManager(new RegionsMemoryExpTimeCache());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(regionManager.GetItems().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void MemoryChangeMonitorCache()
		{
			this.regionManager = new RegionsCacheManager(new RegionsMemorySqlMonitorCache(connectionString));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(regionManager.GetItems().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCache()
		{
			this.regionManager = new RegionsCacheManager(new RegionRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(this.regionManager.GetItems().Count());
				Thread.Sleep(100);
			}
		}
	}
}
