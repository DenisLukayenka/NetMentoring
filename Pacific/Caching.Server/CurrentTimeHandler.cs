using System;
using System.Web;

namespace Caching.Server
{
	public class CurrentTimeHandler : IHttpHandler
	{
		public bool IsReusable => true;

		public void ProcessRequest(HttpContext context)
		{
			context.Response.Write(DateTime.UtcNow.ToString("o"));
		}
	}
}