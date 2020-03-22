using LinqToDB.Mapping;
using System.Collections.Generic;

namespace Pacific.ORM.Models
{
	[Table("Shippers")]
	public class Shipper
	{
		[PrimaryKey, Identity]
		[Column("ShipperID")]
		public int Id { get; set; }

		[Column]
		public string CompanyName { get; set; }

		[Column]
		public string Phone { get; set; }

		[Association(ThisKey = "Id", OtherKey = "ShipVia")]
		public virtual ICollection<Order> Orders { get; set; }
	}
}
