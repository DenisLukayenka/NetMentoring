using LinqToDB.Mapping;

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
		public int RegionID { get; set; }

		[Association(ThisKey = "RegionID", OtherKey = "Id")]
		public virtual Region Region { get; set; }
	}
}
