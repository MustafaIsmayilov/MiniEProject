using MiniEProject.Domain.Enums;

namespace MiniEProject.Application.DTOs.ProductDtos;

public class ProductGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ProductCondition Condition { get; set; }
    public string? UserId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new List<string>();
}

