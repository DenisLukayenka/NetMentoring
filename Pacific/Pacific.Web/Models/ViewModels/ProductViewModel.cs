namespace Pacific.Web.Models.ViewModels
{
	public class ProductViewModel
	{
		public string Name { get; set; }
		public string SupplierName { get; set; }
		public string CategoryName { get; set; }
		public int QuantityPerUnit { get; set; }
		public int UnitPrice { get; set; }
		public int UnitsInStock { get; set; }
		public int UnitsOnOrder { get; set; }
		public int ReorderLevel { get; set; }
		public bool Discontinued { get; set; }
	}
}
