using Pacific.Core.Services.Http;
using Pacific.ORM.HelpModels;
using System;
using static Pacific.Core.ServiceResolvers.Resolvers;

namespace Pacific.Core.Factories.ReportOrder
{
	public class ReporGeneratorFactory : IReportGeneratorFactory
	{
		private ReportGeneratorResolver _resolver;

		public ReporGeneratorFactory(ReportGeneratorResolver resolver)
		{
			this._resolver = resolver;
		}

		public virtual IReportGenerator Create(string acceptHeader)
		{
			if(acceptHeader.Contains("text/xml") || acceptHeader.Contains("application/xml"))
			{
				return this._resolver("xml");
			}

			return this._resolver("xlsx");
		}

		public virtual ReportOptions BuildOptions(string customerId, string dateFrom, string dateTo, string take, string skip)
		{
			ReportOptions options = new ReportOptions()
			{
				CustomerId = customerId,
			};

			if(DateTime.TryParse(dateFrom, out var dateFromDate))
			{
				options.DateFrom = dateFromDate;
			}

			if (DateTime.TryParse(dateTo, out var dateToDate))
			{
				options.DateTo = dateToDate;
			}
			if (int.TryParse(take, out var takeInt))
			{
				options.Take = takeInt;
			}
			if (int.TryParse(skip, out var skipInt))
			{
				options.Skip = skipInt;
			}

			return options;
		}
	}
}
