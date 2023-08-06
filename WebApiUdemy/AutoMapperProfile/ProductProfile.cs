using AutoMapper;
using WebApiUdemy.DTOs;
using WebApiUdemy.Model;

namespace WebApiUdemy.AutoMapperProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, CreateOrEditProductDTO>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
