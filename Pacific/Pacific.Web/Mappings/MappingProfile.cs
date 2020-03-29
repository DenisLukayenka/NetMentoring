using AutoMapper;
using Pacific.ORM.Models;
using Pacific.Web.Models.TableModels;
using Pacific.Web.Models.ViewModels;

namespace Pacific.Web.Mappings
{
	public class MappingProfile: Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductTableView>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
				.ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.CompanyName))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice));

			CreateMap<Employee, EmployeeTableView>()
				.ForMember(dest => dest.ReportsToName, opt => opt.MapFrom(src => 
					src.ReportsTo != null ? src.ReportsTo.FirstName : ""));

			CreateMap<PostEmployeeViewModel, Employee>();

			CreateMap<Category, CategoryTableView>();

			CreateMap<ProductViewModel, Product>()
				.ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => new Supplier { CompanyName = src.SupplierName }))
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category { Name = src.CategoryName }));
		}
	}
}
