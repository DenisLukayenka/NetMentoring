using Pacific.Core.Services.Http;
using System;

namespace Pacific.Core.ServiceResolvers
{
	public class Resolvers
	{
		public delegate IReportGenerator ReportGeneratorResolver(string key);
	}
}
