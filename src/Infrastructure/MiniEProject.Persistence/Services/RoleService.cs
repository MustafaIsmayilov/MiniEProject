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

    public async Task<BaseResponse<string?>> CreateRoleAsync(RoleCreateDto dto)
    {
        var roleName = dto.Role.ToString();

        var existingRole = await _roleManager.FindByNameAsync(roleName);
        if (existingRole is not null)
            return new BaseResponse<string?>("A role with this name already exists.", HttpStatusCode.BadRequest);

        var identityRole = new IdentityRole(roleName);
        var createResult = await _roleManager.CreateAsync(identityRole);

        if (!createResult.Succeeded)
        {
            var errorMessage = string.Join(";", createResult.Errors.Select(e => e.Description));
            return new BaseResponse<string?>(errorMessage, HttpStatusCode.BadRequest);
        }

        foreach (var permission in dto.PermissionList.Distinct())
        {
            var claimResult = await _roleManager.AddClaimAsync(identityRole, new Claim("Permission", permission));
            if (!claimResult.Succeeded)
            {
                var error = string.Join(";", claimResult.Errors.Select(e => e.Description));
                return new BaseResponse<string?>($"Role was created, but permission '{permission}' could not be added: {error}", HttpStatusCode.PartialContent);
            }
        }

        return new BaseResponse<string?>("Role created successfully.", true, HttpStatusCode.Created);
    }

    public async Task<BaseResponse<string?>> UpdateRoleAsync(RoleUpdateDto dto)
    {
        var role = await _roleManager.FindByIdAsync(dto.RoleId);
        if (role is null)
            return new BaseResponse<string?>("Role not found.", HttpStatusCode.NotFound);

        if (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name != role.Name)
        {
            role.Name = dto.Name;
            var updateResult = await _roleManager.UpdateAsync(role);

            if (!updateResult.Succeeded)
            {
                var error = string.Join(";", updateResult.Errors.Select(e => e.Description));
                return new BaseResponse<string?>($"Failed to update role name: {error}", HttpStatusCode.BadRequest);
            }
        }

        if (dto.PermissionList is not null)
        {
            var currentClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in currentClaims.Where(c => c.Type == "Permission"))
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            foreach (var permission in dto.PermissionList.Distinct())
            {
                var addResult = await _roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                if (!addResult.Succeeded)
                {
                    var error = string.Join(";", addResult.Errors.Select(e => e.Description));
                    return new BaseResponse<string?>($"Failed to add new permission '{permission}': {error}", HttpStatusCode.PartialContent);
                }
            }
        }

        return new BaseResponse<string?>("Role updated successfully.", true, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string?>> DeleteRoleAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role is null)
            return new BaseResponse<string?>("Role not found.", HttpStatusCode.NotFound);

        var deleteResult = await _roleManager.DeleteAsync(role);

        if (!deleteResult.Succeeded)
        {
            var error = string.Join(";", deleteResult.Errors.Select(e => e.Description));
            return new BaseResponse<string?>($"Failed to delete role: {error}", HttpStatusCode.BadRequest);
        }

        return new BaseResponse<string?>("Role deleted successfully.", true, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<RoleGetDto>> RoleGetByIdAsync(string RoleId)
    {
        var role = await _roleManager.FindByIdAsync(RoleId);
        if (role is null)
            return new BaseResponse<RoleGetDto>("Role not found.", HttpStatusCode.NotFound);

        var claims = await _roleManager.GetClaimsAsync(role);
        var permissions = claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToList();

        var roleDto = new RoleGetDto
        {
            RoleId = role.Id,
            Name = role.Name,
            Permissions = permissions
        };

        return new BaseResponse<RoleGetDto>("Role and its permissions retrieved successfully.", roleDto, HttpStatusCode.OK);
    }
}


