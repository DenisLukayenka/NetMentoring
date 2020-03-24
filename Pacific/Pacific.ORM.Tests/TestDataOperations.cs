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

		[Test]
		public void Test_BulkCopy()
		{
			var products = this.GetListOfProducts();
			
			using (var db = new NothwindDbContext())
			{
				var categories = db.Categories.ToArray();
				var suppliers = db.Suppliers.ToArray();

				foreach (var product in products)
				{
					var insertData = db.Products
						.Value(p => p.Name, product.Name);

					var category = categories.FirstOrDefault(c => c.Name == product.Category.Name);
					insertData = insertData.Value(p => p.CategoryId, category?.Id ?? db.InsertWithInt32Identity(product.Category));

					var supplier = suppliers.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);
					insertData = insertData.Value(p => p.SupplierId, supplier?.Id ?? db.InsertWithInt32Identity(product.Supplier));

					product.Id = insertData.InsertWithInt32Identity() ?? 0;
				}
			}

			Product[] productsActual;
			using (var db = new NothwindDbContext())
			{
				productsActual = db.Products.ToArray().Where(p => products.FirstOrDefault(src => src.Id == p.Id) != null).ToArray();

				Assert.AreEqual(3, productsActual.Length);
			}
		}

		[Test]
		public void Test_ReplaceProductInOrder()
		{
			var random = new Random();

			using (var db = new NothwindDbContext())
			{
				var notShippedOrders = (from od in db.OrderDetails
									   where od.Order.ShippedDate == null
									   select od).ToArray();

				var targetCategories = db.Categories.LoadWith(p => p.Products).ToArray()
					.Where(c => c.Products.FirstOrDefault(f => notShippedOrders.FirstOrDefault(n => n.ProductId == f.Id) != null) != null);

				foreach(var order in notShippedOrders)
				{
					var targetCategory = targetCategories.FirstOrDefault(tc => tc.Products.FirstOrDefault(p => p.Id == order.ProductId) != null);
					if (targetCategory != null)
					{
						db.OrderDetails
							.Where(od => od.OrderId == order.OrderId && od.ProductId == order.ProductId)
							.Set(od => od.ProductId, targetCategory.Products.ElementAt(random.Next(0, targetCategory.Products.Count - 1)).Id)
							.Update();
					}
					
				}
			}
		}

		private Product[] GetListOfProducts()
		{
			return new[]
			{
				new Product()
				{
					Name = "Гречка",
					Category = new Category()
					{
						Name = "Cat1"
					},
					Supplier = new Supplier()
					{
						CompanyName = "Supplier1"
					}
				},
				new Product()
				{
					Name = "Product2",
					Category = new Category()
					{
						Name = "Cat2"
					},
					Supplier = new Supplier()
					{
						CompanyName = "Supplier2"
					}
				},
				new Product()
				{
					Name = "Product3",
					Category = new Category()
					{
						Name = "Cat3"
					},
					Supplier = new Supplier()
					{
						CompanyName = "Supplier3"
					}
				}
			};
		}
	}
}
