using MiniEProject.Application.DTOs.CategoryDtos;
using MiniEProject.Application.Shared.Responses;

namespace MiniEProject.Application.Abstracts.Services;

public interface ICategoryService
{
    Task<BaseResponse<string>> AddAsync(CategoryCreateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto dto);
    Task<BaseResponse<CategoryGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<CategoryGetDto>> GetByNameAsync(string search);
    Task<BaseResponse<List<CategoryGetDto>>> GetAllNameAsync(string NamePart);
    Task<BaseResponse<List<CategoryGetDto>>> GetAllAsync();
}
