using MiniEProject.Domain.Enums;

namespace MiniEProject.Application.DTOs.RoleDtos;

public record RoleUpdateDto
{
    public string RoleId { get; set; }
    public string? Name { get; set; }//buda qalsin her ehtimala 
    public AccountRoles Role { get; set; }
    public List<string>? PermissionList { get; set; }
}
