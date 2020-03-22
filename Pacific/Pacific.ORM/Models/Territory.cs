using LinqToDB.Mapping;
using System.Collections.Generic;

namespace Pacific.ORM.Models
{
	[Table("Territories")]
	public class Territory
	{
		[Identity]
		[PrimaryKey]
		[Column("TerritoryID")]
		public int Id { get; set; }

		[Column("TerritoryDescription")]
		public string Description { get; set; }

		[Column("RegionID")]
		public int RegionId { get; set; }

		[Association(ThisKey = "RegionId", OtherKey = "Id")]
		public virtual Region Region { get; set; }

		[Association(Relationship = Relationship.OneToMany, ThisKey = "Id", OtherKey = "TerritoryId")]
		public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; } = new List<EmployeeTerritory>();
	}
}
