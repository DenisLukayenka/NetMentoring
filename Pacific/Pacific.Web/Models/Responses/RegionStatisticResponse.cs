using Pacific.ORM.HelpModels;
using System.Collections.Generic;

namespace Pacific.Web.Models.Responses
{
	public class RegionStatisticResponse: IResponse
	{
		public IEnumerable<RegionStatistic> RegionStatistics { get; set; }
	}
}
