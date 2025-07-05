namespace MiniEProject.Application.DTOs.UserAuthenticationDto;

public record UserResetPasswordDto
{
    public string UserId { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
