using MiniEProject.Domain.Enums;

namespace MiniEProject.Application.DTOs.UserAuthenticationDto;

public record UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public RoleEnum Role { get; set; }
}
