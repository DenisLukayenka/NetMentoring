using LinqToDB;
using Pacific.ORM;
using Pacific.ORM.HelpModels;
using Pacific.ORM.Models;
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
	}
}
