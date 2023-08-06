using AutoMapper;
using WebApiUdemy.DTOs;
using WebApiUdemy.Model;

namespace WebApiUdemy.AutoMapperProfile
{
    public class CatergoryProfile : Profile
    {
        public CatergoryProfile()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
        }
    }
}
