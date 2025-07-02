namespace MiniEProject.Application.DTOs.RoleDtos;

public record RoleCreateDto
{
    public string Name { get; set; } = null!;
    public List<string> PermissionList { get; set; } = null!;
}

