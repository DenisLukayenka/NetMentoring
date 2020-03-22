using LinqToDB.Mapping;
using System.Collections.Generic;

namespace Pacific.ORM.Models
{
	[Table("Region")]
	public class Region
	{
		[Identity]
		[PrimaryKey]
		[Column("RegionID")]
		public int Id { get; set; }

		[Column("RegionDescription")]
		public string Description { get; set; }

		[Association(ThisKey = "Id", OtherKey = "RegionId")]
		public virtual ICollection<Territory> Territories { get; set; }
	}
}
