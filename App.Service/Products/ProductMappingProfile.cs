using App.Repository.Products;
using App.Service.Products;
using App.Service.Products.Create;
using App.Service.Products.Dto;
using App.Service.Products.Update;
using AutoMapper;

namespace App.Service.Products;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
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