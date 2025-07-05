namespace MiniEProject.Application.DTOs.UserAuthenticationDto;

public record UserAddRoleDto
{
    public Guid UserId { get; set; }
    public List<Guid> RoleId { get; set; }
}
