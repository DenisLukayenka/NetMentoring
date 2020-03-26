namespace Pacific.Web.Models.TableModels
{
	public class EmployeeViewModel
	{
		public int Id { get; set; }

		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Title { get; set; }
		public string Region { get; set; }
		public string ReportsToName { get; set; }
	}
}
