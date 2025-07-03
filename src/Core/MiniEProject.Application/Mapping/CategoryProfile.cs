using AutoMapper;
using MiniEProject.Application.DTOs.CategoryDtos;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Application.Mapping;

public class CategoryProfile:Profile
{
    public CategoryProfile()
    {
        // Category -> CategoryDeleteDto (sadece Id maplansin)
        CreateMap<Category, CategoryDeleteDto>();

        // CategoryMainCreateDto -> Category
        CreateMap<CategoryMainCreateDto, Category>()
            .ForMember(dest => dest.ParentCategoryId, opt => opt.Ignore())  // ParentCategoryId burada yoxdu, ignore edirik
            .ForMember(dest => dest.SubCategories, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        // Category -> CategoryMainGetDto (SubCategories maplansin)
        CreateMap<Category, CategoryMainGetDto>()
            .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));

        // CategorySubCreateDto -> Category
        CreateMap<CategorySubCreateDto, Category>()
            .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId ?? Guid.Empty)) // null ola bilər, ehtiyac varsa idarə et
            .ForMember(dest => dest.SubCategories, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        // Category -> CategorySubGetDto
        CreateMap<Category, CategorySubGetDto>();

        // CategoryUpdateDto -> Category
        CreateMap<CategoryUpdateDto, Category>()
            .ForMember(dest => dest.SubCategories, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());
    }
}
