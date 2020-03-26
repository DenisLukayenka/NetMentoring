using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Pacific.Core.Services;
using Pacific.Core.Services.Orm;
using Pacific.ORM.Models;
using Pacific.Web.Models.Requests;
using Pacific.Web.Models.Responses;
using Pacific.Web.Models.TableModels;

namespace Pacific.Web.Models.Handlers
{
    public class GenericHandler: IHandlerAsync
    {
        private FileSystemVisitor _visitor;
        private readonly OrmService _ormService;
        private readonly IMapper _mapper;

        public GenericHandler(OrmService ormService, IMapper mapper)
        {
            this._ormService = ormService;
            this._mapper = mapper;
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

        protected async Task<IResponse> Execute(OrmRequest request)
        {
            switch (request.requestType)
            {
                case OrmRequestType.SelectProductsWithCategoryAndSuppliers:
                    return new ProductsResponse
                    {
                        Products = this._mapper.Map<IEnumerable<ProductViewModel>>(
                            await this._ormService.SelectProductsAsync())
                    };
                case OrmRequestType.SelectEmployees:
                    return new EmployeesResponse 
                    { 
                        Employees = this._mapper.Map<IEnumerable<EmployeeViewModel>>(
                            await this._ormService.SelectEmployeesAsync())
                    };
                case OrmRequestType.SelectRegionStatistic:
                    return new RegionStatisticResponse
                    {
                        RegionStatistics = await this._ormService.SelectRegionStatisticAsync()
                    };
            }

            throw new ArgumentException("Request type is incorrect");
        }
    }
}