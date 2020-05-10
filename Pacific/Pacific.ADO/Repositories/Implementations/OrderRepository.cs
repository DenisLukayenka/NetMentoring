using Pacific.ADO.Models;
using Pacific.ADO.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Pacific.ADO.Extensions;
using System;
using Pacific.ADO.Models.HelpModels;

namespace Pacific.ADO.Repositories.Implementations
{
	public class OrderRepository : IOrderRepository
	{
		private readonly DbProviderFactory _providerFactory;
		private readonly string _connectionString;

		public OrderRepository(string connectionString, string provider)
		{
			this._providerFactory = DbProviderFactories.GetFactory(provider);
			this._connectionString = connectionString;
		}

		public virtual IEnumerable<Order> GetOrders()
		{
			var orders = new List<Order>();

			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "select * from dbo.Orders";
					command.CommandType = CommandType.Text;

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var order = this.ReadOrder(reader);
							orders.Add(order);
						}
					}
				}
			}
			return orders;
		}

		public virtual Order GetOrderById(int id)
		{
			Order order;
			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					var param = command.CreateParameter();
					param.ParameterName = "@id";
					param.Value = id;
					command.Parameters.Add(param);

					command.CommandText = "select * from dbo.Orders where OrderID = @id; " + 
						"select * from dbo.[Order Details] as od " +
							"join dbo.Products as p on od.ProductID = p.ProductID " +
							"where od.OrderID = @id";
					command.CommandType = CommandType.Text;

					using (var reader = command.ExecuteReader())
					{
						reader.Read();
						if (!reader.HasRows) return null;

						order = this.ReadOrder(reader);

						var orderDetails = new List<OrderDetails>();
						reader.NextResult();
						while (reader.Read())
						{
							var orderDetail = this.ReadOrderDetail(reader);
							orderDetail.Product = this.ReadProduct(reader);

							orderDetails.Add(orderDetail);
						}
						order.OrderDetails = orderDetails;
					}
				}
			}
			return order;
		}

		public virtual void Add(Order order)
		{
			using (var connection = this._providerFactory.CreateConnection()) 
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.Parameters.Add(this.CreateParameter(order.CustomerID, "@CustomerID", command));
					command.Parameters.Add(this.CreateParameter(order.EmployeeID, "@EmployeeID", command));
					command.Parameters.Add(this.CreateParameter(order.OrderDate, "@OrderDate", command));
					command.Parameters.Add(this.CreateParameter(order.RequiredDate, "@RequiredDate", command));
					command.Parameters.Add(this.CreateParameter(order.ShippedDate, "@ShippedDate", command));
					command.Parameters.Add(this.CreateParameter(order.ShipVia, "@ShipVia", command));
					command.Parameters.Add(this.CreateParameter(order.Freight, "@Freight", command));
					command.Parameters.Add(this.CreateParameter(order.ShipName, "@ShipName", command));
					command.Parameters.Add(this.CreateParameter(order.ShipAddress, "@ShipAddress", command));
					command.Parameters.Add(this.CreateParameter(order.ShipCity, "@ShipCity", command));
					command.Parameters.Add(this.CreateParameter(order.ShipRegion, "@ShipRegion", command));
					command.Parameters.Add(this.CreateParameter(order.ShipPostalCode, "@ShipPostalCode", command));
					command.Parameters.Add(this.CreateParameter(order.ShipCountry, "@ShipCountry", command));

					command.CommandText = "insert into dbo.Orders (" +
						"CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry) " +
						"Values (@CustomerID, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate, @ShipVia, @Freight, @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry) ";

					command.ExecuteNonQuery();
				}
			}
		}

		public virtual void Update(Order order)
		{
			if (order.OrderState != OrderState.New) return;

			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.Parameters.Add(this.CreateParameter(order.OrderID, "@OrderID", command));
					command.Parameters.Add(this.CreateParameter(order.CustomerID, "@CustomerID", command));
					command.Parameters.Add(this.CreateParameter(order.EmployeeID, "@EmployeeID", command));
					command.Parameters.Add(this.CreateParameter(order.RequiredDate, "@RequiredDate", command));
					command.Parameters.Add(this.CreateParameter(order.ShipVia, "@ShipVia", command));
					command.Parameters.Add(this.CreateParameter(order.Freight, "@Freight", command));
					command.Parameters.Add(this.CreateParameter(order.ShipName, "@ShipName", command));
					command.Parameters.Add(this.CreateParameter(order.ShipAddress, "@ShipAddress", command));
					command.Parameters.Add(this.CreateParameter(order.ShipCity, "@ShipCity", command));
					command.Parameters.Add(this.CreateParameter(order.ShipRegion, "@ShipRegion", command));
					command.Parameters.Add(this.CreateParameter(order.ShipPostalCode, "@ShipPostalCode", command));
					command.Parameters.Add(this.CreateParameter(order.ShipCountry, "@ShipCountry", command));

					command.CommandText = "Update dbo.Orders " +
						"set CustomerID = @CustomerID, " +
							"EmployeeID = @EmployeeID, " +
							"RequiredDate = @RequiredDate, " +
							"ShipVia = @ShipVia, " +
							"Freight = @Freight, " +
							"ShipName = @ShipName, " +
							"ShipAddress = @ShipAddress, " +
							"ShipCity = @ShipCity, " +
							"ShipRegion = @ShipRegion, " +
							"ShipPostalCode = @ShipPostalCode, " +
							"ShipCountry = @ShipCountry " +
						"where OrderID = @OrderID";

					command.ExecuteNonQuery();
				}
			}
		}

		public virtual void DeleteNewOrders()
		{
			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "delete from dbo.Orders where OrderDate IS NULL";
					command.ExecuteNonQuery();
				}
			}
		}

		public virtual void DeleteInProgressOrders()
		{
			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "delete from dbo.Orders where OrderDate <> NULL AND ShippedDate IS NULL";
					command.ExecuteNonQuery();
				}
			}
		}

		public void StartOrder(int orderId, DateTime date)
		{
			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.Parameters.Add(this.CreateParameter(orderId, "@OrderID", command));
					command.Parameters.Add(this.CreateParameter(date, "@OrderDate", command));

					command.CommandText = "Update dbo.Orders set OrderDate = @OrderDate where OrderID = @OrderID";

					command.ExecuteNonQuery();
				}
			}
		}

		public void FinishOrder(int orderId, DateTime date)
		{
			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.Parameters.Add(this.CreateParameter(orderId, "@OrderID", command));
					command.Parameters.Add(this.CreateParameter(date, "@ShippedDate", command));

					command.CommandText = "Update dbo.Orders set ShippedDate = @ShippedDate where OrderID = @OrderID";

					command.ExecuteNonQuery();
				}
			}
		}

		public virtual Order GetLastItem()
		{
			Order order;

			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "select top 1 * from dbo.Orders order by OrderID desc";
					command.CommandType = CommandType.Text;

					using (var reader = command.ExecuteReader())
					{
						reader.Read();
						if (!reader.HasRows) return null;

						order = this.ReadOrder(reader);
					}
				}
			}
			return order;
		}

		public IEnumerable<CustOrderHist> GetCustomerOrderHistory(string customerId)
		{
			var hist = new List<CustOrderHist>();

			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "dbo.CustOrderHist";
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.Add(this.CreateParameter(customerId, "@CustomerID", command));

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var historyItem = new CustOrderHist()
							{
								ProductName = reader.GetValue("ProductName").ConvertFromDbValue<string>(),
								Total = reader.GetValue("Total").ConvertFromDbValue<int>(),
							};

							hist.Add(historyItem);
						}
					}
				}
			}
			return hist;
		}

		public IEnumerable<CustOrdersDetail> GetCustOrdersDetail(int orderId)
		{
			var details = new List<CustOrdersDetail>();

			using (var connection = this._providerFactory.CreateConnection())
			{
				connection.ConnectionString = this._connectionString;
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "dbo.CustOrdersDetail";
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.Add(this.CreateParameter(orderId, "@OrderID", command));

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var detail = new CustOrdersDetail()
							{
								ProductName = reader.GetValue("ProductName").ConvertFromDbValue<string>(),
								UnitPrice = reader.GetValue("UnitPrice").ConvertFromDbValue<decimal>(),
								Quantity = reader.GetValue("Quantity").ConvertFromDbValue<Int16>(),
								Discount = reader.GetValue("Discount").ConvertFromDbValue<int>(),
								ExtendedPrice = reader.GetValue("ExtendedPrice").ConvertFromDbValue<decimal>(),
							};

							details.Add(detail);
						}
					}
				}
			}
			return details;
		}

		protected virtual Order ReadOrder(DbDataReader reader)
		{
			return new Order
			{
				OrderID = reader.GetInt32("OrderID"),
				CustomerID = reader.GetValue("CustomerID").ConvertFromDbValue<string>(),
				EmployeeID = reader.GetValue("EmployeeID").ConvertFromDbValue<int>(),
				OrderDate = reader.GetValue("OrderDate").ConvertFromDbValue<DateTime>(),
				RequiredDate = reader.GetValue("RequiredDate").ConvertFromDbValue<DateTime>(),
				ShippedDate = reader.GetValue("ShippedDate").ConvertFromDbValue<DateTime>(),
				ShipVia = reader.GetValue("ShipVia").ConvertFromDbValue<int>(),
				Freight = reader.GetValue("Freight").ConvertFromDbValue<decimal>(),
				ShipName = reader.GetValue("ShipName").ConvertFromDbValue<string>(),
				ShipAddress = reader.GetValue("ShipAddress").ConvertFromDbValue<string>(),
				ShipCity = reader.GetValue("ShipCity").ConvertFromDbValue<string>(),
				ShipRegion = reader.GetValue("ShipRegion").ConvertFromDbValue<string>(),
				ShipPostalCode = reader.GetValue("ShipPostalCode").ConvertFromDbValue<string>(),
				ShipCountry = reader.GetValue("ShipCountry").ConvertFromDbValue<string>(),
			};
		}

		protected virtual OrderDetails ReadOrderDetail(DbDataReader reader)
		{
			return new OrderDetails
			{
				OrderID = reader.GetInt32("OrderID"),
				ProductID = reader.GetInt32("ProductID"),
				UnitPrice = reader.GetValue("UnitPrice").ConvertFromDbValue<decimal>(),
				Quantity = reader.GetValue("Quantity").ConvertFromDbValue<Int16>(),
				Discount = reader.GetValue("Discount").ConvertFromDbValue<Single>(),
			};
		}

		protected virtual Product ReadProduct(DbDataReader reader)
		{
			return new Product
			{
				ProductID = reader.GetInt32("ProductID"),
				ProductName = reader.GetString("ProductName"),
				SupplierID = reader.GetValue("SupplierID").ConvertFromDbValue<int>(),
				CategoryID = reader.GetValue("CategoryID").ConvertFromDbValue<int>(),
				QuantityPerUnit = reader.GetValue("QuantityPerUnit").ConvertFromDbValue<string>(),
				UnitPrice = reader.GetValue("UnitPrice").ConvertFromDbValue<decimal>(),
				UnitsInStock = reader.GetValue("UnitsInStock").ConvertFromDbValue<Int16>(),
				UnitsOnOrder = reader.GetValue("UnitsOnOrder").ConvertFromDbValue<Int16>(),
				ReorderLevel = reader.GetValue("ReorderLevel").ConvertFromDbValue<Int16>(),
				Discontinued = reader.GetValue("Discontinued").ConvertFromDbValue<bool>(),
			};
		}

		protected virtual DbParameter CreateParameter(object value, string parameterName, DbCommand command)
		{
			var parameter = command.CreateParameter();
			parameter.ParameterName = parameterName;
			parameter.Value = value ?? DBNull.Value;

			return parameter;
		}
	}
}
