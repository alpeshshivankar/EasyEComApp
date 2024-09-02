using AutoMapper;
using ECom.Application.Features.CategoryFeatures.Commands;
using ECom.Application.Features.OrderFeatures.Commands;
using ECom.Application.Features.ProductFeatures.Commands;
using ECom.Domain.Entities;

namespace ECom.Application.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<CreateOrderCommand, Order>().ReverseMap();
            CreateMap<CreateProductCommand, Product>().ReverseMap();
        }
    }
}