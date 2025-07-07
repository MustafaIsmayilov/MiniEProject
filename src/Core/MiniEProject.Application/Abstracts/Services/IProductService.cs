using MiniEProject.Application.DTOs.ProductDtos;
using MiniEProject.Application.Shared.Responses;

namespace MiniEProject.Application.Abstracts.Services;

public interface IProductService
{
    Task<BaseResponse<ProductGetDto>> CreateAsync(ProductCreateDto dto);
    Task<BaseResponse<ProductGetDto>> UpdateAsync(Guid id, ProductUpdateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<ProductGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<ProductGetDto>>> GetAllAsync();
}
