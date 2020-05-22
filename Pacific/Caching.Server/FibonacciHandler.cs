using Caching.Core.Extensions;
using System;
using System.Web;

namespace Caching.Server
{
	public class FibonacciHandler : IHttpHandler
	{
		public bool IsReusable => true;

		public void ProcessRequest(HttpContext context)
		{
			var depthStr = context.Request.Params["depth"];

			if(depthStr != null && int.TryParse(depthStr, out int depth))
			{
				var fibonacci = depth.FindFibonacci();
				context.Response.Write(fibonacci + " --- " + DateTime.UtcNow.ToString("o"));
			}
		}
	}
}