using Microsoft.EntityFrameworkCore;
using System.Net;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.CategoryDtos;
using MiniEProject.Application.Shared.Responses;
using MiniEProject.Domain.Entities;
using AutoMapper;

namespace MiniEProject.Persistence.Services;


public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<string>> AddSubCategoryAsync(CategorySubCreateDto dto)
    {
        if (dto.ParentCategoryId == null || dto.ParentCategoryId == Guid.Empty)
            return new BaseResponse<string>("Subcategory must have a valid parent category", HttpStatusCode.BadRequest);

        var parentExists = await _categoryRepository
            .GetByFiltered(c => c.Id == dto.ParentCategoryId)
            .AnyAsync();

        if (!parentExists)
            return new BaseResponse<string>("Parent category not found", HttpStatusCode.BadRequest);

        var exists = await _categoryRepository
            .GetByFiltered(c => c.Name.Trim().ToLower() == dto.Name.Trim().ToLower() &&
                                c.ParentCategoryId == dto.ParentCategoryId)
            .AnyAsync();

        if (exists)
            return new BaseResponse<string>("This subcategory already exists under the specified parent", HttpStatusCode.BadRequest);

        var category = _mapper.Map<Category>(dto);
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangeAsync();

        return new BaseResponse<string>("Subcategory created", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> AddMainCategoryAsync(CategoryMainCreateDto dto)
    {
        var exists = await _categoryRepository
            .GetByFiltered(c => c.Name.Trim().ToLower() == dto.Name.Trim().ToLower())
            .AnyAsync();

        if (exists)
            return new BaseResponse<string>("This main category already exists", HttpStatusCode.BadRequest);

        var category = _mapper.Map<Category>(dto);
        category.ParentCategoryId = Guid.Empty;
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangeAsync();

        return new BaseResponse<string>("Main category created", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<object>> DeleteAsync(Guid id)
    {
        var categoryDb = await _categoryRepository.GetByIdAsync(id);
        if (categoryDb == null)
            return new BaseResponse<object>("Id not found", HttpStatusCode.NotFound);

        _categoryRepository.Delete(categoryDb);
        await _categoryRepository.SaveChangeAsync();

        return new BaseResponse<object>("Category is deleted", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryMainGetDto>>> GetAllAsync()
    {
        var categories = await _categoryRepository
            .GetAll()
            .Where(c => c.ParentCategoryId == Guid.Empty)  
            .Include(c => c.SubCategories)
            .ToListAsync();

        var dtos = _mapper.Map<List<CategoryMainGetDto>>(categories);

        return new BaseResponse<List<CategoryMainGetDto>>("Success", dtos, HttpStatusCode.OK);
    }

    
    public async Task<BaseResponse<CategoryUpdateDto>> UpdateAsync(Guid? id, CategoryUpdateDto dto)
    {
        if (id == null || id == Guid.Empty)
            return new BaseResponse<CategoryUpdateDto>("Invalid Id", HttpStatusCode.BadRequest);

        var categoryDb = await _categoryRepository.GetByIdAsync(id.Value);
        if (categoryDb == null)
            return new BaseResponse<CategoryUpdateDto>("Category not found", HttpStatusCode.NotFound);

        
        _mapper.Map(dto, categoryDb);

        await _categoryRepository.SaveChangeAsync();

        var updatedDto = _mapper.Map<CategoryUpdateDto>(categoryDb);
        return new BaseResponse<CategoryUpdateDto>("Category updated", updatedDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> GetByIdAsync(Guid id)
    {
        var categoryDb = await _categoryRepository.GetByIdAsync(id);
        if (categoryDb == null)
            return new BaseResponse<string>("Category not found", HttpStatusCode.NotFound);

        var dto = _mapper.Map<CategoryMainGetDto>(categoryDb);
        return new BaseResponse<string>("Success", HttpStatusCode.OK); 
    }

    public async Task<BaseResponse<List<CategoryMainGetDto>>> GetByNameAsync(string search)
    {
        var categories = await _categoryRepository
            .GetAll()
            .Where(c => c.Name.ToLower().Contains(search.Trim().ToLower()))
            .Include(c => c.SubCategories)
            .ToListAsync();

        var dtos = _mapper.Map<List<CategoryMainGetDto>>(categories);

        return new BaseResponse<List<CategoryMainGetDto>>("Success", dtos, HttpStatusCode.OK);
    }
}

