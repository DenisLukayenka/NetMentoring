using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caching.Server.Tests
{
	[TestClass]
	public class FibonacciTests
	{
		private string ServerBaseAddress = "http://localhost/ServerTest/";

		[TestInitialize]
		public void Init()
		{
			var uriBuilder = new UriBuilder(ServerBaseAddress);
			if (uriBuilder.Host == "localhost")
				uriBuilder.Host = Dns.GetHostName();

			ServerBaseAddress = uriBuilder.ToString();
		}

		[TestMethod]
		public void NoCache()
		{
			var uri = new Uri(new Uri(ServerBaseAddress), "fibonacci");

			using(var client = new HttpClient(new HttpClientHandler() { Proxy = new WebProxy() }))
			{
				this.ReadFibonacciTestRun(client, uri);
			}
		}

		[TestMethod]
		public void ServerCache()
		{
			var uri = new Uri(new Uri(ServerBaseAddress), "fibonacci.cached");

			using (var client = new HttpClient(new HttpClientHandler()))
			{
				this.ReadFibonacciTestRun(client, uri);
			}
		}

		[TestMethod]
		public void ClientMemoryCache()
		{
			var uri = new Uri(new Uri(ServerBaseAddress), "memory/fibonacci");

			using (var client = new HttpClient(new HttpClientHandler()))
			{
				this.ReadFibonacciTestRun(client, uri);
			}
		}

		private void ReadFibonacciTestRun(HttpClient client, Uri uri)
		{
			for (int i = 0; i < 10; i++)
			{
				var uriStr = uri.ToString() + "?depth=" + i;

				var result = client.GetStringAsync(uriStr).Result;
				Console.WriteLine(result);

				Thread.Sleep(1000);
			}
		}
	}
}
