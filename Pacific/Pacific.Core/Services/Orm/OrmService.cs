﻿using LinqToDB;
using LinqToDB.Data;
using Pacific.ORM;
using Pacific.ORM.HelpModels;
using Pacific.ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pacific.Core.Services.Orm
{
	public class OrmService
	{
		public async Task<IEnumerable<Product>> SelectProductsAsync()
		{
			Product[] products;

			using (var db = new NothwindDbContext())
			{
				products = await db.Products.LoadWith(p => p.Category).LoadWith(p => p.Supplier).ToArrayAsync();
			}

			return products;
		}

		public async Task<IEnumerable<Order>> SelectOrdersAsync(ReportOptions options)
		{
			using (var db = new NothwindDbContext())
			{
				var filteredOrders = db.Orders.Select(x => x);

				if (!string.IsNullOrWhiteSpace(options.CustomerId))
				{
					filteredOrders = filteredOrders.Where(order => order.CustomerId == options.CustomerId);
				}
				if (options.DateFrom != null)
				{
					filteredOrders = filteredOrders.Where(order => order.OrderDate >= options.DateFrom.Value);
				}
				if (options.DateTo != null)
				{
					filteredOrders = filteredOrders.Where(order => order.OrderDate <= options.DateTo.Value);
				}
				if (options.Take != null)
				{
					filteredOrders = filteredOrders.Take(options.Take.Value);
				}
				if (options.Skip != null)
				{
					filteredOrders = filteredOrders.Skip(options.Skip.Value);
				}

				filteredOrders = filteredOrders.OrderBy(o => o.Id);

				return await filteredOrders.ToArrayAsync();
			}
		}

		public async Task<IEnumerable<Employee>> SelectEmployeesAsync()
		{
			using (var db = new NothwindDbContext())
			{
				return await db.Employees.LoadWith(e => e.ReportsTo).ToArrayAsync();
			}
		}

		public async Task<IEnumerable<RegionStatistic>> SelectRegionStatisticAsync()
		{
			using (var db = new NothwindDbContext())
			{
				return await db.Employees.GroupBy(e => e.Region).Select(g => new RegionStatistic
				{
					Region = g.Key,
					EmployeesCount = g.Count()
				}).ToArrayAsync();
			}
		}

		public async Task<IEnumerable<EmployeeShippers>> SelectEmployeeShippersAsync()
		{
			// Used group by to distinct same values
			// P.S. IEqualityComparer for distinct not worked???
			using (var db = new NothwindDbContext())
			{
				return await db.Orders.LoadWith(o => o.Shipper).LoadWith(o => o.Employee)
					.GroupBy(o => new 
					{ 
						o.ShipVia, 
						o.EmployeeId, 
						o.Shipper.CompanyName, 
						o.Employee.FirstName,
						o.Employee.LastName, 
						o.Shipper.Phone
					})
					.Select(o => new EmployeeShippers
					{
						EmployeeId = o.Key.EmployeeId,
						CompanyName = o.Key.CompanyName,
						FirstName = o.Key.FirstName,
						LastName = o.Key.LastName,
						Phone = o.Key.Phone,
						ShipperId = o.Key.ShipVia
					}).ToArrayAsync();
			}
		}

		public async Task<bool> AddEmployeeToDbAsync(Employee employee)
		{
			int? records;
			using (var db = new NothwindDbContext())
			{
				records = await db.InsertAsync(employee);
			}

			
			return records != null;
		}

		public async Task<IEnumerable<Category>> FetchAllCategoriesAsync()
		{
			using (var db = new NothwindDbContext())
			{
				return await db.Categories.ToArrayAsync();
			}
		}

		public Task<bool> MoveProductsToCategoryAsync(IEnumerable<int> productsIds, int targetCategoryId)
		{
			using (var db = new NothwindDbContext())
			{
				db.Products.Where(p => productsIds.Contains(p.Id)).Set(p => p.CategoryId, targetCategoryId).Update();
			}

			return Task.FromResult(true);
		}

		public async Task<bool> AddProductsAsync(IEnumerable<Product> products)
		{
			using (var db = new NothwindDbContext())
			{
				var dbCategories = await db.Categories.ToArrayAsync();
				var dbSuppliers = await db.Suppliers.ToArrayAsync();

				foreach(var product in products)
				{
					var targetCategoryId = dbCategories.FirstOrDefault(c => c.Name == product.Category.Name)?.Id;
					if(targetCategoryId == null)
					{
						targetCategoryId = await db.Categories.Value(c => c.Name, product.Category.Name).InsertWithInt32IdentityAsync();
					}

					product.CategoryId = targetCategoryId ?? throw new Exception("Category id value is not presented");

					var targetSupplierId = dbSuppliers.FirstOrDefault(c => c.CompanyName == product.Supplier.CompanyName)?.Id;
					if (targetSupplierId == null)
					{
						targetSupplierId = await db.Suppliers.Value(c => c.CompanyName, product.Supplier.CompanyName).InsertWithInt32IdentityAsync();
					}

					product.SupplierId = targetSupplierId ?? throw new Exception("Supplier id value is not presented");
				}

				db.BulkCopy(products);
			}

			return true;
		}

		public async Task<IEnumerable<OrderDetail>> SelectNotShippedOrders()
		{
			using (var db = new NothwindDbContext())
			{
				return await db.OrderDetails
					.LoadWith(od => od.Order)
					.LoadWith(od => od.Product.Category)
					.LoadWith(od => od.Product.Supplier)
					.Where(od => od.Order.ShippedDate == null)
					.ToArrayAsync();
			}
		}

		public async Task<IEnumerable<Product>> SelectSimililarProducts(int productId)
		{
			using (var db = new NothwindDbContext())
			{
				var category = await db.Categories
					.LoadWith(c => c.Products)
					.FirstOrDefaultAsync(c => c.Products.FirstOrDefault(p => p.Id == productId) != null);

				return category.Products;
			}
		}

		public async Task<bool> ReplaceProductInOrder(int originProductId, int originOrderId, int productId)
		{
			using (var db = new NothwindDbContext())
			{
				var recordsUpdated = await db.OrderDetails
					.Where(od => od.OrderId == originOrderId && od.ProductId == originProductId)
					.Set(od => od.ProductId, productId)
					.UpdateAsync();

				return recordsUpdated == 1;
			}
		}
	}
}
