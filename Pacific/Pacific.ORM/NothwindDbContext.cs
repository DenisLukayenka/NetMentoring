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
		public ITable<Employee> Employees => GetTable<Employee>();
		public ITable<EmployeeTerritory> EmployeeTerritories => GetTable<EmployeeTerritory>();
		public ITable<Supplier> Suppliers => GetTable<Supplier>();

		public ITable<Product> Products => GetTable<Product>();
	}
}
