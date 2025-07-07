using MiniEProject.Domain.Enums;

namespace MiniEProject.Application.DTOs.RoleDtos;

public record RoleGetDto
{
    public string RoleId { get; set; }
    public string? Name { get; set; }
    public AccountRoles Role { get; set; }
    public List<string> Permissions { get; set; } = new();
}
