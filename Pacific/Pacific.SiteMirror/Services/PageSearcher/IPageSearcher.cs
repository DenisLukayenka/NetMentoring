using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pacific.SiteMirror.Services.PageSearcher
{
	public interface IPageSearcher
	{
		Task<IEnumerable<string>> SearchLinksAsync(string source);
	}
}
