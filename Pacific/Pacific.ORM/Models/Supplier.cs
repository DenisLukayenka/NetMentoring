using LinqToDB.Mapping;
using System.Collections.Generic;

namespace Pacific.ORM.Models
{
	[Table("Suppliers")]
	public class Supplier
	{
		[PrimaryKey, Identity]
		[Column("SupplierID")]
		public int Id { get; set; }

		[Column]
		public string CompanyName { get; set; }

		[Column]
		public string ContactName { get; set; }

		[Column]
		public string ContactTitle { get; set; }

		[Column]
		public string Address { get; set; }

		[Column]
		public string City { get; set; }

		[Column]
		public string Region { get; set; }

		[Column]
		public string PostalCode { get; set; }

		[Column]
		public string Country { get; set; }

		[Column]
		public string Phone { get; set; }

		[Column]
		public string Fax { get; set; }

		[Column]
		public string HomePage { get; set; }

		[Association(ThisKey = "Id", OtherKey = "SupplierId")]
		public virtual ICollection<Product> Products { get; set; }
	}
}
