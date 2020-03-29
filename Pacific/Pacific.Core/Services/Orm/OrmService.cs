using LinqToDB;
using LinqToDB.Data;
using Pacific.ORM;
using Pacific.ORM.HelpModels;
using Pacific.ORM.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

		public async Task<bool> MoveProductsToCategoryAsync(IEnumerable<int> productsIds, int targetCategoryId)
		{
			using (var db = new NothwindDbContext())
			{
				db.Products.Where(p => productsIds.Contains(p.Id)).Set(p => p.CategoryId, targetCategoryId).Update();
			}

			return true;
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

					product.CategoryId = targetCategoryId ?? throw new System.Exception("Category id value is not presented");

					var targetSupplierId = dbSuppliers.FirstOrDefault(c => c.CompanyName == product.Supplier.CompanyName)?.Id;
					if (targetSupplierId == null)
					{
						targetSupplierId = await db.Suppliers.Value(c => c.CompanyName, product.Supplier.CompanyName).InsertWithInt32IdentityAsync();
					}

					product.SupplierId = targetSupplierId ?? throw new System.Exception("Supplier id value is not presented");
				}

				db.BulkCopy(products);
			}

			return true;
		}
	}
}
