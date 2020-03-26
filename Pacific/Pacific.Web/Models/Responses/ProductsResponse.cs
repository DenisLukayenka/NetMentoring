using Pacific.ORM.Models;
using System.Collections.Generic;

namespace Pacific.Web.Models.Responses
{
	public class ProductsResponse: IResponse
	{
		public IEnumerable<Product> Products { get; set; }
	}
}
