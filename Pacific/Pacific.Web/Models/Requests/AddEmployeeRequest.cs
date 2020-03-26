using Pacific.Web.Models.TableModels;

namespace Pacific.Web.Models.Requests
{
	public class AddEmployeeRequest: IRequest
	{
		public PostEmployeeViewModel Employee { get; set; }
	}
}
