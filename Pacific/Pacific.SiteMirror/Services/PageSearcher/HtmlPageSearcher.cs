using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.IO;
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

			return this.SearchStylesheetsLinks(domDocument)
				.Concat(this.SearchTagLinks(domDocument))
				.Concat(this.SearchImageLinks(domDocument));
		}

		protected virtual IEnumerable<string> SearchTagLinks(IHtmlDocument document)
		{
			return document.QuerySelectorAll("a").Select(el => el.GetAttribute("href"));
		}

		protected virtual IEnumerable<string> SearchStylesheetsLinks(IHtmlDocument document)
		{
			return document.QuerySelectorAll("link")
				.Select(el => el.GetAttribute("href"))
				.Select(UpdateStylesheetLink);
		}

		protected virtual IEnumerable<string> SearchImageLinks(IHtmlDocument document)
		{
			return document.QuerySelectorAll("img")
				.Select(el => el.GetAttribute("src"))
				.Select(UpdateImagesLink);
		}

		private string UpdateStylesheetLink(string link)
		{
			if (!Path.HasExtension(link))
			{
				return link + ".css";
			}

			return link;
		}

		private string UpdateImagesLink(string link)
		{
			if (!Path.HasExtension(link))
			{
				return link + ".jpg";
			}

			return link;
		}
	}
}
