using MiniEProject.Domain.Enums;

namespace MiniEProject.Application.DTOs.RoleDtos;

public record RoleCreateDto
{
    public  AccountRoles Role { get; set; }
    public string? Name { get; set; }
    public List<string> PermissionList { get; set; } = null!;
}

