using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Pacific.Core.EventSources;
using Pacific.Core.Services;
using Pacific.Core.Services.Orm;
using Pacific.ORM.Models;
using Pacific.SiteMirror.Services;
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
        private readonly ISiteCopier _siteCopier;

        public GenericHandler(OrmService ormService, IMapper mapper, ISiteCopier siteCopier)
        {
            this._ormService = ormService;
            this._mapper = mapper;
            this._siteCopier = siteCopier;
        }

        public async Task<IResponse> Execute(IRequest request)
        {
            RequestEventSource.Instance.AddRequest();

            switch (request)
            {
                case SystemVisitorRequest c:
                    return this.Execute(c);
                case OrmRequest r:
                    return await this.MetricExecuteWrapper(r);
                case AddEmployeeRequest e:
                    return await this.Execute(e);
                case ProductsMoveToCategoryRequest r:
                    return await this.Execute(r);
                case AddProductsRequest r:
                    return await this.Execute(r);
                case SimilarProductsRequest r:
                    return await this.Execute(r);
                case ReplaceProductRequest r:
                    return await this.Execute(r);
                case CopySiteRequest r:
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

        protected async Task<StatusResponse> Execute(AddEmployeeRequest request)
        {
            return new StatusResponse
            {
                IsSuccess = await this._ormService.AddEmployeeToDbAsync(this._mapper.Map<Employee>(request.Employee)) 
            };
        }

        protected async Task<StatusResponse> Execute(ProductsMoveToCategoryRequest request)
        {
            return new StatusResponse
            {
                IsSuccess = await this._ormService.MoveProductsToCategoryAsync(request.ProductsIds, request.CategoryId)
            };
        }

        protected async Task<StatusResponse> Execute(AddProductsRequest request)
        {
            return new StatusResponse
            {
                IsSuccess = await this._ormService.AddProductsAsync(this._mapper.Map<IEnumerable<Product>>(request.Products))
            };
        }

        protected async Task<ProductsResponse> Execute(SimilarProductsRequest request)
        {
            return new ProductsResponse
            {
                Products = this._mapper.Map<IEnumerable<ProductTableView>>(
                    await this._ormService.SelectSimililarProducts(request.ProductId))
            };
        }

        protected async Task<StatusResponse> Execute(ReplaceProductRequest request)
        {
            return new StatusResponse
            {
                IsSuccess = await this._ormService
                    .ReplaceProductInOrder(request.OriginProductId, request.OriginOrderId, request.TargetProductId)
            };
        }

        protected async Task<StatusResponse> Execute(CopySiteRequest request)
        {
            await this._siteCopier
                    .CopySiteAsync(request.Link, request.TargetPath, request.DomainRestriction, request.Depth);

            return new StatusResponse
            {
                IsSuccess = true
            };
        }

        protected async Task<IResponse> Execute(OrmRequest request)
        {
            switch (request.requestType)
            {
                case OrmRequestType.SelectProductsWithCategoryAndSuppliers:
                    return new ProductsResponse
                    {
                        Products = this._mapper.Map<IEnumerable<ProductTableView>>(
                            await this._ormService.SelectProductsAsync())
                    };
                case OrmRequestType.SelectEmployees:
                    return new EmployeesResponse 
                    { 
                        Employees = this._mapper.Map<IEnumerable<EmployeeTableView>>(
                            await this._ormService.SelectEmployeesAsync())
                    };
                case OrmRequestType.SelectRegionStatistic:
                    return new RegionStatisticResponse
                    {
                        RegionStatistics = await this._ormService.SelectRegionStatisticAsync()
                    };
                case OrmRequestType.SelectEmployeeShippers:
                    return new EmployeeShipperResponse
                    {
                        EmployeeShippers = await this._ormService.SelectEmployeeShippersAsync()
                    };
                case OrmRequestType.SelectCategories:
                    return new ListCategoriesResponse
                    {
                        Categories = this._mapper.Map<IEnumerable<CategoryTableView>>(
                            await this._ormService.FetchAllCategoriesAsync())
                    };
                case OrmRequestType.NotShippedProducts:
                    return new NotShippedOrdersResponse
                    {
                        NotShippedOrders = this._mapper.Map<IEnumerable<NotShippedOrders>>(
                            await this._ormService.SelectNotShippedOrders())
                    };
            }

            throw new ArgumentException("Request type is incorrect");
        }

        private async Task<IResponse> MetricExecuteWrapper(OrmRequest request)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                return await this.Execute(request);
            }
            finally
            {
                RequestEventSource.Instance.AddRequestToDb(sw.ElapsedMilliseconds);
                sw.Stop();
            }
        }
    }
}