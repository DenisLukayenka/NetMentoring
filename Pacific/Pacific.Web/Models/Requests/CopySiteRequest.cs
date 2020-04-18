using Pacific.SiteMirror.Models;

namespace Pacific.Web.Models.Requests
{
	public class CopySiteRequest: IRequest
	{
		public string Link { get; set; }
		public string TargetPath { get; set; }

		public int Depth { get; set; }
		public DomainRestriction DomainRestriction { get; set; }
	}
}
