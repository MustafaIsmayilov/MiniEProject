using AutoMapper;
using System.Net;
using System;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.OrderDtos;
using MiniEProject.Application.Shared.Responses;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
namespace MiniEProject.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly MiniEProjectDbContext _context;
    private readonly IMapper _mapper;

    public OrderService(MiniEProjectDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private decimal CalculateTotalPrice(IEnumerable<OrderProduct> items)
    {
        return items.Sum(p => p.UnitPrice * p.Quantity);
    }

    public async Task<BaseResponse<OrderGetDto>> CreateAsync(OrderCreateDto dto)
    {
        var order = _mapper.Map<Order>(dto);
        order.OrderProducts = _mapper.Map<List<OrderProduct>>(dto.OrderProducts);
        var total = CalculateTotalPrice(order.OrderProducts);

        // ⚠️ TotalPrice modeldə `get;` idi, dəyişmək üçün `set;` əlavə et
        typeof(Order).GetProperty(nameof(Order.TotalPrice))?
            .SetValue(order, total);

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<OrderGetDto>(order);
        return new BaseResponse<OrderGetDto>("Order created", resultDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<OrderGetDto>> UpdateAsync(Guid id, OrderUpdateDto dto)
    {
        var order = await _context.Orders
            .Include(o => o.OrderProducts)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return new BaseResponse<OrderGetDto>("Order not found", null, HttpStatusCode.NotFound);

        order.UserId = dto.UserId;
        _context.OrderProducts.RemoveRange(order.OrderProducts);
        order.OrderProducts = _mapper.Map<List<OrderProduct>>(dto.OrderProducts);

        var total = CalculateTotalPrice(order.OrderProducts);
        typeof(Order).GetProperty(nameof(Order.TotalPrice))?
            .SetValue(order, total);

        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<OrderGetDto>(order);
        return new BaseResponse<OrderGetDto>("Order updated", resultDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
            return new BaseResponse<string>("Order not found", null, HttpStatusCode.NotFound);

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return new BaseResponse<string>("Order deleted", null, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderProducts)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return new BaseResponse<OrderGetDto>("Order not found", null, HttpStatusCode.NotFound);

        var dto = _mapper.Map<OrderGetDto>(order);
        return new BaseResponse<OrderGetDto>("Success", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetAllAsync()
    {
        var orders = await _context.Orders
            .Include(o => o.OrderProducts)
            .ToListAsync();

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return new BaseResponse<List<OrderGetDto>>("Success", dtos, HttpStatusCode.OK);
    }
}

