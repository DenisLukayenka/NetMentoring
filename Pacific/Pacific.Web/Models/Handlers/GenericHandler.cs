using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pacific.Core.Services;
using Pacific.Core.Services.Orm;
using Pacific.ORM.Models;
using Pacific.Web.Models.Requests;
using Pacific.Web.Models.Responses;

namespace Pacific.Web.Models.Handlers
{
    public class GenericHandler: IHandlerAsync
    {
        private FileSystemVisitor _visitor;
        private OrmService _ormService;

        public GenericHandler(OrmService ormService)
        {
            this._ormService = ormService;
        }

        public async Task<IResponse> Execute(IRequest request)
        {
            switch (request)
            {
                case SystemVisitorRequest c:
                    return this.Execute(c);

                case OrmRequest r:
                    return await this.Execute(r);
            }

            throw new ArgumentException("Request type is incorrect");
        }

        protected SystemVisitorResponse Execute(SystemVisitorRequest request)
        {
            this._visitor = new FileSystemVisitor(request.FolderPath);

            return new SystemVisitorResponse()
            {
                Files = this._visitor.Explore().ToArray()
            };
        }

        protected async Task<ProductsResponse> Execute(OrmRequest request)
        {
            switch (request.requestType)
            {
                case OrmRequestType.SelectProductsWithCategoryAndSuppliers:
                    return new ProductsResponse { Products = await this._ormService.SelectProductsAsync() };
            }

            throw new ArgumentException("Request type is incorrect");
        }
    }
}