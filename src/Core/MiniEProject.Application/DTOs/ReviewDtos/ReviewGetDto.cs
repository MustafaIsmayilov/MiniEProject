namespace MiniEProject.Application.DTOs.ReviewDtos;

public class ReviewGetDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string UserId { get; set; } = null!;
    public string? UserName { get; set; }     // İstəyə görə
    public Guid ProductId { get; set; }
    public DateTime CreatedDate { get; set; }
}

