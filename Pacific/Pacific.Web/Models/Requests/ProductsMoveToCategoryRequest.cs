using System.Collections.Generic;

namespace Pacific.Web.Models.Requests
{
	public class ProductsMoveToCategoryRequest: IRequest
	{
		public IEnumerable<int> ProductsIds { get; set; }
		public int CategoryId { get; set; }
	}
}
