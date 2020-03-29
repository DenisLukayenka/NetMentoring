namespace Pacific.Web.Models.TableModels
{
	public class NotShippedOrders
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }

		public string Name { get; set; }
		public string SupplierName { get; set; }

		public string CategoryName { get; set; }
	}
}
