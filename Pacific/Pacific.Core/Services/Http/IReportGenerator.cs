using Pacific.ORM.HelpModels;
using System.Threading.Tasks;

namespace Pacific.Core.Services.Http
{
	public interface IReportGenerator
	{
		Task<byte[]> GenerateReportAsync(ReportOptions options);
	}
}
