using Caching.Core.CacheManagers.Fibonacci;
using System.Web;

namespace Caching.Server
{
	public class FibonacciMemoryCacheHandler: IHttpHandler
	{
		private readonly FibonacciCacheManager _cacheManager;

		public FibonacciMemoryCacheHandler()
		{
			this._cacheManager = new FibonacciCacheManager();
		}

		public bool IsReusable => true;

		public void ProcessRequest(HttpContext context)
		{
			var depthStr = context.Request.Params["depth"];

			if (depthStr != null && int.TryParse(depthStr, out int depth))
			{
				var fibonacci = this._cacheManager.FindFibonacciRec(depth);
				context.Response.Write(fibonacci);
			}
		}
	}
}