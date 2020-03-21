using LinqToDB;
using LinqToDB.Data;
using NUnit.Framework;
using System;

namespace Pacific.ORM.Tests
{
	[TestFixture]
	public class TestLinq2Db
	{
		[SetUp]
		public void InitTests()
		{
			DataConnection.DefaultSettings = new DbSettings();
		}

		[Test]
		public void TestCategoriesDataFetchFromDb()
		{
			using (var db = new NothwindDbContext())
			{
				foreach(var category in db.Categories)
				{
					Console.WriteLine($"{category.Id} | {category.Name} | {category.Description}");
				}
			}

			Assert.True(true);
		}

		[Test]
		public void TestRegionsDataFetchFromDb()
		{
			using (var db = new NothwindDbContext())
			{
				foreach (var region in db.Regions)
				{
					Console.WriteLine($"{region.Id} | {region.Description}");
				}
			}

			Assert.True(true);
		}

		[Test]
		public void TestTerritoriesDataFetchFromDb()
		{
			using (var db = new NothwindDbContext())
			{
				foreach (var ter in db.Territories.LoadWith(t => t.Region))
				{
					Console.WriteLine($"{ter.Id} | {ter.Description} | !!{ter.RegionID}: {ter.Region.Description}!!");
				}
			}

			Assert.True(true);
		}
	}
}
