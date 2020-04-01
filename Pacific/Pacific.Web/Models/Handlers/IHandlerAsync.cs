using Pacific.Web.Models.Requests;
using Pacific.Web.Models.Responses;
using System.Threading.Tasks;

namespace Pacific.Web.Models.Handlers
{
    public interface IHandlerAsync
    {
        Task<IResponse> Execute(IRequest request);
    }
}