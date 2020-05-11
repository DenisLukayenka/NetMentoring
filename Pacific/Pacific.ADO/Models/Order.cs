using System;
using System.Collections.Generic;

namespace Pacific.ADO.Models
{
	public class Order
	{
		public int OrderID { get; set; }

		public string CustomerID { get; set; }

		public int? EmployeeID { get; set; }

		public DateTime? OrderDate { get; set; }
		public DateTime? RequiredDate { get; set; }
		public DateTime? ShippedDate { get; set; }

		public int? ShipVia { get; set; }
		public decimal? Freight { get; set; }
		public string ShipName { get; set; }
		public string ShipAddress { get; set; }
		public string ShipCity { get; set; }
		public string ShipRegion { get; set; }
		public string ShipPostalCode { get; set; }
		public string ShipCountry { get; set; }

		public OrderState OrderState 
		{ 
			get
			{
				return this.OrderDate == null ? OrderState.New : this.ShippedDate == null ? OrderState.InProgress : OrderState.Done;
			}
		}

		public IEnumerable<OrderDetails> OrderDetails { get; set; }
	}

	public enum OrderState
	{
		New,
		InProgress,
		Done
	}
}
