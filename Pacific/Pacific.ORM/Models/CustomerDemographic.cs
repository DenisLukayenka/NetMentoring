using LinqToDB.Mapping;
using System.Collections.Generic;

namespace Pacific.ORM.Models
{
	[Table("CustomerDemographics")]
	public class CustomerDemographic
	{
		[PrimaryKey]
		[Column("CustomerTypeID")]
		public string Id { get; set; }

		[Column("CustomerDesc")]
		public string Description { get; set; }

		[Association(ThisKey = "Id", OtherKey = "TypeId")]
		public virtual ICollection<CustomerCustomerDemographics> CustomerCustomerDemographics { get; set; }
	}
}
