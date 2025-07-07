using MiniEProject.Application.DTOs.OrderProductDtos;
using MiniEProject.Application.Shared.Responses;

namespace MiniEProject.Application.Abstracts.Services;

public interface IOrderProductService
{
    Task<BaseResponse<OrderProductGetDto>> CreateAsync(OrderProductCreateDto dto);
    Task<BaseResponse<OrderProductGetDto>> UpdateAsync(Guid id, OrderProductUpdateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<OrderProductGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<OrderProductGetDto>>> GetAllAsync();
}
