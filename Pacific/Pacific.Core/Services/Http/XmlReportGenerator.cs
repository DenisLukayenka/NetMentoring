using Pacific.Core.Services.Orm;
using Pacific.ORM.HelpModels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pacific.Core.Services.Http
{
	public class XmlReportGenerator : IReportGenerator
	{
		private OrmService _ormService;

		public XmlReportGenerator(OrmService ormService)
		{
			this._ormService = ormService;
		}

		public async Task<byte[]> GenerateReportAsync(ReportOptions options)
		{
			var orders = await this._ormService.SelectOrdersAsync(options);

			XElement ordersXml = new XElement("Orders");
			foreach(var order in orders)
			{
				ordersXml.Add(
					new XElement("Order",
						new XElement("OrderID", order.Id),
						new XElement("CustomerID", order.CustomerId),
						new XElement("OrderDate", order.OrderDate),
						new XElement("ShipName", order.ShipName),
						new XElement("ShipCountry", order.ShipCountry),
						new XElement("ShipCity", order.ShipCity),
						new XElement("ShipRegion", order.ShipRegion),
						new XElement("ShipAddress", order.ShipAddress)));
			}

			return Encoding.UTF8.GetBytes(ordersXml.ToString());
		}
	}
}
