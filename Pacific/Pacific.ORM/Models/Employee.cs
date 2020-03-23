using LinqToDB;
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
		[NotNull]
		public string LastName { get; set; }

		[Column]
		[NotNull]
		public string FirstName { get; set; }

		[Column]
		[Nullable]
		public string Title { get; set; }

		[Column]
		[Nullable]
		public string TitleOfCourtesy { get; set; }

		[Column]
		[Nullable]
		public DateTime BirthDate { get; set; } = DateTime.UtcNow;

		[Column]
		[Nullable]
		public DateTime HireDate { get; set; } = DateTime.UtcNow;

		[Column]
		[Nullable]
		public string Address { get; set; }

		[Column]
		[Nullable]
		public string City { get; set; }

		[Column]
		[Nullable]
		public string Region { get; set; }

		[Column]
		[Nullable]
		public string PostalCode { get; set; }

		[Column]
		[Nullable]
		public string Country { get; set; }

		[Column]
		[Nullable]
		public string HomePhone { get; set; }

		[Column]
		[Nullable]
		public string Extension { get; set; }

		[Column]
		[DataType(LinqToDB.DataType.Image)]
		[Nullable]
		public byte[] Photo { get; set; }

		[Column]
		[Nullable]
		public string Notes { get; set; }

		[Column("ReportsTo")]
		[Nullable]
		public int? ReportsToId { get; set; }

		[Association(ThisKey = "ReportsToId", OtherKey = "Id")]
		public virtual Employee ReportsTo { get; set; }

		[Column]
		[Nullable]
		public string PhotoPath { get; set; }

		[Association(Relationship = Relationship.OneToMany, ThisKey = "Id", OtherKey = "EmployeeId")]
		public virtual ICollection<EmployeeTerritory> Territories { get; set; } = new List<EmployeeTerritory>();

		[Association(ThisKey = "Id", OtherKey = "EmployeeId")]
		public virtual ICollection<Order> Orders { get; set; }
	}
}
