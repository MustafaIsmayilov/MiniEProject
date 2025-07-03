using MiniEProject.Application.DTOs.CategoryDtos;
using MiniEProject.Application.Shared.Responses;

namespace MiniEProject.Application.Abstracts.Services;

public interface ICategoryService
{
    Task<BaseResponse<string>> AddMainCategoryAsync(CategoryMainCreateDto dto);

    Task<BaseResponse<string>> AddSubCategoryAsync(CategorySubCreateDto dto);

    Task<BaseResponse<CategoryUpdateDto>> UpdateAsync(Guid? id, CategoryUpdateDto dto);

    Task<BaseResponse<object>> DeleteAsync(Guid id);

    Task<BaseResponse<string>> GetByIdAsync(Guid id);

    Task<BaseResponse<List<CategoryMainGetDto>>> GetByNameAsync(string search);

    Task<BaseResponse<List<CategoryMainGetDto>>> GetAllAsync();
}
