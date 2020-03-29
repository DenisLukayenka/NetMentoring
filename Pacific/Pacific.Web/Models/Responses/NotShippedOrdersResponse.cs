using Pacific.Web.Models.TableModels;
using System.Collections.Generic;

namespace Pacific.Web.Models.Responses
{
	public class NotShippedOrdersResponse: IResponse
	{
		public IEnumerable<NotShippedOrders> NotShippedOrders { get; set; }
	}
}
