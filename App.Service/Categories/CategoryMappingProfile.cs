using App.Repository.Categories;
using App.Service.Categories.Create;
using App.Service.Categories.Dto;
using App.Service.Categories.Update;
using AutoMapper;

namespace App.Service.Categories;

public class CategoryMappingProfile :Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        
        CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
        
        CreateMap<CreateCategoryRequest, Category>().ForMember(
            dect => dect.Name,
            opt => opt.MapFrom(src => 
                src.Name.ToLowerInvariant()));
        
        CreateMap<UpdateCategoryRequest, Category>().ForMember(
            dect => dect.Name,
            opt => opt.MapFrom(src => 
                src.Name.ToLowerInvariant()));
    }
}