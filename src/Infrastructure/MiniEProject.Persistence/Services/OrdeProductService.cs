using AutoMapper;
using System.Net;
using System;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Application.DTOs.OrderProductDtos;
using MiniEProject.Application.Shared.Responses;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
namespace MiniEProject.Persistence.Services;

public class OrderProductService : IOrderProductService
{
    private readonly MiniEProjectDbContext _context;
    private readonly IMapper _mapper;

    public OrderProductService(MiniEProjectDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponse<OrderProductGetDto>> CreateAsync(OrderProductCreateDto dto)
    {
        var entity = _mapper.Map<OrderProduct>(dto);

        await _context.OrderProducts.AddAsync(entity);
        await _context.SaveChangesAsync();

        var created = await _context.OrderProducts.FindAsync(entity.Id);

        var resultDto = _mapper.Map<OrderProductGetDto>(created!);
        return new BaseResponse<OrderProductGetDto>("OrderProduct created successfully", resultDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<OrderProductGetDto>> UpdateAsync(Guid id, OrderProductUpdateDto dto)
    {
        var entity = await _context.OrderProducts.FindAsync(id);
        if (entity == null)
            return new BaseResponse<OrderProductGetDto>("OrderProduct not found", null, HttpStatusCode.NotFound);

        _mapper.Map(dto, entity);

        _context.OrderProducts.Update(entity);
        await _context.SaveChangesAsync();

        var updatedDto = _mapper.Map<OrderProductGetDto>(entity);
        return new BaseResponse<OrderProductGetDto>("OrderProduct updated successfully", updatedDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var entity = await _context.OrderProducts.FindAsync(id);
        if (entity == null)
            return new BaseResponse<string>("OrderProduct not found", null, HttpStatusCode.NotFound);

        _context.OrderProducts.Remove(entity);
        await _context.SaveChangesAsync();

        return new BaseResponse<string>("OrderProduct deleted successfully", null, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<OrderProductGetDto>> GetByIdAsync(Guid id)
    {
        var entity = await _context.OrderProducts.FindAsync(id);
        if (entity == null)
            return new BaseResponse<OrderProductGetDto>("OrderProduct not found", null, HttpStatusCode.NotFound);

        var dto = _mapper.Map<OrderProductGetDto>(entity);
        return new BaseResponse<OrderProductGetDto>("Success", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<OrderProductGetDto>>> GetAllAsync()
    {
        var list = await _context.OrderProducts.ToListAsync();
        var dtos = _mapper.Map<List<OrderProductGetDto>>(list);
        return new BaseResponse<List<OrderProductGetDto>>("Success", dtos, HttpStatusCode.OK);
    }
}

