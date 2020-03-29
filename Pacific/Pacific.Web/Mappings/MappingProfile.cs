using AutoMapper;
using Pacific.ORM.Models;
using Pacific.Web.Models.TableModels;

namespace Pacific.Web.Mappings
{
	public class MappingProfile: Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductViewModel>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
				.ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.CompanyName))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice));

			CreateMap<Employee, EmployeeViewModel>()
				.ForMember(dest => dest.ReportsToName, opt => opt.MapFrom(src => 
					src.ReportsTo != null ? src.ReportsTo.FirstName : ""));

			CreateMap<PostEmployeeViewModel, Employee>();

			CreateMap<Category, CategoryViewModel>();
		}
	}
}
