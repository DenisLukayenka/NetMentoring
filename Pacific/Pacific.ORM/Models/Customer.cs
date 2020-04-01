using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacific.ORM.Models
{
	[Table("Customers")]
	public class Customer
	{
		[PrimaryKey, Identity]
		[Column("CustomerID")]
		public string Id { get; set; }

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

		[Association(ThisKey = "Id", OtherKey = "CustomerId")]
		public ICollection<Order> Orders { get; set; }

		[Association(ThisKey = "Id", OtherKey = "CustomerId")]
		public ICollection<CustomerCustomerDemographics> CustomerCustomerDemographics { get; set; }
	}
}
