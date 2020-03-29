using Pacific.Web.Models.ViewModels;
using System.Collections.Generic;

namespace Pacific.Web.Models.Requests
{
	public class AddProductsRequest: IRequest
	{
		public IEnumerable<ProductViewModel> Products { get; set; }
	}
}
