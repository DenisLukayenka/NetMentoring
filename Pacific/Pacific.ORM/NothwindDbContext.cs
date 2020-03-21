using LinqToDB;
using LinqToDB.Data;
using Pacific.ORM.Models;

namespace Pacific.ORM
{
	public class NothwindDbContext : DataConnection
	{
		public NothwindDbContext() : base("NorthwindDb") { }

		public ITable<Category> Categories => GetTable<Category>();
		public ITable<Region> Regions => GetTable<Region>();
		public ITable<Territory> Territories => GetTable<Territory>();
	}
}
