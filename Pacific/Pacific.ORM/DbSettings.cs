using System.Linq;
using System.Collections.Generic;
using LinqToDB.Configuration;
using LinqToDB.Common;

namespace Pacific.ORM
{
	public class DbSettings : ILinqToDBSettings
	{
		public DbSettings()
		{
			Configuration.Linq.AllowMultipleQuery = true;
		}

		public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

		public string DefaultConfiguration => "SqlServer";

		public string DefaultDataProvider => "SqlServer";

		public IEnumerable<IConnectionStringSettings> ConnectionStrings
		{
			get
			{
				yield return
					new ConnectionStringSettings
					{
						Name = "NorthwindDb",
						ProviderName = "SqlServer",
						ConnectionString = @"Data Source=localhost;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
					};
			}
		}
	}
}
