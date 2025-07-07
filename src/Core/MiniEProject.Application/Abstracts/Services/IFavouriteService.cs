using MiniEProject.Application.DTOs.FavouriteDtos;
using MiniEProject.Application.Shared.Responses;

namespace MiniEProject.Application.Abstracts.Services;

public interface IFavouriteService
{
    Task<BaseResponse<FavouriteGetDto>> CreateAsync(FavouriteCreateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<List<FavouriteGetDto>>> GetAllAsync();
}


