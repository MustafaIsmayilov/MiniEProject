using Microsoft.EntityFrameworkCore;
using System.Net;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.CategoryDtos;
using MiniEProject.Application.Shared.Responses;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Persistence.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<string>> AddAsync(CategoryCreateDto dto)
    {
        var categoryDb = await _categoryRepository.GetByIdFiltered(
            c => c.Name.Trim().ToLower() == dto.Name.Trim().ToLower())
            .FirstOrDefaultAsync();

        if (categoryDb is not null)
            return new BaseResponse<string>("This category already exists", HttpStatusCode.BadRequest);

        var category = new Category
        {
            Name = dto.Name
        };

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangeAsync();

        return new BaseResponse<string>("Category successfully added", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            return new BaseResponse<string>("Category is not found.", HttpStatusCode.NotFound);

        _categoryRepository.Delete(category);
        await _categoryRepository.SaveChangeAsync();

        return new BaseResponse<string>("Category has been deleted successfully.", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryGetDto>>> GetAllAsync()
    {
        var categories = _categoryRepository.GetAll();
        if (categories == null || !categories.Any())
            return new BaseResponse<List<CategoryGetDto>>("No categories found.", HttpStatusCode.NotFound);

        var dtoList = categories.Select(category => new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name
        }).ToList();

        return new BaseResponse<List<CategoryGetDto>>("All categories retrieved", dtoList, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<CategoryGetDto>> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            return new BaseResponse<CategoryGetDto>("Category not found.", HttpStatusCode.NotFound);

        var dtoCategory = new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name
        };
        return new BaseResponse<CategoryGetDto>("Category retrieved successfully", dtoCategory, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<CategoryGetDto>> GetByNameAsync(string search)
    {
        var categories = _categoryRepository.GetAll();
        var category = categories.FirstOrDefault(c => c.Name.Equals(search, StringComparison.OrdinalIgnoreCase));

        if (category == null)
            return new BaseResponse<CategoryGetDto>("Category not found.", HttpStatusCode.NotFound);

        var dtoCategory = new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name
        };

        return new BaseResponse<CategoryGetDto>("Category found", dtoCategory, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryGetDto>>> GetAllNameAsync(string namePart)
    {
        var categories = _categoryRepository.GetAll();
        var filteredCategories = categories
            .Where(c => c.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase))
            .Select(c => new CategoryGetDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

        if (!filteredCategories.Any())
            return new BaseResponse<List<CategoryGetDto>>("No matching categories found.", HttpStatusCode.NotFound);

        return new BaseResponse<List<CategoryGetDto>>("Matching categories retrieved", filteredCategories, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.Id);
        if (category is null)
            return new BaseResponse<CategoryUpdateDto>("Category not found.", HttpStatusCode.NotFound);

        var existedCategory = await _categoryRepository.GetByIdFiltered(
            c => c.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && c.Id != dto.Id)
            .FirstOrDefaultAsync();

        if (existedCategory is not null)
            return new BaseResponse<CategoryUpdateDto>("This category name is already taken.", HttpStatusCode.BadRequest);

        category.Name = dto.Name;
        

        await _categoryRepository.SaveChangeAsync();

        return new BaseResponse<CategoryUpdateDto>("Category successfully updated.", dto, HttpStatusCode.OK);
    }
}

