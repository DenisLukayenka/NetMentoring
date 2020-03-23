using LinqToDB;
using LinqToDB.Data;
using NUnit.Framework;
using Pacific.ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacific.ORM.Tests
{
	[TestFixture]
	public class TestDataOperations
	{
		[SetUp]
		public void Init()
		{
			DataConnection.DefaultSettings = new DbSettings();
		}

		[Test]
		public void Test_AddEmployee()
		{
			var employee = new Employee()
			{
				Address = "Minsk2",
				FirstName = "Denis2",
				LastName = "Lukayenka2",
				Region = "WA"
			};

			using (var db = new NothwindDbContext())
			{
				db.Insert<Employee>(employee);
			}

			Employee iEmployee;
			using (var db = new NothwindDbContext())
			{
				iEmployee = (from e in db.Employees
							 where "Denis2".Equals(e.FirstName) && e.LastName == "Lukayenka2"
							 select e).Single();

				Assert.AreEqual(employee.FirstName, iEmployee.FirstName);
				Assert.AreEqual(employee.LastName, iEmployee.LastName);
				Assert.AreEqual(employee.Address, iEmployee.Address);
				Assert.AreEqual(employee.Region, iEmployee.Region);
			}
		}

		[Test]
		public void Test_MoveProductToAnotherCategory()
		{
			int srcCategoryId = 2;
			int distCategoryId = 1;

			using (var db = new NothwindDbContext())
			{
				db.Products.Where(p => p.CategoryId == srcCategoryId).Set(p => p.CategoryId, distCategoryId).Update();
			}

			Product[] productsBySrcCategory;
			using (var db = new NothwindDbContext())
			{
				productsBySrcCategory = db.Products.Where(p => p.CategoryId == srcCategoryId).ToArray();

				Assert.AreEqual(0, productsBySrcCategory.Length);
			}
		}
	}
}
