using MiniEProject.Domain.Enums;

namespace MiniEProject.Application.DTOs.UserDtos;

public class UserGetDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public RoleEnum Role { get; set; }
}
