using AutoMapper;
using ECom.Application.Models;
using ECom.Domain.Entities;
using ECom.Infrastructure.Persistance.DataModels;

namespace ECom.Application.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryEntity>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}