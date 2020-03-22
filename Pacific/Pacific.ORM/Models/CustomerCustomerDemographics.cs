using LinqToDB.Mapping;

namespace Pacific.ORM.Models
{
	[Table("CustomerCustomerDemo")]
	public class CustomerCustomerDemographics
	{
		[Column("CustomerID")]
		public string CustomerId { get; set; }
	
		[Association(ThisKey = "CustomerId", OtherKey = "Id")]
		public virtual Customer Customer { get; set; }

		[Column("CustomerTypeID")]
		public string TypeId { get; set; }

		[Association(ThisKey = "TypeId", OtherKey = "Id")]
		public virtual CustomerDemographic Type { get; set; }
	}
}
