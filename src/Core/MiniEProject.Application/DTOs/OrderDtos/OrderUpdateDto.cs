using MiniEProject.Application.DTOs.OrderProductDtos;

namespace MiniEProject.Application.DTOs.OrderDtos;

public class OrderUpdateDto
{
    public string UserId { get; set; } = null!;
    public List<OrderProductUpdateDto> OrderProducts { get; set; } = new();
}
