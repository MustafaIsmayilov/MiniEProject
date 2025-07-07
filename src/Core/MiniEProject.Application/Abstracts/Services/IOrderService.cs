using MiniEProject.Application.DTOs.OrderDtos;
using MiniEProject.Application.Shared.Responses;

namespace MiniEProject.Application.Abstracts.Services;

public interface IOrderService
{
    Task<BaseResponse<OrderGetDto>> CreateAsync(OrderCreateDto dto);
    Task<BaseResponse<OrderGetDto>> UpdateAsync(Guid id, OrderUpdateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<OrderGetDto>>> GetAllAsync();
}


