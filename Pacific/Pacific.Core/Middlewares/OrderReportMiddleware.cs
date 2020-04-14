using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Pacific.Core.Factories.ReportOrder;
using Pacific.ORM.HelpModels;
using System;
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
				var generator = this._factory.Create(context.Request.Headers["Accept"]);
				ReportOptions options;

				if(context.Request.ContentType == "application/x-www-form-urlencoded")
				{
					options = this._factory.BuildOptions(
						context.Request.Form["customerId"],
						context.Request.Form["dateFrom"],
						context.Request.Form["dateTo"],
						context.Request.Form["take"],
						context.Request.Form["skip"]);
				}
				else
				{
					var customerId = context.Request.Query["customerId"];
					var dateFrom = context.Request.Query["dateFrom"];
					var dateTo = context.Request.Query["dateTo"];
					var take = context.Request.Query["take"];
					var skip = context.Request.Query["skip"];

					options = this._factory.BuildOptions(customerId, dateFrom, dateTo, take, skip);
				}

				var result = await generator.GenerateReportAsync(options);

				this.UpdateResponseContentType(options, context);
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

		protected virtual void UpdateResponseContentType(ReportOptions options, HttpContext context)
		{
			var acceptHeader = context.Request.Headers["Accept"].ToString();

			if(acceptHeader != null && (acceptHeader.Contains("text/xml") || acceptHeader.Contains("application/xml")))
			{
				context.Response.ContentType = "text/xml";
			}
			else
			{
				context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				context.Response.Headers.Add("Content-Transfer-Encoding", "binary");
				context.Response.Headers.Add("Content-disposition", "attachment; filename=Report.xlsx");
			}
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
