using Pacific.ADO.Models;
using Pacific.ADO.Models.HelpModels;
using System;
using System.Collections.Generic;

namespace Pacific.ADO.Repositories.Interfaces
{
	public interface IOrderRepository
	{
		IEnumerable<Order> GetOrders();
		Order GetOrderById(int id);

		Order GetLastItem();

		void Add(Order order);

		void Update(Order order);

		void DeleteNewOrders();
		void DeleteInProgressOrders();

		void StartOrder(int orderId, DateTime date);
		void FinishOrder(int orderId, DateTime date);

		IEnumerable<CustOrderHist> GetCustomerOrderHistory(string customerId);
		IEnumerable<CustOrdersDetail> GetCustOrdersDetail(int orderId);
	}
}
