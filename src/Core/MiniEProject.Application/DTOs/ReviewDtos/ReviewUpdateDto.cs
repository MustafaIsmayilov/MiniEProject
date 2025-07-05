namespace MiniEProject.Application.DTOs.ReviewDtos;

public class ReviewUpdateDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
}
