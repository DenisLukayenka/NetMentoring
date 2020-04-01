using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacific.ORM.Models
{
	[Table("Products")]
	public class Product
	{
		[PrimaryKey, Identity]
		[Column("ProductID")]
		public int Id { get; set; }

		[Column("ProductName")]
		public string Name { get; set; }

		[Association(ThisKey = "SupplierId", OtherKey = "Id")]
		public virtual Supplier Supplier { get; set; }
		[Column("SupplierID")]
		public int SupplierId { get; set; }

		[Column("CategoryID")]
		public int CategoryId { get; set; }
		[Association(ThisKey = "CategoryId", OtherKey = "Id")]
		public virtual Category Category { get; set; }

		[Column]
		public string QuantityPerUnit { get; set; }

		[Column]
		[DataType(LinqToDB.DataType.Money)]
		public decimal UnitPrice { get; set; }

		[Column]
		public int UnitsInStock { get; set; }

		[Column]
		public int UnitsOnOrder { get; set; }

		[Column]
		public int ReorderLevel { get; set; }

		[Column]
		public bool Discontinued { get; set; }

		[Association(ThisKey = "Id", OtherKey = "ProductId")]
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
