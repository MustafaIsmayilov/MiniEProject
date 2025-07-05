namespace MiniEProject.Application.DTOs.ReviewDtos;

public class ReviewCreateDto
{
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public Guid ProductId { get; set; }
    public string UserId { get; set; } = null!;
}

