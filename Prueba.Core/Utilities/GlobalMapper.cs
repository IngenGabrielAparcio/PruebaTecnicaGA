using AutoMapper;
using Prueba.Core.DTOs;
using Prueba.Core.Entities;

namespace Prueba.Core.Utilities
{
    public class GlobalMapper : Profile
    {

        public GlobalMapper()
        {
            CreateMap<CatalogDto, Catalog>().ReverseMap(); 
            CreateMap<OrderDto, Order>().ReverseMap();            
            CreateMap<UserDto, Users>().ReverseMap();
            CreateMap<ProductsDto, Products>().ReverseMap();           
        }
    }
}
