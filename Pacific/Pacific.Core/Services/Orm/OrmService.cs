using LinqToDB;
using Pacific.ORM;
using Pacific.ORM.Models;
using System.Collections.Generic;
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
	}
}
