using LinqToDB.Mapping;

namespace Pacific.ORM.Models
{
	[Table("Categories")]
	public class Category
	{
		[Identity]
		[PrimaryKey]
		[Column("CategoryID")]
		public int Id { get; set; }

		[Column("CategoryName")]
		public string Name { get; set; }

		[Column]
		public string Description { get; set; }

		[Column]
		[DataType(LinqToDB.DataType.Image)]
		public byte[] Picture { get; set; }
	}
}
