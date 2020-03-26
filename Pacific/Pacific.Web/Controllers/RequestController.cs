using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pacific.ORM.Models;
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
        private IHandlerAsync _handler;

        public RequestController(IHandlerAsync handler)
        {
            this._handler = handler;
        }

        [HttpGet("GetSystemFiles")]
		public async Task<IResponse> Get([FromQuery] SystemVisitorRequest request)
		{
			return await this._handler.Execute(request);
		}

        [HttpGet("GetProducts")]
        public async Task<IResponse> Get([FromQuery] OrmRequest request)
        {
            return await this._handler.Execute(request);
        }
    }
}