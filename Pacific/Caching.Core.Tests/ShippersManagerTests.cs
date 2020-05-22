using Caching.Core.CacheManagers.Shippers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace Caching.Core.Tests
{
	[TestClass]
	public class ShippersManagerTests
	{
		private ShippersCacheManager shippersManager;
		private string connectionString = "data source=localhost;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

		[TestMethod]
		public void MemoryExpirationTimeCache()
		{
			this.shippersManager = new ShippersCacheManager(new ShippersMemoryExpTimeCache());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(shippersManager.GetItems().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void MemoryChangeMonitorCache()
		{
			this.shippersManager = new ShippersCacheManager(new ShippersMemorySqlMonitorCache(connectionString));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(shippersManager.GetItems().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCache()
		{
			this.shippersManager = new ShippersCacheManager(new ShipperRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(this.shippersManager.GetItems().Count());
				Thread.Sleep(100);
			}
		}
	}
}
