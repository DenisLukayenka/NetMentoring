using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pacific.Web.Models;
using Pacific.Web.Models.Handlers;
using Pacific.Web.Models.Logger;
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
        private ILoggerManager _logger;

        public RequestController(IHandlerAsync handler, ILoggerManager logger)
        {
            this._handler = handler;
            this._logger = logger;
        }

        [HttpGet("GetSystemFiles")]
        public async Task<IResponse> Get([FromQuery] SystemVisitorRequest request)
        {
            this._logger.Info($"Request: GetSystemFiles, was found");
            return await this._handler.Execute(request);
        }

        [HttpGet("FetchDataFromDb")]
        public async Task<IResponse> Get([FromQuery] OrmRequestType requestType)
        {
            this._logger.Info($"Request: FetchDataFromDb, was found");
            return await this._handler.Execute(new OrmRequest { requestType = requestType });
        }

        [HttpPost("AddEmployee")]
        public async Task<IResponse> Post([FromBody] AddEmployeeRequest request)
        {
            this._logger.Info($"Request: AddEmployee, was found");
            return await this._handler.Execute(request);
        }

        [HttpPost("MoveProductsToCategory")]
        public async Task<IResponse> Post([FromBody] ProductsMoveToCategoryRequest request)
        {
            this._logger.Info($"Request: MoveProductsToCategory, was found");
            return await this._handler.Execute(request);
        }

        [HttpPost("AddProductsCollection")]
        public async Task<IResponse> Post([FromBody] AddProductsRequest request)
        {
            this._logger.Info($"Request: AddProductsCollection, was found");
            return await this._handler.Execute(request);
        }

        [HttpPost("GetSimilarProducts")]
        public async Task<IResponse> Post([FromBody] SimilarProductsRequest request) 
        {
            this._logger.Info($"Request: GetSimilarProducts, was found");
            return await this._handler.Execute(request);
        }

        [HttpPost("ReplaceProduct")]
        public async Task<IResponse> Post([FromBody] ReplaceProductRequest request)
        {
            this._logger.Info($"Request: ReplaceProduct, was found");
            return await this._handler.Execute(request);
        }

        [HttpPost("CopySite")]
        public async Task<IResponse> Post([FromBody] CopySiteRequest request)
        {
            this._logger.Info($"Request: CopySite, was found");
            return await this._handler.Execute(request);
        }
    }
}