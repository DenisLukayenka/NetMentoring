using Pacific.Web.Models.TableModels;
using System.Collections.Generic;

namespace Pacific.Web.Models.Responses
{
	public class EmployeesResponse: IResponse
	{
		public IEnumerable<EmployeeTableView> Employees { get; set; }
	}
}
