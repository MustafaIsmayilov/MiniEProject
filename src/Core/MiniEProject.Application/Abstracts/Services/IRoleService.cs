using MiniEProject.Application.DTOs.RoleDtos;
using MiniEProject.Application.Shared.Responses;

namespace MiniEProject.Application.Abstracts.Services;

public interface IRoleService
{
    Task<BaseResponse<RoleGetDto>> RoleGetByIdAsync(string RoleId);
    Task<BaseResponse<string?>> CreateRoleAsync(RoleCreateDto dto);
    Task<BaseResponse<string?>> DeleteRoleAsync(string id);
    Task<BaseResponse<string?>> UpdateRoleAsync(RoleUpdateDto dto);
}
