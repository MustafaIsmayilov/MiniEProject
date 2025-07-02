using MiniEProject.Domain.Enums;

namespace MiniEProject.Application.DTOs.ProductDtos;

public class ProductUpdateDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public ProductCondition? Condition { get; set; }
    public Guid? CategoryId { get; set; }
}

