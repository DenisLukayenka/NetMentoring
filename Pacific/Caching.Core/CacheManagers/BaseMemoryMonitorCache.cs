using System.Data;
using System.Data.SqlClient;
using System.Runtime.Caching;

namespace Caching.Core.CacheManagers
{
	public abstract class BaseMemoryMonitorCache<T> : BaseMemoryCache<T>
	{
		private readonly string _connectionString;

		public BaseMemoryMonitorCache(string connectionString) : base()
		{
			this._connectionString = connectionString;
		}

		protected abstract string GetMonitorCommand();

		protected override CacheItemPolicy GetExpireItemPolicy()
		{
			var policy = new CacheItemPolicy();
			policy.ChangeMonitors.Add(this.InitMonitor(this._connectionString, this.GetMonitorCommand()));

			return policy;
		}

		private SqlChangeMonitor InitMonitor(string connectionString, string commandText)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var result = SqlDependency.Start(connection.ConnectionString);

				var command = connection.CreateCommand();
				command.CommandText = commandText;
				command.CommandType = CommandType.Text;

				var monitor = new SqlChangeMonitor(new SqlDependency(command));
				command.ExecuteNonQuery();

				return monitor;
			}
		}
	}
}
