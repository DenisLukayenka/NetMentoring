using AngleSharp.Html.Parser;
using NUnit.Framework;
using Pacific.SiteMirror.Services;
using Pacific.SiteMirror.Services.FileManager;
using Pacific.SiteMirror.Services.HttpServices;
using Pacific.SiteMirror.Services.PageSearcher;
using System.Threading.Tasks;

namespace Pacific.SiteMirrorTests
{
	public class Tests
	{
		private ISiteCopier _siteCopier;

		[SetUp]
		public void Setup()
		{
			this._siteCopier = new SiteCopier(new HttpClientService(), new FileManager(), new HtmlPageSearcher(new HtmlParser()));
		}

		[Test]
		public void Test1()
		{
			this._siteCopier.CopySiteAsync("http://www.bntu.by/", @"D:\Cash\MirrorTests").Wait();
		}

		[Test]
		public void Test2()
		{
			this._siteCopier.CopySiteAsync("http://www.bntu.by/", @"D:\Cash\MirrorTests", depth: 2).Wait();
		}
	}
}