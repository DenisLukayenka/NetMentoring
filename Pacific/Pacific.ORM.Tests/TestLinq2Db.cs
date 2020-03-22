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
				foreach(var category in db.Categories.LoadWith(c => c.Products))
				{
					Console.WriteLine($"{category.Id} | " +
						$"{category.Name} | " +
						$"{category.Description} |" +
						$"{category.Products.Count} | ");
				}
			}

			Assert.True(true);
		}

		[Test]
		public void TestRegionsDataFetchFromDb()
		{
			using (var db = new NothwindDbContext())
			{
				foreach (var region in db.Regions.LoadWith(r => r.Territories))
				{
					Console.WriteLine($"{region.Id} | {region.Description} | {region.Territories.Count}");
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
					Console.WriteLine($"{ter.Id} | {ter.Description} | !!{ter.RegionId}: {ter.Region.Description}!!");
				}
			}

			Assert.True(true);
		}

		[Test]
		public void TestEmployeesDataFetchFromDb()
		{
			using (var db = new NothwindDbContext())
			{
				foreach (var eml in db.Employees.LoadWith(e => e.ReportsTo))
				{
					Console.WriteLine($"{eml.Id} | " +
						$"{eml.FirstName} | " +
						$"{eml.LastName} | " +
						$"{eml.Title} | " +
						$"{eml.TitleOfCourtesy} | " +
						$"{eml.BirthDate} | " +
						$"{eml.HireDate} | " +
						$"{eml.Address} | " +
						$"{eml.City} | " +
						$"{eml.Region} | " +
						$"{eml.PostalCode} | " +
						$"{eml.Country} | " +
						$"{eml.HomePhone} | " +
						$"{eml.Extension} | " +
						$"{eml.Photo.Length} | " +
						$"{eml.Notes} | " +
						$"-----{eml.ReportsToId} | " +
						$"{eml.PhotoPath} | ");
				}
			}

			Assert.True(true);
		}

		[Test]
		public void TestEmployeeTerritoriesDataFetchFromDb()
		{
			using (var db = new NothwindDbContext())
			{
				foreach (var et in db.EmployeeTerritories.LoadWith(t => t.Employee).LoadWith(t => t.Territory))
				{
					Console.WriteLine($"{et.Employee.Id} | {et.EmployeeId} --|-- {et.Territory.Id}: {et.TerritoryId}!!");
				}
			}

			Assert.True(true);
		}

		[Test]
		public void TestSuppliersDataFetchFromDb()
		{
			using (var db = new NothwindDbContext())
			{
				foreach (var s in db.Suppliers.LoadWith(s => s.Products))
				{
					Console.WriteLine($"{s.Id} | " +
						$"{s.CompanyName} | " +
						$"{s.ContactName} | " +
						$"{s.ContactTitle} | " +
						$"{s.Address} | " +
						$"{s.City} | " +
						$"{s.Region} | " +
						$"{s.PostalCode} | " +
						$"{s.Country} | " +
						$"{s.Phone} | " +
						$"{s.Fax} | " +
						$"{s.HomePage} | " +
						$"{s.Products.Count} | ");
				}
			}

			Assert.True(true);
		}

		[Test]
		public void TestProductsDataFetchFromDb()
		{
			using (var db = new NothwindDbContext())
			{
				foreach (var p in db.Products.LoadWith(p => p.Category).LoadWith(p => p.Supplier))
				{
					Console.WriteLine($"{p.Id} | " +
						$"{p.Name} | " +
						$"{p.SupplierId} | " +
						$"{p.CategoryId} | " +
						$"{p.QuantityPerUnit} | " +
						$"{p.UnitPrice} | " +
						$"{p.UnitsInStock} | " +
						$"{p.UnitsOnOrder} | " +
						$"{p.ReorderLevel} | " +
						$"{p.Discontinued} | " +
						$"{p.Category?.Id} | " +
						$"{p.Supplier.Id} | ");
				}
			}

			Assert.True(true);
		}
	}
}
