using LinqToDB.Mapping;

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
	}
}
