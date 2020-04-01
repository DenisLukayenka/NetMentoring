namespace Pacific.Web.Models.TableModels
{
	public class ProductTableView
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public virtual string SupplierName { get; set; }
		public virtual string CategoryName { get; set; }
		public decimal Price { get; set; }
	}
}
