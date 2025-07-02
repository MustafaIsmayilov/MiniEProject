using MiniEProject.Domain.Enums;

namespace MiniEProject.Application.DTOs.ProductDtos;

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ProductCondition Condition { get; set; }
    public Guid CategoryId { get; set; }
    public string? UserId { get; set; }
}
