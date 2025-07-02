using MiniEProject.Application.DTOs.RoleDtos;
using MiniEProject.Application.Shared.Responses;

namespace MiniEProject.Application.Abstracts.Services;

public interface IRoleService
{
    Task<BaseResponse<List<RoleGetDto>>> GetAllRolesAsync();
    Task<BaseResponse<RoleGetDto>> GetRoleByNameAsync(string roleName);
    Task<BaseResponse<string?>> CreateRoleAsync(RoleCreateDto dto);
    Task<BaseResponse<string?>> DeleteRoleAsync(string roleName);
}
