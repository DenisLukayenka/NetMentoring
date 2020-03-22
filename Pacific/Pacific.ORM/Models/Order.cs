using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace Pacific.ORM.Models
{
	[Table("Orders")]
	public class Order
	{
		[PrimaryKey, Identity]
		[Column("OrderID")]
		public int Id { get; set; }

		[Column("CustomerID")]
		public string CustomerId { get; set; }
		[Association(ThisKey = "CustomerId", OtherKey = "Id")]
		public virtual Customer Customer { get; set; }

		[Column("EmployeeID")]
		public int EmployeeId { get; set; }
		[Association(ThisKey = "EmployeeId", OtherKey = "Id")]
		public virtual Employee Employee { get; set; }

		[Column]
		public DateTime OrderDate { get; set; }

		[Column]
		public DateTime RequiredDate { get; set; }

		[Column]
		public DateTime ShippedDate { get; set; }

		[Column]
		public int ShipVia { get; set; }
		[Association(ThisKey = "ShipVia", OtherKey = "Id")]
		public virtual Shipper Shipper { get; set; }

		[Column]
		public decimal Freight { get; set; }

		[Column]
		public string ShipName { get; set; }

		[Column]
		public string ShipAddress { get; set; }

		[Column]
		public string ShipCity { get; set; }

		[Column]
		public string ShipRegion { get; set; }

		[Column]
		public string ShipPostalCode { get; set; }

		[Column]
		public string ShipCountry { get; set; }

		[Association(ThisKey = "Id", OtherKey = "OrderId")]
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
