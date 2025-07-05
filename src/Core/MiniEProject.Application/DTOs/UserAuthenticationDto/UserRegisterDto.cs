using MiniEProject.Domain.Enums;

namespace MiniEProject.Application.DTOs.UserAuthenticationDto;

public record UserRegisterDto
{
    public string Fullname { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public RoleEnum Role { get; set; }
}
