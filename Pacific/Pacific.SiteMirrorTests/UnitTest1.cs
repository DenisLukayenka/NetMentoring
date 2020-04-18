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
			this._siteCopier.CopySiteAsync("https://docs.microsoft.com/en-us/dotnet/api/system.web.httputility.urldecode?view=netframework-4.8", @"D:\Cash\MirrorTests\1", depth: 1).Wait();
		}

		[Test]
		public void Test3()
		{
			this._siteCopier.CopySiteAsync(
				"https://docs.microsoft.com/en-us/dotnet/api/system.web.httputility.urldecode?view=netframework-4.8", @"D:\Cash\MirrorTests\2"
				, SiteMirror.Models.DomainRestriction.NoRestriction, 
				1).Wait();
		}

		[Test]
		public void Test4()
		{
			this._siteCopier.CopySiteAsync(
				"https://docs.microsoft.com/en-us/dotnet/api/system.web.httputility.urldecode?view=netframework-4.8", @"D:\Cash\MirrorTests\3"
				, SiteMirror.Models.DomainRestriction.NotHigherCurrentDomain,
				1).Wait();
		}

		[Test]
		public void Test5()
		{
			this._siteCopier.CopySiteAsync(
				"https://docs.microsoft.com/en-us/", @"D:\Cash\MirrorTests\4"
				, SiteMirror.Models.DomainRestriction.NotHigherCurrentDomain,
				3).Wait();
		}
	}
}