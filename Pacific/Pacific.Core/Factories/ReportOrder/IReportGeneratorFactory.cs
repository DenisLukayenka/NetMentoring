using Microsoft.AspNetCore.Http;
using Pacific.Core.Services.Http;
using Pacific.ORM.HelpModels;
using System.Collections.Generic;

namespace Pacific.Core.Factories.ReportOrder
{
	public interface IReportGeneratorFactory
	{
		IReportGenerator Create(string acceptHeader);

		ReportOptions BuildOptions(string customerId, string dateFrom, string dateTo, string take, string skip);
	}
}
