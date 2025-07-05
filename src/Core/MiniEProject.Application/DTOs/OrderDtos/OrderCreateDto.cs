using MiniEProject.Application.DTOs.OrderProductDtos;

using MiniEProject.Domain.Entities;

namespace MiniEProject.Application.DTOs.OrderDtos;

public class OrderCreateDto
{
    public string UserId { get; set; } = null!;
    public List<OrderProductCreateDto> OrderProducts { get; set; } = new();


}