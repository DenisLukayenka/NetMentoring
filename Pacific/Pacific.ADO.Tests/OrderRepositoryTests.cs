using NUnit.Framework;
using Pacific.ADO.Repositories.Implementations;
using Pacific.ADO.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Pacific.ADO.Models;
using System;
using System.Linq;

namespace Pacific.ADO.Tests
{
	public class OrderRepositoryTests
	{
		private IOrderRepository _orderRepository;

		[SetUp]
		public void Setup()
		{
			var initializer = new Initializer();

			var builder = new SqlConnectionStringBuilder()
			{
				DataSource = "localhost",
				InitialCatalog = "Northwind",
				IntegratedSecurity = true,
				TrustServerCertificate = false,
			};

			this._orderRepository = new OrderRepository(builder.ConnectionString, "System.Data.SqlClient");
		}

		[Test]
		public void GetOrders_OrdersExists_NotNullResult()
		{
			var orders = this._orderRepository.GetOrders();
			Assert.NotNull(orders);
		}

		[Test]
		public void GetOrderById_OrderExist_OrderWithOrderDetailsAndProduct()
		{
			var order = this._orderRepository.GetOrderById(10993);

			Assert.NotNull(order);
			Assert.NotNull(order.OrderDetails);
		}

		[Test]
		public void Add_SuccessAddOrder()
		{
			var order = new Order()
			{
				CustomerID = "VINET",
				ShipVia = 3,
				Freight = 11,
				ShipName = "AAAAAAAAAAAAAAAAAAAAAAAAA",
			};

			this._orderRepository.Add(order);

			Assert.Pass();
		}

		[Test]
		public void Update_UpdatedSuccess()
		{
			var order = this.CreateOrder();
			this._orderRepository.Add(order);

			var lastOrder = this._orderRepository.GetLastItem();
			lastOrder.ShipName = "new name111";
			lastOrder.ShipCountry = "help";

			this._orderRepository.Update(order);
			var dbOrder = this._orderRepository.GetOrderById(lastOrder.OrderID);

			Assert.AreEqual(dbOrder.ShipName, order.ShipName);
		}

		[Test]
		public void DeleteNewOrders_SuccessDelete()
		{
			this._orderRepository.DeleteNewOrders();

			var orders = this._orderRepository.GetOrders().Where(o => o.OrderState == OrderState.New).ToArray();

			Assert.AreEqual(orders.Length, 0);
		}

		[Test]
		public void DeleteInProgressOrders_SuccessDelete()
		{
			this._orderRepository.DeleteNewOrders();

			var orders = this._orderRepository.GetOrders().Where(o => o.OrderState == OrderState.InProgress).ToArray();

			Assert.AreEqual(orders.Length, 0);
		}

		[Test]
		public void StartOrder_SuccessChangeOrderDate()
		{
			this._orderRepository.Add(this.CreateOrder());
			var dbOrder = this._orderRepository.GetLastItem();

			this._orderRepository.StartOrder(dbOrder.OrderID, DateTime.Now);
			dbOrder = this._orderRepository.GetLastItem();

			Assert.AreEqual(dbOrder.OrderDate.Value.ToShortDateString(), DateTime.Now.ToShortDateString());
		}

		[Test]
		public void FinishOrder_SuccessDelete()
		{
			this._orderRepository.Add(this.CreateOrder());
			var dbOrder = this._orderRepository.GetLastItem();

			this._orderRepository.FinishOrder(dbOrder.OrderID, DateTime.Now);
			dbOrder = this._orderRepository.GetLastItem();

			Assert.AreEqual(dbOrder.ShippedDate.Value.ToShortDateString(), DateTime.Now.ToShortDateString());
		}
		
		[Test]
		public void GetLastItem()
		{
			var order = this._orderRepository.GetLastItem();

			Assert.NotNull(order);
		}

		[Test]
		public void GetCustomerOrderHistory()
		{
			var hist = this._orderRepository.GetCustomerOrderHistory("VINET").ToArray();

			Assert.True(hist.Length > 0);
		}

		[Test]
		public void GetCustOrdersDetail()
		{
			var details = this._orderRepository.GetCustOrdersDetail(10248).ToArray();

			Assert.True(details.Length > 0);
		}

		private Order CreateOrder()
		{
			return new Order()
			{
				OrderID = 11078,
				ShipVia = 3,
				Freight = 11,
				ShipName = "new value",
				ShippedDate = DateTime.Now,
			};
		}
	}
}