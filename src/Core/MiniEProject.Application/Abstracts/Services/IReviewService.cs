using MiniEProject.Application.DTOs.ReviewDtos;
using MiniEProject.Application.Shared.Responses;

namespace MiniEProject.Application.Abstracts.Services;

public interface IReviewService
{
    Task<BaseResponse<ReviewGetDto>> CreateAsync(ReviewCreateDto dto);
    Task<BaseResponse<ReviewGetDto>> UpdateAsync(Guid id, ReviewUpdateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<ReviewGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<ReviewGetDto>>> GetAllAsync();
}
