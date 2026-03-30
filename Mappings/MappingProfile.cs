using AutoMapper;
using InventarioAPI.DTOs;
using InventarioAPI.Models;

namespace InventarioAPI.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {


            CreateMap<Usuario, AuthResponseDto>();

            CreateMap<Categoria, CategoriaDto>();

            CreateMap<CategoriaDto, Categoria>();

            CreateMap<Producto, ProductoDto>().ForMember(dest => dest.NombreCategoria, opt => opt.MapFrom(src => src.Categoria.Nombre));

            CreateMap<CrearProductoDto, Producto>().ForMember(dest => dest.FechaCreacion, opt => opt.Ignore());

            CreateMap<MovimientoInventario, MovimientoDto>().ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.Producto.Nombre));
        

            CreateMap<CrearMovimientoDto, MovimientoInventario>().ForMember(dest => dest.UsuarioId, opt => opt.Ignore());
        }
    }
}
