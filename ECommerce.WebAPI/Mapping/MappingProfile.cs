using AutoMapper;
using ECommerce.WebAPI.Dtos;
using ECommerce.WebAPI.Entities;

namespace ECommerce.WebAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDto, AppUser>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<BasketDto, Basket>().ReverseMap();
        }
    }
}
