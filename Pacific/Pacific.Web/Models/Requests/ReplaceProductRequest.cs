namespace Pacific.Web.Models.Requests
{
	public class ReplaceProductRequest: IRequest
	{
		public int OriginProductId { get; set; }
		public int OriginOrderId { get; set; }
		public int TargetProductId { get; set; }
	}
}
