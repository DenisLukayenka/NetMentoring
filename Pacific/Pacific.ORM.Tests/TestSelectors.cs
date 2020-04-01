using LinqToDB;
using LinqToDB.Data;
using NUnit.Framework;
using System;
using System.Linq;

namespace Pacific.ORM.Tests
{
	[TestFixture]
	class TestSelectors
	{
		[SetUp]
		public void Init()
		{
			DataConnection.DefaultSettings = new DbSettings();
		}

		[Test]
		public void Test_Selector_ProductsWithCategoryAndShipper()
		{
			using (var db = new NothwindDbContext())
			{
				foreach (var product in db.Products
					.LoadWith(p => p.Category)
					.LoadWith(p => p.Supplier))
				{
					Console.WriteLine($"{product.Id}, {product.Name}" +
						$"Category: {product.Category.Name}, " +
						$"Supplier: {product.Supplier.CompanyName}");
				}
			}
		}

		[Test]
		public void Test_Selector_EmployeeWithRegion()
		{
			using (var db = new NothwindDbContext())
			{
				foreach (var employee in db.Employees)
				{
					Console.WriteLine($"{employee.Id}, {employee.FirstName}, {employee.Region}");
				}
			}
		}

		[Test]
		public void Test_Selector_EmployeeStatisticByRegion()
		{
			using (var db = new NothwindDbContext())
			{
				var statistic = from e in db.Employees
								group e by e.Region into er
								select new { Region = er.Key, Count = er.Count() };

				foreach (var data in statistic)
				{
					Console.WriteLine($"{data.Region}: {data.Count}");
				}
			}
		}

		[Test]
		public void Test_Selector_EmployeeShippers()
		{
			using (var db = new NothwindDbContext())
			{
				var statistic = from groupped in (from order in db.Orders
										   join e in db.Employees on order.EmployeeId equals e.Id
										   join s in db.Shippers on order.ShipVia equals s.Id
										   group s by e.Id)
								select new
								{
									Employee = (from e in db.Employees where e.Id == groupped.Key select e).Single(),
									Shippers= from shippers in groupped select shippers
								};

				foreach (var data in statistic)
				{
					Console.WriteLine($"{data.Employee.Id}");
					foreach (var shipper in data.Shippers)
					{
						Console.WriteLine($"----{shipper.Id}, {shipper.CompanyName}");
					}
				}
			}
		}
	}
}
