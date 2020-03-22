using LinqToDB.Mapping;

namespace Pacific.ORM.Models
{
	[Table("Order Details")]
	public class OrderDetail
	{
		[Column("OrderID")]
		public int OrderId { get; set; }
		[Association(ThisKey = "OrderId", OtherKey = "Id")]
		public virtual Order Order { get; set; }

		[Column("ProductID")]
		public int ProductId { get; set; }
		[Association(ThisKey = "ProductId", OtherKey = "Id")]
		public virtual Product Product { get; set; }

		[Column]
		[DataType(LinqToDB.DataType.Money)]
		public decimal UnitPrice { get; set; }

		[Column]
		public int Quantity { get; set; }

		[Column]
		public double Discount { get; set; }
	}
}
