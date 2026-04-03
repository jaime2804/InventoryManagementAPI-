using AutoMapper;
using InventarioAPI.DTOs;
using InventarioAPI.Models;

namespace InventarioAPI.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {


            CreateMap<User, AuthResponseDto>();

            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateProductDto, Product>().ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<InventoryMovement, MovementDto>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        

            CreateMap<CreateMovementDto, InventoryMovement>().ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
}
