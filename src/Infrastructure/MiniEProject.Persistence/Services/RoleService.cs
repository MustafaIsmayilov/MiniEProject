using Microsoft.AspNetCore.Identity;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.RoleDtos;
using MiniEProject.Application.Shared.Responses;
using System.Net;
using System.Security.Claims;

namespace MiniEProject.Persistence.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<BaseResponse<List<RoleGetDto>>> GetAllRolesAsync()
    {
        var roles = _roleManager.Roles.ToList();

        var roleDtos = new List<RoleGetDto>();

        foreach (var role in roles)
        {
            var claims = await _roleManager.GetClaimsAsync(role);
            var permissions = claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();

            roleDtos.Add(new RoleGetDto
            {
                RoleId = role.Id,
                Name = role.Name,
                Permissions = permissions
            });
        }

        return new BaseResponse<List<RoleGetDto>>("All roles retrieved", roleDtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<RoleGetDto>> GetRoleByNameAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
            return new BaseResponse<RoleGetDto>("Role not found", HttpStatusCode.NotFound);

        var claims = await _roleManager.GetClaimsAsync(role);
        var permissions = claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();

        var roleDto = new RoleGetDto
        {
            RoleId = role.Id,
            Name = role.Name,
            Permissions = permissions
        };

        return new BaseResponse<RoleGetDto>("Role found", roleDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string?>> CreateRoleAsync(RoleCreateDto dto)
    {
        var existingRole = await _roleManager.FindByNameAsync(dto.Name);
        if (existingRole != null)
            return new BaseResponse<string?>("Role with this name already exists", HttpStatusCode.BadRequest);

        var role = new IdentityRole(dto.Name);
        var createResult = await _roleManager.CreateAsync(role);

        if (!createResult.Succeeded)
        {
            var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
            return new BaseResponse<string?>($"Role creation failed: {errors}", HttpStatusCode.BadRequest);
        }

        foreach (var permission in dto.PermissionList.Distinct())
        {
            var claimResult = await _roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            if (!claimResult.Succeeded)
            {
                var errors = string.Join("; ", claimResult.Errors.Select(e => e.Description));
                return new BaseResponse<string?>($"Role created but adding permission '{permission}' failed: {errors}", HttpStatusCode.PartialContent);
            }
        }

        return new BaseResponse<string?>("Role created successfully", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<string?>> DeleteRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
            return new BaseResponse<string?>("Role not found", HttpStatusCode.NotFound);

        var deleteResult = await _roleManager.DeleteAsync(role);
        if (!deleteResult.Succeeded)
        {
            var errors = string.Join("; ", deleteResult.Errors.Select(e => e.Description));
            return new BaseResponse<string?>($"Failed to delete role: {errors}", HttpStatusCode.BadRequest);
        }

        return new BaseResponse<string?>("Role deleted successfully", HttpStatusCode.OK);
    }
}

