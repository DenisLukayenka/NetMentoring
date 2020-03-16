using System.Net.Mime;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pacific.Web.Models.Handlers;
using Pacific.Web.Models.Requests;
using Pacific.Web.Models.Responses;

namespace Pacific.Web.Controllers
{
    [ApiController]
    [Route("/api/request")]
	[EnableCors("AllowAll")]
	[Produces(MediaTypeNames.Application.Json)]
    public class RequestController
    {
        private IHandler _handler;

        public RequestController(IHandler handler)
        {
            this._handler = handler;
        }

        [HttpGet("GetSystemFiles")]
		public SystemVisitorResponse Get([FromQuery] SystemVisitorRequest request)
		{
			return this._handler.Execute(request) as SystemVisitorResponse;
		}
    }
}