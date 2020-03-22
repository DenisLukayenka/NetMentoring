using LinqToDB.Mapping;

namespace Pacific.ORM.Models
{
	[Table("EmployeeTerritories")]
	public class EmployeeTerritory
	{
		[Column("EmployeeID")]
		public int EmployeeId { get; set; }

		[Association(Relationship = Relationship.ManyToOne, ThisKey = "EmployeeIDd", OtherKey = "Id")]
		public virtual Employee Employee { get; set; }

		[Column("TerritoryID")]
		public int TerritoryId { get; set; }

		[Association(Relationship = Relationship.ManyToOne, ThisKey = "TerritoryId", OtherKey = "Id")]
		public virtual Territory Territory { get; set; }
	}
}
