using Pacific.ORM.HelpModels;
using System.Collections.Generic;

namespace Pacific.Web.Models.Responses
{
	public class EmployeeShipperResponse: IResponse
	{
		public IEnumerable<EmployeeShippers> EmployeeShippers { get; set; }
	}
}
