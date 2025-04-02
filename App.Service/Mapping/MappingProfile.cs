using App.Repository.Products;
using App.Service.Products;
using AutoMapper;

namespace App.Service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<Product, ProductDto>().ReverseMap();
    }
}