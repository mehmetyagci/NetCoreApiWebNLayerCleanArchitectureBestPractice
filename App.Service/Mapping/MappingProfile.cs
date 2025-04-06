using App.Repository.Products;
using App.Service.Products;
using App.Service.Products.Create;
using App.Service.Products.Update;
using AutoMapper;

namespace App.Service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        
        CreateMap<CreateProductRequest, Product>().ForMember(
                dect => dect.Name,
                opt => opt.MapFrom(src => 
                    src.Name.ToLowerInvariant()));
        
        CreateMap<UpdateProductRequest, Product>().ForMember(
            dect => dect.Name,
            opt => opt.MapFrom(src => 
                src.Name.ToLowerInvariant()));
    }
}