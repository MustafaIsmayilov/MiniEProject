namespace MiniEProject.Application.DTOs.OrderProductDtos;

public class OrderProductCreateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}