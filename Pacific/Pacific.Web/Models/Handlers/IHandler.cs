using Pacific.Web.Models.Requests;
using Pacific.Web.Models.Responses;

namespace Pacific.Web.Models.Handlers
{
    public interface IHandler
    {
        IResponse Execute(IRequest request);
    }
}