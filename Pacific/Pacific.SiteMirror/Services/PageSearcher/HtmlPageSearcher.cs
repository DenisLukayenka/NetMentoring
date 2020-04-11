using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pacific.SiteMirror.Services.PageSearcher
{
	public class HtmlPageSearcher : IPageSearcher
	{
		private IHtmlParser _htmlParser;

		public HtmlPageSearcher(IHtmlParser parser)
		{
			this._htmlParser = parser;
		}

		public async Task<IEnumerable<string>> SearchLinksAsync(string source)
		{
			var domDocument = await this._htmlParser.ParseDocumentAsync(source);

			return domDocument.QuerySelectorAll("a").Select(el => el.GetAttribute("href"));
		}
	}
}
