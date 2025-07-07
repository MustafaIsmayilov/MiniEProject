using MiniEProject.Application.DTOs.OrderProductDtos;

namespace MiniEProject.Application.DTOs.OrderDtos;

public class OrderGetDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderProductGetDto> OrderProducts { get; set; } = new();
}
