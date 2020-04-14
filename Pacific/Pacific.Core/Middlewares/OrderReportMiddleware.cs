using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Pacific.Core.Factories.ReportOrder;
using System.Threading.Tasks;

namespace Pacific.Core.Middlewares
{
	public class OrderReportMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IReportGeneratorFactory _factory;

		public OrderReportMiddleware(RequestDelegate next, IReportGeneratorFactory factory)
		{
			this._next = next;
			this._factory = factory;
		}

		public async Task Invoke(HttpContext context)
		{
			if (ValidateRequest(context.Request))
			{
				var customerId = context.Request.Query["customerId"];
				var dateFrom = context.Request.Query["dateFrom"];
				var dateTo = context.Request.Query["dateTo"];
				var take = context.Request.Query["take"];
				var skip = context.Request.Query["skip"];

				var generator = this._factory.Create(context.Request.Headers["Accept"]);
				var options = this._factory.BuildOptions(customerId, dateFrom, dateTo, take, skip);

				var result = await generator.GenerateReportAsync(options);

				/*context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";*/
				await context.Response.Body.WriteAsync(result, 0, result.Length);
			}
			else
			{
				await this._next.Invoke(context);
			}
		}

		protected virtual bool ValidateRequest(HttpRequest request)
		{
			return request.Path.Value.EndsWith("Report");
		}
	}

	public static class OrderReportMiddlewareExtensions
	{
		public static IApplicationBuilder UseOrderReportMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<OrderReportMiddleware>();
		}
	}
}
