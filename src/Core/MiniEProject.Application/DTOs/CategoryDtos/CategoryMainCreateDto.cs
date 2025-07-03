namespace MiniEProject.Application.DTOs.CategoryDtos;

public record CategoryMainCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

