using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacific.ORM.Models
{
	[Table("Employees")]
	public class Employee
	{
		[PrimaryKey, Identity]
		[Column("EmployeeID")]
		public int Id { get; set; }

		[Column]
		public string LastName { get; set; }

		[Column]
		public string FirstName { get; set; }

		[Column]
		public string Title { get; set; }

		[Column]
		public string TitleOfCourtesy { get; set; }

		[Column]
		public DateTime BirthDate { get; set; }

		[Column]
		public DateTime HireDate { get; set; }

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
		public string HomePhone { get; set; }

		[Column]
		public string Extension { get; set; }

		[Column]
		[DataType(LinqToDB.DataType.Image)]
		public byte[] Photo { get; set; }

		[Column]
		public string Notes { get; set; }

		[Column("ReportsTo")]
		public int ReportsToId { get; set; }

		[Association(ThisKey = "ReportsToId", OtherKey = "Id")]
		public virtual Employee ReportsTo { get; set; }

		[Column]
		public string PhotoPath { get; set; }

		[Association(Relationship = Relationship.OneToMany, ThisKey = "Id", OtherKey = "EmployeeId")]
		public virtual ICollection<EmployeeTerritory> Territories { get; set; } = new List<EmployeeTerritory>();
	}
}
