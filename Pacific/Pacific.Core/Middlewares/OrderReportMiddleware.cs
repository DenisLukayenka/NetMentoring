using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Pacific.Core.Services.Excel;
using Pacific.Core.Services.Orm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pacific.Core.Middlewares
{
	public class OrderReportMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ExcelService _excelService;
		private readonly OrmService _ormService;

		public OrderReportMiddleware(RequestDelegate next, ExcelService excelService, OrmService ormService)
		{
			this._next = next;
			this._excelService = excelService;
			this._ormService = ormService;
		}

		public async Task Invoke(HttpContext context)
		{
			var bytes = await this._excelService.CreateExcelDocument(await this._ormService.SelectOrders());

			context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);

			//await this._next.Invoke(context);
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
