namespace MiniEProject.Application.DTOs.CategoryDtos;

public class CategorySubGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

}