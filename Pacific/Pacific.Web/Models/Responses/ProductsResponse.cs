using Pacific.Web.Models.TableModels;
using System.Collections.Generic;

namespace Pacific.Web.Models.Responses
{
	public class ProductsResponse: IResponse
	{
		public IEnumerable<ProductTableView> Products { get; set; }
	}
}
