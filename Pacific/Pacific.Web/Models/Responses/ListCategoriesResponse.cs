using Pacific.Web.Models.TableModels;
using System.Collections.Generic;

namespace Pacific.Web.Models.Responses
{
	public class ListCategoriesResponse: IResponse
	{
		public IEnumerable<CategoryViewModel> Categories { get; set; }
	}
}
